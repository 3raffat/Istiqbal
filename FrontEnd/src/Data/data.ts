import {
  Building2,
  Calendar,
  DollarSign,
  LayoutDashboard,
  Users,
} from "lucide-react";
import { roomTypes } from "../lib/mock-data";
/*_____________________________________________________________________________________________*/
export const navigation = [
  { name: "لوحة التحكم", href: "/", icon: LayoutDashboard },
  { name: "الغرف", href: "/rooms", icon: Building2 },
  { name: "النزلاء", href: "/guests", icon: Users },
  { name: "الحجوزات", href: "/reservations", icon: Calendar },
  { name: "المدفوعات", href: "/payments", icon: DollarSign },
];
/*_____________________________________________________________________________________________*/
export const getStatusColor = (status: string) => {
  switch (status.trim()) {
    case "Available":
      return "bg-green-100 text-green-700";
    case "Occupied":
      return "bg-red-100 text-red-700";
    case "Cleaning":
      return "bg-yellow-100 text-yellow-700";
    case "UnderMaintenance":
      return "bg-slate-100 text-slate-700";
    default:
      return "bg-slate-100 text-slate-700";
  }
};

export const getStatusText = (status: string) => {
  switch (status) {
    case "Available":
      return "متاحة";
    case "Occupied":
      return "محجوزة";
    case "Cleaning":
      return "تحت التنظيف";
    case "UnderMaintenance":
      return "صيانة";
    default:
      return status;
  }
};
export const getRoomTypeName = (roomTypeId: string) => {
  return roomTypes.find((rt) => rt.id === roomTypeId)?.name || "غير معروف";
};
export const RoomState = [
  "Available",
  "Occupied",
  "UnderMaintenance",
  "Cleaning",
];
/*_____________________________________________________________________________________________*/
