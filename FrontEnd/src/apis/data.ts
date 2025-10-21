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
    config: {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
  });

export const useAmenity = () =>
  useAuthQuery<AmenityResponse>({
    queryKey: ["Amenity"],
    url: "/amenitis",
    config: {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
  });
export const useRoomtype = () =>
  useAuthQuery<RoomTypeResponse>({
    queryKey: ["Roomtype"],
    url: "/room-types",
    config: {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
  });

export const useGuest = () =>
  useAuthQuery<GuestReservationsResponse>({
    queryKey: ["Guest"],
    url: "/guests",
    config: {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
  });
export const useReservation = () =>
  useAuthQuery<ReservationsResponse>({
    queryKey: ["Reservation"],
    url: "/reservation",
    config: {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
  });
