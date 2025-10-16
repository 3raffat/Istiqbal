import { Building2, Calendar, DollarSign } from "lucide-react";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "../../components/ui/card";
import { useRoom } from "../../apis/data";
export default function AdminDashboard() {
  // const stats = {
  //   totalRooms: rooms.length,
  //   availableRooms: rooms.filter((r) => r.status === "available").length,
  //   occupiedRooms: rooms.filter((r) => r.status === "occupied").length,
  //   totalGuests: guests.length,
  //   activeReservations: reservations.filter(
  //     (r) => r.status === "confirmed" || r.status === "checked-in"
  //   ).length,
  //   pendingPayments: payments.filter((p) => p.status === "pending").length,
  //   totalRevenue: payments
  //     .filter((p) => p.status === "completed")
  //     .reduce((sum, p) => sum + p.amount, 0),
  // };

  // const recentReservations = reservations.slice(0, 5);

  return (
    <div className="min-h-screen bg-slate-50">
      <div className="max-w-7xl mx-auto px-4 py-8">
        <div className="mb-8">
          <h1 className="text-4xl font-bold text-slate-900 mb-2">
            لوحة التحكم
          </h1>
          <p className="text-slate-600">نظرة عامة على أداء الفندق</p>
        </div>

        {/* Stats Grid */}
        <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-6 mb-8">
          <Card className="border-slate-200 bg-white">
            <CardHeader className="flex flex-row items-center justify-between pb-2">
              <CardTitle className="text-sm font-medium text-slate-600">
                إجمالي الغرف
              </CardTitle>
              <Building2 className="w-5 h-5 text-slate-400" />
            </CardHeader>
            <CardContent>
              <div className="text-3xl font-bold text-slate-900">
                {/* {stats.totalRooms} */}
              </div>
              <p className="text-xs text-green-600 mt-1">
                {/* متاح: {stats.availableRooms} */}
              </p>
            </CardContent>
          </Card>

          <Card className="border-slate-200 bg-white">
            <CardHeader className="flex flex-row items-center justify-between pb-2">
              <CardTitle className="text-sm font-medium text-slate-600">
                الحجوزات النشطة
              </CardTitle>
              <Calendar className="w-5 h-5 text-slate-400" />
            </CardHeader>
            <CardContent>
              <div className="text-3xl font-bold text-slate-900">
                {/* {stats.activeReservations} */}
              </div>
              <p className="text-xs text-slate-500 mt-1">
                {/* من إجمالي {reservations.length} */}
              </p>
            </CardContent>
          </Card>

          <Card className="border-slate-200 bg-white">
            <CardHeader className="flex flex-row items-center justify-between pb-2">
              <CardTitle className="text-sm font-medium text-slate-600">
                الإيرادات
              </CardTitle>
              <DollarSign className="w-5 h-5 text-slate-400" />
            </CardHeader>
            <CardContent>
              <div className="text-3xl font-bold text-slate-900">
                {/* {stats.totalRevenue.toLocaleString()} */}
              </div>
              <p className="text-xs text-slate-500 mt-1">ريال سعودي</p>
            </CardContent>
          </Card>
        </div>

        {/* Charts Row */}
        <div className="grid lg:grid-cols-2 gap-6 mb-8">
          <Card className="border-slate-200 bg-white">
            <CardHeader>
              <CardTitle className="text-xl">حالة الغرف</CardTitle>
              <CardDescription>توزيع الغرف حسب الحالة</CardDescription>
            </CardHeader>
            <CardContent>
              <div className="space-y-4">
                <div className="flex items-center justify-between">
                  <div className="flex items-center gap-3">
                    <div className="w-4 h-4 rounded-full bg-green-500" />
                    <span className="text-sm text-slate-600">متاحة</span>
                  </div>
                  <span className="font-bold text-slate-900">
                    {/* {stats.availableRooms} */}
                  </span>
                </div>
                <div className="flex items-center justify-between">
                  <div className="flex items-center gap-3">
                    <div className="w-4 h-4 rounded-full bg-red-500" />
                    <span className="text-sm text-slate-600">محجوزة</span>
                  </div>
                  <span className="font-bold text-slate-900">
                    {/* {stats.occupiedRooms} */}
                  </span>
                </div>
                <div className="flex items-center justify-between">
                  <div className="flex items-center gap-3">
                    <div className="w-4 h-4 rounded-full bg-yellow-500" />
                    <span className="text-sm text-slate-600">
                      محجوزة مسبقاً
                    </span>
                  </div>
                  <span className="font-bold text-slate-900">
                    {/* {rooms.filter((r) => r.status === "reserved").length} */}
                  </span>
                </div>
                <div className="flex items-center justify-between">
                  <div className="flex items-center gap-3">
                    <div className="w-4 h-4 rounded-full bg-slate-500" />
                    <span className="text-sm text-slate-600">صيانة</span>
                  </div>
                  <span className="font-bold text-slate-900">
                    {/* {rooms.filter((r) => r.status === "maintenance").length} */}
                  </span>
                </div>
              </div>
            </CardContent>
          </Card>

          <Card className="border-slate-200 bg-white">
            <CardHeader>
              <CardTitle className="text-xl">المدفوعات</CardTitle>
              <CardDescription>حالة المدفوعات</CardDescription>
            </CardHeader>
            <CardContent>
              <div className="space-y-4">
                <div className="flex items-center justify-between">
                  <div className="flex items-center gap-3">
                    <div className="w-4 h-4 rounded-full bg-green-500" />
                    <span className="text-sm text-slate-600">مكتملة</span>
                  </div>
                  <span className="font-bold text-slate-900">
                    {/* {payments.filter((p) => p.status === "completed").length} */}
                  </span>
                </div>
                <div className="flex items-center justify-between">
                  <div className="flex items-center gap-3">
                    <div className="w-4 h-4 rounded-full bg-yellow-500" />
                    <span className="text-sm text-slate-600">قيد الانتظار</span>
                  </div>
                  <span className="font-bold text-slate-900">
                    {/* {stats.pendingPayments} */}
                  </span>
                </div>
                <div className="flex items-center justify-between">
                  <div className="flex items-center gap-3">
                    <div className="w-4 h-4 rounded-full bg-red-500" />
                    <span className="text-sm text-slate-600">مسترجعة</span>
                  </div>
                  <span className="font-bold text-slate-900">
                    {/* {payments.filter((p) => p.status === "refunded").length} */}
                  </span>
                </div>
              </div>
            </CardContent>
          </Card>
        </div>

        {/* Recent Reservations */}
        <Card className="border-slate-200 bg-white">
          <CardHeader>
            <CardTitle className="text-xl">أحدث الحجوزات</CardTitle>
            <CardDescription>آخر 5 حجوزات في النظام</CardDescription>
          </CardHeader>
          <CardContent>
            <div className="space-y-4">
              {/* {recentReservations.map((reservation) => {
                const guest = guests.find((g) => g.id === reservation.guestId);
                const room = rooms.find((r) => r.id === reservation.roomId);

                return (
                  <div
                    key={reservation.id}
                    className="flex items-center justify-between p-4 border rounded-lg"
                  >
                    <div className="flex-1">
                      <div className="font-medium text-slate-900">
                        {guest?.firstName} {guest?.lastName}
                      </div>
                      <div className="text-sm text-slate-500">
                        غرفة رقم {room?.roomNumber}
                      </div>
                    </div>
                    <div className="flex-1 text-center">
                      <div className="text-sm text-slate-600">
                        {reservation.checkIn} - {reservation.checkOut}
                      </div>
                    </div>
                    <div className="flex-1 text-left">
                      <span
                        className={`inline-block px-3 py-1 rounded-full text-xs font-medium ${
                          reservation.status === "confirmed"
                            ? "bg-green-100 text-green-700"
                            : reservation.status === "checked-in"
                            ? "bg-blue-100 text-blue-700"
                            : reservation.status === "pending"
                            ? "bg-yellow-100 text-yellow-700"
                            : "bg-slate-100 text-slate-700"
                        }`}
                      >
                        {reservation.status === "confirmed"
                          ? "مؤكد"
                          : reservation.status === "checked-in"
                          ? "تم تسجيل الدخول"
                          : reservation.status === "pending"
                          ? "قيد الانتظار"
                          : reservation.status}
                      </span>
                    </div>
                    <div className="font-bold text-slate-900">
                      {reservation.totalPrice} ريال
                    </div>
                  </div>
                );
              })} */}
            </div>
          </CardContent>
        </Card>
      </div>
    </div>
  );
}
