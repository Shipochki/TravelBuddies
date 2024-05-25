import "./MyVehicle.css";
import { NoVehicle } from "../../components/NoVehicle/NoVehicle";
import { Vehicle } from "../../components/Vehicle/Vehicle";
import { GetVehicleByOwnerId } from "../../services/VehicleService";
import { useEffect, useState } from "react";
import { Loading } from "../Loading/Loading";
import { NotDriver } from "../../components/NotDriver/NotDriver";

export const MyVehicle = () => {
  const [vehicle, setVehicle] = useState({});
  const [loading, setLoading] = useState(true);

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

  if (loading) {
    return <Loading />;
  }

  return (
    <div className="myvehicle-main">
      {localStorage.role == "driver" ? (
        <>{vehicle.id ? <Vehicle vehicle={vehicle} /> : <NoVehicle />}</>
      ) : (
        <NotDriver />
      )}
    </div>
  );
};
