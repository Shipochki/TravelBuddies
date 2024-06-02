import { Link } from "react-router-dom";
import "./Menu.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faBars,
  faCarSide,
  faIdCard,
  faParagraph,
  faStar,
} from "@fortawesome/free-solid-svg-icons";
import StarIcon from "@mui/icons-material/Star";
import BadgeIcon from "@mui/icons-material/Badge";
import DirectionsCarIcon from '@mui/icons-material/DirectionsCar';
import FeedIcon from '@mui/icons-material/Feed';
import { useState } from "react";
import { Button, Tooltip } from "@mui/material";

export const Menu = () => {
  const [driverVisable, setDriverVisable] = useState(true);
  const [reviewVisable, setReviewVisable] = useState(true);

  const OnSetDriverVisable = () => {
    setDriverVisable(!driverVisable);
  };

  const OnSetReviewVisable = () => {
    setReviewVisable(!reviewVisable);
  };

  return (
    <div id="left-menu-main" className="left-menu-main">
      <div className="review-button">
        <Link
          className="btn-link"
          to={`/reviews?reciverId=${localStorage.userId}&page=1&pageCount=10`}
        >
          <StarIcon />
          <p>Review</p>
        </Link>
      </div>
      {localStorage.role == "client" ? (
        <div className="becomedriver-btn-link">
          <Link className="btn-link" to={`/becomeDriver`}>
            <BadgeIcon />
            <p>Become Driver</p>
          </Link>
        </div>
      ) : (
        <>
        <div className="vehicle-btn-link">
          <Link className="btn-link" to={`/vehicleMenu`}>
            <DirectionsCarIcon />
            <p>Vehicle</p>
          </Link>
        </div>
        <div className="post-btn-link">
          <Link className="btn-link" to={`/postMenu`}>
            <FeedIcon />
            <p>Post</p>
          </Link>
        </div>
        </>
      )}
    </div>
  );
};
