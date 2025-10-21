import { useState } from "react";
import type { ReservationFormData, ReservationResponse } from "../../lib/types";
import { Controller, useForm } from "react-hook-form";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "../../components/ui/dialog";
import { Button } from "../../components/ui/button";
import { Edit, Eye, Plus, X } from "lucide-react";
import { Label } from "../../components/ui/label";
import { GuestCombobox } from "../../components/ui/GuestCombobox";
import { Input } from "../../components/ui/input";
import { Card, CardContent } from "../../components/ui/card";
import { Badge } from "../../components/ui/badge";
import {
  useGuest,
  useReservation,
  useRoom,
  useRoomtype,
} from "../../apis/data";
import { getInitials, getStatusColorr, getStatusTextt } from "../../Data/data";
import { RoomCombobox } from "../../components/ui/RoomCombobox";
import axiosInstance from "../../config/config";
import { getToken } from "../../lib/jwt";

export default function AdminReservationsPage() {
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [isEditDialogOpen, setIsEditDialogOpen] = useState(false);
  const [currentReservationId, setCurrentReservationId] = useState<string>("");
  const reservation = useReservation().data?.data ?? [];
  const room = useRoom().data?.data ?? [];
  const roomtype = useRoomtype().data?.data ?? [];
  const guest = useGuest().data?.data ?? [];
  const {
    register,
    handleSubmit,
    reset,
    control,
    formState: { errors },
  } = useForm<ReservationFormData>();

  const {
    register: registerEdit,
    handleSubmit: handleSubmitEdit,
    reset: resetEdit,
    setValue: setValueEdit,
    control: controlEdit,
    formState: { errors: errorsEdit },
  } = useForm<ReservationFormData>();

  const stats = {
    total: reservation.length,
    pending: reservation.filter(
      (r) => r.status && r.status.toLowerCase() === "pending"
    ).length,
    confirmed: reservation.filter(
      (r) => r.status && r.status.toLowerCase() === "confirmed"
    ).length,
    cancelled: reservation.filter(
      (r) => r.status && r.status.toLowerCase() === "cancelled"
    ).length,
  };
  var token = getToken();

  const handleEditClick = (reservation: ReservationResponse) => {
    setCurrentReservationId(reservation.reservationId);
    const selectedGuest = guest.find(
      (g) => g.fullName === reservation.guestFullName
    )?.id;
    const selectedRoom = room.find(
      (r) => r.number === reservation.roomNumber
    )?.id;
    setValueEdit("guestId", selectedGuest || "");
    setValueEdit("roomId", selectedRoom!);
    setValueEdit("checkInDate", reservation.checkInDate?.split("T")[0] || "");
    setValueEdit("checkOutDate", reservation.checkOutDate?.split("T")[0] || "");
    setValueEdit("status", reservation.status);
    setIsEditDialogOpen(true);
  };

  const onEditSubmit = async (data: ReservationFormData) => {
    try {
      const ReservationRequest = {
        roomId: data.roomId,
        checkInDate: new Date(data.checkInDate).toISOString(),
        checkOutDate: new Date(data.checkOutDate).toISOString(),
        status: data.status,
      };
      console.log(ReservationRequest);
      await axiosInstance.put(
        `/guests/${data.guestId}/reservations/${currentReservationId}`,
        ReservationRequest,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      setIsEditDialogOpen(false);
      resetEdit();
      // Optionally refresh data here
    } catch (error) {
      console.error("[v0] Error updating reservation:", error);
    }
  };

  const handleCancelReservation = async (guestFullName: string) => {
    try {
      const selectedGuest = guest.find((g) => g.fullName === guestFullName)?.id;

      await axiosInstance.delete(
        `/guests/${selectedGuest}/reservations/${currentReservationId}`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
    } catch (error) {
      console.error("[v0] Error cancelling reservation:", error);
    }
  };

  const onSubmit = async (data: ReservationFormData) => {
    try {
      await axiosInstance.post("/reservation", data, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      setIsDialogOpen(false);
      reset();
    } catch (error) {
      console.error("[v0] Error creating reservation:", error);
    }
  };

  return (
    <div className="min-h-screen bg-slate-50">
      <div className="max-w-7xl mx-auto px-4 py-8">
        <div className="flex items-center justify-between mb-8">
          <div>
            <h1 className="text-4xl font-bold text-slate-900 mb-2">
              إدارة الحجوزات
            </h1>
            <p className="text-slate-600">عرض وإدارة جميع حجوزات الفندق</p>
          </div>
          <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
            <DialogTrigger asChild>
              <Button className="bg-amber-600 hover:bg-amber-700">
                <Plus className="w-4 h-4 ml-2" />
                حجز جديد
              </Button>
            </DialogTrigger>
            <DialogContent className="max-w-2xl" dir="rtl">
              <DialogHeader>
                <DialogTitle className="text-2xl">إنشاء حجز جديد</DialogTitle>
              </DialogHeader>
              <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
                <div className="space-y-2">
                  <Label htmlFor="guestId">النزيل *</Label>
                  <Controller
                    name="guestId"
                    control={control}
                    rules={{ required: "يرجى اختيار النزيل" }}
                    render={({ field }) => (
                      <GuestCombobox
                        guests={guest}
                        value={field.value || ""}
                        onValueChange={field.onChange}
                        placeholder="ابحث واختر النزيل..."
                      />
                    )}
                  />
                  {errors.guestId && (
                    <p className="text-sm text-red-600">
                      {errors.guestId.message}
                    </p>
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
                        rooms={room}
                        roomTypes={roomtype}
                        value={field.value || ""}
                        onValueChange={field.onChange}
                        placeholder="ابحث واختر الغرفة..."
                      />
                    )}
                  />
                  {errors.roomId && (
                    <p className="text-sm text-red-600">
                      {errors.roomId.message}
                    </p>
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
                      min={new Date().toISOString().split("T")[0]}
                    />
                    {errors.checkInDate && (
                      <p className="text-sm text-red-600">
                        {errors.checkInDate.message}
                      </p>
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
                      min={new Date().toISOString().split("T")[0]}
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
                    onClick={() => setIsDialogOpen(false)}
                  >
                    إلغاء
                  </Button>
                  <Button
                    type="submit"
                    className="bg-amber-600 hover:bg-amber-700"
                  >
                    إنشاء الحجز
                  </Button>
                </div>
              </form>
            </DialogContent>
          </Dialog>
        </div>

        <div className="grid grid-cols-4 gap-4 mb-8">
          <Card className="border-slate-200 bg-white">
            <CardContent className="pt-6">
              <div className="text-2xl font-bold text-slate-900">
                {stats.total}
              </div>
              <div className="text-sm text-slate-600">الإجمالي</div>
            </CardContent>
          </Card>
          <Card className="border-slate-200 bg-white">
            <CardContent className="pt-6">
              <div className="text-2xl font-bold text-yellow-600">
                {stats.pending}
              </div>
              <div className="text-sm text-slate-600">قيد الانتظار</div>
            </CardContent>
          </Card>
          <Card className="border-slate-200 bg-white">
            <CardContent className="pt-6">
              <div className="text-2xl font-bold text-green-600">
                {stats.confirmed}
              </div>
              <div className="text-sm text-slate-600">مؤكد</div>
            </CardContent>
          </Card>
          <Card className="border-slate-200 bg-white">
            <CardContent className="pt-6">
              <div className="text-2xl font-bold text-red-600">
                {stats.cancelled}
              </div>
              <div className="text-sm text-slate-600">ملغي</div>
            </CardContent>
          </Card>
        </div>

        <div className="space-y-4">
          {reservation.length === 0 ? (
            <Card className="border-slate-200 bg-white">
              <CardContent className="pt-6 text-center py-12">
                <p className="text-slate-500">لا توجد حجوزات</p>
              </CardContent>
            </Card>
          ) : (
            reservation.map((reservation) => (
              <Card
                key={reservation.reservationId}
                className="border-slate-200 bg-white hover:shadow-md transition-shadow"
              >
                <CardContent className="pt-6">
                  <div className="flex flex-col lg:flex-row lg:items-center gap-4">
                    <div className="flex-1">
                      <div className="flex items-center gap-3 mb-2">
                        <div className="w-10 h-10 rounded-full bg-amber-100 flex items-center justify-center text-amber-700 font-bold">
                          {getInitials(reservation.guestFullName)}
                        </div>
                        <div>
                          <h3 className="font-bold text-slate-900">
                            {reservation.guestFullName}
                          </h3>
                        </div>
                      </div>
                    </div>

                    <div className="flex-1">
                      <div className="text-sm">
                        <div className="text-slate-600 mb-1">الغرفة</div>
                        <div className="font-medium text-slate-900">
                          غرفة {reservation.roomNumber} - {reservation.roomtype}
                        </div>
                      </div>
                    </div>

                    <div className="flex-1">
                      <div className="text-sm">
                        <div className="text-slate-600 mb-1">التواريخ</div>
                        <div className="font-medium text-slate-900">
                          {new Date(
                            reservation.checkOutDate
                          ).toLocaleDateString("en-GB")}{" "}
                          →{" "}
                          {new Date(reservation.checkInDate).toLocaleDateString(
                            "en-GB"
                          )}
                        </div>
                      </div>
                    </div>

                    <div className="flex-1">
                      <div className="text-sm mb-2">
                        <div className="text-slate-600 mb-1">المبلغ</div>
                        <div className="font-bold text-slate-900">
                          {reservation.amount} ريال
                        </div>
                      </div>
                      <Badge className={getStatusColorr(reservation.status)}>
                        {getStatusTextt(reservation.status)}
                      </Badge>
                    </div>

                    <div className="flex gap-2">
                      <Button size="sm" variant="outline" title="عرض التفاصيل">
                        <Eye className="w-4 h-4" />
                      </Button>
                      <Button
                        size="sm"
                        variant="outline"
                        title="تعديل"
                        onClick={() => handleEditClick(reservation)}
                      >
                        <Edit className="w-4 h-4" />
                      </Button>
                      {reservation.status.toLowerCase() === "pending" && (
                        <Button
                          size="sm"
                          variant="outline"
                          className="text-red-600 hover:text-red-700 hover:bg-red-50 bg-transparent"
                          title="إلغاء"
                          onClick={() =>
                            handleCancelReservation(reservation.guestFullName)
                          }
                        >
                          <X className="w-4 h-4" />
                        </Button>
                      )}
                    </div>
                  </div>
                </CardContent>
              </Card>
            ))
          )}
        </div>

        <Dialog open={isEditDialogOpen} onOpenChange={setIsEditDialogOpen}>
          <DialogContent className="max-w-2xl" dir="rtl">
            <DialogHeader>
              <DialogTitle className="text-2xl">تعديل الحجز</DialogTitle>
            </DialogHeader>
            <form
              onSubmit={handleSubmitEdit(onEditSubmit)}
              className="space-y-6"
            >
              <div className="space-y-2">
                <Label htmlFor="edit-guestId">النزيل *</Label>
                <Controller
                  name="guestId"
                  control={controlEdit}
                  rules={{ required: "يرجى اختيار النزيل" }}
                  render={({ field }) => (
                    <GuestCombobox
                      guests={guest}
                      value={field.value || ""}
                      onValueChange={field.onChange}
                      placeholder="ابحث واختر النزيل..."
                    />
                  )}
                />
                {errorsEdit.guestId && (
                  <p className="text-sm text-red-600">
                    {errorsEdit.guestId.message}
                  </p>
                )}
              </div>

              <div className="space-y-2">
                <Label htmlFor="edit-roomId">الغرفة *</Label>
                <Controller
                  name="roomId"
                  control={controlEdit}
                  rules={{ required: "يرجى اختيار الغرفة" }}
                  render={({ field }) => (
                    <RoomCombobox
                      rooms={room}
                      roomTypes={roomtype}
                      value={field.value || ""}
                      onValueChange={field.onChange}
                      placeholder="ابحث واختر الغرفة..."
                    />
                  )}
                />
                {errorsEdit.roomId && (
                  <p className="text-sm text-red-600">
                    {errorsEdit.roomId.message}
                  </p>
                )}
              </div>

              <div className="grid grid-cols-2 gap-4">
                <div className="space-y-2">
                  <Label htmlFor="edit-checkIn">تاريخ الوصول *</Label>
                  <Input
                    id="edit-checkIn"
                    type="date"
                    {...registerEdit("checkInDate", {
                      required: "يرجى إدخال تاريخ الوصول",
                    })}
                  />
                  {errorsEdit.checkInDate && (
                    <p className="text-sm text-red-600">
                      {errorsEdit.checkInDate.message}
                    </p>
                  )}
                </div>

                <div className="space-y-2">
                  <Label htmlFor="edit-checkOut">تاريخ المغادرة *</Label>
                  <Input
                    id="edit-checkOut"
                    type="date"
                    {...registerEdit("checkOutDate", {
                      required: "يرجى إدخال تاريخ المغادرة",
                    })}
                  />
                  {errorsEdit.checkOutDate && (
                    <p className="text-sm text-red-600">
                      {errorsEdit.checkOutDate.message}
                    </p>
                  )}
                </div>
              </div>

              <div className="space-y-2">
                <Label htmlFor="edit-status">الحالة *</Label>
                <select
                  id="edit-status"
                  {...registerEdit("status", {
                    required: "يرجى اختيار الحالة",
                  })}
                  className="w-full px-3 py-2 border border-slate-300 rounded-md focus:outline-none focus:ring-2 focus:ring-amber-500"
                >
                  <option value="Pending">قيد الانتظار</option>
                  <option value="Confirmed">مؤكد</option>
                  <option value="CheckedIn">تم تسجيل الدخول</option>
                  <option value="CheckedOut">تم تسجيل الخروج</option>
                  <option value="Cancelled">ملغي</option>
                </select>
                {errorsEdit.status && (
                  <p className="text-sm text-red-600">
                    {errorsEdit.status.message}
                  </p>
                )}
              </div>

              <div className="flex gap-3 justify-end">
                <Button
                  type="button"
                  variant="outline"
                  onClick={() => {
                    setIsEditDialogOpen(false);
                    resetEdit();
                  }}
                >
                  إلغاء
                </Button>
                <Button
                  type="submit"
                  className="bg-amber-600 hover:bg-amber-700"
                >
                  حفظ التعديلات
                </Button>
              </div>
            </form>
          </DialogContent>
        </Dialog>
      </div>
    </div>
  );
}
