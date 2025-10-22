import { Badge } from "../../components/ui/badge";
import { Button } from "../../components/ui/button";
import { Edit, Trash2 } from "lucide-react";
import { getStatusColor, getStatusText } from "../../Data/data";

interface Props {
  room: any;
  onEdit: (room: any) => void;
  onDelete: (id: string) => void;
}

export default function RoomCard({ room, onEdit, onDelete }: Props) {
  return (
    <div className="border rounded-lg p-4 hover:shadow-md transition-shadow">
      <div className="flex items-start justify-between mb-3">
        <div>
          <div className="text-lg font-bold text-slate-900">
            غرفة {room.number}
          </div>
          <div className="text-sm text-slate-600">{room.roomTypeName}</div>
        </div>
        <Badge className={getStatusColor(room.status)}>
          {getStatusText(room.status)}
        </Badge>
      </div>
      <div className="flex gap-2 mt-4">
        <Button
          size="sm"
          variant="outline"
          className="flex-1 bg-transparent"
          onClick={() => onEdit(room)}
        >
          <Edit className="w-3 h-3 ml-1" /> تعديل
        </Button>
        <Button
          size="sm"
          variant="outline"
          className="text-red-600 hover:text-red-700 bg-transparent"
          onClick={() => onDelete(room.id)}
        >
          <Trash2 className="w-3 h-3" />
        </Button>
      </div>
    </div>
  );
}
