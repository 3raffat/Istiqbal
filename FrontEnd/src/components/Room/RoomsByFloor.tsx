import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
} from "../../components/ui/card";
import RoomCard from "./RoomCard";

interface Props {
  groupedRooms: Record<number, any[]>;
  onEdit: (room: any) => void;
  onDelete: (id: string) => void;
}

export default function RoomsByFloor({
  groupedRooms,
  onEdit,
  onDelete,
}: Props) {
  return (
    <div className="space-y-6">
      {Object.keys(groupedRooms)
        .sort((a, b) => Number(b) - Number(a))
        .map((floor) => (
          <Card key={floor} className="border-slate-200 bg-white">
            <CardHeader>
              <CardTitle className="text-xl">الطابق {floor}</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-4">
                {groupedRooms[Number(floor)].map((room) => (
                  <RoomCard
                    key={room.id}
                    room={room}
                    onEdit={onEdit}
                    onDelete={onDelete}
                  />
                ))}
              </div>
            </CardContent>
          </Card>
        ))}
    </div>
  );
}
