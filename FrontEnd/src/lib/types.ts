export interface CreateRoomTypeRequest {
  name: string;
  description: string;
  pricePerNight: number;
  maxOccupancy: number;
  amenitieIds: string[];
}

export interface Guest {
  fullName: string;
  email: string;
}

export interface CreateReservationRequest {
  guestId: string;
  roomId: string;
  checkInDate: string;
  checkOutDate: string;
}

export interface AmenityResponse {
  data: {
    id: string;
    name: string;
  }[];
  status: number;
  message: string;
}

export interface RoomsResponse {
  data: {
    id: string;
    number: number;
    roomTypeName: string;
    floor: number;
    status: "Available" | "Booked" | "Maintenance" | string;
    amountPerNight: number;
  }[];
  status: number;
  message: string;
}

export interface RoomType {
  id: string;
  name: string;
  description: string;
  pricePerNight: number;
  maxOccupancy: number;
  amenities: {
    id: string;
    name: string;
  }[];
}
export interface RoomTypeResponse {
  data: {
    id: string;
    name: string;
    description: string;
    pricePerNight: number;
    maxOccupancy: number;
    amenities: {
      id: string;
      name: string;
    }[];
  }[];
  status: number;
  message: string;
}

export type status = "Available" | "Occupied" | "UnderMaintenance" | "Cleaning";
export const StatusOptions = [
  "Available",
  "Occupied",
  "UnderMaintenance",
  "Cleaning",
];
export interface GuestReservationsResponse {
  data: {
    id: string;
    fullName: string;
    email: string;
    phone: string;
    reservations: {
      reservationId: string;
      guestFullName: string;
      roomtype: string;
      roomNumber: number;
      amount: number;
      checkInDate: string;
      checkOutDate: string;
      status: string;
    }[];
  }[];
  status: number;
  message: string;
}

export interface ReservationResponse {
  reservationId: string;
  guestFullName: string;
  roomtype: string;
  roomNumber: number;
  checkInDate: string;
  checkOutDate: string;
  amount: number;
  status: string;
}
export type ReservationFormData = {
  guestId: string;
  roomId: string;
  checkInDate: string;
  checkOutDate: string;
  status?: string;
};
export interface ReservationsResponse {
  data: {
    reservationId: string;
    guestFullName: string;
    roomtype: string;
    roomNumber: number;
    amount: number;
    checkInDate: string;
    checkOutDate: string;
    status: "Pending" | "Confirmed" | "Cancelled" | string;
  }[];
  status: number;
  message: string;
}

export interface Guest {
  id: string;
  fullName: string;
  email: string;
  phone: string;
}
export interface Room {
  id: string;
  number: number;
  roomTypeName: string;
  floor: number;
  status: string;
  amountPerNight: number;
}
