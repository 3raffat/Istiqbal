import { Users, Bed, Wifi, Wind, Tv, DollarSign } from "lucide-react";
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
  CardTitle,
} from "../../components/ui/card";
import { Badge } from "../../components/ui/badge";
import { Link } from "react-router-dom";
import { Button } from "../../components/ui/button";

export default function RoomsPage() {
  return (
    <div className="min-h-screen bg-background">
      {/* Header */}
      <div className="bg-primary text-primary-foreground py-12">
        <div className="container mx-auto px-4">
          <h1 className="text-4xl font-bold mb-4">Available Rooms</h1>
          {/* {checkIn && checkOut && (
            <p className="opacity-90">
              {checkIn} to {checkOut} • {guests || "2"} guests
            </p>
          )} */}
        </div>
      </div>

      {/* Room Listings */}
      <div className="container mx-auto px-4 py-12">
        <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-6">
          {/* {roomTypes.map((roomType) => (
            <Card
              key={roomType.id}
              className="overflow-hidden hover:shadow-lg transition-shadow"
            >
              <div className="aspect-video bg-gradient-to-br from-primary/20 via-primary/10 to-primary/5 relative flex items-center justify-center">
                <div className="text-center">
                  <Bed className="h-16 w-16 text-primary mx-auto mb-2" />
                  <p className="text-sm font-medium text-muted-foreground">
                    {roomType.name}
                  </p>
                </div>
                <Badge className="absolute top-4 right-4 bg-white text-primary border-primary">
                  Available
                </Badge>
              </div>

              <CardHeader>
                <CardTitle className="text-xl">{roomType.name}</CardTitle>
                <p className="text-sm text-muted-foreground">
                  {roomType.description}
                </p>
              </CardHeader>

              <CardContent className="space-y-4">
                <div className="flex items-center gap-4 text-sm">
                  <div className="flex items-center gap-1">
                    <Users className="h-4 w-4 text-muted-foreground" />
                    <span>{roomType.capacity} guests</span>
                  </div>
                  <div className="flex items-center gap-1">
                    <Bed className="h-4 w-4 text-muted-foreground" />
                    <span>{roomType.bedType}</span>
                  </div>
                </div>

                <div className="flex flex-wrap gap-2">
                  <div className="flex items-center gap-1 text-xs text-muted-foreground">
                    <Wifi className="h-3 w-3" />
                    <span>WiFi</span>
                  </div>
                  <div className="flex items-center gap-1 text-xs text-muted-foreground">
                    <Wind className="h-3 w-3" />
                    <span>A/C</span>
                  </div>
                  <div className="flex items-center gap-1 text-xs text-muted-foreground">
                    <Tv className="h-3 w-3" />
                    <span>TV</span>
                  </div>
                </div>

                <div className="pt-4 border-t">
                  <div className="flex items-baseline gap-1">
                    <DollarSign className="h-5 w-5 text-primary" />
                    <span className="text-3xl font-bold text-primary">
                      {roomType.basePrice}
                    </span>
                    <span className="text-muted-foreground">/ night</span>
                  </div>
                </div>
              </CardContent>

              <CardFooter>
                <Button asChild className="w-full" size="lg">
                  <Link
                    to={`/rooms/${roomType.id}?checkIn=${
                      checkIn || ""
                    }&checkOut=${checkOut || ""}&guests=${guests || "2"}`}
                  >
                    View Details & Book
                  </Link>
                </Button>
              </CardFooter>
            </Card>
          ))} */}
        </div>
      </div>
    </div>
  );
}
