import { BottomNavigation, BottomNavigationAction, Box } from "@mui/material";
import HomeIcon from "@mui/icons-material/Home";
import MenuIcon from "@mui/icons-material/Menu";
import GroupsIcon from "@mui/icons-material/Groups";
import { useState } from "react";
import "./BottomMenu.css";
import { useNavigate } from "react-router-dom";

export const BottomMenu = () => {
  const navigate = useNavigate();
  const [value, setValue] = useState(1);

  const OnSetMenuVisable = () => {
    let doc = window.document.querySelector("#groups-menu-main");
    doc.style.visibility = "hidden";

    let menu = window.document.querySelector("#left-menu-main");
    if (menu.style.visibility == "hidden") {
      menu.style.visibility = "visible";
      menu.addEventListener('click', () => {
        menu.style.visibility = 'hidden';
        setValue(1);
      })
    } else {
      menu.style.visibility = "hidden";
      setValue(1);
    }
  };

  const OnSetGroupsVisable = () => {
    let doc = window.document.querySelector("#left-menu-main");
    doc.style.visibility = "hidden";

    let menu = window.document.querySelector("#groups-menu-main");
    if (menu.style.visibility == "hidden") {
      menu.style.visibility = "visible";
      menu.addEventListener('click', () => {
        menu.style.visibility = 'hidden';
        setValue(1);
      })
    } else {
      menu.style.visibility = "hidden";
      setValue(1);
    }
  };

  const OnSetHome = () => {
    window.document.querySelector("#groups-menu-main").style.visibility =
      "hidden";
    window.document.querySelector("#left-menu-main").style.visibility =
      "hidden";

      navigate('/');
  };

  return (
    <div className="bottom-menu">
      <Box sx={{ width: "100%" }}>
        <BottomNavigation
          showLabels
          value={value}
          onChange={(event, newValue) => {
            setValue(newValue);
          }}
        >
          <BottomNavigationAction
            onClick={OnSetMenuVisable}
            label="Menu"
            icon={<MenuIcon />}
          />
          <BottomNavigationAction
            onClick={OnSetHome}
            label="Home"
            icon={<HomeIcon />}
          />
          <BottomNavigationAction
            onClick={OnSetGroupsVisable}
            label="Groups"
            icon={<GroupsIcon />}
          />
        </BottomNavigation>
      </Box>
    </div>
  );
};
