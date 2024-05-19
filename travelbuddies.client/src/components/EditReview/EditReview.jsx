import { useContext } from 'react'
import { OnUpdateReviewSubmit } from '../../services/ReviewService'
import { GetUserById } from '../../services/UserService'
import { useForm } from '../../utils/hooks/useForm'
import { StarSelector } from '../StarSelector/StarSelector'
import './EditReview.css'
import { GlobalContext } from '../../utils/contexts/GlobalContext'

const EditReviewFromKeys = {
    Id: 'id',
    Text: 'text',
    Rating: 'rating',
    ReciverId: 'reciverId',
}

export const EditReview = ({review, userId}) => {
    const { OnSetUser } = useContext(GlobalContext);

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

    const LoadProfile = async (id) => {
        const result = await GetUserById(id);
        OnSetUser(result);
    }

    const OnEditSubmit = async (id) => {
        window.document.getElementById(`review-${review.id}`).style.display = 'none';

        await OnUpdateReviewSubmit(values);

        await LoadProfile(id);
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
                <button>Submit</button>
            </form>
            <span className='close'
            onClick={() => {
                window.document.getElementById(`review-${review.id}`).style.display = 'none';
            }}
            >&times;</span>
        </div>
    )
}