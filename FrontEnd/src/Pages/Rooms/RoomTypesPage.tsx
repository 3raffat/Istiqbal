import {
  Dialog,
  DialogContent,
  DialogTitle,
  DialogTrigger,
  DialogHeader,
} from "../../components/ui/dialog";
import { Plus, Edit, Trash2, Users, DollarSign } from "lucide-react";
import { useState } from "react";
import { Button } from "../../components/ui/button";
import { Label } from "../../components/ui/label";
import { Input } from "../../components/ui/input";
import { Textarea } from "../../components/ui/textarea";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "../../components/ui/card";
import { Badge } from "../../components/ui/badge";
import { Checkbox } from "../../components/ui/checkbox";
import { useForm } from "react-hook-form";
import type { CreateRoomTypeRequest } from "../../lib/types";
import { useAmenity, useRoomtype } from "../../apis/data";
import axiosInstance from "../../config/config";
import { useQueryClient } from "@tanstack/react-query";
import { getToken } from "../../lib/jwt";

export default function RoomTypesPage() {
  const [isAddOpen, setIsAddOpen] = useState(false);
  const [isEditOpen, setIsEditOpen] = useState(false);
  const [roomtypeId, setroomtypeId] = useState(false);
  const [editingRoomType, setEditingRoomType] = useState<any>(null);
  const roomtypes = useRoomtype();
  const amenity = useAmenity();
  const types = roomtypes.data?.data ?? [];
  const amenitydata = amenity.data?.data ?? [];
  const queryClient = useQueryClient();
  const token = getToken();
  const {
    register: registerAdd,
    handleSubmit: handleSubmitAdd,
    reset: resetAdd,
    setValue: setValueAdd,
    watch: watchAdd,
  } = useForm<CreateRoomTypeRequest>({
    defaultValues: {
      amenitieIds: [],
    },
  });

  const {
    register: registerEdit,
    handleSubmit: handleSubmitEdit,
    reset: resetEdit,
    setValue: setValueEdit,
    watch: watchEdit,
  } = useForm<CreateRoomTypeRequest>({
    defaultValues: {
      amenitieIds: [],
    },
  });

  const selectedAmenitiesAdd = watchAdd("amenitieIds") || [];
  const selectedAmenitiesEdit = watchEdit("amenitieIds") || [];

  const onAddSubmit = async (data: any) => {
    try {
      await axiosInstance.post("/room-types", data, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      queryClient.invalidateQueries({
        queryKey: [`Roomtype`],
      });
      resetAdd({ amenitieIds: [] });
      setIsAddOpen(false);
    } catch (error) {
      console.error("Error adding room type:", error);
    }
  };

  const onEditSubmit = async (data: any) => {
    if (!editingRoomType) return;

    try {
      const payload = {
        id: data.id,
        name: data.name,
        description: data.description,
        pricePerNight: Number(data.pricePerNight),
        maxOccupancy: Number(data.maxOccupancy),
        amenitieIds: data.amenitieIds,
      };

      await axiosInstance.put(`/room-types/${roomtypeId}`, payload, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      queryClient.invalidateQueries({
        queryKey: [`Roomtype`],
      });

      setIsEditOpen(false);
      setEditingRoomType(null);
      resetEdit({ amenitieIds: [] });
    } catch (error) {
      console.error("Error updating room type:", error);
    }
  };

  const handleEdit = (roomType: any) => {
    setEditingRoomType(roomType);
    const amenityIds = roomType.amenities?.map((a: any) => a.id) || [];
    resetEdit({
      name: roomType.name,
      description: roomType.description,
      pricePerNight: roomType.pricePerNight,
      maxOccupancy: roomType.maxOccupancy,
      amenitieIds: amenityIds,
    });
    setIsEditOpen(true);
  };

  const handleDelete = async (id: string) => {
    if (confirm("هل أنت متأكد من حذف نوع الغرفة؟")) {
      try {
        await axiosInstance.delete(`/room-types/${id}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });
        queryClient.invalidateQueries({
          queryKey: [`Roomtype`],
        });
      } catch (error) {
        console.error("Error deleting room type:", error);
      }
    }
  };

  const toggleAmenity = (amenityId: string, isAdd: boolean) => {
    const currentAmenities = isAdd
      ? selectedAmenitiesAdd
      : selectedAmenitiesEdit;
    const setValue = isAdd ? setValueAdd : setValueEdit;

    if (currentAmenities.includes(amenityId)) {
      setValue(
        "amenitieIds",
        currentAmenities.filter((id: string) => id !== amenityId)
      );
    } else {
      setValue("amenitieIds", [...currentAmenities, amenityId]);
    }
  };

  return (
    <div className="p-8">
      <div className="flex items-center justify-between mb-8">
        <div>
          <h1 className="text-3xl font-bold text-slate-900">أنواع الغرف</h1>
          <p className="text-slate-600 mt-1">إدارة أنواع الغرف والمرافق</p>
        </div>
        <Dialog open={isAddOpen} onOpenChange={setIsAddOpen}>
          <DialogTrigger asChild>
            <Button className="bg-amber-500 hover:bg-amber-600">
              <Plus className="w-4 h-4 ml-2" />
              إضافة نوع غرفة
            </Button>
          </DialogTrigger>
          <DialogContent className="max-w-2xl" dir="rtl">
            <DialogHeader>
              <DialogTitle>إضافة نوع غرفة جديد</DialogTitle>
            </DialogHeader>
            <form onSubmit={handleSubmitAdd(onAddSubmit)} className="space-y-4">
              <div>
                <Label htmlFor="add-name">اسم النوع</Label>
                <Input
                  id="add-name"
                  {...registerAdd("name", { required: true })}
                  placeholder="مثال: غرفة ديلوكس"
                />
              </div>
              <div>
                <Label htmlFor="add-description">الوصف</Label>
                <Textarea
                  id="add-description"
                  {...registerAdd("description", { required: true })}
                  placeholder="وصف تفصيلي لنوع الغرفة"
                  rows={3}
                />
              </div>
              <div className="grid grid-cols-2 gap-4">
                <div>
                  <Label htmlFor="add-price">السعر لليلة ($)</Label>
                  <Input
                    id="add-price"
                    type="number"
                    {...registerAdd("pricePerNight", {
                      required: true,
                      min: 0,
                    })}
                    placeholder="500"
                  />
                </div>
                <div>
                  <Label htmlFor="add-occupancy">الحد الأقصى للنزلاء</Label>
                  <Input
                    id="add-occupancy"
                    type="number"
                    {...registerAdd("maxOccupancy", { required: true, min: 1 })}
                    placeholder="2"
                  />
                </div>
              </div>
              <div>
                <Label>المرافق</Label>
                <div className="grid grid-cols-2 gap-3 mt-2">
                  {amenitydata.map((amenity: any) => (
                    <div key={amenity.id} className="flex items-center gap-2">
                      <Checkbox
                        id={`add-amenity-${amenity.id}`}
                        checked={selectedAmenitiesAdd.includes(amenity.id)}
                        onCheckedChange={() => toggleAmenity(amenity.id, true)}
                      />
                      <Label
                        htmlFor={`add-amenity-${amenity.id}`}
                        className="cursor-pointer"
                      >
                        {amenity.name}
                      </Label>
                    </div>
                  ))}
                </div>
              </div>
              <div className="flex gap-2 justify-end">
                <Button
                  type="button"
                  variant="outline"
                  onClick={() => {
                    setIsAddOpen(false);
                    resetAdd({ amenitieIds: [] });
                  }}
                >
                  إلغاء
                </Button>
                <Button
                  type="submit"
                  className="bg-amber-500 hover:bg-amber-600"
                >
                  إضافة
                </Button>
              </div>
            </form>
          </DialogContent>
        </Dialog>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        {types.map((roomType: any) => (
          <Card key={roomType.id} className="hover:shadow-lg transition-shadow">
            <CardHeader>
              <div className="flex items-start justify-between">
                <div>
                  <CardTitle className="text-xl">{roomType.name}</CardTitle>
                  <CardDescription className="mt-2">
                    {roomType.description}
                  </CardDescription>
                </div>
              </div>
            </CardHeader>
            <CardContent>
              <div className="space-y-4">
                <div className="flex items-center justify-between">
                  <div className="flex items-center gap-2 text-slate-600">
                    <DollarSign className="w-4 h-4" />
                    <span className="text-sm">السعر لليلة</span>
                  </div>
                  <span className="font-bold text-lg text-amber-600">
                    {roomType.pricePerNight} $
                  </span>
                </div>
                <div className="flex items-center justify-between">
                  <div className="flex items-center gap-2 text-slate-600">
                    <Users className="w-4 h-4" />
                    <span className="text-sm">الحد الأقصى</span>
                  </div>
                  <span className="font-semibold">
                    {roomType.maxOccupancy} نزلاء
                  </span>
                </div>
                <div>
                  <p className="text-sm text-slate-600 mb-2">المرافق:</p>
                  <div className="flex flex-wrap gap-2">
                    {roomType.amenities?.length > 0 ? (
                      roomType.amenities.map((amenity: any) => (
                        <Badge
                          key={amenity.id}
                          variant="secondary"
                          className="text-xs"
                        >
                          {amenity.name}
                        </Badge>
                      ))
                    ) : (
                      <span className="text-sm text-slate-500">
                        لا توجد مرافق
                      </span>
                    )}
                  </div>
                </div>
                <div className="flex gap-2 pt-4">
                  <Button
                    variant="outline"
                    size="sm"
                    className="flex-1 bg-transparent"
                    onClick={() => {
                      setroomtypeId(roomType.id);
                      handleEdit(roomType);
                    }}
                  >
                    <Edit className="w-4 h-4 ml-1" />
                    تعديل
                  </Button>
                  <Button
                    variant="outline"
                    size="sm"
                    className="text-red-600 hover:text-red-700 hover:bg-red-50 bg-transparent"
                    onClick={() => handleDelete(roomType.id)}
                  >
                    <Trash2 className="w-4 h-4" />
                  </Button>
                </div>
              </div>
            </CardContent>
          </Card>
        ))}
      </div>

      {/* Edit Dialog */}
      <Dialog open={isEditOpen} onOpenChange={setIsEditOpen}>
        <DialogContent className="max-w-2xl" dir="rtl">
          <DialogHeader>
            <DialogTitle>تعديل نوع الغرفة</DialogTitle>
          </DialogHeader>
          <form onSubmit={handleSubmitEdit(onEditSubmit)} className="space-y-4">
            <div>
              <Label htmlFor="edit-name">اسم النوع</Label>
              <Input
                id="edit-name"
                {...registerEdit("name", { required: true })}
                placeholder="مثال: غرفة ديلوكس"
              />
            </div>
            <div>
              <Label htmlFor="edit-description">الوصف</Label>
              <Textarea
                id="edit-description"
                {...registerEdit("description", { required: true })}
                placeholder="وصف تفصيلي لنوع الغرفة"
                rows={3}
              />
            </div>
            <div className="grid grid-cols-2 gap-4">
              <div>
                <Label htmlFor="edit-price">السعر لليلة ($)</Label>
                <Input
                  id="edit-price"
                  type="number"
                  {...registerEdit("pricePerNight", { required: true, min: 0 })}
                  placeholder="500"
                />
              </div>
              <div>
                <Label htmlFor="edit-occupancy">الحد الأقصى للنزلاء</Label>
                <Input
                  id="edit-occupancy"
                  type="number"
                  {...registerEdit("maxOccupancy", { required: true, min: 1 })}
                  placeholder="2"
                />
              </div>
            </div>
            <div>
              <Label>المرافق</Label>
              <div className="grid grid-cols-2 gap-3 mt-2">
                {amenitydata.map((amenity: any) => (
                  <div key={amenity.id} className="flex items-center gap-2">
                    <Checkbox
                      id={`edit-amenity-${amenity.id}`}
                      checked={selectedAmenitiesEdit.includes(amenity.id)}
                      onCheckedChange={() => toggleAmenity(amenity.id, false)}
                    />
                    <Label
                      htmlFor={`edit-amenity-${amenity.id}`}
                      className="cursor-pointer"
                    >
                      {amenity.name}
                    </Label>
                  </div>
                ))}
              </div>
            </div>
            <div className="flex gap-2 justify-end">
              <Button
                type="button"
                variant="outline"
                onClick={() => {
                  setIsEditOpen(false);
                  resetEdit({ amenitieIds: [] });
                }}
              >
                إلغاء
              </Button>
              <Button type="submit" className="bg-amber-500 hover:bg-amber-600">
                حفظ التغييرات
              </Button>
            </div>
          </form>
        </DialogContent>
      </Dialog>
    </div>
  );
}
