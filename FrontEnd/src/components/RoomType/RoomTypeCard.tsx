"use client";
import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
  CardDescription,
} from "../../components/ui/card";
import { Button } from "../../components/ui/button";
import { Badge } from "../../components/ui/badge";
import { Edit, Trash2, DollarSign, Users } from "lucide-react";

interface RoomTypeCardProps {
  roomType: any;
  onEdit: (roomType: any) => void;
  onDelete: (id: string) => void;
}

export function RoomTypeCard({
  roomType,
  onEdit,
  onDelete,
}: RoomTypeCardProps) {
  return (
    <Card className="hover:shadow-lg transition-shadow rounded-xl border border-slate-200">
      <CardHeader>
        <CardTitle className="text-lg font-semibold">{roomType.name}</CardTitle>
        <CardDescription className="mt-1 text-slate-600">
          {roomType.description}
        </CardDescription>
      </CardHeader>
      <CardContent className="space-y-3">
        <div className="flex justify-between text-sm text-slate-700">
          <div className="flex items-center gap-2">
            <DollarSign className="w-4 h-4 text-amber-500" /> السعر لليلة
          </div>
          <span className="font-bold text-amber-600">
            {roomType.pricePerNight} $
          </span>
        </div>
        <div className="flex justify-between text-sm text-slate-700">
          <div className="flex items-center gap-2">
            <Users className="w-4 h-4 text-amber-500" /> الحد الأقصى
          </div>
          <span>{roomType.maxOccupancy} نزلاء</span>
        </div>
        <div>
          <p className="text-sm text-slate-600 mb-1">المرافق:</p>
          <div className="flex flex-wrap gap-2">
            {roomType.amenities?.length ? (
              roomType.amenities.map((a: any) => (
                <Badge
                  key={a.id}
                  variant="secondary"
                  className="text-xs rounded-md"
                >
                  {a.name}
                </Badge>
              ))
            ) : (
              <span className="text-sm text-slate-500">لا توجد مرافق</span>
            )}
          </div>
        </div>
        <div className="flex gap-2 pt-4">
          <Button
            variant="outline"
            size="sm"
            className="flex-1 border-slate-300 hover:bg-amber-50"
            onClick={() => onEdit(roomType)}
          >
            <Edit className="w-4 h-4 ml-1" /> تعديل
          </Button>
          <Button
            variant="outline"
            size="sm"
            className="text-red-600 hover:text-red-700 hover:bg-red-50"
            onClick={() => onDelete(roomType.id)}
          >
            <Trash2 className="w-4 h-4" />
          </Button>
        </div>
      </CardContent>
    </Card>
  );
}
