import {
  faArrowRight,
  faCalendarDays,
  faCheck,
  faCircleXmark,
} from "@fortawesome/free-solid-svg-icons";
import "./Post.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { OnJoinGroupSubmit } from "../../services/UserGroupService";
import { LazyLoadImage } from "react-lazy-load-image-component";
import { Link, useNavigate } from "react-router-dom";

import personImgOffline from "../../utils/images/blank-profile-picture-973460_960_720.png";
import { useContext, useState } from "react";
import { GlobalContext } from "../../utils/contexts/GlobalContext";

import cashImg from "../../utils/images/8993556.png";
import cardImg from "../../utils/images/payment-card-icon-vector-21079946.jpg";
import difPayType from "../../utils/images/cash-vs-credit-ca.png";
import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  IconButton,
  Typography,
  styled,
} from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";

const BootstrapDialog = styled(Dialog)(({ theme }) => ({
  "& .MuiDialogContent-root": {
    padding: theme.spacing(2),
  },
  "& .MuiDialogActions-root": {
    padding: theme.spacing(1),
  },
}));

const PayTypes = {
  0: "Cash",
  1: "Card",
  2: "Cash and Card",
};

const PayTypesImgs = {
  0: cashImg,
  1: cardImg,
  2: difPayType,
};

export const Post = ({ post }) => {
  const { OnSetGroups } = useContext(GlobalContext);
  const navigate = useNavigate();

  const OnJoinSubmit = async (e) => {
    e.preventDefault();

    await OnJoinGroupSubmit(post.GroupId);

    OnSetGroups();
    navigate(`/group/${post.GroupId}`);
  };

  const IsAllreadyInGroupAlert = () => {
    alert(`It seems you're already a member of the group.`);
  };

  const IsBannedFromGroup = () => {
    alert(
      `Apologies, but due to a prior issue, you're unable to participate in the group at this time.`
    );
  };

  const [open, setOpen] = useState(false);

  const handleClickOpen = () => {
    setOpen(true);
  };
  const handleClose = () => {
    setOpen(false);
  };

  return (
    <div className="post-component">
      <div className="post-creator">
        <Link to={`/profile/${post.Creator.Id}`}>
          <LazyLoadImage
            src={
              post.Creator.ProfilePictureLink
                ? Creator.ProfilePictureLink
                : personImgOffline
            }
          />
        </Link>
        <p>{post.Creator.FullName}</p>
      </div>
      <div className="post-cities">
        <p>{post.FromDestinationName}</p>
        <FontAwesomeIcon icon={faArrowRight} />
        <p>{post.ToDestinationName}</p>
      </div>
      <p className="post-dateandtime">
        <FontAwesomeIcon icon={faCalendarDays} /> {post.DateAndTime}
      </p>
      <>
        <Button variant="outlined" onClick={handleClickOpen}>
          Read Description
        </Button>
        <BootstrapDialog
          onClose={handleClose}
          aria-labelledby="customized-dialog-title"
          open={open}
        >
          <DialogTitle sx={{ m: 0, p: 2 }} id="customized-dialog-title">
            Description
          </DialogTitle>
          <IconButton
            aria-label="close"
            onClick={handleClose}
            sx={{
              position: "absolute",
              right: 8,
              top: 8,
              color: (theme) => theme.palette.grey[500],
            }}
          >
            <CloseIcon />
          </IconButton>
          <DialogContent dividers>
            <Typography gutterBottom>{post.Description}</Typography>
          </DialogContent>
          <DialogActions>
            <Button autoFocus onClick={handleClose}>
              Close
            </Button>
          </DialogActions>
        </BootstrapDialog>
      </>
      <div className="post-paymentType-content">
        <div className="post-prices">
          <p className="post-paymentType">
            Payment type: <span>{PayTypes[post.PaymentType]}</span>
          </p>
        </div>
        <img
          className="post-paymentType-img"
          src={PayTypesImgs[post.PaymentType]}
          alt={PayTypes[post.PaymentType]}
        />
      </div>
      <div className="post-more-info">
        <p className="post-priceperseat">
          Price: <span>{post.PricePerSeat} {post.Currency}</span>
        </p>
        <p className="post-freeseats">Seats available: <span>{post.FreeSeats}</span></p>
        <div className="post-bool">
          <p>Is passenger allowed to bring <span>Baggage</span>:</p>
          <FontAwesomeIcon icon={post.Baggage ? faCheck : faCircleXmark} />
        </div>
        <div className="post-bool">
          <p>Is passenger allowed to bring <span>Pets</span>:</p>
          <FontAwesomeIcon icon={post.Pets ? faCheck : faCircleXmark} />
        </div>
      </div>
      <div className="join-button">
        {!post.BlackListsUsers.includes(localStorage.userId) ? (
          <button
            type="submit"
            onClick={(e) => {
              post.Participants.includes(localStorage.userId)
                ? IsAllreadyInGroupAlert()
                : OnJoinSubmit(e);
            }}
          >
            Join
          </button>
        ) : (
          <button
            onClick={() => {
              IsBannedFromGroup();
            }}
          >
            Join
          </button>
        )}
      </div>
    </div>
  );
};
