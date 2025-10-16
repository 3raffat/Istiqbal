import axios from "axios";

const axiosInstance = axios.create({
  baseURL: "https://localhost:7000/api/v1",

  timeout: 3000,
});
export default axiosInstance;
