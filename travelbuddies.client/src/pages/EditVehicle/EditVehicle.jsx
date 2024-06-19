import "./EditVehicle.css";
import { useEffect, useState } from "react";
import {
  GetVehicleByOwnerId,
  OnUpdateVehicleSubmit,
} from "../../services/VehicleService";
import { useForm } from "../../utils/hooks/useForm";
import { NoVehicle } from "../../components/NoVehicle/NoVehicle";
import {
  Box,
  Button,
  FormControl,
  InputLabel,
  MenuItem,
  NativeSelect,
  TextField,
} from "@mui/material";
import { Loading } from "../Loading/Loading";
import { NotDriver } from "../../components/NotDriver/NotDriver";
import backgroundImg from "../../utils/images/white-background-with-blue-geometric-and-white-line-pattern-free-vector.jpg";
import { Field, Form, Formik } from "formik";
import { FormikTextField } from "../../components/FormikTextField/FormikTextField";
import * as Yup from "yup";
import { FormikSwitch } from "../../components/FormikSwitch/FormikSwitch";
import { FormikSelect } from "../../components/FormikSelect/FormikSelect";
import { useNavigate } from "react-router-dom";

const SignupSchema = Yup.object().shape({
  brandName: Yup.string()
    .min(1, "To Short!")
    .max(50, "To Long!")
    .required("Required"),
  modelName: Yup.string()
    .min(1, "To Short!")
    .max(50, "To Long!")
    .required("Required"),
  color: Yup.string()
    .min(1, "To Short!")
    .max(30, "To Long!")
    .required("Required"),
  year: Yup.number().required("Required"),
  seatCount: Yup.number().required("Requried"),
  // image: Yup.mixed()
  //   .test(
  //     "fileSize",
  //     "File too large",
  //     (value) => value && value.size <= 1024 * 1024
  //   ) // 1MB
  //   .test(
  //     "fileType",
  //     "Unsupported file format",
  //     (value) =>
  //       value && ["image/jpeg", "image/png", "image/gif"].includes(value.type)
  //   ),
});

const EditVehicleFromKeys = {
  Id: "id",
  BrandName: "brandname",
  ModelName: "modelname",
  Year: "year",
  Color: "color",
  Fuel: "fuel",
  SeatCount: "seatcount",
  PictureLink: "picturelink",
  ACSystem: "acsystem",
};

const fuel = {
  Diesel: 0,
  Gasoline: 1,
  Electric: 2,
};

export const EditVehicle = () => {
  const [vehicle, setVehicle] = useState({});
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      if (localStorage.role == "driver") {
        const data = await GetVehicleByOwnerId(localStorage.userId);
        setVehicle(data);

        if (data) {
          values[EditVehicleFromKeys.Id] = data.id;
          values[EditVehicleFromKeys.BrandName] = data.brandName;
          values[EditVehicleFromKeys.ModelName] = data.modelName;
          values[EditVehicleFromKeys.Fuel] = fuel[data.fuel];
          values[EditVehicleFromKeys.SeatCount] = data.seatCount;
          values[EditVehicleFromKeys.ACSystem] = data.acSystem;
          values[EditVehicleFromKeys.Year] = data.year;
          values[EditVehicleFromKeys.Color] = data.color;
        }
      }

      setLoading(false);
    };
    fetchData();
  }, []);

  const { values, changeHandler, onSubmit } = useForm(
    {
      [EditVehicleFromKeys.Id]: "",
      [EditVehicleFromKeys.BrandName]: "",
      [EditVehicleFromKeys.ModelName]: "",
      [EditVehicleFromKeys.Fuel]: 0,
      [EditVehicleFromKeys.SeatCount]: 0,
      [EditVehicleFromKeys.PictureLink]: null,
      [EditVehicleFromKeys.ACSystem]: false,
      [EditVehicleFromKeys.Year]: 0,
      [EditVehicleFromKeys.Color]: "",
    },
    OnUpdateVehicleSubmit
  );

  const clickSubmit = async (values) => {
    // if (values[RegisterFromKeys.Password] != repass) {
    //   return;
    // }
    //e.preventDefault();

    const result = await OnUpdateVehicleSubmit(values);

    if (result) {
      navigate(`/myVehicle`);
    } else {
      alert(result);
    }
  };

  if (loading) {
    return <Loading />;
  }

  return (
    <div className="create-vehicle-main">
      {localStorage.role == "driver" ? (
        <>
          <img className="demo-bg" src={backgroundImg} />
          {vehicle ? (
            <div className="create-vehicle-content">
              <div className="create-vehicle-header">
                <h2>Edit your Vehicle</h2>
              </div>
              <Formik
                initialValues={{
                  id: vehicle.id,
                  brandName: vehicle.brandName,
                  modelName: vehicle.modelName,
                  year: vehicle.year,
                  color: vehicle.color,
                  fuel: fuel[vehicle.fuel],
                  seatCount: vehicle.seatCount,
                  acSystem: vehicle.acSystem,
                  image: null,
                }}
                validationSchema={SignupSchema}
                onSubmit={(values) => {
                  clickSubmit(values);
                }}
              >
                {({ isSubmitting, setFieldValue }) => (
                  <Form>
                    <Box
                      display="grid"
                      width="40vw"
                      height="30vw"
                      justifyContent="center"
                      alignItems="center"
                      columnGap="40px"
                      rowGap="10px"
                      flexWrap="wrap"
                      flexDirection="row"
                      sx={{
                        backgroundColor: "white",
                        boxShadow: "0 0 1px 0",
                        borderRadius: "8px",
                        textAlign: "center",
                        justifyItems: "center",
                        gap: "2vw",
                        gridTemplateAreas: `
                        'brandname modelname'
                        'year color'
                        'fuel seatcount'
                        'acsystem upload'
                        'button button'
                        `,
                        padding: "20px",
                      }}
                    >
                      <div className="vehicle-brandname">
                        <FormikTextField
                          name="brandName"
                          type="text"
                          label="Brand"
                          isRequired={true}
                        />
                      </div>
                      <div className="vehicle-modelname">
                        <FormikTextField
                          name="modelName"
                          type="text"
                          label="Model"
                          isRequired={true}
                        />
                      </div>
                      <div className="vehicle-year">
                        <FormikTextField
                          name="year"
                          type="number"
                          label="Year"
                          isRequired={true}
                        />
                      </div>
                      <div className="vehicle-color">
                        <FormikTextField
                          name="color"
                          type="text"
                          label="Color"
                          isRequired={true}
                        />
                      </div>
                      <div className="vehicle-fuel">
                        <Field
                          name="fuel"
                          label="Choose a Fuel"
                          component={FormikSelect}
                        >
                          <MenuItem value={0}>Diesel</MenuItem>
                          <MenuItem value={1}>Gasoline</MenuItem>
                          <MenuItem value={2}>Electric</MenuItem>
                        </Field>
                      </div>
                      <div className="vehicle-seatcount">
                        <FormikTextField
                          name="seatCount"
                          type="number"
                          label="Seat Count"
                          isRequired={true}
                        />
                      </div>
                      <div className="vehicle-acsystem">
                        <Field
                          name="acSystem"
                          label="AC System"
                          component={FormikSwitch}
                        />
                      </div>
                      <div className="vehicle-upload">
                        <label>
                          Upload Vehicle Img
                          <input
                            type="file"
                            id="picturelink"
                            name="image"
                            hidden
                            onChange={(event) => {
                              setFieldValue(
                                "image",
                                event.currentTarget.files[0]
                              );
                            }}
                          />
                        </label>
                      </div>{" "}
                      <Button
                        type="submit"
                        variant="contained"
                        color="primary"
                        disabled={isSubmitting}
                        sx={{ mt: 2, gridArea: "button", margin: "0" }}
                      >
                        Save
                      </Button>
                    </Box>
                  </Form>
                )}
              </Formik>
            </div>
          ) : (
            <NoVehicle />
          )}
        </>
      ) : (
        <NotDriver />
      )}
    </div>
  );
};
