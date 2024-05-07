import { Link } from "react-router-dom"
import './MyPosts.css'
import { Post } from "../../components/Post/Post"

export const MyPosts = ({posts}) => {

    return(
        <div className="myposts-main">
            {localStorage.role == 'driver' ? (
                <div className="myposts-content">
                    {posts.map((p) => (
                        <div className="myposts-post">
                            <p>{p.fromDestinationName}</p>
                            <p>{p.toDestinationName}</p>
                            <p>{p.description}</p>
                            <p>{p.pricePerSeat}</p>
                            <p>{p.freeSeats}</p>
                            <p>{p.baggage}</p>
                            <p>{p.pets}</p>
                            <p>{p.dateAndTime}</p>
                        </div>
                    ))}
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