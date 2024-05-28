import { useEffect, useState } from "react";
import { Review } from "../../components/Review/Review";
import "./Reviews.css";
import { GetAllReviewByReciverId } from "../../services/ReviewService";
import { useNavigate, useParams } from "react-router-dom";
import { Loading } from "../Loading/Loading";

import backgroundImg from "../../utils/images/white-background-with-blue-geometric-and-white-line-pattern-free-vector.jpg";
import { useForm } from "../../utils/hooks/useForm";
import { Pagination } from "@mui/material";
import { serializer } from "../../utils/common/serializer";

const ReviewsQueryFromKeys = {
  ReciverId: "reciverId",
  Page: "page",
  Count: "pageCount",
};

export const Reviews = () => {
  const navigate = useNavigate();
  const { id } = useParams();
  const [data, setReviews] = useState({});
  const [loading, setLoading] = useState(true);
  const [page, setPage] = useState(1);
  const [pageCount] = useState(10);
  const [isDeleted, setIsDeleted] = useState(false);

  const { values, changeHandler, onSubmit } = useForm({
    [ReviewsQueryFromKeys.ReciverId]: data.reciverId,
    [ReviewsQueryFromKeys.Page]: page,
    [ReviewsQueryFromKeys.Count]: pageCount,
  });

  useEffect(() => {
    const fetchData = async () => {
      const data = await GetAllReviewByReciverId();
      setReviews(data);

      values[ReviewsQueryFromKeys.ReciverId] = data.reciverId;

      setLoading(false);
    };
    fetchData();

    // if(isDeleted){
    //   fetchData();
    //   setIsDeleted(false);
    // }

    const handlePopState = () => {
      // When user navigates back, trigger data reload
      //fetchData();
      fetchData();
    };

    // Add event listener for browser navigation back
    window.addEventListener('popstate', handlePopState);

    // Cleanup: remove event listener when component unmounts
    return () => {
      window.removeEventListener('popstate', handlePopState);
    };
  }, []);

  const OnChangePage = async (event, value) => {
    setPage(value);
    values[ReviewsQueryFromKeys.Page] = value;
    changeHandler(event);

    navigate(`/reviews?${serializer(values)}`);

    const data = await GetAllReviewByReciverId();
    setReviews(data);

    values[ReviewsQueryFromKeys.ReciverId] = data.reciverId;
  };

  const OnSetData = async (id) => {
    const data = await GetAllReviewByReciverId();
    setReviews(data);
  }

  if (loading) {
    return <Loading />;
  }

  return (
    <div className="reviews-main">
      {/* <img className="demo-bg" src={backgroundImg} /> */}
      <div className="reviews-content">
        <div className="reviews-content-header">
          <h2>Reviews</h2>
        </div>
        <div className="reviews">
          {data.reviews.length > 0 ? (
            data.reviews.map((r) => <Review key={r.id} review={r} setData={OnSetData}/>)
          ) : (
            <p>No reviews</p>
          )}
          <Pagination
            onChange={OnChangePage}
            count={Math.ceil(data.countReviews / 10)}
            color="primary"
          />
        </div>
      </div>
    </div>
  );
};
