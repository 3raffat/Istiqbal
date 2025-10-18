import { Badge } from "./ui/badge";

interface StatusBadgeProps {
  status: string;
  type?: "room" | "payment" | "reservation";
}

export function StatusBadge({ status, type = "room" }: StatusBadgeProps) {
  const getStatusColor = () => {
    if (type === "room") {
      switch (status) {
        case "Available":
          return "bg-primary/20 text-primary border-primary/30";
        case "Occupied":
          return "bg-destructive/20 text-destructive border-destructive/30";
        case "Maintenance":
          return "bg-chart-4/20 text-chart-4 border-chart-4/30";
        case "Cleaning":
          return "bg-chart-2/20 text-chart-2 border-chart-2/30";
        default:
          return "bg-muted text-muted-foreground border-border";
      }
    } else if (type === "payment") {
      switch (status) {
        case "Paid":
          return "bg-primary/20 text-primary border-primary/30";
        case "Pending":
          return "bg-chart-4/20 text-chart-4 border-chart-4/30";
        case "Failed":
          return "bg-destructive/20 text-destructive border-destructive/30";
        case "Unpaid":
          return "bg-muted text-muted-foreground border-border";
        case "Refunded":
          return "bg-muted text-muted-foreground border-border";
        default:
          return "bg-muted text-muted-foreground border-border";
      }
    } else {
      // reservation status
      const statusLower = status.toLowerCase();
      if (statusLower === "confirmed")
        return "bg-primary/20 text-primary border-primary/30";
      if (statusLower === "pending")
        return "bg-chart-4/20 text-chart-4 border-chart-4/30";
      if (statusLower === "checked-in")
        return "bg-chart-2/20 text-chart-2 border-chart-2/30";
      if (statusLower === "cancelled")
        return "bg-destructive/20 text-destructive border-destructive/30";
      return "bg-muted text-muted-foreground border-border";
    }
  };

  return <Badge className={getStatusColor()}>{status}</Badge>;
}
