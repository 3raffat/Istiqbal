import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
} from "react-router-dom";
import AdminLayout from "../Pages/Dashboard/AdminLayout";
import AdminDashboard from "../Pages/Dashboard/AdminDashboard";
import AdminRoomsPage from "../Pages/Rooms/AdminRoomsPage";
import AdminGuestsPage from "../Pages/Guests/AdminGuestsPage";
import RoomTypesPage from "../Pages/Rooms/RoomTypesPage";
import AdminReservationsPage from "../Pages/Reservation/AdminReservationsPage";
import AdminLogin from "../Pages/Login/AdminLogin";
import ProtectedRoute from "../components/ProtectedRoute";
import AuthRoute from "../components/AuthRoute";

export const router = createBrowserRouter(
  createRoutesFromElements(
    <>
      <Route element={<AuthRoute />}>
        <Route path="login" element={<AdminLogin />} />
      </Route>
      <Route element={<ProtectedRoute />}>
        <Route path="/admin" element={<AdminLayout />}>
          <Route index element={<AdminDashboard />} />
          <Route path="reservations" element={<AdminReservationsPage />} />
          <Route path="room-types" element={<RoomTypesPage />} />
          <Route path="rooms" element={<AdminRoomsPage />} />
          <Route path="guests" element={<AdminGuestsPage />} />
          <Route path="payments" element={"Payments"} />
          <Route path="employees" element={"Employees"} />
        </Route>
      </Route>
    </>
  )
);
