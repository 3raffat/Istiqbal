import {
  Calendar,
  DollarSign,
  DoorOpen,
  LayoutDashboard,
  Settings,
  UserCog,
  Users,
} from "lucide-react";

export const navigation = [
  { name: "Dashboard", to: "/admin", icon: LayoutDashboard },
  { name: "Reservations", to: "/admin/reservations", icon: Calendar },
  { name: "Rooms", to: "/admin/rooms", icon: DoorOpen },
  { name: "Guests", to: "/admin/guests", icon: Users },
  { name: "Payments", to: "/admin/payments", icon: DollarSign },
  { name: "Employees", to: "/admin/employees", icon: UserCog },
];
