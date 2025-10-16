import type { RoomType, Room, Amenity, Guest, Reservation, Payment, Feedback } from "./types"

export const amenities: Amenity[] = [
  { id: "1", name: "واي فاي مجاني", icon: "wifi", category: "room" },
  { id: "2", name: "تلفزيون", icon: "tv", category: "room" },
  { id: "3", name: "مكيف هواء", icon: "wind", category: "room" },
  { id: "4", name: "ميني بار", icon: "glass-water", category: "room" },
  { id: "5", name: "خدمة الغرف", icon: "concierge-bell", category: "hotel" },
  { id: "6", name: "مسبح", icon: "person-swimming", category: "hotel" },
  { id: "7", name: "صالة رياضية", icon: "dumbbell", category: "hotel" },
  { id: "8", name: "موقف سيارات", icon: "square-parking", category: "hotel" },
]

export const roomTypes: RoomType[] = [
  {
    id: "1",
    name: "غرفة عادية",
    description: "غرفة مريحة مع سرير مزدوج وجميع المرافق الأساسية",
    basePrice: 300,
    capacity: 2,
    amenities: ["1", "2", "3"],
  },
  {
    id: "2",
    name: "غرفة ديلوكس",
    description: "غرفة فاخرة مع إطلالة رائعة وخدمات إضافية",
    basePrice: 500,
    capacity: 2,
    amenities: ["1", "2", "3", "4"],
  },
  {
    id: "3",
    name: "جناح عائلي",
    description: "جناح واسع يتسع لعائلة كاملة مع غرفة معيشة منفصلة",
    basePrice: 800,
    capacity: 4,
    amenities: ["1", "2", "3", "4"],
  },
  {
    id: "4",
    name: "جناح ملكي",
    description: "جناح فاخر مع جميع المرافق الراقية وخدمة VIP",
    basePrice: 1500,
    capacity: 4,
    amenities: ["1", "2", "3", "4"],
  },
]

export const rooms: Room[] = [
  { id: "1", roomNumber: "101", roomTypeId: "1", floor: 1, status: "available" },
  { id: "2", roomNumber: "102", roomTypeId: "1", floor: 1, status: "occupied" },
  { id: "3", roomNumber: "103", roomTypeId: "1", floor: 1, status: "available" },
  { id: "4", roomNumber: "201", roomTypeId: "2", floor: 2, status: "available" },
  { id: "5", roomNumber: "202", roomTypeId: "2", floor: 2, status: "reserved" },
  { id: "6", roomNumber: "203", roomTypeId: "2", floor: 2, status: "available" },
  { id: "7", roomNumber: "301", roomTypeId: "3", floor: 3, status: "available" },
  { id: "8", roomNumber: "302", roomTypeId: "3", floor: 3, status: "maintenance" },
  { id: "9", roomNumber: "401", roomTypeId: "4", floor: 4, status: "available" },
  { id: "10", roomNumber: "402", roomTypeId: "4", floor: 4, status: "available" },
]

export const guests: Guest[] = [
  {
    id: "1",
    firstName: "أحمد",
    lastName: "محمد",
    email: "ahmed@example.com",
    phone: "+966501234567",
    nationality: "السعودية",
    idNumber: "1234567890",
  },
  {
    id: "2",
    firstName: "فاطمة",
    lastName: "علي",
    email: "fatima@example.com",
    phone: "+966509876543",
    nationality: "السعودية",
    idNumber: "0987654321",
  },
]

export const reservations: Reservation[] = [
  {
    id: "1",
    guestId: "1",
    roomId: "2",
    checkIn: "2025-01-15",
    checkOut: "2025-01-18",
    status: "checked-in",
    totalPrice: 900,
    specialRequests: "غرفة في طابق عالي",
    createdAt: "2025-01-10T10:00:00Z",
  },
  {
    id: "2",
    guestId: "2",
    roomId: "5",
    checkIn: "2025-01-20",
    checkOut: "2025-01-25",
    status: "confirmed",
    totalPrice: 2500,
    createdAt: "2025-01-12T14:30:00Z",
  },
]

export const payments: Payment[] = [
  {
    id: "1",
    reservationId: "1",
    amount: 900,
    method: "card",
    status: "completed",
    transactionDate: "2025-01-15T09:00:00Z",
  },
  {
    id: "2",
    reservationId: "2",
    amount: 2500,
    method: "transfer",
    status: "pending",
    transactionDate: "2025-01-12T14:35:00Z",
  },
]

export const feedbacks: Feedback[] = [
  {
    id: "1",
    reservationId: "1",
    guestId: "1",
    rating: 5,
    comment: "تجربة رائعة، خدمة ممتازة ونظافة عالية",
    category: "overall",
    createdAt: "2025-01-18T12:00:00Z",
  },
]
