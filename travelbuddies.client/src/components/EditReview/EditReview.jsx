import { Rating } from '@mui/material'
import { OnUpdateReviewSubmit } from '../../services/ReviewService'
import { GetUserById } from '../../services/UserService'
import { useForm } from '../../utils/hooks/useForm'
import './EditReview.css'
import { useState } from 'react'

const EditReviewFromKeys = {
    Id: 'id',
    Text: 'text',
    Rating: 'rating',
    ReciverId: 'reciverId',
}

export const EditReview = ({review, userId, setUser}) => {
    const [stars, setStars] = useState(review.rating);

    const {values, changeHandler, onSubmit} = useForm({
        [EditReviewFromKeys.Id]: review.id,
        [EditReviewFromKeys.Text]: review.text,
        [EditReviewFromKeys.Rating]: review.rating,
        [EditReviewFromKeys.ReciverId]: userId,
    }, OnUpdateReviewSubmit)

    const onChangeStar = (event, stars) => {
        setStars(stars);
    }

    const OnEditSubmit = async (id) => {
        window.document.getElementById(`review-${review.id}`).style.display = 'none';

        values[EditReviewFromKeys.ReciverId] = userId;
        values[EditReviewFromKeys.Rating] = stars;

        await OnUpdateReviewSubmit(values);

        const data = await GetUserById(id);

        setUser(data);
    }

    return(
        <div id={`review-${review.id}`} className="edit-review-main">
            <form className='review-form' onSubmit={(e) => {
                e.preventDefault();
                OnEditSubmit(userId);
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