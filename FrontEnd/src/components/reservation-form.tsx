// "use client"

// import { useForm } from "react-hook-form"
// import { zodResolver } from "@hookform/resolvers/zod"
// import { Button } from "@/components/ui/button"
// import { Input } from "@/components/ui/input"
// import { Label } from "@/components/ui/label"
// import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
// import { reservationSchema } from "@/lib/validations"
// import type { Guest, Room } from "@/lib/types"
// import { useState, useEffect } from "react"

// interface ReservationFormData {
//   guestId: string
//   roomId: string
//   checkInDate: string
//   checkOutDate: string
//   totalAmount: number
//   status: string
// }

// interface ReservationFormProps {
//   guests: Guest[]
//   rooms: Room[]
//   onSubmit: (data: ReservationFormData) => Promise<void>
//   defaultValues?: Partial<ReservationFormData>
//   submitLabel?: string
// }

// export function ReservationForm({
//   guests,
//   rooms,
//   onSubmit,
//   defaultValues,
//   submitLabel = "Create Reservation",
// }: ReservationFormProps) {
//   const [isLoading, setIsLoading] = useState(false)
//   const {
//     register,
//     handleSubmit,
//     formState: { errors },
//     setValue,
//     watch,
//   } = useForm<ReservationFormData>({
//     resolver: zodResolver(reservationSchema),
//     defaultValues: defaultValues || {
//       status: "Confirmed",
//     },
//   })

//   const selectedGuestId = watch("guestId")
//   const selectedRoomId = watch("roomId")
//   const selectedStatus = watch("status")
//   const checkInDate = watch("checkInDate")
//   const checkOutDate = watch("checkOutDate")

//   // Calculate total amount based on room and dates
//   useEffect(() => {
//     if (selectedRoomId && checkInDate && checkOutDate) {
//       const room = rooms.find((r) => r.id === selectedRoomId)
//       if (room) {
//         const nights = Math.ceil(
//           (new Date(checkOutDate).getTime() - new Date(checkInDate).getTime()) / (1000 * 60 * 60 * 24),
//         )
//         if (nights > 0) {
//           setValue("totalAmount", room.type.pricePerNight * nights)
//         }
//       }
//     }
//   }, [selectedRoomId, checkInDate, checkOutDate, rooms, setValue])

//   const handleFormSubmit = async (data: ReservationFormData) => {
//     setIsLoading(true)
//     await onSubmit(data)
//     setIsLoading(false)
//   }

//   return (
//     <form onSubmit={handleSubmit(handleFormSubmit)} className="space-y-4">
//       <div className="space-y-2">
//         <Label htmlFor="guestId">Guest</Label>
//         <Select value={selectedGuestId} onValueChange={(value) => setValue("guestId", value)}>
//           <SelectTrigger className="bg-background border-border">
//             <SelectValue placeholder="Select guest" />
//           </SelectTrigger>
//           <SelectContent className="bg-card border-border">
//             {guests.map((guest) => (
//               <SelectItem key={guest.id} value={guest.id}>
//                 {guest.fullName} - {guest.email}
//               </SelectItem>
//             ))}
//           </SelectContent>
//         </Select>
//         {errors.guestId && <p className="text-sm text-destructive">{errors.guestId.message}</p>}
//       </div>

//       <div className="space-y-2">
//         <Label htmlFor="roomId">Room</Label>
//         <Select value={selectedRoomId} onValueChange={(value) => setValue("roomId", value)}>
//           <SelectTrigger className="bg-background border-border">
//             <SelectValue placeholder="Select room" />
//           </SelectTrigger>
//           <SelectContent className="bg-card border-border">
//             {rooms
//               .filter((room) => room.status === "Available")
//               .map((room) => (
//                 <SelectItem key={room.id} value={room.id}>
//                   Room {room.number} - {room.type.name} (${room.type.pricePerNight}/night)
//                 </SelectItem>
//               ))}
//           </SelectContent>
//         </Select>
//         {errors.roomId && <p className="text-sm text-destructive">{errors.roomId.message}</p>}
//       </div>

//       <div className="grid grid-cols-2 gap-4">
//         <div className="space-y-2">
//           <Label htmlFor="checkInDate">Check-in Date</Label>
//           <Input id="checkInDate" type="date" {...register("checkInDate")} className="bg-background border-border" />
//           {errors.checkInDate && <p className="text-sm text-destructive">{errors.checkInDate.message}</p>}
//         </div>

//         <div className="space-y-2">
//           <Label htmlFor="checkOutDate">Check-out Date</Label>
//           <Input id="checkOutDate" type="date" {...register("checkOutDate")} className="bg-background border-border" />
//           {errors.checkOutDate && <p className="text-sm text-destructive">{errors.checkOutDate.message}</p>}
//         </div>
//       </div>

//       <div className="space-y-2">
//         <Label htmlFor="totalAmount">Total Amount ($)</Label>
//         <Input
//           id="totalAmount"
//           type="number"
//           step="0.01"
//           placeholder="0.00"
//           {...register("totalAmount", { valueAsNumber: true })}
//           className="bg-background border-border"
//           readOnly
//         />
//         {errors.totalAmount && <p className="text-sm text-destructive">{errors.totalAmount.message}</p>}
//       </div>

//       <div className="space-y-2">
//         <Label htmlFor="status">Status</Label>
//         <Select value={selectedStatus} onValueChange={(value) => setValue("status", value)}>
//           <SelectTrigger className="bg-background border-border">
//             <SelectValue placeholder="Select status" />
//           </SelectTrigger>
//           <SelectContent className="bg-card border-border">
//             <SelectItem value="Confirmed">Confirmed</SelectItem>
//             <SelectItem value="Pending">Pending</SelectItem>
//             <SelectItem value="Cancelled">Cancelled</SelectItem>
//             <SelectItem value="Completed">Completed</SelectItem>
//           </SelectContent>
//         </Select>
//         {errors.status && <p className="text-sm text-destructive">{errors.status.message}</p>}
//       </div>

//       <Button
//         type="submit"
//         className="w-full bg-primary text-primary-foreground hover:bg-primary/90"
//         disabled={isLoading}
//       >
//         {isLoading ? "Saving..." : submitLabel}
//       </Button>
//     </form>
//   )
// }
