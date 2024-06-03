import "./VehicleMenu.css";
import DirectionsCarIcon from "@mui/icons-material/DirectionsCar";
import AddCircleOutlineIcon from "@mui/icons-material/AddCircleOutline";
import EditIcon from "@mui/icons-material/Edit";
import { Link } from "react-router-dom";
import backgroundImg from "../../utils/images/white-background-with-blue-geometric-and-white-line-pattern-free-vector.jpg";

export const VehicleMenu = () => {
  return (
    <div className="vehicle-menu">
        <img className="demo-bg" src={backgroundImg} />
        <div className="vehicle-menu-content"> 
        <div className="vehicle-menu-header">
          <h2>Vehicle Menu</h2>
        </div>
      <div className="vehicle-menu-links">
        <Link to={"/myVehicle"} className="vehicle-menu-my">
          <h3>My</h3>
          <DirectionsCarIcon />
        </Link>
        <Link to={"/createVehicle"} className="vehicle-menu-add">
          <h3>Add</h3>
          <AddCircleOutlineIcon />
        </Link>
        <Link to={"/editVehicle"}>
          <h3>Edit</h3>
          <EditIcon />
        </Link>
      </div>
        </div>
       
    </div>
  );
};
