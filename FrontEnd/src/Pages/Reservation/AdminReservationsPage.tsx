"use client";

import { useState } from "react";
import { useQueryClient } from "@tanstack/react-query";
import {
  Dialog,
  DialogContent,
  DialogTrigger,
  DialogHeader,
  DialogTitle,
} from "../../components/ui/dialog";
import { Button } from "../../components/ui/button";
import { Plus } from "lucide-react";

import {
  useGuest,
  useReservation,
  useRoom,
  useRoomtype,
} from "../../apis/data";
import { getToken } from "../../lib/jwt";
import { ReservationForm } from "../../components/Reservation/ReservationForm";
import { ReservationCard } from "../../components/Reservation/ReservationCard";
import { ReservationStats } from "../../components/Reservation/ReservationStats";
import axiosInstance from "../../config/config";
export default function AdminReservationsPage() {
  const [isCreateDialogOpen, setIsCreateDialogOpen] = useState(false);
  const [isEditDialogOpen, setIsEditDialogOpen] = useState(false);
  const [currentReservation, setCurrentReservation] = useState<any>(null);

  const reservations = useReservation().data?.data ?? [];
  const rooms = useRoom().data?.data ?? [];
  const roomTypes = useRoomtype().data?.data ?? [];
  const guests = useGuest().data?.data ?? [];

  const queryClient = useQueryClient();
  const token = getToken();

  // Stats
  const stats = {
    total: reservations.length,
    pending: reservations.filter((r) => r.status?.toLowerCase() === "pending")
      .length,
    confirmed: reservations.filter(
      (r) => r.status?.toLowerCase() === "confirmed"
    ).length,
    cancelled: reservations.filter(
      (r) => r.status?.toLowerCase() === "cancelled"
    ).length,
  };

  // Handlers
  const handleEdit = (reservation: any) => {
    setCurrentReservation(reservation);
    setIsEditDialogOpen(true);
  };

  const handleCancel = async (reservation: any) => {
    try {
      const guestId = guests.find(
        (g) => g.fullName === reservation.guestFullName
      )?.id;
      await fetch(
        `/guests/${guestId}/reservations/${reservation.reservationId}`,
        {
          method: "DELETE",
          headers: { Authorization: `Bearer ${token}` },
        }
      );
      queryClient.invalidateQueries({ queryKey: ["Reservation"] });
    } catch (error) {
      console.error("Error cancelling reservation:", error);
    }
  };

  const handleCreateSubmit = async (data: any) => {
    console.log(data);
    try {
      await axiosInstance.post("/reservation", data, {
        headers: { Authorization: `Bearer ${token}` },
      });
      queryClient.invalidateQueries({ queryKey: ["Reservation"] });
      setIsCreateDialogOpen(false);
    } catch (error) {
      console.error("Error creating reservation:", error);
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

          {/* Create Reservation Dialog */}
          <Dialog
            open={isCreateDialogOpen}
            onOpenChange={setIsCreateDialogOpen}
          >
            <DialogTrigger asChild>
              <Button className="bg-amber-600 hover:bg-amber-700">
                <Plus className="w-4 h-4 ml-2" /> حجز جديد
              </Button>
            </DialogTrigger>
            <DialogContent className="max-w-2xl" dir="rtl">
              <DialogHeader>
                <DialogTitle className="text-2xl">إنشاء حجز جديد</DialogTitle>
              </DialogHeader>
              <ReservationForm
                guests={guests}
                rooms={rooms}
                roomTypes={roomTypes}
                onSubmit={handleCreateSubmit}
                onCancel={() => setIsCreateDialogOpen(false)}
              />
            </DialogContent>
          </Dialog>
        </div>

        {/* Reservation Stats */}
        <ReservationStats {...stats} />

        {/* Reservation List */}
        <div className="space-y-4">
          {reservations.length === 0 ? (
            <div className="text-center py-12 text-slate-500">
              لا توجد حجوزات
            </div>
          ) : (
            reservations.map((reservation) => (
              <ReservationCard
                key={reservation.reservationId}
                reservation={reservation}
                onEdit={handleEdit}
                onCancel={handleCancel}
              />
            ))
          )}
        </div>

        {/* Edit Reservation Dialog */}
        {currentReservation && (
          <Dialog open={isEditDialogOpen} onOpenChange={setIsEditDialogOpen}>
            <DialogContent className="max-w-2xl" dir="rtl">
              <DialogHeader>
                <DialogTitle className="text-2xl">تعديل الحجز</DialogTitle>
              </DialogHeader>
              <ReservationForm
                guests={guests}
                rooms={rooms}
                roomTypes={roomTypes}
                defaultValues={currentReservation}
                onSubmit={(data) => {
                  console.log("Edit data:", data);
                  setIsEditDialogOpen(false);
                }}
                onCancel={() => setIsEditDialogOpen(false)}
              />
            </DialogContent>
          </Dialog>
        )}
      </div>
    </div>
  );
}
