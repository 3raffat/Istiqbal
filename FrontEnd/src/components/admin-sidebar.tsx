// "use client"

// import Link from "next/link"
// import { usePathname } from "next/navigation"
// import { cn } from "@/lib/utils"
// import {
//   Hotel,
//   LayoutDashboard,
//   DoorOpen,
//   Calendar,
//   Users,
//   DollarSign,
//   Settings,
//   ChevronLeft,
//   UserCog,
// } from "lucide-react"
// import { Button } from "@/components/ui/button"

// const navigation = [
//   { name: "Dashboard", href: "/admin", icon: LayoutDashboard },
//   { name: "Reservations", href: "/admin/reservations", icon: Calendar },
//   { name: "Rooms", href: "/admin/rooms", icon: DoorOpen },
//   { name: "Guests", href: "/admin/guests", icon: Users },
//   { name: "Payments", href: "/admin/payments", icon: DollarSign },
//   { name: "Employees", href: "/admin/employees", icon: UserCog },
//   { name: "Settings", href: "/admin/settings", icon: Settings },
// ]

// export function AdminSidebar() {
//   const pathname = usePathname()

//   return (
//     <aside className="hidden lg:flex lg:flex-col w-64 bg-card border-r border-border">
//       <div className="h-16 flex items-center px-6 border-b border-border">
//         <Link href="/admin" className="flex items-center gap-2 font-bold text-lg text-foreground">
//           <div className="h-8 w-8 rounded-lg bg-primary flex items-center justify-center">
//             <Hotel className="h-5 w-5 text-primary-foreground" />
//           </div>
//           <span>Istiqbal Admin</span>
//         </Link>
//       </div>

//       <nav className="flex-1 px-3 py-6 space-y-1 overflow-y-auto">
//         {navigation.map((item) => {
//           const isActive = pathname === item.href || (item.href !== "/admin" && pathname?.startsWith(item.href))
//           return (
//             <Link
//               key={item.name}
//               href={item.href}
//               className={cn(
//                 "flex items-center gap-3 px-4 py-3 rounded-lg text-sm font-medium transition-all",
//                 isActive
//                   ? "bg-primary text-primary-foreground shadow-sm"
//                   : "text-muted-foreground hover:bg-accent hover:text-accent-foreground",
//               )}
//             >
//               <item.icon className="h-5 w-5 flex-shrink-0" />
//               <span>{item.name}</span>
//             </Link>
//           )
//         })}
//       </nav>

//       <div className="p-4 border-t border-border">
//         <Button variant="ghost" className="w-full justify-start gap-2" asChild>
//           <Link href="/">
//             <ChevronLeft className="h-4 w-4" />
//             <span>Back to Website</span>
//           </Link>
//         </Button>
//       </div>
//     </aside>
//   )
// }
