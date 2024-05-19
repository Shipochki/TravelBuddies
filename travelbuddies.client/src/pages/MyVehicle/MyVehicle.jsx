import { LazyLoadImage } from 'react-lazy-load-image-component'
import './MyVehicle.css'
import { Link } from "react-router-dom"
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCheck, faX } from '@fortawesome/free-solid-svg-icons'
import { NoVehicle } from '../../components/NoVehicle/NoVehicle'
import { Vehicle } from '../../components/Vehicle/Vehicle'

export const MyVehicle = ({vehicle}) => {

    return(
        <div className="myvehicle-main">
            {vehicle.length != 0 ? (
                <Vehicle vehicle={vehicle}/>
            ) : (
                <NoVehicle/>
            )}
        </div>
    )
}