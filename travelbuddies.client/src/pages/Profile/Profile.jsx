import { LazyLoadImage } from 'react-lazy-load-image-component';
import './Profile.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheck, faX } from '@fortawesome/free-solid-svg-icons';

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
                    {user.reviews.length > 0 ? (
                        user.reviews.map((r) => {
                            <div className='profile-review'>
                                <LazyLoadImage src={r.creator.profilePictureLink}/>
                                <p>Rating: {r.rating}</p>
                                <p>Text: {r.text}</p>
                            </div>
                        })
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