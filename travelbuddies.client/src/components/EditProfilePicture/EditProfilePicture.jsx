import { useState } from "react";
import "./EditProfilePicture.css";
import { useForm } from "../../utils/hooks/useForm";
import {
  GetOnlyUserById,
  OnUpdateProfilePicutreSubmit,
} from "../../services/UserService";

const EditProfilePictureFromKeys = {
  ProfilePicture: "profilePicture",
};

export const EditProfilePicture = ({ setUser }) => {
  const [nameFile, setNameFile] = useState("");

  const { values, changeHandler, onSubmit } = useForm(
    {
      [EditProfilePictureFromKeys.ProfilePicture]: null,
    },
    OnUpdateProfilePicutreSubmit
  );

  const OnUpdateSubmit = async (e) => {
    e.preventDefault();

    await OnUpdateProfilePicutreSubmit();

    values[EditProfilePictureFromKeys.ProfilePicture] = null;

    const data = await GetOnlyUserById(localStorage.userId);
    setNameFile('');

    setUser(data);

    window.document.getElementById(`edit-profile-picture`).style.display =
      "none";
  };

  const onChangeFile = (e) => {
    changeHandler(e);

    const path = e.target.value.split("\\");
    const name = path[path.length - 1];

    setNameFile(name);
  };

  return (
    <div id="edit-profile-picture" className="edit-profile-picture-main">
      <form onSubmit={OnUpdateSubmit}>
        <div className="profile-picture-upload">
          <label>
            Upload New Profile Picture
            <input
              type="file"
              id="profilepicture"
              name={EditProfilePictureFromKeys.ProfilePicture}
              value={values[EditProfilePictureFromKeys.ProfilePicture]}
              onChange={onChangeFile}
              required
              hidden
            />
          </label>
          <span>{nameFile}</span>
        </div>
        <button className="edit-profile-picture-btn">Update</button>
      </form>
      <span
        className="close"
        onClick={() => {
          window.document.getElementById(`edit-profile-picture`).style.display =
            "none";
        }}
      >
        &times;
      </span>
    </div>
  );
};
