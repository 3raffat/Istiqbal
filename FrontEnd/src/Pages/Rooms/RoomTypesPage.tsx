"use client";
import { useState } from "react";
import {
  Dialog,
  DialogTrigger,
  DialogContent,
  DialogHeader,
  DialogTitle,
} from "../../components/ui/dialog";
import { Button } from "../../components/ui/button";
import { Plus } from "lucide-react";
import { useRoomtype, useAmenity } from "../../apis/data";
import axiosInstance from "../../config/config";
import { useQueryClient } from "@tanstack/react-query";
import { getToken } from "../../lib/jwt";
import { RoomTypeCard } from "../../components/RoomType/RoomTypeCard";
import { RoomTypeForm } from "../../components/RoomType/RoomTypeForm";

export default function RoomTypesPage() {
  const [isAddOpen, setIsAddOpen] = useState(false);
  const [isEditOpen, setIsEditOpen] = useState(false);
  const [editingRoomType, setEditingRoomType] = useState<any>(null);

  const roomtypes = useRoomtype();
  const amenities = useAmenity();
  const types = roomtypes.data?.data ?? [];
  const amenitydata = amenities.data?.data ?? [];
  const queryClient = useQueryClient();
  const token = getToken();

  const handleAddSubmit = async (data: any) => {
    await axiosInstance.post("/room-types", data, {
      headers: { Authorization: `Bearer ${token}` },
    });
    queryClient.invalidateQueries({ queryKey: ["Roomtype"] });
    setIsAddOpen(false);
  };

  const handleEditSubmit = async (data: any) => {
    if (!editingRoomType) return;
    await axiosInstance.put(`/room-types/${editingRoomType.id}`, data, {
      headers: { Authorization: `Bearer ${token}` },
    });
    queryClient.invalidateQueries({ queryKey: ["Roomtype"] });
    setIsEditOpen(false);
  };

  const handleEdit = (roomType: any) => {
    setEditingRoomType(roomType);
    setIsEditOpen(true);
  };

  const handleDelete = async (id: string) => {
    if (confirm("هل أنت متأكد من حذف نوع الغرفة؟")) {
      await axiosInstance.delete(`/room-types/${id}`, {
        headers: { Authorization: `Bearer ${token}` },
      });
      queryClient.invalidateQueries({ queryKey: ["Roomtype"] });
    }
  };

  return (
    <div className="p-8 space-y-8" dir="rtl">
      <div className="flex justify-between items-center">
        <div>
          <h1 className="text-3xl font-bold text-slate-900">أنواع الغرف</h1>
          <p className="text-slate-600 mt-1">إدارة أنواع الغرف والمرافق</p>
        </div>
        <Dialog open={isAddOpen} onOpenChange={setIsAddOpen}>
          <DialogTrigger asChild>
            <Button className="bg-amber-500 hover:bg-amber-600 text-white shadow-md">
              <Plus className="w-4 h-4 ml-2" /> إضافة نوع غرفة
            </Button>
          </DialogTrigger>
          <DialogContent
            className="max-w-2xl max-h-[80vh] overflow-y-auto"
            dir="rtl"
          >
            <DialogHeader>
              <DialogTitle>إضافة نوع غرفة جديد</DialogTitle>
            </DialogHeader>
            <RoomTypeForm
              amenities={amenitydata}
              onSubmit={handleAddSubmit}
              onCancel={() => setIsAddOpen(false)}
              submitLabel="إضافة"
            />
          </DialogContent>
        </Dialog>
      </div>

      {types.length === 0 ? (
        <div className="text-center py-20 text-slate-500 border border-dashed rounded-lg">
          لا توجد أنواع غرف مضافة بعد
        </div>
      ) : (
        <div className="grid gap-6 grid-cols-1 md:grid-cols-2 lg:grid-cols-3">
          {types.map((r: any) => (
            <RoomTypeCard
              key={r.id}
              roomType={r}
              onEdit={handleEdit}
              onDelete={handleDelete}
            />
          ))}
        </div>
      )}

      <Dialog open={isEditOpen} onOpenChange={setIsEditOpen}>
        <DialogContent
          className="max-w-2xl max-h-[80vh] overflow-y-auto"
          dir="rtl"
        >
          <DialogHeader>
            <DialogTitle>تعديل نوع الغرفة</DialogTitle>
          </DialogHeader>
          <RoomTypeForm
            defaultValues={editingRoomType}
            amenities={amenitydata}
            onSubmit={handleEditSubmit}
            onCancel={() => setIsEditOpen(false)}
            submitLabel="حفظ التغييرات"
          />
        </DialogContent>
      </Dialog>
    </div>
  );
}
