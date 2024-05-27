import { useEffect, useState } from "react";
import { Review } from "../../components/Review/Review";
import "./Reviews.css";
import { GetAllReviewByReciverId } from "../../services/ReviewService";
import { useParams } from "react-router-dom";
import { Loading } from "../Loading/Loading";

import backgroundImg from '../../utils/images/white-background-with-blue-geometric-and-white-line-pattern-free-vector.jpg'
import { useForm } from "../../utils/hooks/useForm";
import { Pagination } from "@mui/material";

const ReviewsQueryFromKeys = {
    Id: 'id',
    Page: 'page',
    Count: 'count'
}

export const Reviews = () => {
  const { id } = useParams();
  const [reviews, setReviews] = useState([]);
  const [loading, setLoading] = useState(true);

    const {values, changeHandler, onSubmit} = useForm({
        [ReviewsQueryFromKeys.Id]: '',
        [ReviewsQueryFromKeys.Page]: '1',
        [ReviewsQueryFromKeys.Count]: '10'
    });

  useEffect(() => {
    const fetchData = async () => {
      const data = await GetAllReviewByReciverId(id);
      setReviews(data);

      setLoading(false);
    };
    fetchData();
  }, []);

  if (loading) {
    return <Loading />;
  }

  return (
    <div className="reviews-main">
      <img className="demo-bg" src={backgroundImg} />
      <div className="reviews-content">
        <div className="reviews-content-header">
          <h2>Reviews</h2>
        </div>
        <div className="reviews">
          {reviews.length > 0 ? (
            reviews.map((r) => <Review key={r.id} review={r} />)
          ) : (
            <p>No reviews</p>
          )}
          <Pagination count={10} color="primary" />
        </div>
      </div>
    </div>
  );
};
