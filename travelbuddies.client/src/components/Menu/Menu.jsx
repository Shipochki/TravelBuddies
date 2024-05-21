import { Link } from "react-router-dom"
import './Menu.css'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faCarSide, faIdCard, faStar } from "@fortawesome/free-solid-svg-icons"

export const Menu = () => {
    return(
        <div className="left-menu-main">
            <div className="reviews-link">
                <h4>{<FontAwesomeIcon icon={faStar}/>} Review</h4>
                <Link to={`/reviews/${localStorage.userId}`}>My Reviews</Link>
            </div>
            <div className="vehicle-links">
                <h4>{<FontAwesomeIcon icon={faCarSide}/>} Vehicle</h4>
                <Link to={'/myVehicle'}>My Vehicle</Link>
                <Link to={'/createVehicle'}>Add Vehicle</Link>
                <Link to={'/editVehicle'}>Edit Vehicle</Link>
            </div>
            <div className="driver-links">
                <h4>{<FontAwesomeIcon icon={faIdCard}/>} Driver</h4>
                <Link to={'/becomeDriver'}>Become Driver</Link>
                <Link to={'/myPosts'}>My Posts</Link>
                <Link to={'/createPost'}>Add Post</Link>
            </div>
        </div>
    )
}