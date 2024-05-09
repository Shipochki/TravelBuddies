import './NoVehicle.css'
import { Link } from "react-router-dom"

export const NoVehicle = () => {
    return (
        <div className='you-dont-have-vehicle'>
            <h3>You don't have added vehicle</h3>
            <div className='dont-vehicle-links'>
                <Link to={'/createVehicle'}>Add Vehicle</Link>
            </div>
        </div>
    )
}