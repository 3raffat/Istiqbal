import { Home, Hotel, Menu } from "lucide-react";
import { Link } from "react-router-dom";
import { Button } from "../components/ui/button";
import { Badge } from "../components/ui/badge";

interface IProps {}

const Header = ({}: IProps) => {
  return (
    <>
      <header className="border-b  sticky top-0 z-50 backdrop-blur-sm bg-background/95">
        <div className="container mx-auto px-4">
          <div className="flex items-center justify-between h-16">
            <Link
              to={"/"}
              className="flex items-center gap-2 font-bold text-xl text-foreground hover:text-primary transition-colors"
            >
              <Hotel className="h-6 w-6 text-primary" />
              <span>Istiqbal</span>
            </Link>

            <nav className="hidden md:flex items-center gap-6">
              <Link
                to="/"
                className="flex items-center gap-2 text-sm font-medium text-muted-foreground hover:text-primary transition-colors"
              >
                <Home className="h-4 w-4" />
                <Badge
                  variant="secondary"
                  className="bg-primary/10 text-primary hover:bg-primary/20"
                >
                  Home
                </Badge>
              </Link>
              <Link
                to="/rooms"
                className="text-sm font-medium text-muted-foreground hover:text-primary transition-colors"
              >
                Rooms
              </Link>
              <Link
                to="/admin"
                className="text-sm font-medium text-muted-foreground hover:text-primary transition-colors"
              >
                Admin
              </Link>
            </nav>

            <div className="flex items-center gap-2">
              <Button asChild variant="ghost" className="hidden md:inline-flex">
                <Link to="/admin">Admin Portal</Link>
              </Button>
              <Button variant="ghost" size="icon" className="md:hidden">
                <Menu className="h-5 w-5" />
              </Button>
            </div>
          </div>
        </div>
      </header>
    </>
  );
};

export default Header;
