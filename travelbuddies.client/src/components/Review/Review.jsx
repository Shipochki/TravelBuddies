import './Review.css';
import { LazyLoadImage } from 'react-lazy-load-image-component';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPencil, faX } from '@fortawesome/free-solid-svg-icons';
import { OnDeleteReviewSubmit } from '../../services/ReviewService';
import { useNavigate } from 'react-router-dom';

export const Review = ({review}) => {
    const navigate = useNavigate();

    const LoadProfile = () => {
        window.location.reload();
        navigate(`/profile/${review.creator.id}`)
    }

    const ConfirmDelete = async (reviewId) => {
        const text = `Are you sure you want to delete this review:\n ${review.text}`
        if(confirm(text) == true){
            await OnDeleteReviewSubmit(reviewId);

             LoadProfile();
        }
    }

    return(
        <div className='profile-review'>
            <div className='review-creator-info'>
                <LazyLoadImage
                onClick={LoadProfile} 
                src={review.creator.profilePictureLink 
                ? review.creator.profilePictureLink 
                : 'https://sttravelbuddies001.blob.core.windows.net/web/blank-profile-picture-973460_960_720.png'}/> 
                <p>{review.creator.fullName}</p>
            </div>
            
            <div className='review-rating-createdon-info'>
                <p className='review-rating'>{review.rating}/5</p>
                <p className='review-createdon'>{review.createdOn}</p>
            </div>
            <div className='review-text-editing'>
                 <p>{(review.text)}</p>
                {(review.creator.id == localStorage.userId
                 || localStorage.role == 'admin') && (
                    <div className='review-editing-buttons last-child'>
                        {review.creator.id == localStorage.userId && (
                            <div className='button-container'>
                                <button 
                                    onClick={() => {
                                        window.document.getElementById(`review-${review.id}`).style.display = 'flex';
                                    }}
                                    className='review-button'>
                                    <FontAwesomeIcon icon={faPencil}/>
                                </button>
                                <span className='button-info'>Edit</span>
                            </div>
                        )}
                        <div className='button-container'>
                            <button
                                onClick={(e) => {
                                    e.preventDefault()
                                    ConfirmDelete(review.id);
                                }}
                                className='delete-review-button'>
                                <FontAwesomeIcon icon={faX}/>
                            </button>
                            <span className='button-info'>Delete</span>
                        </div>
                    </div>
                )}
            </div>
           
        </div>
    )
}