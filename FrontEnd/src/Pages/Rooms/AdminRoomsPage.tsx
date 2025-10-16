import { useState } from "react";
import { Building2, Edit, Trash2 } from "lucide-react";
import { Controller, useForm } from "react-hook-form";
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
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "../../components/ui/select";
import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
} from "../../components/ui/card";
import { useToast } from "../../hooks/use-toast";
import { getStatusColor, getStatusText } from "../../Data/data";
import { useAmenity, useRoom, useRoomtype } from "../../apis/data";
import { Badge } from "../../components/ui/badge";
import { MultiSelect } from "../../components/ui/MultiSelect";
import axiosInstance from "../../config/config";
import { QueryClient, useQueryClient } from "@tanstack/react-query";

type RoomFormData = {
  roomTypeId: string;
  amenitiesIds: [];
};

export default function AdminRoomsPage() {
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [editModel, setEditModel] = useState(true);
  const queryClient = useQueryClient();
  const { toast } = useToast();
  const data = useRoom();
  const rooms = data.data?.data ?? [];
  const amanity = useAmenity();
  const amanityData = amanity.data?.data ?? [];
  const types = useRoomtype();
  const dataa = types.data?.data ?? [];
  const roomtype = dataa?.map((x) => ({
    id: x.id,
    name: x.name,
  }));
  console.log();
  const {
    handleSubmit,
    reset,
    control,
    formState: { errors },
  } = useForm<RoomFormData>();

  const HandelDelete = async (id: string) => {
    if (confirm("هل أنت متأكد من حذف هذه الغرفة؟")) {
      console.log(id);
      await axiosInstance.delete(`/rooms/${id}`);
      queryClient.invalidateQueries({
        queryKey: [`room`],
      });
      toast({
        title: "تم الحذف",
        description: "تم حذف الغرفة بنجاح",
      });
    }
  };
  const onSubmit = async (data: RoomFormData) => {
    if (editModel) {
      await axiosInstance.put("/rooms", data);
      queryClient.invalidateQueries({
        queryKey: [`room`],
      });
      toast({
        title: "تم التحديث",
        description: "تم تحديث الغرفة بنجاح",
      });
    } else {
      await axiosInstance.post("/rooms", data);
      queryClient.invalidateQueries({
        queryKey: [`room`],
      });
      toast({
        title: "تم الإضافة",
        description: "تم إضافة الغرفة بنجاح",
      });
    }
  };
  const groupedRooms = rooms?.reduce((acc, room) => {
    if (!acc[room.floor]) {
      acc[room.floor] = [];
    }
    acc[room.floor].push(room);
    return acc;
  }, {} as Record<number, typeof rooms>);
  return (
    <div className="min-h-screen bg-slate-50">
      <div className="max-w-7xl mx-auto px-4 py-8">
        <div className="flex items-center justify-between mb-8">
          <div>
            <h1 className="text-4xl font-bold text-slate-900 mb-2">
              إدارة الغرف
            </h1>
            <p className="text-slate-600">عرض وإدارة جميع غرف الفندق</p>
          </div>
          <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
            <DialogTrigger asChild>
              <Button
                onClick={() => {
                  setEditModel(false);
                }}
                className="bg-amber-600 hover:bg-amber-700"
              >
                <Building2 className="w-4 h-4 ml-2" />
                إضافة غرفة جديدة
              </Button>
            </DialogTrigger>
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
                {/* 1️⃣ Room Type */}
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                  <div className="flex flex-col gap-2">
                    <Label htmlFor="roomTypeId">نوع الغرفة</Label>
                    <Controller
                      control={control}
                      name="roomTypeId"
                      rules={{ required: "يجب اختيار نوع الغرفة" }}
                      render={({ field }) => (
                        <Select
                          onValueChange={field.onChange}
                          value={field.value}
                        >
                          <SelectTrigger className="w-full">
                            <SelectValue placeholder="اختر نوع الغرفة" />
                          </SelectTrigger>
                          <SelectContent>
                            {roomtype?.map((type) => (
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

                  {/* 2️⃣ MultiSelect field */}
                  <div className="flex flex-col gap-2">
                    <Label>الخدمات</Label>
                    <Controller
                      control={control}
                      name="amenitiesIds"
                      rules={{ required: "يجب اختيار خدمة واحدة على الأقل" }}
                      render={({ field }) => (
                        <MultiSelect
                          onChange={(val) => field.onChange(val)}
                          options={amanityData.map((a) => ({
                            label: a.name,
                            value: a.id,
                          }))}
                          value={field.value}
                        />
                      )}
                    />
                    {errors.amenitiesIds && (
                      <span className="text-red-500 text-sm">
                        {errors.amenitiesIds.message}
                      </span>
                    )}
                  </div>
                </div>

                {/* 3️⃣ Action buttons */}
                <div className="flex justify-end gap-3 pt-4 border-t border-border">
                  <Button
                    type="button"
                    variant="outline"
                    onClick={() => {
                      setIsDialogOpen(false);
                      reset();
                    }}
                  >
                    إلغاء
                  </Button>
                  <Button
                    type="submit"
                    className="bg-amber-600 hover:bg-amber-700"
                  >
                    {editModel ? "تحديث" : "إضافة"}
                  </Button>
                </div>
              </form>
            </DialogContent>
          </Dialog>
        </div>

        {/* Stats */}
        <div className="grid md:grid-cols-4 gap-4 mb-8">
          <Card className="border-slate-200 bg-white">
            <CardContent className="pt-6">
              <div className="text-2xl font-bold text-slate-900">
                {rooms?.length}
              </div>
              <div className="text-sm text-slate-600">إجمالي الغرف</div>
            </CardContent>
          </Card>
          <Card className="border-slate-200 bg-white">
            <CardContent className="pt-6">
              <div className="text-2xl font-bold text-green-600">
                {rooms?.filter((r) => r.status === "Available").length}
              </div>
              <div className="text-sm text-slate-600">متاحة</div>
            </CardContent>
          </Card>
          <Card className="border-slate-200 bg-white">
            <CardContent className="pt-6">
              <div className="text-2xl font-bold text-red-600">
                {rooms?.filter((r) => r.status === "Occupied").length}
              </div>
              <div className="text-sm text-slate-600">محجوزة</div>
            </CardContent>
          </Card>
          <Card className="border-slate-200 bg-white">
            <CardContent className="pt-6">
              <div className="text-2xl font-bold text-yellow-600">
                {rooms?.filter((r) => r.status === "Cleaning").length}
              </div>
              <div className="text-sm text-slate-600">تحت التنظيف</div>
            </CardContent>
          </Card>
        </div>

        {/* Rooms by Floor */}
        <div className="space-y-6">
          {Object.keys(groupedRooms!)
            .sort((a, b) => Number(b) - Number(a))
            .map((floor) => (
              <Card key={floor} className="border-slate-200 bg-white">
                <CardHeader>
                  <CardTitle className="text-xl">الطابق {floor}</CardTitle>
                </CardHeader>
                <CardContent>
                  <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-4">
                    {groupedRooms![Number(floor)].map((room) => (
                      <div
                        key={room.id}
                        className="border rounded-lg p-4 hover:shadow-md transition-shadow"
                      >
                        <div className="flex items-start justify-between mb-3">
                          <div>
                            <div className="text-lg font-bold text-slate-900">
                              غرفة {room.number}
                            </div>
                            <div className="text-sm text-slate-600">
                              {room.roomTypeId}
                            </div>
                          </div>
                          <Badge className={getStatusColor(room.status)}>
                            {getStatusText(room.status)}
                          </Badge>
                        </div>

                        <div className="flex gap-2 mt-4">
                          <Button
                            size="sm"
                            variant="outline"
                            className="flex-1 bg-transparent"
                            // onClick={() => handleEdit(room)}
                          >
                            <Edit className="w-3 h-3 ml-1" />
                            تعديل
                          </Button>
                          <Button
                            size="sm"
                            variant="outline"
                            className="text-red-600 hover:text-red-700 bg-transparent"
                            onClick={() => HandelDelete(room.id)}
                          >
                            <Trash2 className="w-3 h-3" />
                          </Button>
                        </div>
                      </div>
                    ))}
                  </div>
                </CardContent>
              </Card>
            ))}
        </div>
      </div>
    </div>
  );
}
