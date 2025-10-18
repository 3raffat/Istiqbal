import { useForm } from "react-hook-form";
import { Button } from "./ui/button";
import { Input } from "./ui/input";
import { Label } from "./ui/label";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "./ui/select";
import { useState } from "react";
import type { RoomType } from "../lib/types";

interface RoomFormData {
  number: number;
  roomTypeId: string;
  floor: number;
  status: string;
}

interface RoomFormProps {
  roomTypes: RoomType[];
  onSubmit: (data: RoomFormData) => Promise<void>;
  defaultValues?: Partial<RoomFormData>;
  submitLabel?: string;
}

export function RoomForm({
  roomTypes,
  onSubmit,
  defaultValues,
  submitLabel = "Create Room",
}: RoomFormProps) {
  const [isLoading, setIsLoading] = useState(false);
  const {
    register,
    handleSubmit,
    formState: { errors },
    setValue,
    watch,
  } = useForm<RoomFormData>({
    defaultValues: defaultValues || {
      status: "Available",
    },
  });

  const selectedRoomTypeId = watch("roomTypeId");
  const selectedStatus = watch("status");

  const handleFormSubmit = async (data: RoomFormData) => {
    setIsLoading(true);
    await onSubmit(data);
    setIsLoading(false);
  };

  return (
    <form onSubmit={handleSubmit(handleFormSubmit)} className="space-y-4">
      <div className="space-y-2">
        <Label htmlFor="number">Room Number</Label>
        <Input
          id="number"
          type="number"
          placeholder="e.g., 301"
          {...register("number", { valueAsNumber: true })}
          className="bg-background border-border"
        />
        {errors.number && (
          <p className="text-sm text-destructive">{errors.number.message}</p>
        )}
      </div>

      <div className="space-y-2">
        <Label htmlFor="roomTypeId">Room Type</Label>
        <Select
          value={selectedRoomTypeId}
          onValueChange={(value) => setValue("roomTypeId", value)}
        >
          <SelectTrigger className="bg-background border-border">
            <SelectValue placeholder="Select room type" />
          </SelectTrigger>
          <SelectContent className="bg-card border-border">
            {roomTypes.map((type) => (
              <SelectItem key={type.id} value={type.id}>
                {type.name} - ${type.basePrice}/night (Max: {type.capacity}{" "}
                guests)
              </SelectItem>
            ))}
          </SelectContent>
        </Select>
        {errors.roomTypeId && (
          <p className="text-sm text-destructive">
            {errors.roomTypeId.message}
          </p>
        )}
      </div>

      <div className="space-y-2">
        <Label htmlFor="floor">Floor</Label>
        <Input
          id="floor"
          type="number"
          placeholder="e.g., 3"
          {...register("floor", { valueAsNumber: true })}
          className="bg-background border-border"
        />
        {errors.floor && (
          <p className="text-sm text-destructive">{errors.floor.message}</p>
        )}
      </div>

      <div className="space-y-2">
        <Label htmlFor="status">Status</Label>
        <Select
          value={selectedStatus}
          onValueChange={(value) => setValue("status", value as string)}
        >
          <SelectTrigger className="bg-background border-border">
            <SelectValue placeholder="Select status" />
          </SelectTrigger>
          <SelectContent className="bg-card border-border">
            <SelectItem value={"Available"}>Available</SelectItem>
            <SelectItem value={"Occupied"}>Occupied</SelectItem>
            <SelectItem value={"Maintenance"}>Maintenance</SelectItem>
            <SelectItem value={"Cleaning"}>Cleaning</SelectItem>
          </SelectContent>
        </Select>
        {errors.status && (
          <p className="text-sm text-destructive">{errors.status.message}</p>
        )}
      </div>

      <Button
        type="submit"
        className="w-full bg-primary text-primary-foreground hover:bg-primary/90"
        disabled={isLoading}
      >
        {isLoading ? "Saving..." : submitLabel}
      </Button>
    </form>
  );
}
