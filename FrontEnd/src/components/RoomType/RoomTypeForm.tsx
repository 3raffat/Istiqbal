"use client";

import * as React from "react";
import { Controller, useForm } from "react-hook-form";
import { Label } from "../../components/ui/label";
import { Input } from "../../components/ui/input";
import { Textarea } from "../../components/ui/textarea";
import { Button } from "../../components/ui/button";
import { CheckIcon, ChevronDownIcon } from "lucide-react";
import * as SelectPrimitive from "@radix-ui/react-select";
import { cn } from "../../lib/utils";
import type { CreateRoomTypeRequest } from "../../lib/types";

interface RoomTypeFormProps {
  defaultValues?: CreateRoomTypeRequest;
  amenities: { id: string; name: string }[];
  onSubmit: (data: CreateRoomTypeRequest) => void;
  onCancel: () => void;
  submitLabel: string;
}

export function RoomTypeForm({
  defaultValues,
  amenities,
  onSubmit,
  onCancel,
  submitLabel,
}: RoomTypeFormProps) {
  const {
    register,
    handleSubmit,
    control,
    formState: { errors },
  } = useForm<CreateRoomTypeRequest>({
    defaultValues: {
      ...defaultValues,
      amenitieIds: defaultValues?.amenitieIds ?? [],
    },
  });

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
      {/* بيانات النوع */}
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        <div>
          <Label className="mb-1">اسم النوع</Label>
          <Input
            {...register("name", { required: "الاسم مطلوب" })}
            placeholder="مثال: غرفة ديلوكس"
            className={errors.name ? "border-red-500" : ""}
          />
        </div>
        <div>
          <Label className="mb-1">السعر لليلة ($)</Label>
          <Input
            type="number"
            {...register("pricePerNight", { required: true })}
            placeholder="500"
          />
        </div>
        <div>
          <Label className="mb-1">الحد الأقصى للنزلاء</Label>
          <Input
            type="number"
            {...register("maxOccupancy", { required: true })}
            placeholder="2"
          />
        </div>
      </div>

      <div>
        <Label className="mb-1">الوصف</Label>
        <Textarea
          {...register("description")}
          rows={3}
          placeholder="وصف تفصيلي لنوع الغرفة"
        />
      </div>

      {/* Styled MultiSelect */}
      <div>
        <Label className="mb-1">المرافق</Label>
        <Controller
          name="amenitieIds"
          control={control}
          render={({ field }) => {
            const toggle = (id: string) => {
              if (field.value.includes(id)) {
                field.onChange(field.value.filter((v) => v !== id));
              } else {
                field.onChange([...field.value, id]);
              }
            };

            return (
              <SelectPrimitive.Root>
                <SelectPrimitive.Trigger className="w-full flex items-center justify-between rounded-lg border px-3 py-2 text-sm">
                  {field.value.length > 0
                    ? amenities
                        .filter((a) => field.value.includes(a.id))
                        .map((a) => a.name)
                        .join(", ")
                    : "اختر المرافق..."}
                  <ChevronDownIcon className="ml-2 h-4 w-4" />
                </SelectPrimitive.Trigger>

                <SelectPrimitive.Content className="bg-white border rounded-md mt-1 shadow-md z-50">
                  <SelectPrimitive.Viewport className="p-2 max-h-60 overflow-y-auto">
                    {amenities.map((a) => (
                      <div
                        key={a.id}
                        className={cn(
                          "flex items-center justify-between cursor-pointer px-2 py-1 rounded hover:bg-gray-100",
                          field.value.includes(a.id) ? "bg-gray-200" : ""
                        )}
                        onClick={() => toggle(a.id)}
                      >
                        <span>{a.name}</span>
                        {field.value.includes(a.id) && (
                          <CheckIcon className="h-4 w-4" />
                        )}
                      </div>
                    ))}
                  </SelectPrimitive.Viewport>
                </SelectPrimitive.Content>
              </SelectPrimitive.Root>
            );
          }}
        />
      </div>

      <div className="flex justify-end gap-3 pt-4">
        <Button type="button" variant="outline" onClick={onCancel}>
          إلغاء
        </Button>
        <Button
          type="submit"
          className="bg-amber-500 hover:bg-amber-600 text-white"
        >
          {submitLabel}
        </Button>
      </div>
    </form>
  );
}
