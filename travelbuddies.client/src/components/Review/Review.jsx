import { useContext } from 'react';
import { GetUserById } from '../../services/UserService';
import './Review.css';
import { GlobalContext } from '../../utils/contexts/GlobalContext';
import { LazyLoadImage } from 'react-lazy-load-image-component';

export const Review = ({review}) => {
    const { OnSetUser } = useContext(GlobalContext);

    const LoadProfile = async (e) => {
        e.preventDefault();
        const result = await GetUserById(review.creator.id);
        OnSetUser(result);
    }

    return(
        <div className='profile-review'>
            <LazyLoadImage
            onClick={LoadProfile} 
            src={review.creator.profilePictureLink}/>
            <p>{review.rating}</p>
            <p>{review.text}</p>
        </div>
    )
}