import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { OnCreateReviewSubmit } from "../../services/ReviewService"
import { useForm } from "../../utils/hooks/useForm"
import { StarSelector } from "../StarSelector/StarSelector"
import './CreateReview.css'
import { faPlus } from "@fortawesome/free-solid-svg-icons"
import { GetUserById } from "../../services/UserService"
import { useContext } from "react"
import { GlobalContext } from "../../utils/contexts/GlobalContext"

const ReviewFromKeys = {
    Text: 'text',
    Rating: 'rating',
    ReciverId: 'reciverId',
}

export const CreateReview = ({user}) => {
    const { OnSetUser } = useContext(GlobalContext);

    const {values, changeHandler, onSubmit} = useForm({
        [ReviewFromKeys.Text]: '',
        [ReviewFromKeys.Rating]: 0,
        [ReviewFromKeys.ReciverId]: user.id,
    }, OnCreateReviewSubmit)

    const onChangeStar = (star) => {
        values[ReviewFromKeys.Rating] = star;
        changeHandler;
    }

    const OnCreateSubmit = async (e) => {
        e.preventDefault();
        const result = await GetUserById(user.id);
        OnSetUser(result);
    }

    return(
        <div className="create-review-main">
            <form className='review-form' onSubmit={onSubmit}>
                <StarSelector totalStars={5} onSelect={onChangeStar}/>
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