import { Rating } from '@mui/material'
import { OnUpdateReviewSubmit } from '../../services/ReviewService'
import { GetUserById } from '../../services/UserService'
import { useForm } from '../../utils/hooks/useForm'
import './EditReview.css'
import { useContext, useState } from 'react'
import { GlobalContext } from '../../utils/contexts/GlobalContext'
import { useParams } from 'react-router-dom'

const EditReviewFromKeys = {
    Id: 'id',
    Text: 'text',
    Rating: 'rating',
    ReciverId: 'reciverId',
}

export const EditReview = ({review}) => {
    const { OnSetUser } = useContext(GlobalContext);
    const { id } = useParams();
    const [stars, setStars] = useState(review.rating);

    const {values, changeHandler, onSubmit} = useForm({
        [EditReviewFromKeys.Id]: review.id,
        [EditReviewFromKeys.Text]: review.text,
        [EditReviewFromKeys.Rating]: review.rating,
        [EditReviewFromKeys.ReciverId]: id,
    }, OnUpdateReviewSubmit)

    const onChangeStar = (event, stars) => {
        setStars(stars);
    }

    const OnEditSubmit = async () => {
        window.document.getElementById(`review-${review.id}`).style.display = 'none';

        values[EditReviewFromKeys.ReciverId] = id;
        values[EditReviewFromKeys.Rating] = stars;

        await OnUpdateReviewSubmit(values);

        OnSetUser(id);
    }

    return(
        <div id={`review-${review.id}`} className="edit-review-main">
            <form className='review-form' onSubmit={(e) => {
                e.preventDefault();
                OnEditSubmit();
            }}>
                {/* <StarSelector totalStars={5} onSelect={onChangeStar}/> */}
                <Rating 
                    name="size-medium"
                    value={stars}
                    onChange={onChangeStar}
                    />
                <input 
                    type='text'
                    placeholder='Your review here'
                    name={[EditReviewFromKeys.Text]}
                    value={values[EditReviewFromKeys.Text]}
                    onChange={changeHandler}
                />
                <button>Save</button>
            </form>
            <span className='close'
            onClick={() => {
                window.document.getElementById(`review-${review.id}`).style.display = 'none';
            }}
            >&times;</span>
        </div>
    )
}