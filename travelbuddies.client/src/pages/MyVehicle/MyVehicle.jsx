import './MyVehicle.css'
import { NoVehicle } from '../../components/NoVehicle/NoVehicle'
import { Vehicle } from '../../components/Vehicle/Vehicle'
import { GetVehicleByOwnerId } from '../../services/VehicleService';
import { useEffect, useState } from 'react';

export const MyVehicle = () => {
    const [vehicle, setVehicle] = useState({});

    useEffect(() => {
        const fetchData = async () => {
            const data = await GetVehicleByOwnerId(localStorage.userId);
            setVehicle(data);
        }
        fetchData();
    }, []);

    return(
        <div className="myvehicle-main">
            {vehicle.id ? (
                <Vehicle vehicle={vehicle}/>
            ) : (
                <NoVehicle/>
            )}
        </div>
    )
}