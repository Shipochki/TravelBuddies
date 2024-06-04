import "./CreateVehicle.css";
import {
  GetVehicleByOwnerId,
  OnCreateVehicleSubmit,
} from "../../services/VehicleService";
import { useForm } from "../../utils/hooks/useForm";
import { useContext, useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { NotDriver } from "../../components/NotDriver/NotDriver";
import { GlobalContext } from "../../utils/contexts/GlobalContext";
import {
  Box,
  Button,
  FormControl,
  FormControlLabel,
  InputLabel,
  MenuItem,
  NativeSelect,
  Switch,
  TextField,
} from "@mui/material";
import { Loading } from "../Loading/Loading";
import backgroundImg from "../../utils/images/white-background-with-blue-geometric-and-white-line-pattern-free-vector.jpg";
import * as Yup from "yup";
import { Field, Form, Formik } from "formik";
import { FormikTextField } from "../../components/FormikTextField/FormikTextField";
import CloudUploadIcon from "@mui/icons-material/CloudUpload";
import { styled } from "@mui/material/styles";
import { FormikFileInput } from "../../components/FormikFileInput/FormikFileInput";
import { FormikSwitch } from "../../components/FormikSwitch/FormikSwitch";
import { FormikSelect } from "../../components/FormikSelect/FormikSelect";

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

const VehicleFromKeys = {
  BrandName: "brandname",
  ModelName: "modelname",
  Year: "year",
  Color: "color",
  Fuel: "fuel",
  SeatCount: "seatcount",
  PictureLink: "picturelink",
  ACSystem: "acsystem",
};

const VisuallyHiddenInput = styled("input")({
  clip: "rect(0 0 0 0)",
  clipPath: "inset(50%)",
  height: 1,
  overflow: "hidden",
  position: "absolute",
  bottom: 0,
  left: 0,
  whiteSpace: "nowrap",
  width: 1,
});

export const CreateVehicle = () => {
  const [vehicle, setVehicle] = useState({});
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      if (localStorage.role == "driver") {
        const data = await GetVehicleByOwnerId(localStorage.userId);
        setVehicle(data);
      }

      setLoading(false);
    };
    fetchData();
  }, []);

  const { values, changeHandler, onSubmit } = useForm(
    {
      [VehicleFromKeys.BrandName]: "",
      [VehicleFromKeys.ModelName]: "",
      [VehicleFromKeys.Year]: 0,
      [VehicleFromKeys.Color]: "",
      [VehicleFromKeys.Fuel]: 0,
      [VehicleFromKeys.SeatCount]: 0,
      [VehicleFromKeys.PictureLink]: null,
      [VehicleFromKeys.ACSystem]: false,
    },
    OnCreateVehicleSubmit
  );

  const OnClickSubmit = async (e) => {
    e.preventDefault();

    await OnCreateVehicleSubmit(values);

    const result = await GetVehicleByOwnerId(localStorage.userId);

    setVehicle(result);
  };

  const clickSubmit = async (values) => {
    const result = await OnCreateVehicleSubmit(values);

    if (result) {
      navigate(`/myVehicle`);
    } else {
      alert(result);
    }
  };

  const [nameFile, setNameFile] = useState("");

  const onChangeFile = (e) => {
    changeHandler(e);

    const path = e.target.value.split("\\");
    const name = path[path.length - 1];

    setNameFile(name);
  };

  const [isACSystem, setIsACSystem] = useState(false);

  const handleIsACSystem = () => {
    setIsACSystem(!isACSystem);
    values[VehicleFromKeys.ACSystem] = !isACSystem;
    changeHandler;
  };

  if (loading) {
    return <Loading />;
  }

  return (
    <div className="create-vehicle-main">
      {localStorage.role == "driver" ? (
        <>
          <img className="demo-bg" src={backgroundImg} />
          {!vehicle ? (
            <div className="create-vehicle-content">
              <div className="create-vehicle-header">
                <h2>Add your Vehicle</h2>
              </div>
              <Formik
                initialValues={{
                  brandName: "",
                  modelName: "",
                  year: 0,
                  color: "",
                  fuel: "",
                  seatCount: 0,
                  acSystem: false,
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
                            required
                            hidden
                            onChange={(event) => {
                              setFieldValue(
                                "image",
                                event.currentTarget.files[0]
                              );
                            }}
                          />
                        </label>
                        <span>{nameFile}</span>
                      </div>{" "}
                      <Button
                        type="submit"
                        variant="contained"
                        color="primary"
                        disabled={isSubmitting}
                        sx={{ mt: 2, gridArea: "button", margin: "0" }}
                      >
                        Submit
                      </Button>
                    </Box>
                  </Form>
                )}
              </Formik>
              {/* <form className="create-vehicle-form" onSubmit={OnClickSubmit}>
                <div className="vehicle-brandname">
                  <input
                    type="text"
                    id="brandname"
                    placeholder="BrandName"
                    className="inputModel"
                    name={VehicleFromKeys.BrandName}
                    value={values[VehicleFromKeys.BrandName]}
                    onChange={changeHandler}
                    required
                  />
                  <TextField
                    type="text"
                    name={VehicleFromKeys.BrandName}
                    value={values[VehicleFromKeys.BrandName]}
                    onChange={changeHandler}
                    label="Brand"
                    autoComplete="off"
                    sx={{
                      width: "14vw",
                    }}
                    minLength={1}
                    maxLength={50}
                    required
                  />
                </div>
                <div className="vehicle-modelname">
                  <input
                    type="text"
                    id="modelname"
                    placeholder="ModelName"
                    className="inputModel"
                    name={VehicleFromKeys.ModelName}
                    value={values[VehicleFromKeys.ModelName]}
                    onChange={changeHandler}
                    required
                  />
                  <TextField
                    type="text"
                    name={VehicleFromKeys.ModelName}
                    value={values[VehicleFromKeys.ModelName]}
                    onChange={changeHandler}
                    label="Model"
                    autoComplete="off"
                    sx={{
                      width: "14vw",
                    }}
                    minLength={1}
                    maxLength={70}
                    required
                  />
                </div>
                <div className="vehicle-year">
                  <label>Year</label>
                  <input
                    type="number"
                    id="year"
                    className="inputModel"
                    name={VehicleFromKeys.Year}
                    value={values[VehicleFromKeys.Year]}
                    onChange={changeHandler}
                    required
                  />
                  <TextField
                    type="number"
                    name={VehicleFromKeys.Year}
                    value={values[VehicleFromKeys.Year]}
                    onChange={changeHandler}
                    label="Year"
                    autoComplete="off"
                    sx={{
                      width: "8vw",
                    }}
                    required
                  />
                </div>
                <div className="vehicle-color">
                  <input
                    type="text"
                    id="color"
                    className="inputModel"
                    placeholder="Color"
                    name={VehicleFromKeys.Color}
                    value={values[VehicleFromKeys.Color]}
                    onChange={changeHandler}
                    required
                  />
                  <TextField
                    type="text"
                    name={VehicleFromKeys.Color}
                    value={values[VehicleFromKeys.Color]}
                    onChange={changeHandler}
                    label="Color"
                    autoComplete="off"
                    sx={{
                      width: "14vw",
                    }}
                    required
                  />
                </div>
                <div className='vehicle-fuel'>
                    <label for="fuel">Choose a Fuel:</label>
                    <select 
                        value={values[VehicleFromKeys.Fuel]} 
                        onChange={changeHandler} 
                        name="fuel" 
                        id="fuel">
                        <option value={0}>Diesel</option>
                        <option value={1}>Gasoline</option>
                        <option value={2}>Electric</option>
                    </select>
                </div>
                <Box sx={{ minWidth: 120, gridArea: "fuel" }}>
                  <FormControl fullWidth>
                    <InputLabel variant="standard" htmlFor="fuel">
                      Choose a Fuel
                    </InputLabel>
                    <NativeSelect
                      value={values[VehicleFromKeys.Fuel]}
                      onChange={changeHandler}
                      inputProps={{
                        name: "fuel",
                        id: "fuel",
                      }}
                    >
                      <option value={0}>Diesel</option>
                      <option value={1}>Gasoline</option>
                      <option value={2}>Electric</option>
                    </NativeSelect>
                  </FormControl>
                </Box>
                <div className="vehicle-seatcount">
                  <label>SeatCount</label>
                  <input
                    type="number"
                    id="seatcount"
                    name={VehicleFromKeys.SeatCount}
                    value={values[VehicleFromKeys.SeatCount]}
                    onChange={changeHandler}
                    required
                  />
                  <TextField
                    type="number"
                    name={VehicleFromKeys.SeatCount}
                    value={values[VehicleFromKeys.SeatCount]}
                    onChange={changeHandler}
                    label="Seat count"
                    autoComplete="off"
                    sx={{
                      width: "8vw",
                    }}
                    required
                  />
                </div>
                <div className="vehicle-acsystme">
                  <input
                    type="checkbox"
                    checked={values[VehicleFromKeys.ACSystem]}
                    onChange={handleIsACSystem}
                  />
                  <label>ACSystem</label>
                </div>
                <div className="vehicle-upload">
                  <label>
                    Upload Vehicle Img
                    <input
                      type="file"
                      id="picturelink"
                      name={VehicleFromKeys.PictureLink}
                      value={values[VehicleFromKeys.PictureLink]}
                      onChange={onChangeFile}
                      required
                      hidden
                    />
                  </label>
                  <span>{nameFile}</span>
                </div>
                <button className="vehicle-submit-button">Add</button>
              </form> */}
            </div>
          ) : (
            <div className="allready-have-vehicle">
              <h3>You allready have added vehicle</h3>
              <div className="allready-vehicle-links">
                <Link to={"/myVehicle"}>My Vehicle</Link>
                <Link to={"/editVehicle"}>Edit Vehicle</Link>
              </div>
            </div>
          )}
        </>
      ) : (
        <NotDriver />
      )}
    </div>
  );
};
