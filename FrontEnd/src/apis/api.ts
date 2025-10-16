import type { AxiosRequestConfig } from "axios";
import axiosInstance from "../config/config";
import { useQuery, type UseQueryOptions } from "@tanstack/react-query";

interface IQuery<T> {
  queryKey: string[];
  url: string;
  config?: AxiosRequestConfig;
  options?: UseQueryOptions<T>; // optional extra options
}
const useAuthQuery = <T>({ queryKey, url, config, options }: IQuery<T>) => {
  return useQuery<T>({
    queryKey,
    queryFn: async () => {
      const { data } = await axiosInstance.get<T>(url, config);
      return data;
    },
    ...options,
  });
};
export default useAuthQuery;
