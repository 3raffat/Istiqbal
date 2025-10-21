import {
  Building2,
  Calendar,
  DollarSign,
  Hotel,
  LayoutDashboard,
  Users,
} from "lucide-react";

export const navigation = [
  { name: "لوحة التحكم", to: "/admin", icon: LayoutDashboard },
  { name: "أنواع الغرف", to: "/admin/room-types", icon: Hotel },
  { name: "الغرف", to: "/admin/rooms", icon: Building2 },
  { name: "النزلاء", to: "/admin/guests", icon: Users },
  { name: "الحجوزات", to: "/admin/reservations", icon: Calendar },
  { name: "المدفوعات", to: "/admin/payments", icon: DollarSign },
];
