import { faArrowRight, faCalendar, faCheck, faCircleXmark } from '@fortawesome/free-solid-svg-icons';
import './Post.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { OnJoinGroupSubmit } from '../../services/UserGroupService';
import { LazyLoadImage } from 'react-lazy-load-image-component';
import { Link } from 'react-router-dom';
import { useState } from 'react';
import MapContainer from '../Map/Map'

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
}) => {
    const [groupId, setGroupId] = useState(GroupId);

    const onSubmit = () => {
        OnJoinGroupSubmit(groupId);
    }

    return (
        <div className='post-component'>
            <div className='post-creator'>
                <Link to={`/profile?id:${Creator.Id}`}>
                    <LazyLoadImage src={Creator.ProfilePictureLink ?? 'https://sttravelbuddies001.blob.core.windows.net/web/blank-profile-picture-973460_960_720.png'}/>
                </Link>
                <p>{Creator.FullName}</p>
            </div>
            {/* <MapContainer originCity={FromDestinationName} destinationCity={ToDestinationName}/> */}
            <div className='post-cities'>
                <p>{FromDestinationName}</p>
                <FontAwesomeIcon icon={faArrowRight}/>
                <p>{ToDestinationName}</p>
            </div>
            <p className='post-dateandtime'><FontAwesomeIcon icon={faCalendar}/> {DateAndTime}</p>
            <p className='post-decription'>{Description}</p>
            <div className='post-more-info'>
                <p className='post-priceperseat'>Price: {PricePerSeat}$</p>
                <p className='post-freeseats'>Seats available: {FreeSeats}</p>
                <p className='post-bool'>Baggage <FontAwesomeIcon icon={Baggage ? faCheck : faCircleXmark} /></p>
                <p className='post-bool'>Pets <FontAwesomeIcon icon={Pets ? faCheck : faCircleXmark} /></p>
            </div>
            <div className='join-button'>
                <button type='submit' onClick={onSubmit}>Join</button>
            </div>
        </div>
    )
}