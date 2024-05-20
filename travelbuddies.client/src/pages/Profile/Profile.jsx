import { LazyLoadImage } from 'react-lazy-load-image-component';
import './Profile.css';
import { Review } from '../../components/Review/Review';
import { CreateReview } from '../../components/CreateReview/CreateReview';
import { Vehicle } from '../../components/Vehicle/Vehicle';
import { EditReview } from '../../components/EditReview/EditReview';

export const Profile = ({user}) => {

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
                        <Vehicle vehicle={user.vehicle}/>
                    ) : (
                        <div>
                            User don't have a vehicle
                        </div>
                    )}
                </div>
                <div className='profile-reviews'>
                    <h4>Reviews</h4>
                    {user.id != localStorage.userId && (
                       <CreateReview user={user}/>  
                    )}
                    {user.reviews.length > 0 ? (
                        user.reviews.map((r) => (
                            <>
                                <Review key={r.id} review={r} userId={user.id}/>
                                <EditReview key={r.id} userId={user.id} review={r}/>
                            </>
                        ))
                    ): (
                        <div>
                            User don't have any reviews
                        </div>
                    )}
                </div>
            </div>
        </div>
    )
}