import "./EditProfile.css";
import { useEffect, useState } from "react";
import { Loading } from "../Loading/Loading";
import {
  GetOnlyUserById,
  OnUpdateProfileSubmit,
} from "../../services/UserService";
import { Box, Button, Fab, TextField } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import { EditProfilePicture } from "../../components/EditProfilePicture/EditProfilePicture";
import { useForm } from "../../utils/hooks/useForm";
import profileImg from "../../utils/images/blank-profile-picture-973460_960_720.png";
import { Form, Formik } from "formik";
import { FormikTextField } from "../../components/FormikTextField/FormikTextField";
import * as Yup from "yup";
import { useNavigate } from "react-router-dom";

const EditProfileFromKeys = {
  FirstName: "firstName",
  LastName: "lastName",
  Email: "email",
  City: "city",
  Country: "country",
};

const SignupSchema = Yup.object().shape({
  firstname: Yup.string()
    .min(3, "To Short!")
    .max(50, "To Long!")
    .required("Required"),
  lastname: Yup.string()
    .min(3, "To Short!")
    .max(50, "To Long!")
    .required("Required"),
  city: Yup.string().min(1, "To Short!").max(100, "To Long!"),
  country: Yup.string().min(1, "To Short!").max(100, "To Long!"),
});

export const EditProfile = () => {
  const [user, setUser] = useState({});
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

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

  const clickSubmit = async (values) => {
    // if (values[RegisterFromKeys.Password] != repass) {
    //   return;
    // }
    //e.preventDefault();

    const result = await OnUpdateProfileSubmit(values);

    if (result) {
      alert(result);
    } else {
      navigate(`/profile/${localStorage.userId}`);
    }
  };

  if (loading) {
    return <Loading />;
  }

  return (
    <div className="edit-profile-main">
      <div className="edit-profile-header">
        <h2>Edit Profile</h2>
      </div>
      <div className="edit-profile-content">
        <div
          style={{
            backgroundImage: `url(${
              localStorage.profilePictureLink != "undefined"
                ? localStorage.profilePictureLink
                : profileImg
            })`,
          }}
          className="edit-profile-img"
        >
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
        <EditProfilePicture setUser={setUser} />
        <Formik
          initialValues={{
            firstname: user.firstName,
            lastname: user.lastName,
            city: user.city,
            country: user.country,
          }}
          validationSchema={SignupSchema}
          onSubmit={(values) => {
            //   if (values.image) {
            //     const reader = new FileReader();
            //     reader.onloadend = () => {
            //       setPreview(reader.result);
            //     };
            //     reader.readAsDataURL(values.image);
            //   }
            clickSubmit(values);
          }}
        >
          {({ isSubmitting }) => (
            <Form>
              <Box
                display="flex"
                justifyContent="center"
                columnGap="40px"
                rowGap="10px"
                flexWrap="wrap"
                flexDirection="row"
                alignItems="center"
              >
                <FormikTextField
                  name="firstname"
                  type="text"
                  label="FirstName"
                  isRequired={true}
                />
                <FormikTextField
                  name="lastname"
                  type="text"
                  label="LastName"
                  isRequired={true}
                />
                <FormikTextField
                  name="country"
                  type="text"
                  label="Country"
                  isRequired={false}
                />
                <FormikTextField
                  name="city"
                  type="text"
                  label="City"
                  isRequired={false}
                />
              </Box>
              <Button
                type="submit"
                variant="contained"
                color="primary"
                disabled={isSubmitting}
                sx={{ mt: 2 }}
              >
                Save
              </Button>
            </Form>
          )}
        </Formik>
      </div>
    </div>
  );
};
