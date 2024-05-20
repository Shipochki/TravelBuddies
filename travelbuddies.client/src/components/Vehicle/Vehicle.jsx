import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import './Vehicle'
import { LazyLoadImage } from 'react-lazy-load-image-component'
import { faCheck, faX } from '@fortawesome/free-solid-svg-icons'

export const Vehicle = ({vehicle}) => {

    return(
        <div className='vehicle'>
            <div className='vehicle-info'>
                <p>Brand: {vehicle.brandName}</p>
                <p>Model: {vehicle.modelName}</p>
                <p>Year: {vehicle.year}</p>
                <p>Color: {vehicle.color}</p>
                <p>Fuel: {vehicle.fuel}</p>
                <p>Seat count: {vehicle.seatCount}</p>
                <p>ACSystem <FontAwesomeIcon icon={vehicle.acSystem ? faCheck : faX}/></p>
            </div>
            <LazyLoadImage src={vehicle.pictureLink}/>
        </div>
    )
}