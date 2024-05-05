import { Review } from '../../components/Review/Review';
import './Reviews.css';

export const Reviews = ({reviews}) => {

    return(
        <div className='reviews-main'>
            <div className='reviews-content'>
                {reviews.length > 0 ? (
                    reviews.map((r) => (
                        <Review review={r}/>
                    ))): (
                    <p>No reviews</p>
                )}
            </div>
        </div>
    )
}