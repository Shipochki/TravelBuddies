import { LazyLoadImage } from 'react-lazy-load-image-component';
import './Profile.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheck, faX } from '@fortawesome/free-solid-svg-icons';
import { Review } from '../../components/Review/Review';
import { StarSelector } from '../../components/StarSelector/StarSelector';
import { useForm } from '../../utils/hooks/useForm';
import { OnCreateReviewSubmit } from '../../services/ReviewService';

const ReviewFromKeys = {
    Text: 'text',
    Rating: 'rating'
}

export const Profile = ({user}) => {
    const {values, changeHandler, onSubmit} = useForm({
        [ReviewFromKeys.Text]: '',
        [ReviewFromKeys.Rating]: 0,
    }, OnCreateReviewSubmit)

    const onChangeStar = (star) => {
        values[ReviewFromKeys.Rating] = star;
        changeHandler;
    }

    return (
        <div className='profile-main'>
            <div className='profile-content'>
                <div className='profile-content-info'>
                    <LazyLoadImage 
                    src={user.profilePictureLink != null
                         ? user.profilePictureLink
                          : 'https://sttravelbuddies001.blob.core.windows.net/web/blank-profile-picture-973460_960_720.png'}
                          />
                    <div className='profile-info-content'>
                        <h3>{user.firstName} {user.lastName}</h3>
                        <div>
                            <p>Email: {user.email}</p>
                            <p>Country: {user.country ? user.country : 'None'}</p>
                            <p>City: {user.city ? user.city : 'None'}</p>
                        </div>
                    </div>
                </div>
                <div className='profile-vehicle'>
                    <h4>Vehicle</h4>
                    {user.vehicle ? (
                        <div className='vehicle'>
                            <div className='vehicle-info'>
                                <p>{user.vehicle.brandName}</p>
                                <p>{user.vehicle.modelName}</p>
                                <p>{user.vehicle.fuel}</p>
                                <p>{user.vehicle.seatCount}</p>
                                <p>ACSystem <FontAwesomeIcon icon={user.vehicle.aCSystem == true ? faCheck : faX}/></p>
                            </div>
                            <LazyLoadImage src={user.vehicle.pictureLink}/>
                        </div>
                    ) : (
                        <div>
                            User don't have vehicle
                        </div>
                    )}
                </div>
                <div className='profile-reviews'>
                    <h4>Reviews</h4>
                    {user.reviews.length > 0 ? (
                        user.reviews.map((r) => {
                            <Review review={r}/>
                        })
                    ): (
                        <div>
                            User don't have any reviews
                        </div>
                    )}
                    {user.id != localStorage.userId && (
                        <form className='review-form' onSubmit={onSubmit}>
                            <StarSelector totalStars={5} onSelect={onChangeStar}/>
                            <input 
                            type='text'
                            placeholder='Your review here'
                            name={[ReviewFromKeys.Text]}
                            value={values[ReviewFromKeys.Text]}
                            onChange={changeHandler}
                            />
                            <button>Submit</button>
                        </form>
                    )}
                </div>
            </div>
        </div>
    )
}