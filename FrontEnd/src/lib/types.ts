export interface RoomType {
  id: string;
  name: string;
  description: string;
  basePrice: number;
  capacity: number;
  amenities: string[];
}

export interface Room {
  id: string;
  roomNumber: string;
  roomTypeId: string;
  floor: number;
  status: "available" | "occupied" | "maintenance" | "reserved";
}

export interface Amenity {
  id: string;
  name: string;
  icon: string;
  category: "room" | "hotel";
}

export interface Guest {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  nationality: string;
  idNumber: string;
}

export interface Reservation {
  id: string;
  guestId: string;
  roomId: string;
  checkIn: string;
  checkOut: string;
  status: "pending" | "confirmed" | "checked-in" | "checked-out" | "cancelled";
  totalPrice: number;
  specialRequests?: string;
  createdAt: string;
}

export interface Payment {
  id: string;
  reservationId: string;
  amount: number;
  method: "cash" | "card" | "transfer";
  status: "pending" | "completed" | "refunded";
  transactionDate: string;
}

export interface Feedback {
  id: string;
  reservationId: string;
  guestId: string;
  rating: number;
  comment: string;
  category: "cleanliness" | "service" | "location" | "value" | "overall";
  createdAt: string;
}
export interface RoomsResponse {
  data: {
    id: string;
    number: number;
    roomTypeId: string;
    floor: number;
    status: string;
    amountPerNight: number;
    amenities: {
      id: string;
      name: string;
    }[];
  }[];
  success: boolean;
  message: string;
  timestamp: string;
}
export interface AmenityResponse {
  data: {
    id: string;
    name: string;
  }[];
  success: boolean;
  message: string;
  timestamp: string;
}
export interface RoomTypeResponse {
  data: {
    id: string;
    name: string;
    description: string;
    pricePerNight: number;
    maxOccupancy: number;
  }[];
  success: boolean;
  message: string;
  timestamp: string; // or Date if you convert it
}
