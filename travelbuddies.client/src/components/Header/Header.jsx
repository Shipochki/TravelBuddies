import { Link, useNavigate } from "react-router-dom";
import { UserMenu } from "../UserMenu/UserMenu";
import "./Header.css";
import imgLogo from "../../utils/images/logo-no-background.png";
import carLogo from "../../utils/images/auto-car-logo-design-icon-vector-illustration-auto-car-logo-design-icon-vector-illustration-symbol-service-automobile-silhouette-157364282.jpg";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faBars } from "@fortawesome/free-solid-svg-icons";
import {
  Box,
  Button,
  Divider,
  Drawer,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
} from "@mui/material";
import HomeIcon from "@mui/icons-material/Home";
import StarIcon from "@mui/icons-material/Star";
import DirectionsCarIcon from "@mui/icons-material/DirectionsCar";
import AddCircleOutlineIcon from "@mui/icons-material/AddCircleOutline";
import PostAddIcon from "@mui/icons-material/PostAdd";
import EditIcon from "@mui/icons-material/Edit";
import DynamicFeedIcon from "@mui/icons-material/DynamicFeed";
import { useState } from "react";

export const Header = () => {
  const [open, setOpen] = useState(false);
  const navigate = useNavigate();

  const toggleDrawer = (newOpen) => () => {
    setOpen(newOpen);
  };

  const DrawerList = (
    <Box sx={{ width: 250 }} role="presentation" onClick={toggleDrawer(false)}>
      <List>
        <ListItem disablePadding>
          <ListItemButton
            onClick={() => {
              toggleDrawer(false);
              navigate("/");
            }}
          >
            <ListItemIcon>
              <HomeIcon />
            </ListItemIcon>
            <ListItemText primary={"Home"} />
          </ListItemButton>
        </ListItem>
        <ListItem disablePadding>
          <ListItemButton
            onClick={() => {
              toggleDrawer(false);
              navigate(
                `/reviews?reciverId=${localStorage.userId}&page=1&pageCount=10`
              );
            }}
          >
            <ListItemIcon>
              <StarIcon />
            </ListItemIcon>
            <ListItemText primary={"My Reviews"} />
          </ListItemButton>
        </ListItem>
      </List>
      <Divider />
      <List></List>
      {localStorage.role == "client" ? (
        <ListItem disablePadding>
          <ListItemButton
            onClick={() => {
              toggleDrawer(false);
              navigate(`/becomeDriver`);
            }}
          >
            <ListItemIcon>
              <DirectionsCarIcon />
            </ListItemIcon>
            <ListItemText primary={"Become Driver"} />
          </ListItemButton>
        </ListItem>
      ) : (
        <>
          <ListItem disablePadding>
            <ListItemButton
              onClick={() => {
                toggleDrawer(false);
                navigate(`/myVehicle`);
              }}
            >
              <ListItemIcon>
                <DirectionsCarIcon />
              </ListItemIcon>
              <ListItemText primary={"My Vehicle"} />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding>
            <ListItemButton
              onClick={() => {
                toggleDrawer(false);
                navigate(`/createVehicle`);
              }}
            >
              <ListItemIcon>
                <AddCircleOutlineIcon />
              </ListItemIcon>
              <ListItemText primary={"Add Vehicle"} />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding>
            <ListItemButton
              onClick={() => {
                toggleDrawer(false);
                navigate(`/editVehicle`);
              }}
            >
              <ListItemIcon>
                <EditIcon />
              </ListItemIcon>
              <ListItemText primary={"Edit Vehicle"} />
            </ListItemButton>
          </ListItem>
        </>
      )}
      <Divider />
      {localStorage.role == "driver" && (
        <List>
          <ListItem disablePadding>
            <ListItemButton
              onClick={() => {
                toggleDrawer(false);
                navigate(`/myPosts`);
              }}
            >
              <ListItemIcon>
                <DynamicFeedIcon />
              </ListItemIcon>
              <ListItemText primary={"My Posts"} />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding>
            <ListItemButton
              onClick={() => {
                toggleDrawer(false);
                navigate(`/createPost`);
              }}
            >
              <ListItemIcon>
                <PostAddIcon />
              </ListItemIcon>
              <ListItemText primary={"Add Post"} />
            </ListItemButton>
          </ListItem>
        </List>
      )}
    </Box>
  );

  return (
    <div className="header">
      <img className="header-img" src={carLogo} />
      <div className="header-left-side">
        {localStorage.accessToken && (
          <div>
            <Button onClick={toggleDrawer(true)}>
              <FontAwesomeIcon className="header-bars" icon={faBars} />
            </Button>
            <Drawer open={open} onClose={toggleDrawer(false)}>
              {DrawerList}
            </Drawer>
          </div>
        )}
        <div className="logo-content">
          <Link to={"/"} className="logo">
            <img src={imgLogo} alt="logo" />
          </Link>
        </div>
      </div>
      <div className="navigation">
        <ul>
          {!localStorage.accessToken && (
            <>
              <li className="navigation-home">
                <Link to={"/"}>Home</Link>
              </li>
              <li>
                <Link to={"/about"}>About</Link>
              </li>
            </>
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
