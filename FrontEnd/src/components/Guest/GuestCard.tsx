import { Card, CardContent } from "../../components/ui/card";
import { Badge } from "../../components/ui/badge";
import { Button } from "../../components/ui/button";
import { Users, Mail, Phone } from "lucide-react";
import { GuestFormData } from "./GuestForm";

interface GuestCardProps {
  guest: any;
  onEdit: (guest: any) => void;
}

export function GuestCard({ guest, onEdit }: GuestCardProps) {
  return (
    <Card className="border-slate-200 bg-white hover:shadow-md transition-shadow">
      <CardContent className="pt-6">
        <div className="flex items-start justify-between">
          <div className="flex-1">
            <div className="flex items-center gap-3 mb-3">
              <div className="w-12 h-12 rounded-full bg-amber-100 flex items-center justify-center">
                <Users className="w-6 h-6 text-amber-600" />
              </div>
              <div>
                <h3 className="text-lg font-bold text-slate-900">
                  {guest.fullName}
                </h3>
                {guest.reservations.length > 0 && (
                  <Badge className="bg-green-100 text-green-700">
                    نزيل حالي
                  </Badge>
                )}
              </div>
            </div>

            <div className="grid md:grid-cols-2 gap-4 text-sm">
              <div className="flex items-center gap-2 text-slate-600">
                <Mail className="w-4 h-4" />
                <span>{guest.email}</span>
              </div>
              <div className="flex items-center gap-2 text-slate-600">
                <Phone className="w-4 h-4" />
                <span>{guest.phone}</span>
              </div>
            </div>

            <div className="mt-4 pt-4 border-t">
              <div className="text-sm text-slate-600 mb-2">سجل الحجوزات:</div>
              <div className="flex gap-4 text-sm">
                <span>
                  <span className="font-medium text-slate-900">
                    {guest.reservations.length}
                  </span>{" "}
                  حجز
                </span>
                <span>
                  <span className="font-medium text-slate-900">
                    {guest.reservations.reduce(
                      (sum: number, r: any) => sum + r.amount,
                      0
                    )}
                  </span>{" "}
                  $
                </span>
              </div>
            </div>
          </div>

          <div className="flex gap-2">
            <Button size="sm" variant="outline" onClick={() => onEdit(guest)}>
              تعديل
            </Button>
          </div>
        </div>
      </CardContent>
    </Card>
  );
}
