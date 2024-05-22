import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { OnCreateReviewSubmit } from "../../services/ReviewService"
import { useForm } from "../../utils/hooks/useForm"
import './CreateReview.css'
import { faPlus } from "@fortawesome/free-solid-svg-icons"
import { useState } from "react"
import { useNavigate } from "react-router-dom"
import { OnCreateMessageSubmit } from "../../services/MessageService"
import { Rating } from "@mui/material"
import { GetUserById } from "../../services/UserService"

const ReviewFromKeys = {
    Text: 'text',
    Rating: 'rating',
    ReciverId: 'reciverId',
}

export const CreateReview = ({user, setUser}) => {
    const navigate = useNavigate();
    const [stars, setStars] = useState(1);

    const {values, changeHandler, onSubmit} = useForm({
        [ReviewFromKeys.Text]: '',
        [ReviewFromKeys.Rating]: 0,
        [ReviewFromKeys.ReciverId]: user.id,
    }, OnCreateReviewSubmit)

    const onChangeStar = (event, stars) => {
        setStars(stars);
    }

    const OnCreateSubmit = async (e) => {
        e.preventDefault();

        values[ReviewFromKeys.ReciverId] = user.id;
        values[ReviewFromKeys.Rating] = stars;
        
        await OnCreateReviewSubmit(values);

        values[ReviewFromKeys.Rating] = 1;
        values[ReviewFromKeys.Text] = '';
        setStars(1);
    
        const result = await GetUserById(user.id);

        setUser(result);
    }

    return(
        <div className="create-review-main">
            <form className='review-form' onSubmit={OnCreateSubmit}>
                {/* <StarSelector onSelect={onChangeStar}/> */}
                <Rating 
                    name="size-medium"
                    value={stars}
                    onChange={onChangeStar}
                    />
                <input 
                    type='text'
                    placeholder='Your review here'
                    name={[ReviewFromKeys.Text]}
                    value={values[ReviewFromKeys.Text]}
                    onChange={changeHandler}
                />
                <button><FontAwesomeIcon icon={faPlus}/> Add a review</button>
            </form>
        </div>
    )
}