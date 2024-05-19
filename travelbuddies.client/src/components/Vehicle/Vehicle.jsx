import './Vehicle'

export const Vehicle = ({vehicle}) => {

    return(
        <div className='vehicle'>
            <div className='vehicle-info'>
                <p>Brand: {vehicle.brandName}</p>
                <p>Model: {vehicle.modelName}</p>
                <p>Fuel: {vehicle.fuel}</p>
                <p>Seat count: {vehicle.seatCount}</p>
                <p>ACSystem <FontAwesomeIcon icon={vehicle.acSystem ? faCheck : faX}/></p>
            </div>
            <LazyLoadImage src={vehicle.pictureLink}/>
        </div>
    )
}