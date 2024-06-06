import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import './Vehicle.css'
import { LazyLoadImage } from 'react-lazy-load-image-component'
import { faCheck, faX } from '@fortawesome/free-solid-svg-icons'
import vehicleImg from '../../utils/images/2015-mercedes-benz-c-class-photos-and-info-news-car-and-driver-photo-558534-s-original.jpg'

export const Vehicle = ({vehicle}) => {

    return(
        <div className='vehicle'>
            <div className='vehicle-info'>
                <p>Brand: <span>{vehicle.brandName}</span></p>
                <p>Model: <span>{vehicle.modelName}</span></p>
                <p>Year: <span>{vehicle.year}</span></p>
                <p>Color: <span>{vehicle.color}</span></p>
                <p>Fuel: <span>{vehicle.fuel}</span></p>
                <p>Seat count: <span>{vehicle.seatCount}</span></p>
                <p>ACSystem: <span><FontAwesomeIcon icon={vehicle.acSystem ? faCheck : faX}/></span></p>
            </div>
            <LazyLoadImage className='vehicle-img' src={vehicle.pictureLink ? vehicle.pictureLink : vehicleImg}/>
            {/* <LazyLoadImage className='vehicle-img' src={vehicleImg}/> */}
        </div>
    )
}