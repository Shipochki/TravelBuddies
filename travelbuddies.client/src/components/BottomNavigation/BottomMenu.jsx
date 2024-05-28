import { BottomNavigation, BottomNavigationAction, Box } from "@mui/material";
import HomeIcon from "@mui/icons-material/Home";
import MenuIcon from "@mui/icons-material/Menu";
import GroupsIcon from "@mui/icons-material/Groups";
import { useState } from "react";
import './BottomMenu.css'

export const BottomMenu = () => {
  const [value, setValue] = useState(0);

  return (
    <div className="bottom-menu">
      <Box sx={{ width: 500 }}>
        <BottomNavigation
          showLabels
          value={value}
          onChange={(event, newValue) => {
            setValue(newValue);
          }}
        >
          <BottomNavigationAction label="Recents" icon={<MenuIcon />} />
          <BottomNavigationAction label="Favorites" icon={<HomeIcon />} />
          <BottomNavigationAction label="Nearby" icon={<GroupsIcon />} />
        </BottomNavigation>
      </Box>
    </div>
  );
};
