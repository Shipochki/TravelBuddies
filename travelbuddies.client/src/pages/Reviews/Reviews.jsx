import { useEffect, useState } from 'react';
import { Review } from '../../components/Review/Review';
import './Reviews.css';
import { GetAllReviewByReciverId } from '../../services/ReviewService';
import { useParams } from 'react-router-dom';
import { Loading } from '../Loading/Loading';

export const Reviews = () => {
    const {id} = useParams();
    const [reviews, setReviews] = useState([])
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchData = async () => {
            const data = await GetAllReviewByReciverId(id)
            setReviews(data);

            setLoading(false);
        };
        fetchData();
    }, []);

    if(loading){
        return <Loading/>
    }

    return(
        <div className='reviews-main'>
            <div className='reviews-content'>
                <div className='reviews-content-header'>
                    <h2>Reviews</h2>
                </div>
                <div className='reviews'>
                    {reviews.length > 0 ? (
                        reviews.map((r) => (
                            <Review key={r.id} review={r}/>
                        ))): (
                        <p>No reviews</p>
                    )}
                </div>
            </div>
        </div>
    )
}