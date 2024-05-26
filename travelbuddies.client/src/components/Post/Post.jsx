import { faArrowRight, faCalendarDays, faCheck, faCircleXmark } from '@fortawesome/free-solid-svg-icons';
import './Post.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { OnJoinGroupSubmit } from '../../services/UserGroupService';
import { LazyLoadImage } from 'react-lazy-load-image-component';
import { Link, redirect, useNavigate } from 'react-router-dom';

import personImgOffline from '../../utils/images/blank-profile-picture-973460_960_720.png'
import { useContext } from 'react';
import { GlobalContext } from '../../utils/contexts/GlobalContext';

export const Post = ({post}) => {
    const { OnSetGroups }= useContext(GlobalContext);
    const navigate = useNavigate();

    const OnJoinSubmit = async (e) => {
        e.preventDefault();
        
        await OnJoinGroupSubmit(post.GroupId);

        OnSetGroups();
        navigate(`/group/${post.GroupId}`);
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
                <Link to={`/profile/${post.Creator.Id}`}>
                    <LazyLoadImage src={post.Creator.ProfilePictureLink ? Creator.ProfilePictureLink : personImgOffline}/>
                </Link>
                <p>{post.Creator.FullName}</p>
            </div>
            <div className='post-cities'>
                <p>{post.FromDestinationName}</p>
                <FontAwesomeIcon icon={faArrowRight}/>
                <p>{post.ToDestinationName}</p>
            </div>
            <p className='post-dateandtime'><FontAwesomeIcon icon={faCalendarDays}/> {post.DateAndTime}</p>
            <p className='post-decription'>{post.Description}</p>
            <div className='post-more-info'>
                <p className='post-priceperseat'>Price: {post.PricePerSeat} {post.Currency}</p>
                <p className='post-freeseats'>Seats available: {post.FreeSeats}</p>
                <p className='post-bool'>Baggage <FontAwesomeIcon icon={post.Baggage ? faCheck : faCircleXmark} /></p>
                <p className='post-bool'>Pets <FontAwesomeIcon icon={post.Pets ? faCheck : faCircleXmark} /></p>
            </div>
            <div className='join-button'>
                {!post.BlackListsUsers.includes(localStorage.userId) ? (
                    <button type='submit' onClick={(e) =>
                        {post.Participants.includes(localStorage.userId) ? 
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