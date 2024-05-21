import { Review } from '../../components/Review/Review';
import './Reviews.css';

export const Reviews = ({reviews}) => {

    return(
        <div className='reviews-main'>
            <div className='reviews-content'>
                <div className='reviews-content-header'>
                    <h2>My Reviews</h2>
                </div>
                {reviews.length > 0 ? (
                    reviews.map((r) => (
                        <Review key={r.id} review={r}/>
                    ))): (
                    <p>No reviews</p>
                )}
            </div>
        </div>
    )
}