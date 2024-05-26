import "./Review.css";
import { LazyLoadImage } from "react-lazy-load-image-component";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencil, faX } from "@fortawesome/free-solid-svg-icons";
import { OnDeleteReviewSubmit } from "../../services/ReviewService";
import { useNavigate, useParams } from "react-router-dom";
import { GetUserById } from "../../services/UserService";
import { Fab, IconButton, Tooltip } from "@mui/material";
import { DeleteForever, DeleteForeverOutlined } from "@mui/icons-material";
import EditIcon from "@mui/icons-material/Edit";

import personImgOffline from "../../utils/images/blank-profile-picture-973460_960_720.png";
import { useContext } from "react";
import { GlobalContext } from "../../utils/contexts/GlobalContext";

export const Review = ({ review }) => {
  const { OnSetUser } = useContext(GlobalContext);
  const navigate = useNavigate();
  const { id } = useParams();

  const LoadProfile = async (e) => {
    // if(id != userId){
    //     e.preventDefault();
    //     const data = await GetUserById(review.creator.id);
    //     setUser(data);
    // }

    OnSetUser(review.creator.id);

    navigate(`/profile/${review.creator.id}`);
  };

  const ConfirmDelete = async (reviewId) => {
    const text = `Are you sure you want to delete this review:\n ${review.text}`;
    if (confirm(text) == true) {
      await OnDeleteReviewSubmit(reviewId);

      // const data = await GetUserById(userId);

      // setUser(data);

      OnSetUser(id);
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
              // <div className='button-container'>
              //     <button
              //         onClick={() => {
              //             window.document.getElementById(`review-${review.id}`).style.display = 'flex';
              //         }}
              //         className='review-button'>
              //         <FontAwesomeIcon icon={faPencil}/>
              //     </button>
              //     <span className='button-info'>Edit</span>
              // </div>
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
              {/* <button
                                onClick={(e) => {
                                    e.preventDefault()
                                    ConfirmDelete(e, review.id);
                                }}
                                className='delete-review-button'>
                                <FontAwesomeIcon icon={faX}/>
                            </button> */}
              {/* <IconButton
                onClick={(e) => {
                  e.preventDefault();
                  ConfirmDelete(e, review.id);
                }}
                aria-label="delete"
              >
                <DeleteForever sx={{ color: "red" }} />
              </IconButton> */}
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
              {/* <span className="button-info">Delete</span> */}
            </div>
          </div>
        )}
      </div>
    </div>
  );
};
