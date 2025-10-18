import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
} from "react-router-dom";
import AdminLayout from "../pages/Dashboard/AdminLayout";
import AdminDashboard from "../pages/Dashboard/AdminDashboard";
import AdminRoomsPage from "../pages/Rooms/AdminRoomsPage";

export const router = createBrowserRouter(
  createRoutesFromElements(
    <>
      <Route path="/" element={<AdminLayout />}>
        <Route index element={<AdminDashboard />} />
        <Route path="Reservations" element={""} />
        <Route path="Rooms" element={<AdminRoomsPage />} />
        <Route path="Guests" element={"Guests"} />
        <Route path="Payments" element={"Payments"} />
        <Route path="Employees" element={"Employees"} />
      </Route>
    </>
  )
);
