import { faArrowRight, faCalendarDays, faCheck, faCircleXmark } from '@fortawesome/free-solid-svg-icons';
import './Post.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { OnJoinGroupSubmit } from '../../services/UserGroupService';
import { LazyLoadImage } from 'react-lazy-load-image-component';
import { Link, useNavigate } from 'react-router-dom';

import personImgOffline from '../../utils/images/blank-profile-picture-973460_960_720.png'

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
    Participants,
    BlackListsUsers
}) => {
    const navigate = useNavigate();

    const OnJoinSubmit = async (e) => {
        e.preventDefault();
        
        await OnJoinGroupSubmit(GroupId);

        navigate(`/group/${GroupId}`)
    }

    const IsAllreadyInGroupAlert = () => {
        alert(`It seems you're already a member of the group.`)
    }

    const IsBannedFromGroup = () => {
        alert(`Apologies, but due to a prior issue, you're unable to participate in the group at this time.`)
    }

    return (
        <div className='post-component'>
            <div className='post-creator'>
                <Link to={`/profile/${Creator.Id}`}>
                    <LazyLoadImage src={Creator.ProfilePictureLink ? Creator.ProfilePictureLink : personImgOffline}/>
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
                {!BlackListsUsers.includes(localStorage.userId) ? (
                    <button type='submit' onClick={(e) =>
                        {Participants.includes(localStorage.userId) ? 
                        IsAllreadyInGroupAlert() :
                        OnJoinSubmit(e)}}>Join</button>) : (
                    <button onClick={() => {
                        IsBannedFromGroup();
                    }}>Join</button>
                )}
            </div>
        </div>
    )
}