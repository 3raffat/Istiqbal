import { useForm, Controller } from "react-hook-form";
import { Label } from "../../components/ui/label";
import { Input } from "../../components/ui/input";
import { Button } from "../../components/ui/button";
import { GuestCombobox } from "../../components/ui/GuestCombobox";
import { RoomCombobox } from "../../components/ui/RoomCombobox";
import type { ReservationFormData } from "../../lib/types";

interface ReservationFormProps {
  guests: any[];
  rooms: any[];
  roomTypes: any[];
  defaultValues?: ReservationFormData;
  onSubmit: (data: ReservationFormData) => void;
  onCancel: () => void;
}

export function ReservationForm({
  guests,
  rooms,
  roomTypes,
  defaultValues,
  onSubmit,
  onCancel,
}: ReservationFormProps) {
  const {
    register,
    handleSubmit,
    control,
    reset,
    formState: { errors },
  } = useForm<ReservationFormData>({
    defaultValues,
  });

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
      <div className="space-y-2">
        <Label htmlFor="guestId">النزيل *</Label>
        <Controller
          name="guestId"
          control={control}
          rules={{ required: "يرجى اختيار النزيل" }}
          render={({ field }) => (
            <GuestCombobox
              guests={guests}
              value={field.value || ""}
              onValueChange={field.onChange}
              placeholder="ابحث واختر النزيل..."
            />
          )}
        />
        {errors.guestId && (
          <p className="text-sm text-red-600">{errors.guestId.message}</p>
        )}
      </div>

      <div className="space-y-2">
        <Label htmlFor="roomId">الغرفة *</Label>
        <Controller
          name="roomId"
          control={control}
          rules={{ required: "يرجى اختيار الغرفة" }}
          render={({ field }) => (
            <RoomCombobox
              rooms={rooms}
              roomTypes={roomTypes}
              value={field.value || ""}
              onValueChange={field.onChange}
              placeholder="ابحث واختر الغرفة..."
            />
          )}
        />
        {errors.roomId && (
          <p className="text-sm text-red-600">{errors.roomId.message}</p>
        )}
      </div>

      <div className="grid grid-cols-2 gap-4">
        <div className="space-y-2">
          <Label htmlFor="checkIn">تاريخ الوصول *</Label>
          <Input
            id="checkIn"
            type="date"
            {...register("checkInDate", {
              required: "يرجى إدخال تاريخ الوصول",
            })}
          />
          {errors.checkInDate && (
            <p className="text-sm text-red-600">{errors.checkInDate.message}</p>
          )}
        </div>
        <div className="space-y-2">
          <Label htmlFor="checkOut">تاريخ المغادرة *</Label>
          <Input
            id="checkOut"
            type="date"
            {...register("checkOutDate", {
              required: "يرجى إدخال تاريخ المغادرة",
            })}
          />
          {errors.checkOutDate && (
            <p className="text-sm text-red-600">
              {errors.checkOutDate.message}
            </p>
          )}
        </div>
      </div>

      <div className="flex gap-3 justify-end">
        <Button
          type="button"
          variant="outline"
          onClick={() => {
            onCancel();
            reset();
          }}
        >
          إلغاء
        </Button>
        <Button type="submit" className="bg-amber-600 hover:bg-amber-700">
          حفظ
        </Button>
      </div>
    </form>
  );
}
