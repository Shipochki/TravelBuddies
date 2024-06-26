import "./Review.css";
import { LazyLoadImage } from "react-lazy-load-image-component";
import { OnDeleteReviewSubmit } from "../../services/ReviewService";
import { useNavigate, useParams } from "react-router-dom";
import { Fab, Tooltip } from "@mui/material";
import { DeleteForeverOutlined } from "@mui/icons-material";
import EditIcon from "@mui/icons-material/Edit";

import personImgOffline from "../../utils/images/blank-profile-picture-973460_960_720.png";
import { useContext } from "react";
import { GlobalContext } from "../../utils/contexts/GlobalContext";
import { EditReview } from "../EditReview/EditReview";

export const Review = ({ review, setData }) => {
  const { OnSetUser } = useContext(GlobalContext);
  const navigate = useNavigate();
  const { id } = useParams();

  const LoadProfile = async (e) => {
    OnSetUser(review.creator.id);

    navigate(`/profile/${review.creator.id}`);
  };

  const ConfirmDelete = async (reviewId) => {
    const text = `Are you sure you want to delete this review:\n ${review.text}`;
    if (confirm(text) == true) {
      await OnDeleteReviewSubmit(reviewId);

      setData(id);
    }
  };

  return (
    <div className="profile-review">
      <div className="review-creator-info">
        <LazyLoadImage
          onClick={LoadProfile}
          src={
            review.creator.profilePictureLink
              ? review.creator.profilePictureLink
              : personImgOffline
          }
        />
        <p>{review.creator.fullName}</p>
      </div>
      <div className="review-rating-createdon-info">
        <p className="review-rating">{review.rating}/5</p>
        <p className="review-createdon">{review.createdOn}</p>
      </div>
      <div className="review-text-editing">
        <p>{review.text}</p>
        {(review.creator.id == localStorage.userId ||
          localStorage.role == "admin") && (
          <div className="review-editing-buttons last-child">
            {review.creator.id == localStorage.userId && (
              <Tooltip title="Edit">
                <Fab
                sx={{
                    height: '40px',
                    width: '40px',
                }}
                  onClick={() => {
                    window.document.getElementById(
                      `review-${review.id}`
                    ).style.display = "flex";
                  }}
                  color="primary"
                  aria-label="edit"
                >
                  <EditIcon />
                </Fab>
              </Tooltip>
            )}
            <div className="button-container">
              <Tooltip title="Delete">
                <Fab
                sx={{
                    height: '40px',
                    width: '40px',
                }}
                onClick={(e) => {
                    e.preventDefault();
                    ConfirmDelete(review.id);
                }} color="error" aria-label="delete">
                  <DeleteForeverOutlined />
                </Fab>
              </Tooltip>
            </div>
          </div>
        )}
      </div>
      <EditReview key={`edit-review-key-${review.id}`} review={review}/>
    </div>
  );
};
