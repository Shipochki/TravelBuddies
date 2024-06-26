import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { OnCreateReviewSubmit } from "../../services/ReviewService"
import { useForm } from "../../utils/hooks/useForm"
import './CreateReview.css'
import { faPlus } from "@fortawesome/free-solid-svg-icons"
import { useContext, useState } from "react"
import { Rating } from "@mui/material"
import { GlobalContext } from "../../utils/contexts/GlobalContext"

const ReviewFromKeys = {
    Text: 'text',
    Rating: 'rating',
    ReciverId: 'reciverId',
}

export const CreateReview = ({user}) => {
    const { OnSetUser } = useContext(GlobalContext);
    const [stars, setStars] = useState(0);

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

        values[ReviewFromKeys.Rating] = 0;
        values[ReviewFromKeys.Text] = '';
        setStars(0);
    
        OnSetUser(user.id);
    }

    return(
        <div className="create-review-main">
            <form className='review-form' onSubmit={OnCreateSubmit}>
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
                    maxLength={2000}
                />
                <button
                disabled={(values[ReviewFromKeys.Text] == '' || stars == 0)}
                ><FontAwesomeIcon icon={faPlus}/> Add a review</button>
            </form>
        </div>
    )
}