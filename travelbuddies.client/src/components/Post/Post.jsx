import { faArrowRight, faCalendar, faCheck, faCircleXmark } from '@fortawesome/free-solid-svg-icons';
import './Post.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { OnJoinGroupSubmit } from '../../services/UserGroupService';
import { LazyLoadImage } from 'react-lazy-load-image-component';
import { Link } from 'react-router-dom';
import { useContext, useState } from 'react';
import { GetUserById } from '../../services/UserService';
import { GlobalContext } from '../../utils/contexts/GlobalContext';

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
    const { OnSetUser } = useContext(GlobalContext);

    const onSubmit = () => {
        OnJoinGroupSubmit(GroupId);
    }

    return (
        <div className='post-component'>
            <div className='post-creator'>
                <Link onClick={async (e) => {
                                e.preventDefault();
                                const result = await GetUserById(Creator.Id);
                                OnSetUser(result);}}>
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