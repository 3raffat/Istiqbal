import * as React from "react";
import { Check, ChevronsUpDown } from "lucide-react";
import { Popover, PopoverContent, PopoverTrigger } from "./popover";
import { Button } from "./button";
import {
  Command,
  CommandEmpty,
  CommandInput,
  CommandItem,
  CommandList,
} from "./command";
import { cn } from "../../lib/utils";
import type { Room, RoomType } from "../../lib/types";

interface RoomComboboxProps {
  rooms: Room[];
  roomTypes: RoomType[];
  value: string;
  onValueChange: (value: string) => void;
  placeholder?: string;
}

export function RoomCombobox({
  rooms,
  roomTypes,
  value,
  onValueChange,
  placeholder,
}: RoomComboboxProps) {
  const [open, setOpen] = React.useState(false);

  const getRoomType = (roomTypeId: string) =>
    roomTypes.find((rt) => rt.id === roomTypeId);

  const selectedRoom = rooms.find((room) => room.id === value);
  const selectedRoomType = selectedRoom
    ? getRoomType(selectedRoom.roomTypeName)
    : null;

  return (
    <Popover open={open} onOpenChange={setOpen}>
      <PopoverTrigger asChild>
        <Button
          variant="outline"
          role="combobox"
          aria-expanded={open}
          className="w-full justify-between text-right bg-transparent"
        >
          {value ? (
            <span>
              غرفة {selectedRoom?.number} - {selectedRoomType?.name} (
              {selectedRoomType?.pricePerNight} $ /ليلة)
            </span>
          ) : (
            <span className="text-slate-500">
              {placeholder || "اختر الغرفة..."}
            </span>
          )}
          <ChevronsUpDown className="mr-2 h-4 w-4 shrink-0 opacity-50" />
        </Button>
      </PopoverTrigger>

      <PopoverContent className="w-full p-0" align="start">
        <Command dir="rtl">
          <CommandInput placeholder="ابحث عن غرفة..." className="text-right" />
          <CommandList>
            <CommandEmpty>لا توجد غرف</CommandEmpty>
            {rooms.map((room) => {
              const roomType = getRoomType(room.roomTypeName);
              return (
                <CommandItem
                  key={room.id}
                  value={room.id}
                  onSelect={() => {
                    onValueChange(room.id);
                    setOpen(false);
                  }}
                  className="flex items-center justify-between cursor-pointer"
                >
                  <div className="flex items-center gap-2">
                    <Check
                      className={cn(
                        "h-4 w-4",
                        value === room.id ? "opacity-100" : "opacity-0"
                      )}
                    />
                    <div>
                      <div className="font-medium">
                        غرفة {room.number} - {roomType?.name}
                      </div>
                      <div className="text-sm text-slate-500">
                        {roomType?.pricePerNight} $ /ليلة
                      </div>
                    </div>
                  </div>
                </CommandItem>
              );
            })}
          </CommandList>
        </Command>
      </PopoverContent>
    </Popover>
  );
}
