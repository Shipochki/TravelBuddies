import { Link } from "react-router-dom"
import './MyPosts.css'

export const MyPosts = ({posts}) => {

    return(
        <div className="myposts-main">
            {localStorage.role == 'driver' ? (
                <div className="myposts-content">

                </div>
            ) : (
                <div className="not-driver">
                    <h3>You are not Driver</h3>
                    <Link to={'/becomeDriver'}>Bceome Driver</Link>
                </div>
            )}
        </div>
    )
}