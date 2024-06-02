import "./VehicleMenu.css";
import DirectionsCarIcon from "@mui/icons-material/DirectionsCar";
import AddCircleOutlineIcon from "@mui/icons-material/AddCircleOutline";
import EditIcon from "@mui/icons-material/Edit";
import { Link } from "react-router-dom";

export const VehicleMenu = () => {
  return (
    <div className="vehicle-menu">
        <div className="vehicle-menu-header">
          <h2>Vehicle Menu</h2>
        </div>
      <div className="vehicle-menu-content">
        <Link to={"/myVehicle"} className="vehicle-menu-my">
          <h3>My Vehicle</h3>
          <DirectionsCarIcon />
        </Link>
        <Link to={"/createVehicle"} className="vehicle-menu-add">
          <h3>Add Vehicle</h3>
          <AddCircleOutlineIcon />
        </Link>
        <Link to={"/editVehicle"}>
          <h3>Edit Vehicle</h3>
          <EditIcon />
        </Link>
      </div>
    </div>
  );
};
