import { useEffect } from "react";
import { useForm, Controller } from "react-hook-form";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogDescription,
  DialogTrigger,
} from "../../components/ui/dialog";
import { Button } from "../../components/ui/button";
import { Label } from "../../components/ui/label";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "../../components/ui/select";
import { StatusOptions, type status } from "../../lib/types";
import { getStatusText } from "../../Data/data";

type RoomFormData = {
  roomTypeId: string;
  roomStatus: status;
};

interface Props {
  open: boolean;
  setOpen: (val: boolean) => void;
  editModel: boolean;
  onSubmit: (data: RoomFormData) => void;
  roomTypes: { id: string; name: string }[];
  defaultValues: RoomFormData;
}

export default function RoomFormDialog({
  open,
  setOpen,
  editModel,
  onSubmit,
  roomTypes,
  defaultValues,
}: Props) {
  const {
    handleSubmit,
    control,
    reset,
    formState: { errors },
  } = useForm<RoomFormData>({
    defaultValues,
  });

  // ✅ Reset form whenever dialog opens or defaultValues change
  useEffect(() => {
    if (open) reset(defaultValues);
  }, [open, defaultValues, reset]);

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger asChild></DialogTrigger>
      <DialogContent className="sm:max-w-[500px]">
        <DialogHeader>
          <DialogTitle>
            {editModel ? "تعديل الغرفة" : "إضافة غرفة جديدة"}
          </DialogTitle>
          <DialogDescription>
            {editModel
              ? "قم بتعديل معلومات الغرفة"
              : "أدخل معلومات الغرفة الجديدة"}
          </DialogDescription>
        </DialogHeader>

        <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
          {/* Room Type */}
          <div className="flex flex-col gap-2">
            <Label htmlFor="roomTypeId">نوع الغرفة</Label>
            <Controller
              control={control}
              name="roomTypeId"
              rules={{ required: "يجب اختيار نوع الغرفة" }}
              render={({ field }) => (
                <Select onValueChange={field.onChange} value={field.value}>
                  <SelectTrigger className="w-full">
                    <SelectValue placeholder="اختر نوع الغرفة" />
                  </SelectTrigger>
                  <SelectContent>
                    {roomTypes.map((type) => (
                      <SelectItem key={type.id} value={type.id}>
                        {type.name}
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              )}
            />
            {errors.roomTypeId && (
              <span className="text-red-500 text-sm">
                {errors.roomTypeId.message}
              </span>
            )}
          </div>

          {/* Room Status */}
          <div className="flex flex-col gap-2">
            <Label htmlFor="roomStatus">الحالة</Label>
            <Controller
              control={control}
              name="roomStatus"
              rules={{ required: "يجب اختيار الحالة" }}
              render={({ field }) => (
                <Select onValueChange={field.onChange} value={field.value}>
                  <SelectTrigger className="w-full">
                    <SelectValue placeholder="اختر الحالة" />
                  </SelectTrigger>
                  <SelectContent>
                    {StatusOptions.map((status) => (
                      <SelectItem key={status} value={status}>
                        {getStatusText(status)}
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              )}
            />
            {errors.roomStatus && (
              <span className="text-red-500 text-sm">
                {errors.roomStatus.message}
              </span>
            )}
          </div>

          {/* Action Buttons */}
          <div className="flex justify-end gap-3 pt-4 border-t border-border">
            <Button
              type="button"
              variant="outline"
              onClick={() => setOpen(false)}
            >
              إلغاء
            </Button>
            <Button type="submit" className="bg-amber-600 hover:bg-amber-700">
              {editModel ? "تحديث" : "إضافة"}
            </Button>
          </div>
        </form>
      </DialogContent>
    </Dialog>
  );
}
