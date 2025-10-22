import { Card, CardContent } from "../../components/ui/card";

interface RoomStatsProps {
  total: number;
  available: number;
  maintenance: number;
  occupied: number;
  cleaning: number;
}

export default function RoomStats({
  total,
  available,
  maintenance,
  occupied,
  cleaning,
}: RoomStatsProps) {
  const stats = [
    { label: "إجمالي الغرف", value: total, color: "text-slate-900" },
    { label: "متاحة", value: available, color: "text-green-600" },
    { label: "تحت الصيانة", value: maintenance, color: "text-slate-600" },
    { label: "محجوزة", value: occupied, color: "text-red-600" },
    { label: "تحت التنظيف", value: cleaning, color: "text-yellow-600" },
  ];

  return (
    <div className="grid md:grid-cols-5 gap-4 mb-8">
      {stats.map((s) => (
        <Card key={s.label} className="border-slate-200 bg-white">
          <CardContent className="pt-6">
            <div className={`text-2xl font-bold ${s.color}`}>{s.value}</div>
            <div className="text-sm text-slate-600">{s.label}</div>
          </CardContent>
        </Card>
      ))}
    </div>
  );
}
