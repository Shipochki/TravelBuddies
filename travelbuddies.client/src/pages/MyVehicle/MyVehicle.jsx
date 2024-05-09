import { LazyLoadImage } from 'react-lazy-load-image-component'
import './MyVehicle.css'
import { Link } from "react-router-dom"
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCheck, faX } from '@fortawesome/free-solid-svg-icons'
import { NoVehicle } from '../../components/NoVehicle/NoVehicle'

export const MyVehicle = ({vehicle}) => {

    return(
        <div className="myvehicle-main">
            {vehicle.length != 0 ? (
                    <div className='myvehicle-content'>
                        <div className='myvehicle-info'>
                            <p>Brand: {vehicle.brandName}</p>
                            <p>Model: {vehicle.modelName}</p>
                            <p>Fuel: {vehicle.fuel}</p>
                            <p>Seat count: {vehicle.seatCount}</p>
                            <p>ACSystem <FontAwesomeIcon icon={vehicle.acSystem ? faCheck : faX}/></p>
                        </div>
                        <LazyLoadImage src={vehicle.pictureLink}/>
                    </div>
            ) : (
                <NoVehicle/>
            )}
        </div>
    )
}