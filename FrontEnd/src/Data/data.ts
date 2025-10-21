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

export const getStatusColorr = (status: string) => {
  if (!status) return "bg-slate-100 text-slate-700";
  const normalizedStatus = status.toLowerCase();
  switch (normalizedStatus) {
    case "confirmed":
      return "bg-green-100 text-green-700";
    case "checkedin":
    case "checked-in":
      return "bg-blue-100 text-blue-700";
    case "checkedout":
    case "checked-out":
      return "bg-slate-100 text-slate-700";
    case "pending":
      return "bg-yellow-100 text-yellow-700";
    case "cancelled":
      return "bg-red-100 text-red-700";
    default:
      return "bg-slate-100 text-slate-700";
  }
};

export const getStatusTextt = (status: string) => {
  if (!status) return "غير محدد";
  const normalizedStatus = status.toLowerCase();
  switch (normalizedStatus) {
    case "confirmed":
      return "مؤكد";
    case "checkedin":
    case "checked-in":
      return "تم تسجيل الدخول";
    case "checkedout":
    case "checked-out":
      return "تم تسجيل الخروج";
    case "pending":
      return "قيد الانتظار";
    case "cancelled":
      return "ملغي";
    default:
      return status;
  }
};
export const getInitials = (name: string) => {
  if (!name) return "?";
  const parts = name.split(" ");
  if (parts.length >= 2) {
    return parts[0].charAt(0) + parts[parts.length - 1].charAt(0);
  }
  return name.charAt(0);
};
