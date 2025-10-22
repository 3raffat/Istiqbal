import { Card, CardContent } from "../../components/ui/card";
import { Badge } from "../../components/ui/badge";
import { Button } from "../../components/ui/button";
import { Edit, Eye, X } from "lucide-react";
import { getInitials, getStatusColorr, getStatusTextt } from "../../Data/data";
import type { ReservationResponse } from "../../lib/types";

interface ReservationCardProps {
  reservation: ReservationResponse;
  onEdit: (reservation: ReservationResponse) => void;
  onCancel: (reservation: ReservationResponse) => void;
}

export function ReservationCard({
  reservation,
  onEdit,
  onCancel,
}: ReservationCardProps) {
  return (
    <Card className="border-slate-200 bg-white hover:shadow-md transition-shadow">
      <CardContent className="pt-6">
        <div className="flex flex-col lg:flex-row lg:items-center gap-4">
          {/* Guest Info */}
          <div className="flex-1 flex items-center gap-3 mb-2">
            <div className="w-10 h-10 rounded-full bg-amber-100 flex items-center justify-center text-amber-700 font-bold">
              {getInitials(reservation.guestFullName)}
            </div>
            <h3 className="font-bold text-slate-900">
              {reservation.guestFullName}
            </h3>
          </div>

          {/* Room Info */}
          <div className="flex-1">
            <div className="text-sm text-slate-600 mb-1">الغرفة</div>
            <div className="font-medium text-slate-900">
              غرفة {reservation.roomNumber} - {reservation.roomtype}
            </div>
          </div>

          {/* Dates */}
          <div className="flex-1">
            <div className="text-sm text-slate-600 mb-1">التواريخ</div>
            <div className="font-medium text-slate-900">
              {new Date(reservation.checkOutDate).toLocaleDateString("en-GB")} →{" "}
              {new Date(reservation.checkInDate).toLocaleDateString("en-GB")}
            </div>
          </div>

          {/* Amount & Status */}
          <div className="flex-1">
            <div className="text-sm mb-2 text-slate-600">المبلغ</div>
            <div className="font-bold text-slate-900">
              {reservation.amount} $
            </div>
            <Badge className={getStatusColorr(reservation.status)}>
              {getStatusTextt(reservation.status)}
            </Badge>
          </div>

          {/* Actions */}
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
                onClick={() => onCancel(reservation)}
              >
                <X className="w-4 h-4" />
              </Button>
            )}
          </div>
        </div>
      </CardContent>
    </Card>
  );
}
