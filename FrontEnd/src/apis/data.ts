import type {
  AmenityResponse,
  GuestResponse,
  RoomsResponse,
  RoomTypeResponse,
} from "../lib/types";
import useAuthQuery from "./api";

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
  useAuthQuery<GuestResponse>({
    queryKey: ["Guest"],
    url: "/guests",
  });
