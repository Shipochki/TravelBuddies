import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import './MyPost.css'
import { faArrowRight } from '@fortawesome/free-solid-svg-icons'
import { Link } from 'react-router-dom'

export const MyPost = ({post}) => {

    return(
        <div className='mypost-main'>
            <div className='mypost-content'>
                <div className='mypost-destin'>
                    <p>{post.fromDestinationName}</p>
                    <FontAwesomeIcon icon={faArrowRight}/>
                    <p>{post.toDestinationName}</p>
                </div>
                <p className='mypost-date-time'>{post.dateAndTime}</p>
                <textarea className='mypost-desc'>{post.description}</textarea>
                <div className='mypost-info'>
                    <p className='mypost-seats'>Avalible seats: {post.freeSeats}</p>
                    <p className='mypost-pricePerSeat'>Price per seats: {post.pricePerSeat} EUR</p>
                    <p className='mypost-baggage'>{post.baggage}</p>
                    <p className='mypost-pets'>{post.pets}</p>
                </div>
            </div>
            <div className='mypost-navigation'>
                <Link to={'/editPost'}>Edit Post</Link>
                <Link to={'/deletePost'}>Delete Post</Link>
                <Link to={'/completePost'}>Complete Post</Link>
            </div>
        </div>
    )
}