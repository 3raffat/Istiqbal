import { useForm } from "react-hook-form";
import {
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogDescription,
} from "../../components/ui/dialog";
import { Button } from "../../components/ui/button";
import { Label } from "../../components/ui/label";
import { Input } from "../../components/ui/input";

export type GuestFormData = {
  fullName: string;
  email: string;
  phone: string;
};

interface GuestFormProps {
  defaultValues?: GuestFormData;
  onSubmit: (data: GuestFormData) => void;
  onCancel: () => void;
  isEditing?: boolean;
}

export function GuestForm({
  defaultValues,
  onSubmit,
  onCancel,
  isEditing,
}: GuestFormProps) {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<GuestFormData>({
    defaultValues,
  });

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
      <div>
        <Label htmlFor="fullName">الاسم الكامل</Label>
        <Input
          id="fullName"
          {...register("fullName", { required: "الاسم الكامل مطلوب" })}
        />
        {errors.fullName && (
          <p className="text-sm text-red-600 mt-1">{errors.fullName.message}</p>
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
        />
        {errors.email && (
          <p className="text-sm text-red-600 mt-1">{errors.email.message}</p>
        )}
      </div>

      <div>
        <Label htmlFor="phone">رقم الهاتف</Label>
        <Input
          id="phone"
          {...register("phone", { required: "رقم الهاتف مطلوب" })}
        />
        {errors.phone && (
          <p className="text-sm text-red-600 mt-1">{errors.phone.message}</p>
        )}
      </div>

      <div className="flex gap-2 justify-end">
        <Button type="button" variant="outline" onClick={onCancel}>
          إلغاء
        </Button>
        <Button type="submit" className="bg-amber-600 hover:bg-amber-700">
          {isEditing ? "تحديث" : "إضافة"}
        </Button>
      </div>
    </form>
  );
}
