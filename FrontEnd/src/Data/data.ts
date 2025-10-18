/*_____________________________________________________________________________________________*/

export const getStatusColor = (status: string) => {
  switch (status.trim()) {
    case "Available":
      return "bg-green-100 text-green-700";
    case "Occupied":
      return "bg-red-100 text-red-700";
    case "Cleaning":
      return "bg-yellow-100 text-yellow-700";
    case "UnderMaintenance":
      return "bg-slate-100 text-slate-700";
    default:
      return "bg-slate-100 text-slate-700";
  }
};
export const getStatusText = (status: string) => {
  switch (status.trim()) {
    case "Available":
      return "متاحة";
    case "Occupied":
      return "محجوزة";
    case "Cleaning":
      return "تحت التنظيف";
    case "UnderMaintenance":
      return "صيانة";
    default:
      return status;
  }
};
