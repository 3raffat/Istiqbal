import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
} from "react-router-dom";
import AdminLayout from "../Pages/Dashboard/AdminLayout";
import AdminRoomsPage from "../Pages/Rooms/AdminRoomsPage";
import AdminGuestsPage from "../Pages/Guests/AdminGuestsPage";
import RoomTypesPage from "../Pages/Rooms/RoomTypesPage";
import AdminReservationsPage from "../Pages/Reservation/AdminReservationsPage";
import AdminLogin from "../Pages/Login/AdminLogin";
import ProtectedRoute from "../components/ProtectedRoute";
import AuthRoute from "../components/AuthRoute";
import EmployeesPage from "../Pages/Employee/EmployeesPage";

export const router = createBrowserRouter(
  createRoutesFromElements(
    <>
      <Route element={<AuthRoute />}>
        <Route path="login" element={<AdminLogin />} />
      </Route>
      <Route path="/" element={<ProtectedRoute />}>
        <Route path="/admin" element={<AdminLayout />}>
          <Route index element={<AdminReservationsPage />} />
          <Route path="room-types" element={<RoomTypesPage />} />
          <Route path="rooms" element={<AdminRoomsPage />} />
          <Route path="guests" element={<AdminGuestsPage />} />
          <Route path="employees" element={<EmployeesPage />} />
        </Route>
      </Route>
    </>
  )
);
