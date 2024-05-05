import { Link } from "react-router-dom"
import './Menu.css'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faCarSide, faIdCard, faStar } from "@fortawesome/free-solid-svg-icons"
import { GetAllReviewByReciverId } from "../../services/ReviewService"
import { useContext } from "react"
import { GlobalContext } from "../../utils/contexts/GlobalContext"

export const Menu = () => {
    const { OnSetReviews } = useContext(GlobalContext);

    return(
        <div className="left-menu-main">
            <div className="reviews-link">
                <h4>{<FontAwesomeIcon icon={faStar}/>} Review</h4>
                <Link 
                    onClick={async (e) => {
                        e.preventDefault();
                        const result = await GetAllReviewByReciverId(localStorage.userId);
                        OnSetReviews(result);
                        }}
                >My Reviews</Link>
            </div>
            <div className="vehicle-links">
                <h4>{<FontAwesomeIcon icon={faCarSide}/>} Vehicle</h4>
                <Link to={'/myvehicle'}>My Vehicle</Link>
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