import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
} from "react-router-dom";
import Layout from "../Pages/Layout";
import Home from "../Pages/Home";
import RoomsPage from "../Pages/Rooms/RoomsPage";

export const router = createBrowserRouter(
  createRoutesFromElements(
    <>
      <Route path="/" element={<Layout />}>
        <Route index element={<Home />} />
        <Route path="/rooms" element={<RoomsPage />} />
      </Route>
    </>
  )
);
