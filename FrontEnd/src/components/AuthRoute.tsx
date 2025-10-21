import { Navigate, Outlet } from "react-router-dom";
import { getToken } from "../lib/jwt";

export default function AuthRoute({ redirectTo = "/admin" }) {
  const token = getToken();

  return !token ? <Outlet /> : <Navigate to={redirectTo} replace />;
}
