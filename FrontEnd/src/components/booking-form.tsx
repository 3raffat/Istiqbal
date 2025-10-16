// "use client"

// import { useState } from "react"
// import { useRouter } from "next/navigation"
// import { useForm } from "react-hook-form"
// import { Button } from "@/components/ui/button"
// import { Card, CardContent, CardFooter, CardHeader, CardTitle } from "@/components/ui/card"
// import { Input } from "@/components/ui/input"
// import { Form, FormField, FormItem, FormLabel } from "@/components/ui/form"
// import { Textarea } from "@/components/ui/textarea"
// import type { RoomType } from "@/lib/types"
// import { Calendar, Users } from "lucide-react"

// interface BookingFormProps {
//   roomType: RoomType
//   initialCheckIn?: string
//   initialCheckOut?: string
//   initialGuests?: string
// }

// interface BookingFormData {
//   checkIn: string
//   checkOut: string
//   guests: string
//   firstName: string
//   lastName: string
//   email: string
//   phone: string
//   specialRequests: string
// }

// export function BookingForm({
//   roomType,
//   initialCheckIn = "",
//   initialCheckOut = "",
//   initialGuests = "2",
// }: BookingFormProps) {
//   const router = useRouter()
//   const [isSubmitting, setIsSubmitting] = useState(false)

//   const form = useForm<BookingFormData>({
//     defaultValues: {
//       checkIn: initialCheckIn,
//       checkOut: initialCheckOut,
//       guests: initialGuests,
//       firstName: "",
//       lastName: "",
//       email: "",
//       phone: "",
//       specialRequests: "",
//     },
//   })

//   const calculateNights = () => {
//     const checkIn = form.watch("checkIn")
//     const checkOut = form.watch("checkOut")
//     if (!checkIn || !checkOut) return 0
//     const start = new Date(checkIn)
//     const end = new Date(checkOut)
//     const nights = Math.ceil((end.getTime() - start.getTime()) / (1000 * 60 * 60 * 24))
//     return nights > 0 ? nights : 0
//   }

//   const nights = calculateNights()
//   const totalPrice = nights * roomType.basePrice

//   const onSubmit = async (data: BookingFormData) => {
//     setIsSubmitting(true)

//     await new Promise((resolve) => setTimeout(resolve, 1500))

//     const reservationData = {
//       roomTypeId: roomType.id,
//       checkIn: data.checkIn,
//       checkOut: data.checkOut,
//       numberOfGuests: Number.parseInt(data.guests),
//       totalPrice,
//       guest: {
//         firstName: data.firstName,
//         lastName: data.lastName,
//         email: data.email,
//         phone: data.phone,
//       },
//       specialRequests: data.specialRequests,
//     }

//     console.log("[v0] Reservation data:", reservationData)

//     router.push(`/booking/payment?total=${totalPrice}&nights=${nights}`)
//   }

//   return (
//     <Card>
//       <CardHeader>
//         <CardTitle>Book This Room</CardTitle>
//       </CardHeader>
//       <Form {...form}>
//         <form onSubmit={form.handleSubmit(onSubmit)}>
//           <CardContent className="space-y-4">
//             <FormField
//               control={form.control}
//               name="checkIn"
//               rules={{ required: true }}
//               render={({ field }) => (
//                 <FormItem>
//                   <FormLabel className="flex items-center gap-2">
//                     <Calendar className="h-4 w-4" />
//                     Check-in Date
//                   </FormLabel>
//                   <Input type="date" min={new Date().toISOString().split("T")[0]} {...field} />
//                 </FormItem>
//               )}
//             />

//             <FormField
//               control={form.control}
//               name="checkOut"
//               rules={{ required: true }}
//               render={({ field }) => (
//                 <FormItem>
//                   <FormLabel className="flex items-center gap-2">
//                     <Calendar className="h-4 w-4" />
//                     Check-out Date
//                   </FormLabel>
//                   <Input type="date" min={form.watch("checkIn") || new Date().toISOString().split("T")[0]} {...field} />
//                 </FormItem>
//               )}
//             />

//             <FormField
//               control={form.control}
//               name="guests"
//               rules={{ required: true }}
//               render={({ field }) => (
//                 <FormItem>
//                   <FormLabel className="flex items-center gap-2">
//                     <Users className="h-4 w-4" />
//                     Number of Guests
//                   </FormLabel>
//                   <Input type="number" min="1" max={roomType.capacity} {...field} />
//                 </FormItem>
//               )}
//             />

//             {nights > 0 && (
//               <div className="p-4 bg-primary/10 rounded-lg space-y-2">
//                 <div className="flex justify-between text-sm">
//                   <span>
//                     ${roomType.basePrice} × {nights} nights
//                   </span>
//                   <span>${totalPrice}</span>
//                 </div>
//                 <div className="flex justify-between font-bold text-lg pt-2 border-t border-primary/20">
//                   <span>Total</span>
//                   <span className="text-primary">${totalPrice}</span>
//                 </div>
//               </div>
//             )}

//             <div className="pt-4 border-t space-y-4">
//               <h3 className="font-semibold">Guest Information</h3>

//               <div className="grid grid-cols-2 gap-4">
//                 <FormField
//                   control={form.control}
//                   name="firstName"
//                   rules={{ required: true }}
//                   render={({ field }) => (
//                     <FormItem>
//                       <FormLabel>First Name</FormLabel>
//                       <Input {...field} />
//                     </FormItem>
//                   )}
//                 />

//                 <FormField
//                   control={form.control}
//                   name="lastName"
//                   rules={{ required: true }}
//                   render={({ field }) => (
//                     <FormItem>
//                       <FormLabel>Last Name</FormLabel>
//                       <Input {...field} />
//                     </FormItem>
//                   )}
//                 />
//               </div>

//               <FormField
//                 control={form.control}
//                 name="email"
//                 rules={{ required: true }}
//                 render={({ field }) => (
//                   <FormItem>
//                     <FormLabel>Email</FormLabel>
//                     <Input type="email" {...field} />
//                   </FormItem>
//                 )}
//               />

//               <FormField
//                 control={form.control}
//                 name="phone"
//                 rules={{ required: true }}
//                 render={({ field }) => (
//                   <FormItem>
//                     <FormLabel>Phone Number</FormLabel>
//                     <Input type="tel" {...field} />
//                   </FormItem>
//                 )}
//               />

//               <FormField
//                 control={form.control}
//                 name="specialRequests"
//                 render={({ field }) => (
//                   <FormItem>
//                     <FormLabel>Special Requests (Optional)</FormLabel>
//                     <Textarea placeholder="Any special requirements or requests..." rows={3} {...field} />
//                   </FormItem>
//                 )}
//               />
//             </div>
//           </CardContent>

//           <CardFooter>
//             <Button type="submit" className="w-full" size="lg" disabled={isSubmitting}>
//               {isSubmitting ? "Processing..." : "Continue to Payment"}
//             </Button>
//           </CardFooter>
//         </form>
//       </Form>
//     </Card>
//   )
// }
