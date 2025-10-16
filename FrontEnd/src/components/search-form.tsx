// import { Calendar, Search, Users } from "lucide-react";
// import { Form, FormField, FormItem, FormLabel } from "./ui/form";
// import { Input } from "./ui/input";
// import { useNavigate } from "react-router-dom";
// import { useForm } from "react-hook-form";
// import { Card } from "./ui/card";
// import { Button } from "./ui/button";

// interface SearchFormData {
//   checkIn: string;
//   checkOut: string;
//   guests: string;
// }

// export function SearchForm() {
//   const router = useNavigate();

//   const form = useForm<SearchFormData>({
//     defaultValues: {
//       checkIn: "",
//       checkOut: "",
//       guests: "2",
//     },
//   });

//   const onSubmit = (data: SearchFormData) => {
//     const params = new URLSearchParams({
//       checkIn: data.checkIn,
//       checkOut: data.checkOut,
//       guests: data.guests,
//     });
//     router(`/rooms?${params.toString()}`);
//   };

//   return (
//     <Card className="p-6 bg-card backdrop-blur">
//       <Form {...form}>
//         <form
//           onSubmit={form.handleSubmit(onSubmit)}
//           className="grid md:grid-cols-4 gap-4"
//         >
//           <FormField
//             control={form.control}
//             name="checkIn"
//             rules={{ required: true }}
//             render={({ field }) => (
//               <FormItem>
//                 <FormLabel className="flex items-center gap-2">
//                   <Calendar className="h-4 w-4" />
//                   Check-in
//                 </FormLabel>
//                 <Input
//                   type="date"
//                   min={new Date().toISOString().split("T")[0]}
//                   {...field}
//                 />
//               </FormItem>
//             )}
//           />

//           <FormField
//             control={form.control}
//             name="checkOut"
//             rules={{ required: true }}
//             render={({ field }) => (
//               <FormItem>
//                 <FormLabel className="flex items-center gap-2">
//                   <Calendar className="h-4 w-4" />
//                   Check-out
//                 </FormLabel>
//                 <Input
//                   type="date"
//                   min={
//                     form.watch("checkIn") ||
//                     new Date().toISOString().split("T")[0]
//                   }
//                   {...field}
//                 />
//               </FormItem>
//             )}
//           />

//           <FormField
//             control={form.control}
//             name="guests"
//             rules={{ required: true }}
//             render={({ field }) => (
//               <FormItem>
//                 <FormLabel className="flex items-center gap-2">
//                   <Users className="h-4 w-4" />
//                   Guests
//                 </FormLabel>
//                 <Input type="number" min="1" max="10" {...field} />
//               </FormItem>
//             )}
//           />

//           <div className="flex items-end">
//             <Button type="submit" className="w-full" size="lg">
//               <Search className="h-4 w-4 mr-2" />
//               Search Rooms
//             </Button>
//           </div>
//         </form>
//       </Form>
//     </Card>
//   );
// }
