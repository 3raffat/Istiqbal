import { Link } from "react-router-dom";
import { Hotel, Star, MapPin, Shield, Clock } from "lucide-react";
import { Button } from "../components/ui/button";
import { Card } from "../components/ui/card";

interface IProps {}

const Home = ({}: IProps) => {
  return (
    <>
      {" "}
      <div className="min-h-screen">
        {/* Hero Section */}
        <section className="relative h-[600px] flex items-center justify-center">
          <div className="absolute inset-0 bg-gradient-to-br from-primary via-teal-700 to-teal-950" />

          <div className="relative z-10 container mx-auto px-4 text-center text-white">
            <div className="flex items-center justify-center gap-2 mb-4">
              <Hotel className="h-12 w-12" />
              <h1 className="text-5xl font-bold">Istiqbal Hotel</h1>
            </div>
            <p className="text-xl mb-8 text-teal-100 max-w-2xl mx-auto text-balance">
              Experience luxury and comfort in the heart of the city. Book your
              perfect stay today.
            </p>

            {/* Search Form */}
            <div className="max-w-4xl mx-auto">{/* <SearchForm /> */}</div>
          </div>
        </section>

        {/* Features Section */}
        <section className="py-16 bg-background">
          <div className="container mx-auto px-4">
            <h2 className="text-3xl font-bold text-center mb-12">
              Why Choose Istiqbal
            </h2>
            <div className="grid md:grid-cols-4 gap-8">
              <Card className="p-6 text-center">
                <div className="flex justify-center mb-4">
                  <div className="h-12 w-12 rounded-full bg-primary/10 flex items-center justify-center">
                    <Star className="h-6 w-6 text-primary" />
                  </div>
                </div>
                <h3 className="font-semibold mb-2">Premium Quality</h3>
                <p className="text-sm text-muted-foreground">
                  5-star rated rooms with luxury amenities
                </p>
              </Card>
              <Card className="p-6 text-center">
                <div className="flex justify-center mb-4">
                  <div className="h-12 w-12 rounded-full bg-primary/10 flex items-center justify-center">
                    <MapPin className="h-6 w-6 text-primary" />
                  </div>
                </div>
                <h3 className="font-semibold mb-2">Prime Location</h3>
                <p className="text-sm text-muted-foreground">
                  Located in the city center near attractions
                </p>
              </Card>

              <Card className="p-6 text-center">
                <div className="flex justify-center mb-4">
                  <div className="h-12 w-12 rounded-full bg-primary/10 flex items-center justify-center">
                    <Shield className="h-6 w-6 text-primary" />
                  </div>
                </div>
                <h3 className="font-semibold mb-2">Secure Booking</h3>
                <p className="text-sm text-muted-foreground">
                  Safe and encrypted payment processing
                </p>
              </Card>

              <Card className="p-6 text-center">
                <div className="flex justify-center mb-4">
                  <div className="h-12 w-12 rounded-full bg-primary/10 flex items-center justify-center">
                    <Clock className="h-6 w-6 text-primary" />
                  </div>
                </div>
                <h3 className="font-semibold mb-2">24/7 Support</h3>
                <p className="text-sm text-muted-foreground">
                  Round-the-clock customer service
                </p>
              </Card>
            </div>
          </div>
        </section>

        {/* CTA Section */}
        <section className="py-16 bg-primary/5">
          <div className="container mx-auto px-4 text-center">
            <h2 className="text-3xl font-bold mb-4">
              Ready to Book Your Stay?
            </h2>
            <p className="text-muted-foreground mb-8 max-w-2xl mx-auto">
              Browse our available rooms and find the perfect accommodation for
              your needs
            </p>
            <Button asChild size="lg">
              <Link to="/rooms">View All Rooms</Link>
            </Button>
          </div>
        </section>
      </div>
    </>
  );
};

export default Home;
