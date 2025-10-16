import type React from "react";
import { useEffect, useState } from "react";
import { Loader2 } from "lucide-react";
import { useNavigate } from "react-router-dom";

export function AdminGuard({ children }: { children: React.ReactNode }) {
  const router = useNavigate();
  const [isChecking, setIsChecking] = useState(true);

  useEffect(() => {
    // const checkAuth = () => {
    //   if (!isAuthenticated()) {
    //     router.push("/admin/login")
    //   } else {
    //     setIsChecking(false)
    //   }
    // }
    // checkAuth()
  }, [router]);

  if (isChecking) {
    return (
      <div className="min-h-screen bg-slate-50 flex items-center justify-center">
        <div className="text-center">
          <Loader2 className="w-8 h-8 animate-spin text-amber-500 mx-auto mb-4" />
          <p className="text-slate-600">جاري التحقق من الصلاحيات...</p>
        </div>
      </div>
    );
  }

  return <>{children}</>;
}
