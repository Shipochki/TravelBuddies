import { faArrowRight, faCalendar, faCheck, faCircleXmark } from '@fortawesome/free-solid-svg-icons';
import './Post.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { OnJoinGroupSubmit } from '../../services/UserGroupService';

export const Post = ({post}) => {
    return (
        <div className='post-component'>
            <p className='post-cities'>{post.FromDestinationName} {<FontAwesomeIcon icon={faArrowRight}/>} {post.ToDestinationName}</p>
            <p className='post-decription'>{post.Description}</p>
            <p className='post-priceperseat'>{post.PricePerSeat}$</p>
            <p className='post-freeseats'>Free seats: {post.FreeSeats}</p>
            <p className='post-bool'>Baggage <FontAwesomeIcon icon={post.Baggage ? faCheck : faCircleXmark} /></p>
            <p className='post-bool'>Pets <FontAwesomeIcon icon={post.Pets ? faCheck : faCircleXmark} /></p>
            <p className='post-dateandtime'><FontAwesomeIcon icon={faCalendar}/> {post.DateAndTime}</p>
            <button type='submit' onClick={OnJoinGroupSubmit(post.GroupId)}>Join</button>
        </div>
    )
}