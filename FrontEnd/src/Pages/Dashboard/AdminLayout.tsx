import { Link, Outlet, useLocation, useNavigate } from "react-router-dom";
import { Button } from "../../components/ui/button";
import {
  Hotel,
  LogOut,
  Building2,
  Users,
  Calendar,
  UserPlus,
} from "lucide-react";
import { getRole, logout } from "../../lib/jwt";

const navigation = [
  {
    name: "أنواع الغرف",
    to: "/admin/room-types",
    icon: Hotel,
    roles: ["Admin"],
  },
  { name: "الغرف", to: "/admin/rooms", icon: Building2, roles: ["Admin"] },
  {
    name: "النزلاء",
    to: "/admin/guests",
    icon: Users,
    roles: ["Admin", "Receptionist"],
  },
  {
    name: "الحجوزات",
    to: "/admin",
    icon: Calendar,
    roles: ["Admin", "Receptionist"],
  },
  {
    name: "اضافة موظف",
    to: "/admin/employees",
    icon: UserPlus,
    roles: ["Admin"],
  },
];

export default function AdminLayout() {
  const pathname = useLocation().pathname;
  const router = useNavigate();

  const userRole = getRole() || "Receptionist";

  const handleLogout = () => {
    logout();
    router("/login");
  };

  const filteredNav = navigation.filter((item) =>
    item.roles.includes(userRole)
  );

  return (
    <div className="min-h-screen bg-slate-50" dir="rtl">
      {/* Sidebar */}
      <aside className="fixed right-0 top-0 h-full w-64 bg-slate-900 text-white p-6 z-10">
        {/* Logo */}
        <div className="flex items-center gap-3 mb-8">
          <div className="w-10 h-10 rounded-lg bg-amber-500 flex items-center justify-center">
            <Hotel className="w-6 h-6" />
          </div>
          <div>
            <h1 className="font-bold text-lg">استقبال</h1>
            <p className="text-xs text-slate-400">لوحة التحكم</p>
          </div>
        </div>

        {/* Navigation */}
        <nav className="space-y-2 mb-8">
          {filteredNav.map((item) => {
            const isActive = pathname === item.to;
            return (
              <Link
                key={item.to}
                to={item.to}
                className={`flex items-center gap-3 px-4 py-3 rounded-lg transition-colors ${
                  isActive
                    ? "bg-amber-500 text-white"
                    : "text-slate-300 hover:bg-slate-800 hover:text-white"
                }`}
              >
                <item.icon className="w-5 h-5" />
                <span className="font-medium">{item.name}</span>
              </Link>
            );
          })}
        </nav>

        {/* User Info & Logout */}
        <div className="absolute bottom-6 left-6 right-6">
          <Button
            onClick={handleLogout}
            variant="outline"
            className="w-full border-slate-700 text-slate-300 hover:bg-slate-800 hover:text-white bg-transparent"
          >
            <LogOut className="w-4 h-4 ml-2" />
            تسجيل الخروج
          </Button>
        </div>
      </aside>

      {/* Main Content */}
      <main className="mr-64">
        <Outlet />
      </main>
    </div>
  );
}
