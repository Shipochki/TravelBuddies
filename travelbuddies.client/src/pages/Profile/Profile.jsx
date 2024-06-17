import { LazyLoadImage } from 'react-lazy-load-image-component';
import './Profile.css';
import { Review } from '../../components/Review/Review';
import { CreateReview } from '../../components/CreateReview/CreateReview';
import { Vehicle } from '../../components/Vehicle/Vehicle';
import { useContext, useEffect, useState } from 'react';
import { Link, useParams } from 'react-router-dom';

import personImgOffline from '../../utils/images/blank-profile-picture-973460_960_720.png'
import { GlobalContext } from '../../utils/contexts/GlobalContext';
import { Loading } from '../Loading/Loading';
import { Rating } from '@mui/material';

import backgroundImg from "../../utils/images/white-background-with-blue-geometric-and-white-line-pattern-free-vector.jpg";

export const Profile = ({user}) => {
    const { OnSetUser } = useContext(GlobalContext);
    const {id} = useParams();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if(!user.id || user.id != id){
            OnSetUser(id);
        }
        setTimeout(() => {
            setLoading(false);
          }, 500);

        const handlePopState = () => {
            OnSetUser(id);
          };
      
          window.addEventListener('popstate', handlePopState);
      
          return () => {
            window.removeEventListener('popstate', handlePopState);
          };
    }, [id]);

    if(loading){
        return <Loading/>
    }

    return (
        <div className='profile-main'>
            <img className="demo-bg" src={backgroundImg} />
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
                                <p>Rating: {user.rating}</p>
                                <Rating name="read-only" value={Math.round(user.rating)} readOnly />
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
                    {user.reviews && user.reviews.length ? (
                        user.reviews.map((r) => (
                            <>
                                <Review key={`review-key-${r.id}`} review={r} setData={OnSetUser}/>
                            </>
                        ))
                    ): (
                        <div>
                            User don't have any reviews
                        </div>
                    )}
                    {user.reviews && user.reviews.length == 3 && (
                        <div>
                            <Link className='load-more-reviews' to={`/reviews?reciverId=${user.id}&page=1&pageCount=10`}>Load more</Link>
                        </div>
                    )}
                </div>
            </div>
        </div>
    )
}