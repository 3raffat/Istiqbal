import { Hotel, Lock, Mail } from "lucide-react";
import { useState } from "react";
import { set, useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "../../components/ui/card";
import { Label } from "../../components/ui/label";
import { Input } from "../../components/ui/input";
import { Button } from "../../components/ui/button";
import axiosInstance from "../../config/config";
import {
  decodeJwtSafe,
  setExpire,
  setRefToken,
  setRole,
  setToken,
} from "../../lib/jwt";

interface LoginFormData {
  email: string;
  password: string;
}
interface ApiResponse {
  data: {
    email: string;
    token: {
      accessToken: string;
      refreshToken: string;
      expiresOnUtc: string;
    };
  };
  status: number;
  message: string;
}

export default function AdminLogin() {
  const router = useNavigate();
  const [loading, setLoading] = useState(false);

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormData>();

  const onSubmit = async (data: LoginFormData) => {
    setLoading(true);
    try {
      const response = await axiosInstance.post<ApiResponse>(
        "/auth/login",
        data
      );
      router("/admin");
      const token = response.data.data.token.accessToken;
      const reftoken = response.data.data.token.refreshToken;
      const expire = response.data.data.token.expiresOnUtc;
      setToken(token);
      setRefToken(reftoken);
      setExpire(expire);
      const payload = decodeJwtSafe(token);
      setRole(payload?.role ?? null);
    } catch (error: any) {
      console.error("[v0] Login error:", error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-slate-900 via-slate-800 to-slate-900 flex items-center justify-center p-4">
      <div className="w-full max-w-md">
        {/* Logo/Brand */}
        <div className="text-center mb-8">
          <div className="inline-flex items-center justify-center w-16 h-16 rounded-full bg-amber-500 mb-4">
            <Hotel className="w-8 h-8 text-white" />
          </div>
          <h1 className="text-3xl font-bold text-white mb-2">استقبال</h1>
          <p className="text-slate-400">نظام إدارة الفنادق</p>
        </div>

        <Card className="border-slate-700 bg-slate-800/50 backdrop-blur">
          <CardHeader className="text-center">
            <CardTitle className="text-2xl text-white">
              تسجيل دخول الموظفين
            </CardTitle>
            <CardDescription className="text-slate-400">
              أدخل بيانات الدخول للوصول إلى لوحة التحكم
            </CardDescription>
          </CardHeader>
          <CardContent>
            <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
              <div className="space-y-2">
                <Label htmlFor="email" className="text-slate-200">
                  البريد الإلكتروني
                </Label>
                <div className="relative">
                  <Mail className="absolute right-3 top-1/2 -translate-y-1/2 w-5 h-5 text-slate-400" />
                  <Input
                    id="email"
                    type="email"
                    {...register("email", {
                      required: "البريد الإلكتروني مطلوب",
                      pattern: {
                        value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                        message: "البريد الإلكتروني غير صحيح",
                      },
                    })}
                    className="pr-10 bg-slate-900/50 border-slate-600 text-white placeholder:text-slate-500 focus:border-amber-500"
                    placeholder="example@hotel.com"
                    disabled={loading}
                  />
                </div>
                {errors.email && (
                  <p className="text-sm text-red-400">{errors.email.message}</p>
                )}
              </div>

              <div className="space-y-2">
                <Label htmlFor="password" className="text-slate-200">
                  كلمة المرور
                </Label>
                <div className="relative">
                  <Lock className="absolute right-3 top-1/2 -translate-y-1/2 w-5 h-5 text-slate-400" />
                  <Input
                    id="password"
                    type="password"
                    {...register("password", {
                      required: "كلمة المرور مطلوبة",
                      minLength: {
                        value: 6,
                        message: "كلمة المرور يجب أن تكون 6 أحرف على الأقل",
                      },
                    })}
                    className="pr-10 bg-slate-900/50 border-slate-600 text-white placeholder:text-slate-500 focus:border-amber-500"
                    placeholder="••••••••"
                    disabled={loading}
                  />
                </div>
                {errors.password && (
                  <p className="text-sm text-red-400">
                    {errors.password.message}
                  </p>
                )}
              </div>

              <Button
                type="submit"
                className="w-full bg-amber-500 hover:bg-amber-600 text-white font-medium"
                disabled={loading}
              >
                {loading ? "جاري تسجيل الدخول..." : "تسجيل الدخول"}
              </Button>
            </form>
          </CardContent>
        </Card>
      </div>
    </div>
  );
}
