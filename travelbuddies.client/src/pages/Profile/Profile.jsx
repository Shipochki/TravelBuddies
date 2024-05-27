import { LazyLoadImage } from 'react-lazy-load-image-component';
import './Profile.css';
import { Review } from '../../components/Review/Review';
import { CreateReview } from '../../components/CreateReview/CreateReview';
import { Vehicle } from '../../components/Vehicle/Vehicle';
import { EditReview } from '../../components/EditReview/EditReview';
import { useContext, useEffect, useState } from 'react';
import { GetUserById } from '../../services/UserService';
import { Link, useParams } from 'react-router-dom';

import personImgOffline from '../../utils/images/blank-profile-picture-973460_960_720.png'
import { GlobalContext } from '../../utils/contexts/GlobalContext';
import { Loading } from '../Loading/Loading';
import { StarGenerator } from '../../components/StarGenerator/StarGenerator';

export const Profile = ({user}) => {
    const { OnSetUser } = useContext(GlobalContext);
    const {id} = useParams();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if(!user.id || user.id != id){
            OnSetUser(id);
        }
        // const fetchData = async () => {
        //     const data = await GetUserById(id);
        //     setUser(data);
        // };
        // fetchData();

        setTimeout(() => {
            setLoading(false); // Set loading to false after data is fetched
          }, 500);

        const handlePopState = () => {
            // When user navigates back, trigger data reload
            //fetchData();
            OnSetUser(id);
          };
      
          // Add event listener for browser navigation back
          window.addEventListener('popstate', handlePopState);
      
          // Cleanup: remove event listener when component unmounts
          return () => {
            window.removeEventListener('popstate', handlePopState);
          };
    }, [id]);

    if(loading){
        return <Loading/>
    }

    return (
        <div className='profile-main'>
            <div className='profile-content'>
                <div className='profile-content-info'>
                    <LazyLoadImage 
                    src={user.profilePictureLink != null
                         ? user.profilePictureLink
                          : personImgOffline}
                          />
                    <div className='profile-info-content'>
                        <h3>{user.firstName} {user.lastName}</h3>
                        <div>
                            <p>Email: {user.email}</p>
                            <p>Country: {user.country ? user.country : 'None'}</p>
                            <p>City: {user.city ? user.city : 'None'}</p>
                            <div>
                                <StarGenerator num={Math.round(user.rating)}/>
                                <p>Rating: {user.rating}</p>
                            </div>
                            
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
                    {user.reviews ? (
                        user.reviews.map((r) => (
                            <>
                                <Review key={`review-key-${r.id}`} review={r}/>
                                <EditReview key={`edit-review-key-${r.id}`} review={r}/>
                            </>
                        ))
                    ): (
                        <div>
                            User don't have any reviews
                        </div>
                    )}
                    {user.reviews && user.reviews.length == 3 && (
                        <div>
                            <Link className='load-more-reviews' to={`/reviews/${user.id}`}>Load more</Link>
                        </div>
                    )}
                </div>
            </div>
        </div>
    )
}