"use client";

import * as React from "react";
import * as SelectPrimitive from "@radix-ui/react-select";
import { CheckIcon, ChevronDownIcon, ChevronUpIcon } from "lucide-react";
import { cn } from "../../lib/utils";

interface MultiSelectOption {
  id: string;
  value: string;
}

interface MultiSelectProps {
  options: MultiSelectOption[];
  selectedIds: string[];
  onChange: (ids: string[]) => void;
  placeholder?: string;
}

export function MultiSelect({
  options,
  selectedIds,
  onChange,
  placeholder = "Select...",
}: MultiSelectProps) {
  const toggleSelect = (id: string) => {
    if (selectedIds.includes(id)) {
      onChange(selectedIds.filter((sid) => sid !== id));
    } else {
      onChange([...selectedIds, id]);
    }
  };

  return (
    <SelectPrimitive.Root>
      <SelectPrimitive.Trigger className="w-full flex items-center justify-between rounded-lg border px-3 py-2 text-sm">
        {selectedIds.length > 0
          ? options
              .filter((o) => selectedIds.includes(o.id))
              .map((o) => o.value)
              .join(", ")
          : placeholder}
        <ChevronDownIcon className="ml-2 h-4 w-4" />
      </SelectPrimitive.Trigger>
      <SelectPrimitive.Content className="bg-white border rounded-md mt-1 shadow-md">
        <SelectPrimitive.Viewport className="p-2">
          {options.map((option) => (
            <div
              key={option.id}
              className={cn(
                "flex items-center justify-between cursor-pointer px-2 py-1 rounded hover:bg-gray-100",
                selectedIds.includes(option.id) ? "bg-gray-200" : ""
              )}
              onClick={() => toggleSelect(option.id)}
            >
              <span>{option.value}</span>
              {selectedIds.includes(option.id) && (
                <CheckIcon className="h-4 w-4" />
              )}
            </div>
          ))}
        </SelectPrimitive.Viewport>
      </SelectPrimitive.Content>
    </SelectPrimitive.Root>
  );
}
