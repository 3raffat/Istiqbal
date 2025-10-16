import type { Room, Guest, Reservation, Payment, Feedback, RoomType } from "./types"
import {
  rooms as initialRooms,
  guests as initialGuests,
  reservations as initialReservations,
  payments as initialPayments,
  feedbacks as initialFeedbacks,
  roomTypes as initialRoomTypes,
} from "./mock-data"

class Store {
  private getFromStorage<T>(key: string, defaultValue: T): T {
    if (typeof window === "undefined") return defaultValue
    const stored = localStorage.getItem(key)
    return stored ? JSON.parse(stored) : defaultValue
  }

  private saveToStorage<T>(key: string, value: T): void {
    if (typeof window === "undefined") return
    localStorage.setItem(key, JSON.stringify(value))
  }

  // Rooms
  getRooms(): Room[] {
    return this.getFromStorage("rooms", initialRooms)
  }

  addRoom(room: Room): void {
    const rooms = this.getRooms()
    rooms.push(room)
    this.saveToStorage("rooms", rooms)
  }

  updateRoom(id: string, updates: Partial<Room>): void {
    const rooms = this.getRooms()
    const index = rooms.findIndex((r) => r.id === id)
    if (index !== -1) {
      rooms[index] = { ...rooms[index], ...updates }
      this.saveToStorage("rooms", rooms)
    }
  }

  deleteRoom(id: string): void {
    const rooms = this.getRooms().filter((r) => r.id !== id)
    this.saveToStorage("rooms", rooms)
  }

  // Guests
  getGuests(): Guest[] {
    return this.getFromStorage("guests", initialGuests)
  }

  addGuest(guest: Guest): void {
    const guests = this.getGuests()
    guests.push(guest)
    this.saveToStorage("guests", guests)
  }

  updateGuest(id: string, updates: Partial<Guest>): void {
    const guests = this.getGuests()
    const index = guests.findIndex((g) => g.id === id)
    if (index !== -1) {
      guests[index] = { ...guests[index], ...updates }
      this.saveToStorage("guests", guests)
    }
  }

  deleteGuest(id: string): void {
    const guests = this.getGuests().filter((g) => g.id !== id)
    this.saveToStorage("guests", guests)
  }

  // Reservations
  getReservations(): Reservation[] {
    return this.getFromStorage("reservations", initialReservations)
  }

  addReservation(reservation: Reservation): void {
    const reservations = this.getReservations()
    reservations.push(reservation)
    this.saveToStorage("reservations", reservations)
  }

  updateReservation(id: string, updates: Partial<Reservation>): void {
    const reservations = this.getReservations()
    const index = reservations.findIndex((r) => r.id === id)
    if (index !== -1) {
      reservations[index] = { ...reservations[index], ...updates }
      this.saveToStorage("reservations", reservations)
    }
  }

  deleteReservation(id: string): void {
    const reservations = this.getReservations().filter((r) => r.id !== id)
    this.saveToStorage("reservations", reservations)
  }

  // Payments
  getPayments(): Payment[] {
    return this.getFromStorage("payments", initialPayments)
  }

  addPayment(payment: Payment): void {
    const payments = this.getPayments()
    payments.push(payment)
    this.saveToStorage("payments", payments)
  }

  updatePayment(id: string, updates: Partial<Payment>): void {
    const payments = this.getPayments()
    const index = payments.findIndex((p) => p.id === id)
    if (index !== -1) {
      payments[index] = { ...payments[index], ...updates }
      this.saveToStorage("payments", payments)
    }
  }

  // Feedbacks
  getFeedbacks(): Feedback[] {
    return this.getFromStorage("feedbacks", initialFeedbacks)
  }

  // Room Types
  getRoomTypes(): RoomType[] {
    return this.getFromStorage("roomTypes", initialRoomTypes)
  }
}

export const store = new Store()
