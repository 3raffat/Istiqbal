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
import type { Guest } from "../../lib/types";

interface GuestComboboxProps {
  guests: Guest[];
  value: string;
  onValueChange: (value: string) => void;
  placeholder?: string;
  emptyText?: string;
}

export function GuestCombobox({
  guests,
  value,
  onValueChange,
  placeholder = "اختر النزيل...",
  emptyText = "لم يتم العثور على نزيل",
}: GuestComboboxProps) {
  const [open, setOpen] = React.useState(false);

  const selectedGuest = guests.find((guest) => guest.id === value);

  return (
    <Popover open={open} onOpenChange={setOpen}>
      <PopoverTrigger asChild>
        <Button
          variant="outline"
          role="combobox"
          aria-expanded={open}
          className="w-full justify-between text-right bg-transparent"
        >
          {selectedGuest ? (
            <span>
              {selectedGuest.fullName} - {selectedGuest.email}
            </span>
          ) : (
            <span className="text-slate-500">{placeholder}</span>
          )}
          <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
        </Button>
      </PopoverTrigger>

      <PopoverContent className="w-full p-0" align="start">
        <Command dir="rtl">
          <CommandInput placeholder="ابحث عن نزيل..." className="text-right" />
          <CommandList>
            <CommandEmpty>{emptyText}</CommandEmpty>
            {guests.map((guest) => (
              <CommandItem
                key={guest.id}
                value={guest.id}
                onSelect={() => {
                  onValueChange(guest.id);
                  setOpen(false);
                }}
                className="flex items-center gap-2 cursor-pointer"
              >
                <Check
                  className={cn(
                    "h-4 w-4",
                    value === guest.id ? "opacity-100" : "opacity-0"
                  )}
                />
                <div className="flex flex-col">
                  <span className="font-medium">{guest.fullName}</span>
                  <span className="text-sm text-slate-500">{guest.email}</span>
                </div>
              </CommandItem>
            ))}
          </CommandList>
        </Command>
      </PopoverContent>
    </Popover>
  );
}
