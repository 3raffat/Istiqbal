import { Hotel, Mail, Phone, MapPin } from "lucide-react";
import { Link } from "react-router-dom";
interface IProps {}

const Footer = ({}: IProps) => {
  return (
    <>
      <footer className="bg-background border-t border-border mt-auto">
        <div className="container mx-auto px-4 py-12">
          <div className="grid md:grid-cols-4 gap-8">
            <div>
              <div className="flex items-center gap-2 mb-4">
                <Hotel className="h-6 w-6 text-primary" />
                <span className="font-bold text-foreground text-lg">
                  Istiqbal
                </span>
              </div>
              <p className="text-sm text-muted-foreground">
                Experience luxury and comfort in the heart of the city.
              </p>
            </div>

            <div>
              <h3 className="font-semibold text-foreground mb-4">
                Quick Links
              </h3>
              <ul className="space-y-2 text-sm">
                <li>
                  <Link
                    to="/rooms"
                    className="text-muted-foreground hover:text-primary transition-colors"
                  >
                    Browse Rooms
                  </Link>
                </li>
                <li>
                  <Link
                    to="/admin"
                    className="text-muted-foreground hover:text-primary transition-colors"
                  >
                    Admin Portal
                  </Link>
                </li>
              </ul>
            </div>

            <div>
              <h3 className="font-semibold text-foreground mb-4">Contact</h3>
              <ul className="space-y-2 text-sm text-muted-foreground">
                <li className="flex items-center gap-2">
                  <Phone className="h-4 w-4" />
                  <span>+1 (555) 123-4567</span>
                </li>
                <li className="flex items-center gap-2">
                  <Mail className="h-4 w-4" />
                  <span>info@istiqbal.com</span>
                </li>
                <li className="flex items-center gap-2">
                  <MapPin className="h-4 w-4" />
                  <span>123 Hotel Street, City</span>
                </li>
              </ul>
            </div>

            <div>
              <h3 className="font-semibold text-foreground mb-4">Hours</h3>
              <p className="text-sm text-muted-foreground">
                Check-in: 3:00 PM
                <br />
                Check-out: 11:00 AM
                <br />
                <span className="text-primary font-medium">24/7 Reception</span>
              </p>
            </div>
          </div>

          <div className="border-t border-border mt-8 pt-8 text-center text-sm text-muted-foreground">
            <p>&copy; 2025 Istiqbal Hotel. All rights reserved.</p>
          </div>
        </div>
      </footer>
    </>
  );
};

export default Footer;
