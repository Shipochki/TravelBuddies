import { Link } from "react-router-dom"
import './MyPosts.css'
import { Post } from "../../components/Post/Post"
import { NotDriver } from "../../components/NotDriver/NotDriver"

export const MyPosts = ({posts}) => {

    return(
        <div className="myposts-main">
            {localStorage.role == 'driver' ? (
                <div className="myposts-content">
                    {posts.length > 0 ? (
                        <>
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
                            <div>
                                <Link to={'/editPost'}>Edit Post</Link>
                                <Link to={'/deletePost'}>Delete Post</Link>
                                <Link to={'/completePost'}>Complete Post</Link>
                            </div>
                        </div>
                            ))}
                        </>
                    ): (
                        <div>
                            <h2>You don't have created Posts</h2>
                        </div>
                    )}
                </div>
            ) : (
                <NotDriver/>
            )}
        </div>
    )
}