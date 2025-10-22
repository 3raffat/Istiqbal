import { getToken } from "../lib/jwt";
import type {
  AmenityResponse,
  GuestReservationsResponse,
  ReservationsResponse,
  RoomsResponse,
  RoomTypeResponse,
} from "../lib/types";
import useAuthQuery from "./api";
const token = getToken();
export const useRoom = () =>
  useAuthQuery<RoomsResponse>({
    queryKey: ["room"],
    url: "/rooms",
  });

export const useAmenity = () =>
  useAuthQuery<AmenityResponse>({
    queryKey: ["Amenity"],
    url: "/amenitis",
  });
export const useRoomtype = () =>
  useAuthQuery<RoomTypeResponse>({
    queryKey: ["Roomtype"],
    url: "/room-types",
  });

export const useGuest = () =>
  useAuthQuery<GuestReservationsResponse>({
    queryKey: ["Guest"],
    url: "/guests",
  });
export const useReservation = () =>
  useAuthQuery<ReservationsResponse>({
    queryKey: ["Reservation"],
    url: "/reservation",
  });
