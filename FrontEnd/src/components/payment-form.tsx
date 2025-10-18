// "use client";

// import { useForm } from "react-hook-form";
// import { Button } from "./ui/button";
// import { Input } from "./ui/input";
// import { Label } from "./ui/label";
// import {
//   Select,
//   SelectContent,
//   SelectItem,
//   SelectTrigger,
//   SelectValue,
// } from "./ui/select";
// import { useState } from "react";

// // interface PaymentFormData {
// //   reservationId: string;
// //   amount: number;
// //   paymentMethod: PaymentMethod;
// //   status: PaymentStatus;
// // }

// // interface PaymentFormProps {
// //   reservations: Reservation[];
// //   onSubmit: (data: PaymentFormData) => Promise<void>;
// //   defaultValues?: Partial<PaymentFormData>;
// //   submitLabel?: string;
// // }

// export function PaymentForm({
//   reservations,
//   onSubmit,
//   defaultValues,
//   submitLabel = "Create Payment",
// }: PaymentFormProps) {
//   const [isLoading, setIsLoading] = useState(false);
//   const {
//     register,
//     handleSubmit,
//     formState: { errors },
//     setValue,
//     watch,
//   } = useForm<PaymentFormData>({
//     resolver: zodResolver(paymentSchema),
//     defaultValues: defaultValues || {
//       paymentMethod: PaymentMethod.Cash,
//       status: PaymentStatus.Paid,
//     },
//   });

//   const selectedReservationId = watch("reservationId");
//   const selectedPaymentMethod = watch("paymentMethod");
//   const selectedStatus = watch("status");

//   const handleFormSubmit = async (data: PaymentFormData) => {
//     setIsLoading(true);
//     await onSubmit(data);
//     setIsLoading(false);
//   };

//   return (
//     <form onSubmit={handleSubmit(handleFormSubmit)} className="space-y-4">
//       <div className="space-y-2">
//         <Label htmlFor="reservationId">Reservation</Label>
//         <Select
//           value={selectedReservationId}
//           onValueChange={(value) => setValue("reservationId", value)}
//         >
//           <SelectTrigger className="bg-background border-border">
//             <SelectValue placeholder="Select reservation" />
//           </SelectTrigger>
//           <SelectContent className="bg-card border-border">
//             {reservations.map((reservation) => (
//               <SelectItem key={reservation.id} value={reservation.id}>
//                 {reservation.guest.fullName} - Room {reservation.room.number} ($
//                 {reservation.totalAmount})
//               </SelectItem>
//             ))}
//           </SelectContent>
//         </Select>
//         {errors.reservationId && (
//           <p className="text-sm text-destructive">
//             {errors.reservationId.message}
//           </p>
//         )}
//       </div>

//       <div className="space-y-2">
//         <Label htmlFor="amount">Amount ($)</Label>
//         <Input
//           id="amount"
//           type="number"
//           step="0.01"
//           placeholder="0.00"
//           {...register("amount", { valueAsNumber: true })}
//           className="bg-background border-border"
//         />
//         {errors.amount && (
//           <p className="text-sm text-destructive">{errors.amount.message}</p>
//         )}
//       </div>

//       <div className="space-y-2">
//         <Label htmlFor="paymentMethod">Payment Method</Label>
//         <Select
//           value={selectedPaymentMethod}
//           onValueChange={(value) =>
//             setValue("paymentMethod", value as PaymentMethod)
//           }
//         >
//           <SelectTrigger className="bg-background border-border">
//             <SelectValue placeholder="Select payment method" />
//           </SelectTrigger>
//           <SelectContent className="bg-card border-border">
//             <SelectItem value={PaymentMethod.Cash}>Cash</SelectItem>
//             <SelectItem value={PaymentMethod.CreditCard}>
//               Credit Card
//             </SelectItem>
//             <SelectItem value={PaymentMethod.DebitCard}>Debit Card</SelectItem>
//             <SelectItem value={PaymentMethod.BankTransfer}>
//               Bank Transfer
//             </SelectItem>
//           </SelectContent>
//         </Select>
//         {errors.paymentMethod && (
//           <p className="text-sm text-destructive">
//             {errors.paymentMethod.message}
//           </p>
//         )}
//       </div>

//       <div className="space-y-2">
//         <Label htmlFor="status">Payment Status</Label>
//         <Select
//           value={selectedStatus}
//           onValueChange={(value) => setValue("status", value as PaymentStatus)}
//         >
//           <SelectTrigger className="bg-background border-border">
//             <SelectValue placeholder="Select status" />
//           </SelectTrigger>
//           <SelectContent className="bg-card border-border">
//             <SelectItem value={PaymentStatus.Paid}>Paid</SelectItem>
//             <SelectItem value={PaymentStatus.Unpaid}>Unpaid</SelectItem>
//             <SelectItem value={PaymentStatus.Pending}>Pending</SelectItem>
//             <SelectItem value={PaymentStatus.Failed}>Failed</SelectItem>
//             <SelectItem value={PaymentStatus.Refunded}>Refunded</SelectItem>
//           </SelectContent>
//         </Select>
//         {errors.status && (
//           <p className="text-sm text-destructive">{errors.status.message}</p>
//         )}
//       </div>

//       <Button
//         type="submit"
//         className="w-full bg-primary text-primary-foreground hover:bg-primary/90"
//         disabled={isLoading}
//       >
//         {isLoading ? "Saving..." : submitLabel}
//       </Button>
//     </form>
//   );
// }
