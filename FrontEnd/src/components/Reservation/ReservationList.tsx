import { Card, CardContent } from "../ui/card";
import { ReservationCard } from "./ReservationCard";

interface ReservationListProps {
  reservations: any[];
  onEdit: (reservation: any) => void;
  onCancel: (reservation: any) => void;
}

export function ReservationList({
  reservations,
  onEdit,
  onCancel,
}: ReservationListProps) {
  if (!reservations.length) {
    return (
      <Card className="border-slate-200 bg-white">
        <CardContent className="pt-6 text-center py-12">
          <p className="text-slate-500">لا توجد حجوزات</p>
        </CardContent>
      </Card>
    );
  }

  return (
    <div className="space-y-4">
      {reservations.map((res) => (
        <ReservationCard
          key={res.reservationId}
          reservation={res}
          onEdit={onEdit}
          onCancel={onCancel}
        />
      ))}
    </div>
  );
}
