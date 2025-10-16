import * as React from "react";
import * as Popover from "@radix-ui/react-popover";
import { CheckIcon, ChevronDownIcon } from "lucide-react";
import { cn } from "../../lib/utils";

interface Option {
  label: string;
  value: string;
}

interface MultiSelectProps {
  options?: Option[] | null;
  value?: string[] | null;
  onChange?: (value: string[]) => void;
  placeholder?: string;
}

export function MultiSelect({
  options = [],
  value = [],
  onChange = () => {},
  placeholder = "Select options",
}: MultiSelectProps) {
  const [open, setOpen] = React.useState(false);

  const toggleOption = (val: string) => {
    const currentValue = value || [];
    if (currentValue.includes(val)) {
      onChange(currentValue.filter((v) => v !== val));
    } else {
      onChange([...currentValue, val]);
    }
  };

  const selectedLabels =
    options
      ?.filter((opt) => value?.includes(opt.value))
      ?.map((opt) => opt.label) || [];

  return (
    <Popover.Root open={open} onOpenChange={setOpen}>
      <Popover.Trigger asChild>
        <button
          className={cn(
            "flex items-center justify-between gap-2 rounded-md border border-input bg-transparent px-3 py-2 text-sm shadow-sm outline-none transition focus-visible:ring-2 focus-visible:ring-ring"
          )}
          type="button"
        >
          <span className="truncate max-w-[180px]">
            {selectedLabels.length > 0
              ? selectedLabels.join(", ")
              : placeholder}
          </span>
          <ChevronDownIcon className="size-4 opacity-50" />
        </button>
      </Popover.Trigger>

      <Popover.Portal>
        <Popover.Content
          sideOffset={4}
          className="z-50 w-[var(--radix-popover-trigger-width)] rounded-md border bg-popover p-1 shadow-md"
        >
          {options?.map((opt) => (
            <button
              key={opt.value}
              onClick={() => toggleOption(opt.value)}
              className={cn(
                "relative flex w-full cursor-pointer select-none items-center rounded-sm py-1.5 pl-8 pr-2 text-sm hover:bg-accent hover:text-accent-foreground",
                value?.includes(opt.value) && "bg-accent text-accent-foreground"
              )}
            >
              {value?.includes(opt.value) && (
                <CheckIcon className="absolute left-2 size-4" />
              )}
              {opt.label}
            </button>
          ))}
        </Popover.Content>
      </Popover.Portal>
    </Popover.Root>
  );
}
