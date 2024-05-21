import { OnUpdateReviewSubmit } from '../../services/ReviewService'
import { useForm } from '../../utils/hooks/useForm'
import { StarSelector } from '../StarSelector/StarSelector'
import './EditReview.css'
import { useNavigate } from 'react-router-dom'

const EditReviewFromKeys = {
    Id: 'id',
    Text: 'text',
    Rating: 'rating',
    ReciverId: 'reciverId',
}

export const EditReview = ({review, userId}) => {
    const navigate = useNavigate();

    const {values, changeHandler, onSubmit} = useForm({
        [EditReviewFromKeys.Id]: review.id,
        [EditReviewFromKeys.Text]: review.text,
        [EditReviewFromKeys.Rating]: review.rating,
        [EditReviewFromKeys.ReciverId]: userId,
    }, OnUpdateReviewSubmit)

    const onChangeStar = (star) => {
        values[EditReviewFromKeys.Rating] = star;
        changeHandler;
    }

    const OnEditSubmit = async (id) => {
        window.document.getElementById(`review-${review.id}`).style.display = 'none';

        await OnUpdateReviewSubmit(values);

        window.location.reload();
        navigate(`/profile/${id}`)
    }

    return(
        <div id={`review-${review.id}`} className="edit-review-main">
            <form className='review-form' onSubmit={(e) => {
                e.preventDefault();
                OnEditSubmit(userId);
            }}>
                <StarSelector totalStars={5} onSelect={onChangeStar}/>
                <input 
                    type='text'
                    placeholder='Your review here'
                    name={[EditReviewFromKeys.Text]}
                    value={values[EditReviewFromKeys.Text]}
                    onChange={changeHandler}
                />
                <button>Edit</button>
            </form>
            <span className='close'
            onClick={() => {
                window.document.getElementById(`review-${review.id}`).style.display = 'none';
            }}
            >&times;</span>
        </div>
    )
}