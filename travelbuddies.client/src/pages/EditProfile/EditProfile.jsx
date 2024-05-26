import "./EditProfile.css";
import { useEffect, useState } from "react";
import { Loading } from "../Loading/Loading";
import {
  GetOnlyUserById,
  OnUpdateProfileSubmit,
} from "../../services/UserService";
import { Fab, TextField } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import { EditProfilePicture } from "../../components/EditProfilePicture/EditProfilePicture";
import { useForm } from "../../utils/hooks/useForm";

const EditProfileFromKeys = {
  FirstName: "firstName",
  LastName: "lastName",
  Email: "email",
  City: "city",
  Country: "country",
};

export const EditProfile = () => {
  const [user, setUser] = useState({});
  const [loading, setLoading] = useState(true);

  const { values, changeHandler, onSubmit } = useForm(
    {
      [EditProfileFromKeys.FirstName]: user.firstName,
      [EditProfileFromKeys.LastName]: user.lastName,
      [EditProfileFromKeys.Email]: user.email,
      [EditProfileFromKeys.City]: user.city,
      [EditProfileFromKeys.Country]: user.country,
    },
    OnUpdateProfileSubmit
  );

  useEffect(() => {
    const fetchData = async () => {
      const data = await GetOnlyUserById(localStorage.userId);
      setUser(data);

      values[EditProfileFromKeys.FirstName] = data.firstName;
      values[EditProfileFromKeys.LastName] = data.lastName;
      values[EditProfileFromKeys.Email] = data.email;
      values[EditProfileFromKeys.City] = data.city;
      values[EditProfileFromKeys.Country] = data.country;

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
        <div className="edit-profile-img">
          <Fab
            onClick={() => {
              window.document.getElementById(
                "edit-profile-picture"
              ).style.display = "flex";
            }}
            color="primary"
            aria-label="edit"
          >
            <EditIcon />
          </Fab>
        </div>
        <EditProfilePicture />
        <form id="edit-profile" onSubmit={onSubmit}>
          <div className="edit-profile-info">
            <TextField
              type="text"
              name={EditProfileFromKeys.FirstName}
              value={values[EditProfileFromKeys.FirstName]}
              onChange={changeHandler}
              label="FirstName"
              autoComplete="off"
              sx={{
                width: "14vw",
              }}
              required
            />
             <TextField
              type="text"
              name={EditProfileFromKeys.LastName}
              value={values[EditProfileFromKeys.LastName]}
              onChange={changeHandler}
              label="LastName"
              autoComplete="off"
              sx={{
                width: "14vw",
              }}
              required
            />
             <TextField
              type="text"
              name={EditProfileFromKeys.Email}
              value={values[EditProfileFromKeys.Email]}
              onChange={changeHandler}
              label="Email"
              autoComplete="off"
              sx={{
                width: "14vw",
              }}
              disabled
              required
            />
             <TextField
              type="text"
              name={EditProfileFromKeys.City}
              value={values[EditProfileFromKeys.City]}
              onChange={changeHandler}
              label="City"
              autoComplete="off"
              sx={{
                width: "14vw",
              }}
              required
            />
             <TextField
              type="text"
              name={EditProfileFromKeys.Country}
              value={values[EditProfileFromKeys.Country]}
              onChange={changeHandler}
              label="Country"
              autoComplete="off"
              sx={{
                width: "14vw",
              }}
              required
            />
          </div>
          <button className="edit-profile-btn">Save</button>
        </form>
      </div>
    </div>
  );
};
