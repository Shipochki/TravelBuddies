import { useState } from "react";
import { OnRegisterSubmit } from "../../services/UserService";
import "./Register.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faCarSide
} from "@fortawesome/free-solid-svg-icons";
import { useNavigate } from "react-router-dom";
import * as Yup from "yup";
import {
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  IconButton,
} from "@mui/material";
import { Form, Formik } from "formik";
import { FormikTextField } from "../../components/FormikTextField/FormikTextField";
import { FormikFileInput } from "../../components/FormikFileInput/FormikFileInput";
import { styled } from "@mui/material/styles";
import CloseIcon from "@mui/icons-material/Close";

const SignupSchema = Yup.object().shape({
  email: Yup.string()
    .email("Invalid email address")
    .min(3, "To Short!")
    .max(345, "To Long!")
    .required("Required"),
  password: Yup.string()
    .min(8, "To Short!")
    .max(16, "To Long!")
    .matches(/[a-z]/, "Must contain at least one lowercase letter")
    .matches(/[A-Z]/, "Must contain at least one uppercase letter")
    .matches(/\d/, "Must contain at least one number")
    .matches(/[@$!%*?&]/, "Must contain at least one special character")
    .required("Required"),
  repassword: Yup.string()
    .oneOf([Yup.ref("password"), null], "Passwords must match")
    .required("Required"),
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
  image: Yup.mixed()
    .required("A file is required")
    .test(
      "fileSize",
      "File too large",
      (value) => value && value.size <= 1024 * 1024
    ) // 1MB
    .test(
      "fileType",
      "Unsupported file format",
      (value) =>
        value && ["image/jpeg", "image/png", "image/gif"].includes(value.type)
    ),
});

const BootstrapDialog = styled(Dialog)(({ theme }) => ({
  "& .MuiDialogContent-root": {
    padding: theme.spacing(2),
  },
  "& .MuiDialogActions-root": {
    padding: theme.spacing(1),
  },
  "& .MuiBox-root": {
    //padding: "10% 30%"
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
  },
  "& .css-f5zjvo": {
    width: "400px",
  },
}));

export const Regiser = () => {
  const navigate = useNavigate();
  const [error, setError] = useState("");

  const clickSubmit = async (values) => {
    const result = await OnRegisterSubmit(values);

    if (result) {
      alert(result);
      setError(result);
    } else {
      navigate("/login");
    }
  };
  const [open, setOpen] = useState(false);

  const handleClickOpen = () => {
    setOpen(true);
  };
  const handleClose = () => {
    setOpen(false);
  };

  return (
    <div className="register-main">
      <div className="register-background-content">
        <div className="register-welcome">
          <FontAwesomeIcon icon={faCarSide} />
          <h2>Welcome</h2>
        </div>
        <div className="register-content">
          <h2>Register</h2>
          <Formik
            initialValues={{
              email: "",
              password: "",
              repassword: "",
              firstname: "",
              lastname: "",
              city: "",
              country: "",
              image: null,
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
                  <FormikTextField
                    name="email"
                    type="email"
                    label="Email"
                    isRequired={true}
                  />
                  <FormikTextField
                    name="password"
                    type="password"
                    label="Password"
                    isRequired={true}
                  />
                  <FormikTextField
                    name="repassword"
                    type="password"
                    label="Confirm Password"
                    isRequired={true}
                  />
                  <>
                    <Button
                      sx={{ minWidth: "182px" }}
                      variant="outlined"
                      onClick={handleClickOpen}
                    >
                      Upload picture
                    </Button>
                    <BootstrapDialog
                      onClose={handleClose}
                      aria-labelledby="customized-dialog-title"
                      open={open}
                    >
                      <DialogTitle
                        sx={{ m: 0, p: 2 }}
                        id="customized-dialog-title"
                      >
                        Your picture
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
                        <FormikFileInput name="image" label="Upload Image" />
                      </DialogContent>
                      <DialogActions>
                        <Button autoFocus onClick={handleClose}>
                          Save
                        </Button>
                      </DialogActions>
                    </BootstrapDialog>
                  </>
                </Box>
                <Button
                  type="submit"
                  variant="contained"
                  color="primary"
                  disabled={isSubmitting}
                  sx={{ 
                    mt: 2, 
                    width: "90px",
                    height: "40px",
                    fontSize: "medium"
                  }}
                >
                  Submit
                </Button>
              </Form>
            )}
          </Formik>
        </div>
      </div>
    </div>
  );
};
