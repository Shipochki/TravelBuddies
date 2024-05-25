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
import { useState } from "react";
import { Tooltip } from "@mui/material";

export const Menu = () => {
  const [driverVisable, setDriverVisable] = useState(false);
  const [reviewVisable, setReviewVisable] = useState(false);

  const OnSetDriverVisable = () => {
    setDriverVisable(!driverVisable);
  };

  const OnSetReviewVisable = () => {
    setReviewVisable(!reviewVisable);
  }

  return (
    <div className="left-menu-main">
      <div className="reviews-link">
        <div className="reviews-link-menu">
          <h4>{<FontAwesomeIcon icon={faStar} />} Review</h4>
          <Tooltip title="Review links">
            <FontAwesomeIcon
              className="menu-driver-bars"
              onClick={OnSetReviewVisable}
              icon={faBars}
            />
          </Tooltip>
        </div>
        {reviewVisable && (
          <Link to={`/reviews/${localStorage.userId}`}>My Reviews</Link>
        )}
      </div>
      <div className="driver-links">
        <div className="driver-links-menu">
          <h4>{<FontAwesomeIcon icon={faIdCard} />} Driver</h4>
          <Tooltip title="Driver links">
            <FontAwesomeIcon
              className="menu-driver-bars"
              onClick={OnSetDriverVisable}
              icon={faBars}
            />
          </Tooltip>
        </div>
        {driverVisable && (
          <>
            <Link to={"/becomeDriver"}>Become Driver</Link>
            <div className="vehicle-links">
              <h5>{<FontAwesomeIcon icon={faCarSide} />} Vehicle</h5>
              <Link to={"/myVehicle"}>My Vehicle</Link>
              <Link to={"/createVehicle"}>Add Vehicle</Link>
              <Link to={"/editVehicle"}>Edit Vehicle</Link>
            </div>
            <div className="post-links">
              <h5>{<FontAwesomeIcon icon={faParagraph} />} Post</h5>
              <Link to={"/myPosts"}>My Posts</Link>
              <Link to={"/createPost"}>Add Post</Link>
            </div>
          </>
        )}
      </div>
    </div>
  );
};
