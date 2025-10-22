import { useState } from "react";
import { Building2 } from "lucide-react";
import { useToast } from "../../hooks/use-toast";
import { useRoom, useRoomtype } from "../../apis/data";
import { getToken } from "../../lib/jwt";
import axiosInstance from "../../config/config";
import { useQueryClient } from "@tanstack/react-query";

import { type status } from "../../lib/types";
import { Button } from "../../components/ui/button";
import RoomStats from "../../components/Room/RoomStats";
import RoomFormDialog from "../../components/Room/RoomFormDialog";
import RoomsByFloor from "../../components/Room/RoomsByFloor";

type RoomFormData = {
  roomTypeId: string;
  roomStatus: status;
};

export default function AdminRoomsPage() {
  const [roomId, setRoomId] = useState<string>();
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [editModel, setEditModel] = useState(false);
  const [defaultFormValues, setDefaultFormValues] = useState<RoomFormData>({
    roomTypeId: "",
    roomStatus: "Available",
  });

  const queryClient = useQueryClient();
  const { toast } = useToast();
  const token = getToken();

  const data = useRoom();
  const rooms = data.data?.data ?? [];

  const types = useRoomtype();
  const dataa = types.data?.data ?? [];
  const roomTypes = dataa.map((x) => ({ id: x.id, name: x.name }));

  const groupedRooms = rooms.reduce((acc, room) => {
    if (!acc[room.floor]) acc[room.floor] = [];
    acc[room.floor].push(room);
    return acc;
  }, {} as Record<number, typeof rooms>);

  // DELETE
  const handleDelete = async (id: string) => {
    if (!confirm("هل أنت متأكد من حذف هذه الغرفة؟")) return;

    try {
      await axiosInstance.delete(`/rooms/${id}`, {
        headers: { Authorization: `Bearer ${token}` },
      });
      queryClient.invalidateQueries({ queryKey: [`room`] });
      toast({ title: "تم الحذف بنجاح", description: "تم حذف الغرفة بنجاح" });
    } catch {
      toast({
        title: "خطأ",
        description: "حدث خطأ أثناء حذف الغرفة",
        variant: "destructive",
      });
    }
  };

  // EDIT
  const handleEdit = (room: any) => {
    const selectedTypeId =
      dataa.find((x) => x.name === room.roomTypeName)?.id ?? "";
    setRoomId(room.id);
    setEditModel(true);
    setDefaultFormValues({
      roomTypeId: selectedTypeId,
      roomStatus: room.status,
    });
    setIsDialogOpen(true);
  };

  // ADD
  const handleAddRoom = () => {
    setEditModel(false);
    setDefaultFormValues({
      roomTypeId: "",
      roomStatus: "Available",
    });
    setIsDialogOpen(true);
  };

  // FORM SUBMIT
  const onSubmit = async (data: RoomFormData) => {
    try {
      if (editModel) {
        await axiosInstance.put(`/rooms/${roomId}`, data, {
          headers: { Authorization: `Bearer ${token}` },
        });
        toast({
          title: "تم التحديث بنجاح",
          description: "تم تحديث بيانات الغرفة بنجاح",
        });
      } else {
        await axiosInstance.post("/rooms", data, {
          headers: { Authorization: `Bearer ${token}` },
        });
        toast({
          title: "تم الإضافة بنجاح",
          description: "تم إضافة الغرفة الجديدة بنجاح",
        });
      }
      queryClient.invalidateQueries({ queryKey: [`room`] });
      setIsDialogOpen(false);
    } catch {
      toast({
        title: "خطأ",
        description: "حدث خطأ أثناء حفظ البيانات",
        variant: "destructive",
      });
    }
  };

  return (
    <div className="min-h-screen bg-slate-50">
      <div className="max-w-7xl mx-auto px-4 py-8">
        {/* Header */}
        <div className="flex items-center justify-between mb-8">
          <div>
            <h1 className="text-4xl font-bold text-slate-900 mb-2">
              إدارة الغرف
            </h1>
            <p className="text-slate-600">عرض وإدارة جميع غرف الفندق</p>
          </div>

          {/* Add Room Button */}
          <Button
            onClick={handleAddRoom}
            className="bg-amber-600 hover:bg-amber-700 flex items-center gap-2"
          >
            <Building2 className="w-4 h-4" />
            إضافة غرفة جديدة
          </Button>
        </div>

        {/* Room Form Dialog */}
        <RoomFormDialog
          open={isDialogOpen}
          setOpen={setIsDialogOpen}
          editModel={editModel}
          onSubmit={onSubmit}
          roomTypes={roomTypes}
          defaultValues={defaultFormValues}
        />

        {/* Stats */}
        <RoomStats
          total={rooms.length}
          available={rooms.filter((r) => r.status === "Available").length}
          maintenance={
            rooms.filter((r) => r.status === "UnderMaintenance").length
          }
          occupied={rooms.filter((r) => r.status === "Occupied").length}
          cleaning={rooms.filter((r) => r.status === "Cleaning").length}
        />

        {/* Rooms by Floor */}
        <RoomsByFloor
          groupedRooms={groupedRooms}
          onEdit={handleEdit}
          onDelete={handleDelete}
        />
      </div>
    </div>
  );
}
