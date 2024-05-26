import "./EditProfile.css";
import { useEffect, useState } from "react";
import { Loading } from "../Loading/Loading";
import { GetUserById } from "../../services/UserService";
import personImgOffline from "../../utils/images/blank-profile-picture-973460_960_720.png";
import { Fab } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import { LazyLoadImage } from "react-lazy-load-image-component";

export const EditProfile = () => {
  const [user, setUser] = useState({});
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      const data = await GetUserById(localStorage.userId);
      setUser(data);
      setLoading(false);
    };
    fetchData();
  }, []);

  if (loading) {
    return <Loading />;
  }

  return (
    <div className="edit-profile-main">
      <div className="edit-profile-header">
        <h2>Edit Profile</h2>
      </div>
      <div className="edit-profile-content">
        <form id="edit-profile">
          <div className="edit-profile-img">
            <Fab color="primary" aria-label="edit">
              <EditIcon />
            </Fab>
          </div>
          <div className="edit-profile-info"></div>
        </form>
      </div>
    </div>
  );
};
