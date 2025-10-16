// "use client"

// import { Bell, Search, User, Menu, LogOut } from "lucide-react"
// import { useRouter } from "next/navigation"
// import { Button } from "@/components/ui/button"
// import { Input } from "@/components/ui/input"
// import {
//   DropdownMenu,
//   DropdownMenuContent,
//   DropdownMenuItem,
//   DropdownMenuLabel,
//   DropdownMenuSeparator,
//   DropdownMenuTrigger,
// } from "@/components/ui/dropdown-menu"
// import { Badge } from "@/components/ui/badge"
// import { useAuth } from "@/lib/auth-context"

// export function AdminHeader() {
//   const { user, logout } = useAuth()
//   const router = useRouter()

//   const handleLogout = () => {
//     logout()
//     router.push("/admin/login")
//   }

//   return (
//     <header className="sticky top-0 z-10 border-b border-border bg-card/95 backdrop-blur supports-[backdrop-filter]:bg-card/60">
//       <div className="flex h-16 items-center gap-4 px-6">
//         {/* Mobile menu button */}
//         <Button variant="ghost" size="icon" className="lg:hidden">
//           <Menu className="h-5 w-5" />
//         </Button>

//         {/* Search bar */}
//         <div className="flex-1 max-w-md">
//           <div className="relative">
//             <Search className="absolute left-3 top-1/2 -translate-y-1/2 h-4 w-4 text-muted-foreground" />
//             <Input type="search" placeholder="Search reservations, guests, rooms..." className="pl-9 bg-background" />
//           </div>
//         </div>

//         {/* Right side actions */}
//         <div className="flex items-center gap-2">
//           {/* Notifications */}
//           <DropdownMenu>
//             <DropdownMenuTrigger asChild>
//               <Button variant="ghost" size="icon" className="relative">
//                 <Bell className="h-5 w-5" />
//                 <Badge className="absolute -top-1 -right-1 h-5 w-5 flex items-center justify-center p-0 text-xs">
//                   3
//                 </Badge>
//               </Button>
//             </DropdownMenuTrigger>
//             <DropdownMenuContent align="end" className="w-80">
//               <DropdownMenuLabel>Notifications</DropdownMenuLabel>
//               <DropdownMenuSeparator />
//               <DropdownMenuItem className="flex flex-col items-start gap-1 p-3">
//                 <p className="font-medium">New reservation received</p>
//                 <p className="text-sm text-muted-foreground">John Smith booked Room 301 for 4 nights</p>
//                 <p className="text-xs text-muted-foreground">5 minutes ago</p>
//               </DropdownMenuItem>
//               <DropdownMenuItem className="flex flex-col items-start gap-1 p-3">
//                 <p className="font-medium">Payment confirmed</p>
//                 <p className="text-sm text-muted-foreground">$450 received from Sarah Johnson</p>
//                 <p className="text-xs text-muted-foreground">1 hour ago</p>
//               </DropdownMenuItem>
//               <DropdownMenuItem className="flex flex-col items-start gap-1 p-3">
//                 <p className="font-medium">Check-in reminder</p>
//                 <p className="text-sm text-muted-foreground">3 guests checking in today</p>
//                 <p className="text-xs text-muted-foreground">2 hours ago</p>
//               </DropdownMenuItem>
//             </DropdownMenuContent>
//           </DropdownMenu>

//           {/* User menu */}
//           <DropdownMenu>
//             <DropdownMenuTrigger asChild>
//               <Button variant="ghost" size="icon" className="rounded-full">
//                 <div className="h-8 w-8 rounded-full bg-primary flex items-center justify-center">
//                   <User className="h-4 w-4 text-primary-foreground" />
//                 </div>
//               </Button>
//             </DropdownMenuTrigger>
//             <DropdownMenuContent align="end">
//               <DropdownMenuLabel>
//                 <div>
//                   <p className="font-medium">{user?.name || "Admin User"}</p>
//                   <p className="text-xs text-muted-foreground">{user?.email || "admin@luxstay.com"}</p>
//                 </div>
//               </DropdownMenuLabel>
//               <DropdownMenuSeparator />
//               <DropdownMenuItem>Profile Settings</DropdownMenuItem>
//               <DropdownMenuItem>Preferences</DropdownMenuItem>
//               <DropdownMenuSeparator />
//               <DropdownMenuItem className="text-destructive" onClick={handleLogout}>
//                 <LogOut className="h-4 w-4 mr-2" />
//                 Sign Out
//               </DropdownMenuItem>
//             </DropdownMenuContent>
//           </DropdownMenu>
//         </div>
//       </div>
//     </header>
//   )
// }
