import "./EditProfile.css";
import { useContext, useEffect, useState } from "react";
import { Loading } from "../Loading/Loading";
import {
  GetOnlyUserById,
  OnUpdateProfileSubmit,
} from "../../services/UserService";
import { Box, Button, Fab, TextField } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import { EditProfilePicture } from "../../components/EditProfilePicture/EditProfilePicture";
import profileImg from "../../utils/images/blank-profile-picture-973460_960_720.png";
import { Form, Formik } from "formik";
import { FormikTextField } from "../../components/FormikTextField/FormikTextField";
import * as Yup from "yup";
import { useNavigate } from "react-router-dom";

import backgroundImg from "../../utils/images/white-background-with-blue-geometric-and-white-line-pattern-free-vector.jpg";
import { GlobalContext } from "../../utils/contexts/GlobalContext";


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
  const { OnSetUser } = useContext(GlobalContext);
  const [user, setUser] = useState({});
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      const data = await GetOnlyUserById(localStorage.userId);
      setUser(data);

      setLoading(false);
    };
    fetchData();
  }, []);

  const clickSubmit = async (values) => {
    const result = await OnUpdateProfileSubmit(values);

    if (result) {
      alert(result);
    } else {
      OnSetUser(localStorage.userId);
      navigate(`/profile/${localStorage.userId}`);
    }
  };

  if (loading) {
    return <Loading />;
  }

  return (
    <div className="edit-profile-main">
      <img className="demo-bg" src={backgroundImg} />
      <div className="edit-profile-container">
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
    </div>
  );
};
