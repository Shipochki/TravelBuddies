import { useContext } from 'react';
import { GetUserById } from '../../services/UserService';
import './Review.css';
import { GlobalContext } from '../../utils/contexts/GlobalContext';

export const Review = ({review}) => {
    const { OnSetUser } = useContext(GlobalContext);

    return(
        <div className='profile-review'>
            <LazyLoadImage
            onClick={async (e) => {
                e.preventDefault();
                const result = await GetUserById(m.creatorId);
                OnSetUser(result);
                }} 
            src={review.creator.profilePictureLink}/>
            <p>{review.rating}</p>
            <p>{review.text}</p>
        </div>
    )
}