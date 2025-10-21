import { Badge } from "../../components/ui/badge";
import { Button } from "../../components/ui/button";
import { Edit, Eye, X } from "lucide-react";

interface ReservationCardProps {
  reservation: any;
  onEdit: (reservation: any) => void;
  onCancel: (reservationId: string) => void;
}

const getStatusColor = (status: string) => {
  const statusMap: Record<string, string> = {
    pending: "bg-yellow-100 text-yellow-800",
    confirmed: "bg-green-100 text-green-800",
    checkedin: "bg-blue-100 text-blue-800",
    checkedout: "bg-slate-100 text-slate-800",
    cancelled: "bg-red-100 text-red-800",
  };
  return statusMap[status.toLowerCase()] || "bg-slate-100 text-slate-800";
};

const getStatusText = (status: string) => {
  const statusMap: Record<string, string> = {
    pending: "قيد الانتظار",
    confirmed: "مؤكد",
    checkedin: "حالي",
    checkedout: "منتهي",
    cancelled: "ملغي",
  };
  return statusMap[status.toLowerCase()] || status;
};

const getInitials = (name: string) =>
  name
    .split(" ")
    .map((n) => n[0])
    .join("")
    .toUpperCase()
    .slice(0, 2);

export const ReservationCard = ({
  reservation,
  onEdit,
  onCancel,
}: ReservationCardProps) => {
  return (
    <div className="border-slate-200 bg-white hover:shadow-md transition-shadow rounded-md p-4 flex flex-col lg:flex-row lg:items-center gap-4">
      <div className="flex-1 flex items-center gap-3 mb-2">
        <div className="w-10 h-10 rounded-full bg-amber-100 flex items-center justify-center text-amber-700 font-bold">
          {getInitials(reservation.guestFullName)}
        </div>
        <div>
          <h3 className="font-bold text-slate-900">
            {reservation.guestFullName}
          </h3>
        </div>
      </div>

      <div className="flex-1 text-sm">
        <div className="text-slate-600 mb-1">الغرفة</div>
        <div className="font-medium text-slate-900">
          غرفة {reservation.roomNumber} - {reservation.roomtype}
        </div>
      </div>

      <div className="flex-1 text-sm">
        <div className="text-slate-600 mb-1">التواريخ</div>
        <div className="font-medium text-slate-900">
          {new Date(reservation.checkInDate).toLocaleDateString("ar-SA")} →{" "}
          {new Date(reservation.checkOutDate).toLocaleDateString("ar-SA")}
        </div>
      </div>

      <div className="flex-1">
        <div className="text-sm mb-2">
          <div className="text-slate-600 mb-1">المبلغ</div>
          <div className="font-bold text-slate-900">
            {reservation.amount} ريال
          </div>
        </div>
        <Badge className={getStatusColor(reservation.status)}>
          {getStatusText(reservation.status)}
        </Badge>
      </div>

      <div className="flex gap-2">
        <Button size="sm" variant="outline" title="عرض التفاصيل">
          <Eye className="w-4 h-4" />
        </Button>
        <Button
          size="sm"
          variant="outline"
          title="تعديل"
          onClick={() => onEdit(reservation)}
        >
          <Edit className="w-4 h-4" />
        </Button>
        {reservation.status.toLowerCase() === "pending" && (
          <Button
            size="sm"
            variant="outline"
            className="text-red-600 hover:text-red-700 hover:bg-red-50 bg-transparent"
            title="إلغاء"
            onClick={() => onCancel(reservation.reservationId)}
          >
            <X className="w-4 h-4" />
          </Button>
        )}
      </div>
    </div>
  );
};
