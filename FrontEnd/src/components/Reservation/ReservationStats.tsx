import { Card, CardContent } from "../../components/ui/card";

interface ReservationStatsProps {
  total: number;
  pending: number;
  confirmed: number;
  cancelled: number;
}

export function ReservationStats({
  total,
  pending,
  confirmed,
  cancelled,
}: ReservationStatsProps) {
  return (
    <div className="grid grid-cols-4 gap-4 mb-8">
      <Card className="border-slate-200 bg-white">
        <CardContent className="pt-6">
          <div className="text-2xl font-bold text-slate-900">{total}</div>
          <div className="text-sm text-slate-600">الإجمالي</div>
        </CardContent>
      </Card>
      <Card className="border-slate-200 bg-white">
        <CardContent className="pt-6">
          <div className="text-2xl font-bold text-yellow-600">{pending}</div>
          <div className="text-sm text-slate-600">قيد الانتظار</div>
        </CardContent>
      </Card>
      <Card className="border-slate-200 bg-white">
        <CardContent className="pt-6">
          <div className="text-2xl font-bold text-green-600">{confirmed}</div>
          <div className="text-sm text-slate-600">مؤكد</div>
        </CardContent>
      </Card>
      <Card className="border-slate-200 bg-white">
        <CardContent className="pt-6">
          <div className="text-2xl font-bold text-red-600">{cancelled}</div>
          <div className="text-sm text-slate-600">ملغي</div>
        </CardContent>
      </Card>
    </div>
  );
}
