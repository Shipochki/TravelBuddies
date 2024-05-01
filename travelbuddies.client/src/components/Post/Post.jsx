import { faArrowRight, faCalendar, faCheck, faCircleXmark } from '@fortawesome/free-solid-svg-icons';
import './Post.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { OnJoinGroupSubmit } from '../../services/UserGroupService';

export const Post = ({
    FromDestinationName,
    ToDestinationName,
    Description,
    PricePerSeat,
    FreeSeats,
    Baggage,
    Pets,
    DateAndTime,
    GroupId
}) => {
    console.log(FromDestinationName);
    return (
        <div className='post-component'>
            <p className='post-cities'>{FromDestinationName} {<FontAwesomeIcon icon={faArrowRight}/>} {ToDestinationName}</p>
            <p className='post-decription'>{Description}</p>
            <p className='post-priceperseat'>{PricePerSeat}$</p>
            <p className='post-freeseats'>Free seats: {FreeSeats}</p>
            <p className='post-bool'>Baggage <FontAwesomeIcon icon={Baggage ? faCheck : faCircleXmark} /></p>
            <p className='post-bool'>Pets <FontAwesomeIcon icon={Pets ? faCheck : faCircleXmark} /></p>
            <p className='post-dateandtime'><FontAwesomeIcon icon={faCalendar}/> {DateAndTime}</p>
            <button type='submit' onClick={OnJoinGroupSubmit(GroupId)}>Join</button>
        </div>
    )
}