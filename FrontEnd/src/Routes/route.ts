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
  { name: "Dashboard", to: "/", icon: LayoutDashboard },
  { name: "Reservations", to: "/reservations", icon: Calendar },
  { name: "Rooms", to: "/rooms", icon: DoorOpen },
  { name: "Guests", to: "/guests", icon: Users },
  { name: "Payments", to: "/payments", icon: DollarSign },
  { name: "Employees", to: "Zxcf/employees", icon: UserCog },
];
