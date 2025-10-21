import { useState } from "react";
import { Users, Mail, Phone } from "lucide-react";
import { useForm } from "react-hook-form";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "../../components/ui/dialog";
import { Button } from "../../components/ui/button";
import { Label } from "../../components/ui/label";
import { Input } from "../../components/ui/input";
import { Card, CardContent } from "../../components/ui/card";
import { Badge } from "../../components/ui/badge";
import { useGuest } from "../../apis/data";
import axiosInstance from "../../config/config";
import { useQueryClient } from "@tanstack/react-query";
import { getToken } from "../../lib/jwt";

type GuestFormData = {
  fullName: string;
  email: string;
  phone: string;
};

export default function AdminGuestsPage() {
  const [guestId, setGuestId] = useState();
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [editingGuest, setEditingGuest] = useState<object | null>(null);
  const guest = useGuest();
  const queryClient = useQueryClient();
  const guestsData = guest.data?.data ?? [];
  const reservation = guestsData.flatMap((x) => x.reservations);
  var token = getToken();

  const {
    register,
    handleSubmit,
    reset,
    setValue,
    formState: { errors },
  } = useForm<GuestFormData>();

  const onSubmit = async (data: GuestFormData) => {
    if (editingGuest) {
      await axiosInstance.put(`/guests/${guestId}`, data, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
    } else {
      await axiosInstance.post("/guests", data, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
    }
    queryClient.invalidateQueries({
      queryKey: [`Guest`],
    });
    setIsDialogOpen(false);
    reset();
    setEditingGuest(null);
  };

  const handleEdit = (guest: any) => {
    setEditingGuest(guest);
    setGuestId(guest.id);
    setValue("fullName", guest.fullName);
    setValue("email", guest.email);
    setValue("phone", guest.phone);
    setIsDialogOpen(true);
  };

  const handleAddNew = () => {
    setEditingGuest(null);
    reset();
    setIsDialogOpen(true);
  };

  return (
    <div className="min-h-screen bg-slate-50">
      <div className="max-w-7xl mx-auto px-4 py-8">
        <div className="flex items-center justify-between mb-8">
          <div>
            <h1 className="text-4xl font-bold text-slate-900 mb-2">
              إدارة النزلاء
            </h1>
            <p className="text-slate-600">عرض وإدارة معلومات النزلاء</p>
          </div>
          <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
            <DialogTrigger asChild>
              <Button
                onClick={handleAddNew}
                className="bg-amber-600 hover:bg-amber-700"
              >
                <Users className="w-4 h-4 ml-2" />
                إضافة نزيل جديد
              </Button>
            </DialogTrigger>
            <DialogContent className="sm:max-w-[500px]">
              <DialogHeader>
                <DialogTitle>
                  {editingGuest ? "تعديل بيانات النزيل" : "إضافة نزيل جديد"}
                </DialogTitle>
                <DialogDescription>
                  {editingGuest
                    ? "قم بتعديل معلومات النزيل"
                    : "أدخل معلومات النزيل الجديد"}
                </DialogDescription>
              </DialogHeader>
              <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
                <div>
                  <Label htmlFor="fullName">الاسم الكامل</Label>
                  <Input
                    id="fullName"
                    {...register("fullName", {
                      required: "الاسم الكامل مطلوب",
                    })}
                  />
                  {errors.fullName && (
                    <p className="text-sm text-red-600 mt-1">
                      {errors.fullName.message}
                    </p>
                  )}
                </div>

                <div>
                  <Label htmlFor="email">البريد الإلكتروني</Label>
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
                    placeholder="ahmed@example.com"
                  />
                  {errors.email && (
                    <p className="text-sm text-red-600 mt-1">
                      {errors.email.message}
                    </p>
                  )}
                </div>

                <div>
                  <Label htmlFor="phone">رقم الهاتف</Label>
                  <Input
                    id="phone"
                    {...register("phone", { required: "رقم الهاتف مطلوب" })}
                    placeholder="+966 50 123 4567"
                  />
                  {errors.phone && (
                    <p className="text-sm text-red-600 mt-1">
                      {errors.phone.message}
                    </p>
                  )}
                </div>

                <div className="flex gap-2 justify-end">
                  <Button
                    type="button"
                    variant="outline"
                    onClick={() => {
                      setIsDialogOpen(false);
                      reset();
                      setEditingGuest(null);
                    }}
                  >
                    إلغاء
                  </Button>
                  <Button
                    type="submit"
                    className="bg-amber-600 hover:bg-amber-700"
                  >
                    {editingGuest ? "تحديث" : "إضافة"}
                  </Button>
                </div>
              </form>
            </DialogContent>
          </Dialog>
        </div>

        {/* Stats */}
        <div className="grid md:grid-cols-2 gap-4 mb-8">
          <Card className="border-slate-200 bg-white">
            <CardContent className="pt-6">
              <div className="text-2xl font-bold text-slate-900">
                {guestsData.length}
              </div>
              <div className="text-sm text-slate-600">إجمالي النزلاء</div>
            </CardContent>
          </Card>
          <Card className="border-slate-200 bg-white">
            <CardContent className="pt-6">
              <div className="text-2xl font-bold text-blue-600">
                {reservation.length}
              </div>
              <div className="text-sm text-slate-600">إجمالي الحجوزات</div>
            </CardContent>
          </Card>
        </div>
        {/* Guests List */}
        <div className="space-y-4">
          {guestsData.map((guest) => {
            return (
              <Card
                key={guest.id}
                className="border-slate-200 bg-white hover:shadow-md transition-shadow"
              >
                <CardContent className="pt-6">
                  <div className="flex items-start justify-between">
                    <div className="flex-1">
                      <div className="flex items-center gap-3 mb-3">
                        <div className="w-12 h-12 rounded-full bg-amber-100 flex items-center justify-center">
                          <Users className="w-6 h-6 text-amber-600" />
                        </div>
                        <div>
                          <h3 className="text-lg font-bold text-slate-900">
                            {guest.fullName}
                          </h3>
                          {guest.reservations.length > 0 && (
                            <Badge className="bg-green-100 text-green-700">
                              نزيل حالي
                            </Badge>
                          )}
                        </div>
                      </div>

                      <div className="grid md:grid-cols-2 gap-4 text-sm">
                        <div className="flex items-center gap-2 text-slate-600">
                          <Mail className="w-4 h-4" />
                          <span>{guest.email}</span>
                        </div>
                        <div className="flex items-center gap-2 text-slate-600">
                          <Phone className="w-4 h-4" />
                          <span>{guest.phone}</span>
                        </div>
                      </div>

                      <div className="mt-4 pt-4 border-t">
                        <div className="text-sm text-slate-600 mb-2">
                          سجل الحجوزات:
                        </div>
                        <div className="flex gap-4 text-sm">
                          <span>
                            <span className="font-medium text-slate-900">
                              {guest.reservations.length}{" "}
                            </span>
                            حجز
                          </span>
                          <span>
                            <span className="font-medium text-slate-900">
                              {guest.reservations.reduce(
                                (sum, r) => sum + r.amount,
                                0
                              )}
                            </span>{" "}
                            $
                          </span>
                        </div>
                      </div>
                    </div>

                    <div className="flex gap-2">
                      <Button
                        size="sm"
                        variant="outline"
                        onClick={() => {
                          setEditingGuest(guest);
                          handleEdit(guest);
                        }}
                      >
                        تعديل
                      </Button>
                    </div>
                  </div>
                </CardContent>
              </Card>
            );
          })}
        </div>
      </div>
    </div>
  );
}
