import { Navigate, Outlet } from "react-router-dom";
import { isAuthenticated } from "../lib/jwt";

export default function ProtectedRoute({ redirectTo = "/login" }) {
  return isAuthenticated() ? <Outlet /> : <Navigate to={redirectTo} replace />;
}
