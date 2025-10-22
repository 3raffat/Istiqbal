"use client";
import { useState } from "react";
import { Users } from "lucide-react";
import {
  Dialog,
  DialogTrigger,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogDescription,
} from "../../components/ui/dialog";
import { Button } from "../../components/ui/button";
import { Card, CardContent } from "../../components/ui/card";
import { useGuest } from "../../apis/data";
import axiosInstance from "../../config/config";
import { useQueryClient } from "@tanstack/react-query";
import { getToken } from "../../lib/jwt";
import { GuestForm, GuestFormData } from "../../components/Guest/GuestForm";
import { GuestCard } from "../../components/Guest/GuestCard";

export default function AdminGuestsPage() {
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [editingGuest, setEditingGuest] = useState<any>(null);
  const guestQuery = useGuest();
  const queryClient = useQueryClient();
  const guestsData = guestQuery.data?.data ?? [];
  const reservation = guestsData.flatMap((x) => x.reservations);
  const token = getToken();

  const handleSubmit = async (data: GuestFormData) => {
    if (editingGuest) {
      await axiosInstance.put(`/guests/${editingGuest.id}`, data, {
        headers: { Authorization: `Bearer ${token}` },
      });
    } else {
      await axiosInstance.post("/guests", data, {
        headers: { Authorization: `Bearer ${token}` },
      });
    }
    queryClient.invalidateQueries({ queryKey: ["Guest"] });
    setIsDialogOpen(false);
    setEditingGuest(null);
  };

  const handleEdit = (guest: any) => {
    setEditingGuest(guest);
    setIsDialogOpen(true);
  };

  const handleAddNew = () => {
    setEditingGuest(null);
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
              <GuestForm
                defaultValues={editingGuest}
                onSubmit={handleSubmit}
                onCancel={() => {
                  setIsDialogOpen(false);
                  setEditingGuest(null);
                }}
                isEditing={!!editingGuest}
              />
            </DialogContent>
          </Dialog>
        </div>

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

        <div className="space-y-4">
          {guestsData.map((guest) => (
            <GuestCard key={guest.id} guest={guest} onEdit={handleEdit} />
          ))}
        </div>
      </div>
    </div>
  );
}
