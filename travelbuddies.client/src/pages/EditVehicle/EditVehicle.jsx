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
      [EditVehicleFromKeys]: "",
    },
    OnUpdateVehicleSubmit
  );

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
    values[EditVehicleFromKeys.ACSystem] = !isACSystem;
    changeHandler;
  };

  const onUpdateSubmit = async (e) => {
    e.preventDefault();

    await OnUpdateVehicleSubmit(values);

    const data = await GetVehicleByOwnerId(localStorage.userId);
    setVehicle(data);

    values[EditVehicleFromKeys.Id] = data.id;
    values[EditVehicleFromKeys.BrandName] = data.brandName;
    values[EditVehicleFromKeys.ModelName] = data.modelName;
    values[EditVehicleFromKeys.Fuel] = fuel[data.fuel];
    values[EditVehicleFromKeys.SeatCount] = data.seatCount;
    values[EditVehicleFromKeys.ACSystem] = data.acSystem;
    values[EditVehicleFromKeys.Year] = data.year;
    values[EditVehicleFromKeys.Color] = data.color;
  };

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
                        <span>{nameFile}</span>
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
              {/* <form className="create-vehicle-form" onSubmit={onUpdateSubmit}>
                <div className="vehicle-brandname">
                  <input
                    type="text"
                    id="brandname"
                    placeholder="BrandName"
                    className="inputModel"
                    name={EditVehicleFromKeys.BrandName}
                    value={values[EditVehicleFromKeys.BrandName]}
                    onChange={changeHandler}
                    required
                  />
                  <TextField
                    type="text"
                    name={EditVehicleFromKeys.BrandName}
                    value={values[EditVehicleFromKeys.BrandName]}
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
                    name={EditVehicleFromKeys.ModelName}
                    value={values[EditVehicleFromKeys.ModelName]}
                    onChange={changeHandler}
                    required
                  />
                  <TextField
                    type="text"
                    name={EditVehicleFromKeys.ModelName}
                    value={values[EditVehicleFromKeys.ModelName]}
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
                    name={EditVehicleFromKeys.Year}
                    value={values[EditVehicleFromKeys.Year]}
                    onChange={changeHandler}
                    required
                  />
                  <TextField
                    type="number"
                    name={EditVehicleFromKeys.Year}
                    value={values[EditVehicleFromKeys.Year]}
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
                    name={EditVehicleFromKeys.Color}
                    value={values[EditVehicleFromKeys.Color]}
                    onChange={changeHandler}
                    required
                  />
                  <TextField
                    type="text"
                    name={EditVehicleFromKeys.Color}
                    value={values[EditVehicleFromKeys.Color]}
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
                        value={values[EditVehicleFromKeys.Fuel]} 
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
                      value={values[EditVehicleFromKeys.Fuel]}
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
                    name={EditVehicleFromKeys.SeatCount}
                    value={values[EditVehicleFromKeys.SeatCount]}
                    onChange={changeHandler}
                    required
                  />
                  <TextField
                    type="number"
                    name={EditVehicleFromKeys.SeatCount}
                    value={values[EditVehicleFromKeys.SeatCount]}
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
                    checked={values[EditVehicleFromKeys.ACSystem]}
                    onChange={handleIsACSystem}
                  />
                  <label>ACSystem</label>
                </div>
                <div className="vehicle-upload">
                  <label>
                    Upload new vehicle img
                    <input
                      type="file"
                      id="picturelink"
                      name={EditVehicleFromKeys.PictureLink}
                      value={values[EditVehicleFromKeys.PictureLink]}
                      onChange={onChangeFile}
                      hidden
                    />
                  </label>
                  <span>{nameFile}</span>
                </div>
                <button className="vehicle-submit-button">Save</button>
              </form> */}
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
