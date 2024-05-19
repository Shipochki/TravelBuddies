import { useContext } from 'react';
import { GetUserById } from '../../services/UserService';
import './Review.css';
import { GlobalContext } from '../../utils/contexts/GlobalContext';
import { LazyLoadImage } from 'react-lazy-load-image-component';
import { StarGenerator } from '../StarGenerator/StarGenerator';

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
            src={review.creator.creatorProfileLink 
                ? review.creator.creatorProfileLink 
                : 'https://sttravelbuddies001.blob.core.windows.net/web/blank-profile-picture-973460_960_720.png'}/> 
            <StarGenerator num={review.rating}/>
            <p>{review.text}</p>
        </div>
    )
}