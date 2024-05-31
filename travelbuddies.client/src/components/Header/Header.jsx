import { Link } from "react-router-dom";
import { UserMenu } from "../UserMenu/UserMenu";
import "./Header.css";
import imgLogo from "../../utils/images/logo-no-background.png";
import carLogo from "../../utils/images/auto-car-logo-design-icon-vector-illustration-auto-car-logo-design-icon-vector-illustration-symbol-service-automobile-silhouette-157364282.jpg";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faBars } from "@fortawesome/free-solid-svg-icons";
import { Button } from "@mui/material";

export const Header = () => {
  return (
    <div className="header">
      <img className="header-img" src={carLogo} />
      <div className="header-left-side">
        {localStorage.accessToken && (
            <Button sx={{
                borderRadius: "100px",
                width: "50px",
                height: "50px",
                padding: "0",
            }}
            id="header-bars-btn"
            >
                <FontAwesomeIcon className="header-bars" icon={faBars} />
            </Button>
        )}
        <div className="logo-content">
          <Link to={"/"} className="logo">
            <img src={imgLogo} alt="logo" />
          </Link>
        </div>
      </div>
      <div className="navigation">
        <ul>
          <li className="navigation-home">
            <Link to={"/"}>Home</Link>
          </li>
          {!localStorage.accessToken && (
            <li>
              <Link to={"/about"}>About</Link>
            </li>
          )}
          <li>
            {localStorage.accessToken ? (
              <UserMenu />
            ) : (
              <Link to={"/login"}>👤 Log In</Link>
            )}
          </li>
        </ul>
      </div>
    </div>
  );
};
