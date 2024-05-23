import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import './MyPost.css'
import { faArrowRight } from '@fortawesome/free-solid-svg-icons'
import { Link } from 'react-router-dom'
import EditIcon from '@mui/icons-material/Edit';
import DeleteForeverOutlinedIcon from '@mui/icons-material/DeleteForeverOutlined';
import CheckCircleOutlineOutlinedIcon from '@mui/icons-material/CheckCircleOutlineOutlined';

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
                    <p className='mypost-baggage'>Baggage: {post.baggage ? 'Yes' : 'No'}</p>
                    <p className='mypost-pets'>Pets: {post.pets ? 'Yes': 'No'}</p>
                </div>
            </div>
            <div className='mypost-navigation'>
                <Link className='edit-post-btn' to={'/editPost'}><EditIcon fontSize='16px'/> Edit</Link>
                <Link className='complete-post-btn' to={'/completePost'}><CheckCircleOutlineOutlinedIcon fontSize='16px'/> Complete</Link>
                <Link className='delete-post-btn' to={'/deletePost'}><DeleteForeverOutlinedIcon fontSize='16px'/> Delete</Link>
            </div>
        </div>
    )
}