import { faArrowRight, faCalendarDays, faCheck, faCircleXmark } from '@fortawesome/free-solid-svg-icons';
import './Post.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { OnJoinGroupSubmit } from '../../services/UserGroupService';
import { LazyLoadImage } from 'react-lazy-load-image-component';
import { Link, useNavigate } from 'react-router-dom';

export const Post = ({
    FromDestinationName,
    ToDestinationName,
    Description,
    PricePerSeat,
    FreeSeats,
    Baggage,
    Pets,
    DateAndTime,
    GroupId,
    Creator,
    Participants
}) => {
    const navigate = useNavigate();

    const OnJoinSubmit = async (e) => {
        e.preventDefault();
        
        await OnJoinGroupSubmit(GroupId);

        navigate(`/group/${GroupId}`)
    }

    return (
        <div className='post-component'>
            <div className='post-creator'>
                <Link to={`/profile/${Creator.Id}`}>
                    <LazyLoadImage src={Creator.ProfilePictureLink ?? 'https://sttravelbuddies001.blob.core.windows.net/web/blank-profile-picture-973460_960_720.png'}/>
                </Link>
                <p>{Creator.FullName}</p>
            </div>
            <div className='post-cities'>
                <p>{FromDestinationName}</p>
                <FontAwesomeIcon icon={faArrowRight}/>
                <p>{ToDestinationName}</p>
            </div>
            <p className='post-dateandtime'><FontAwesomeIcon icon={faCalendarDays}/> {DateAndTime}</p>
            <p className='post-decription'>{Description}</p>
            <div className='post-more-info'>
                <p className='post-priceperseat'>Price: {PricePerSeat}$</p>
                <p className='post-freeseats'>Seats available: {FreeSeats}</p>
                <p className='post-bool'>Baggage <FontAwesomeIcon icon={Baggage ? faCheck : faCircleXmark} /></p>
                <p className='post-bool'>Pets <FontAwesomeIcon icon={Pets ? faCheck : faCircleXmark} /></p>
            </div>
            <div className='join-button'>
                <button type='submit' onClick={(e) =>
                        {Participants.includes(localStorage.userId) ? 
                        alert(`You are allready in group: 
                        ${FromDestinationName} -> ${ToDestinationName}
                        ${DateAndTime}`) :
                        OnJoinSubmit(e)}}>Join</button>
            </div>
        </div>
    )
}