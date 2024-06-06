import "./MyVehicle.css";
import { NoVehicle } from "../../components/NoVehicle/NoVehicle";
import { Vehicle } from "../../components/Vehicle/Vehicle";
import { GetVehicleByOwnerId, OnDeleteVehicleSubmit } from "../../services/VehicleService";
import { useEffect, useState } from "react";
import { Loading } from "../Loading/Loading";
import { NotDriver } from "../../components/NotDriver/NotDriver";
import { DeleteForeverOutlined } from "@mui/icons-material";
import EditIcon from "@mui/icons-material/Edit";

import backgroundImg from "../../utils/images/white-background-with-blue-geometric-and-white-line-pattern-free-vector.jpg";
import { Box, SpeedDial, SpeedDialAction, SpeedDialIcon } from "@mui/material";
import { useNavigate } from "react-router-dom";

export const MyVehicle = () => {
  const [vehicle, setVehicle] = useState({});
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      if (localStorage.role == "driver") {
        const data = await GetVehicleByOwnerId(localStorage.userId);
        setVehicle(data);
      }

      setLoading(false);
    };
    fetchData();
  }, []);

  const onDeleteVehicle = async (e) => {
    const text = `Are you sure you want to delete vehicle?`;
    if (confirm(text) == true) {
      e.preventDefault();

      await OnDeleteVehicleSubmit(vehicle.id);

      const data = await GetVehicleByOwnerId(localStorage.userId);
      setVehicle(data);
    }
  };

  if (loading) {
    return <Loading />;
  }

  return (
    <div className="myvehicle-main">
      <img className="demo-bg" src={backgroundImg} />
      {localStorage.role == "driver" ? (
        <>
          {vehicle && vehicle.id ? (
            <div className="myvehicle-content">
              <Vehicle vehicle={vehicle} />
              <Box
                sx={{ height: 320, transform: "translateZ(0px)", flexGrow: 1 }}
              >
                <SpeedDial
                  ariaLabel="SpeedDial basic example"
                  sx={{ position: "absolute", bottom: 16, right: 16 }}
                  icon={<SpeedDialIcon />}
                >
                  <SpeedDialAction
                    key={"edit-vehicle"}
                    icon={<EditIcon/>}
                    tooltipTitle={"Edit"}
                    onClick={() => {
                      navigate('/editVehicle')
                    }}
                  />
                  <SpeedDialAction
                    key={"delete-vehicle"}
                    icon={<DeleteForeverOutlined/>}
                    tooltipTitle={"Delete"}
                    onClick={onDeleteVehicle}
                  />
                </SpeedDial>
              </Box>
            </div>
          ) : (
            <NoVehicle />
          )}
        </>
      ) : (
        <NotDriver />
      )}
    </div>
  );
};
