import { useContext, useEffect, useState } from "react";
import { useForm } from "../../utils/hooks/useForm";
import {
  GetPostsByOwnerId,
  OnCreatePostSubmit,
} from "../../services/PostService";
import "./CreatePost.css";
import Calendar from "../../components/Calendar/Calendar";
import { NotDriver } from "../../components/NotDriver/NotDriver";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faArrowRight,
  faCalendarDays,
  faCheck,
} from "@fortawesome/free-solid-svg-icons";
import { NoVehicle } from "../../components/NoVehicle/NoVehicle";
import { GlobalContext } from "../../utils/contexts/GlobalContext";
import { GetVehicleByOwnerId } from "../../services/VehicleService";
import { GetAllCities } from "../../services/CityService";
import {
  Alert,
  Box,
  FormControl,
  InputLabel,
  NativeSelect,
  Snackbar,
  TextField,
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import { Loading } from "../Loading/Loading";
import { GetAllGroupByUserId } from "../../services/GroupService";

const CreatePostFromKeys = {
  FromDestination: "fromDestinationCityId",
  ToDestination: "toDestinationCityId",
  Description: "description",
  PricePerSeat: "pricePerSeat",
  FreeSeats: "freeSeats",
  Baggage: "baggage",
  Pets: "pets",
  Date: "date",
  Time: "time",
  PaymentType: "paymentType",
  Currency: "currency",
};

export const CreatePost = () => {
  const navigate = useNavigate();
  const { OnSetGroups } = useContext(GlobalContext);
  const [cities, setCities] = useState([]);
  const [vehicle, setVehicle] = useState({});
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      if (localStorage.role == "driver") {
        const data = await GetAllCities();
        setCities(data);

        const dataVeh = await GetVehicleByOwnerId(localStorage.userId);
        setVehicle(dataVeh);
      }

      setLoading(false);
    };
    fetchData();
  }, []);

  const { OnSetPostsByOwner } = useContext(GlobalContext);

  const { values, changeHandler, onSubmit } = useForm({
    [CreatePostFromKeys.FromDestination]: "",
    [CreatePostFromKeys.ToDestination]: "",
    [CreatePostFromKeys.Description]: "",
    [CreatePostFromKeys.PricePerSeat]: 0,
    [CreatePostFromKeys.FreeSeats]: 0,
    [CreatePostFromKeys.Baggage]: false,
    [CreatePostFromKeys.Pets]: false,
    [CreatePostFromKeys.Date]: "",
    [CreatePostFromKeys.Time]: "",
    [CreatePostFromKeys.PaymentType]: 0,
    [CreatePostFromKeys.Currency]: "EUR",
  });

  const clickHandler = async (e) => {
    e.preventDefault();

    const fromdes = cities.filter(
      (c) => c.name == values[CreatePostFromKeys.FromDestination]
    )[0];
    const todes = cities.filter(
      (c) => c.name == values[CreatePostFromKeys.ToDestination]
    )[0];

    values[CreatePostFromKeys.FromDestination] = fromdes.id;
    values[CreatePostFromKeys.ToDestination] = todes.id;
    values[CreatePostFromKeys.FreeSeats] = Number(
      values[CreatePostFromKeys.FreeSeats]
    );
    values[CreatePostFromKeys.PricePerSeat] = Number(
      values[CreatePostFromKeys.PricePerSeat]
    );

    const isSuccess = await OnCreatePostSubmit(values);

    if (isSuccess) {
      const data = await GetAllGroupByUserId(localStorage.userId);
      OnSetGroups(data);

      HandleAlertOpen();

      navigate("/myPosts");
    }
  };

  const [filteredCities, setFilteredCities] = useState([]);
  const [filteredToDesCities, setFilteredToDesCities] = useState([]);

  const handleFromDesChange = (event) => {
    const value = event.target.value; // Convert input value to lowercase
    setFilteredCities(
      value
        ? cities.filter((city) =>
            city.name.toLowerCase().startsWith(value.toLowerCase())
          )
        : []
    );
    values[CreatePostFromKeys.FromDestination] = value;
  };

  const handleToDesChange = (event) => {
    const value = event.target.value; // Convert input value to lowercase
    setFilteredToDesCities(
      value
        ? cities.filter((city) =>
            city.name.toLowerCase().startsWith(value.toLowerCase())
          )
        : []
    );
    values[CreatePostFromKeys.ToDestination] = value;
  };

  const handleFromDesSelectCity = (city) => {
    values[CreatePostFromKeys.FromDestination] = city;
    changeHandler;
    setFilteredCities([]);
  };

  const handleToDesSelectCity = (city) => {
    values[CreatePostFromKeys.ToDestination] = city;
    changeHandler;
    setFilteredToDesCities([]);
  };

  const handleDate = (date) => {
    console.log(date);
    values[CreatePostFromKeys.Date] = date;
    changeHandler;
  };

  const [calendarVisible, setCalendarVisible] = useState(false);

  const toggleCalendar = () => {
    setCalendarVisible(!calendarVisible);
  };

  const [isPets, setIsPets] = useState(false);

  const handleIsPets = () => {
    setIsPets(!isPets);
    values[CreatePostFromKeys.Pets] = !isPets;
    changeHandler;
  };

  const [isBaggage, setIsBaggage] = useState(false);

  const handleIsBaggage = () => {
    setIsBaggage(!isBaggage);
    values[CreatePostFromKeys.Baggage] = !isBaggage;
    changeHandler;
  };

  const [alertOpen, setAlertOpen] = useState(false);

  const HandleAlertOpen = () => {
    setAlertOpen(true);
  };

  const HandleAlerClose = (event, reason) => {
    if (reason === "clickaway") {
      return;
    }
    setAlertOpen(false);
  };

  if (loading) {
    return <Loading />;
  }

  return (
    <div className="create-post-main">
      {localStorage.role == "driver" ? (
        <>
          {vehicle.id ? (
            <>
              <div className="create-post-header">
                <h2>Create Post</h2>
              </div>
              <form id="create-post" method="POST" onSubmit={clickHandler}>
                <div className="cities-inputs">
                  <div className="city-input">
                    <TextField
                      type="text"
                      name={CreatePostFromKeys.FromDestination}
                      value={values[CreatePostFromKeys.FromDestination]}
                      onChange={handleFromDesChange}
                      label="From destination..."
                      autoComplete="off"
                      sx={{
                        width: "14vw",
                      }}
                      required
                    />
                    {/* <input
                            type="text"
                            name={CreatePostFromKeys.FromDestination}
                            value={values[CreatePostFromKeys.FromDestination]}
                            onChange={handleFromDesChange}
                            placeholder="From destination..."
                            autoComplete="off"
                            required
                        /> */}
                    {filteredCities.length > 0 && (
                      <ul>
                        {filteredCities
                          .map((city) => (
                            <li
                              key={city.id}
                              onClick={() => handleFromDesSelectCity(city.name)}
                            >
                              {city.name}
                            </li>
                          ))
                          .slice(0, 10)}
                      </ul>
                    )}
                  </div>
                  <FontAwesomeIcon icon={faArrowRight} />
                  <div className="city-input">
                    <TextField
                      type="text"
                      name={CreatePostFromKeys.ToDestination}
                      value={values[CreatePostFromKeys.ToDestination]}
                      onChange={handleToDesChange}
                      label="To destination..."
                      autoComplete="off"
                      sx={{
                        width: "14vw",
                      }}
                      required
                    />
                    {/* <input
                            type="text"
                            name={CreatePostFromKeys.ToDestination}
                            value={values[CreatePostFromKeys.ToDestination]}
                            onChange={handleToDesChange}
                            placeholder="To destination..."
                            autoComplete="off"
                            required
                        /> */}
                    {filteredToDesCities.length > 0 && (
                      <ul>
                        {filteredToDesCities
                          .map((city) => (
                            <li
                              key={city.id}
                              onClick={() => handleToDesSelectCity(city.name)}
                            >
                              {city.name}
                            </li>
                          ))
                          .slice(0, 10)}
                      </ul>
                    )}
                  </div>
                </div>
                <textarea
                  type="text"
                  className="createpost-description"
                  name={CreatePostFromKeys.Description}
                  value={values[CreatePostFromKeys.Description]}
                  onChange={changeHandler}
                  placeholder="Description...*"
                />
                <div className="create-post-date-time">
                  <div className="create-post-date-time-info">
                    <input
                      type="text"
                      id="date"
                      className="create-post-date"
                      value={values[CreatePostFromKeys.Date.slice(0, 17)]}
                      placeholder="Choose date*"
                      disabled
                    />
                    <input
                      type="text"
                      id="time"
                      className="create-post-date"
                      value={values[CreatePostFromKeys.Time]}
                      placeholder="Choose time*"
                      disabled
                    />
                  </div>
                  {calendarVisible && (
                    <div className="calendar-time">
                      <Calendar handle={handleDate} />
                      <input
                        type="time"
                        className="create-post-time-input"
                        name={CreatePostFromKeys.Time}
                        value={values[CreatePostFromKeys.Time]}
                        onChange={changeHandler}
                        required
                      />
                    </div>
                  )}
                  <div className="btn-calendar-container">
                    {!calendarVisible && (
                      <span className="tooltip-btn-calendar-info">
                        Date and Time selector
                      </span>
                    )}
                    <button
                      className="create-post-btn-calendar"
                      type="button"
                      onClick={toggleCalendar}
                    >
                      <FontAwesomeIcon
                        icon={!calendarVisible ? faCalendarDays : faCheck}
                      />
                    </button>
                  </div>
                </div>
                <div className="create-post-bools">
                  <div className="create-post-baggage">
                    <p>Baggage:</p>
                    <label>{isBaggage ? "Yes" : "No"}</label>
                    <input
                      type="checkbox"
                      checked={isBaggage}
                      onChange={handleIsBaggage}
                    />
                  </div>
                  <div className="create-post-pets">
                    <p>Pets:</p>
                    <label>{isPets ? "Yes" : "No"}</label>
                    <input
                      type="checkbox"
                      checked={isPets}
                      onChange={handleIsPets}
                    />
                  </div>
                </div>
                <div className="create-post-nums-inputs">
                  <div className="create-post-price">
                    <label>Price per seat:</label>
                    <label>
                      <input
                        type="number"
                        name={CreatePostFromKeys.PricePerSeat}
                        value={values[CreatePostFromKeys.PricePerSeat]}
                        onChange={changeHandler}
                        placeholder="10"
                        required
                        min={0}
                        max={100}
                      />
                    </label>
                  </div>
                  <div className="create-post-seats">
                    <label>Available seats:</label>
                    <input
                      type="number"
                      name={CreatePostFromKeys.FreeSeats}
                      value={values[CreatePostFromKeys.FreeSeats]}
                      onChange={changeHandler}
                      placeholder="3"
                      required
                      min={1}
                    />
                  </div>
                </div>
                <div className="boxs-choses">
                  <Box sx={{ minWidth: 120 }}>
                    <FormControl fullWidth>
                      <InputLabel variant="standard" htmlFor="paymentType">
                        Choose a Payment type
                      </InputLabel>
                      <NativeSelect
                        value={values[CreatePostFromKeys.PaymentType]}
                        onChange={changeHandler}
                        inputProps={{
                          name: "paymentType",
                          id: "paymentType",
                        }}
                      >
                        <option value={0}>Cash</option>
                        <option value={1}>Card</option>
                        <option value={2}>Cash and Card</option>
                      </NativeSelect>
                    </FormControl>
                  </Box>
                  <Box sx={{ minWidth: 120 }}>
                    <FormControl fullWidth>
                      <InputLabel variant="standard" htmlFor="currency">
                        Choose a Currency
                      </InputLabel>
                      <NativeSelect
                        value={values[CreatePostFromKeys.Currency]}
                        onChange={changeHandler}
                        inputProps={{
                          name: "currency",
                          id: "currency",
                        }}
                      >
                        <option value={"EUR"}>EUR</option>
                        <option value={"BGN"}>BGN</option>
                        <option value={"USD"}>USD</option>
                        <option value={"GBP"}>GBP</option>
                      </NativeSelect>
                    </FormControl>
                  </Box>
                </div>
                {/* {(values[CreatePostFromKeys.PaymentType] == 1 ||
                  values[CreatePostFromKeys.PaymentType] == 2) && (
                  <div className="create-post-payment-provider">
                    <Box sx={{ minWidth: 120 }}>
                    <FormControl fullWidth>
                      <InputLabel variant="standard" htmlFor="paymentType">
                        Choose a Payment type
                      </InputLabel>
                      <NativeSelect
                        value={values[CreatePostFromKeys.PaymentType]}
                        onChange={changeHandler}
                        inputProps={{
                          name: "paymentType",
                          id: "paymentType",
                        }}
                      >
                        <option value={0}>Cash</option>
                        <option value={1}>Card</option>
                        <option value={2}>Cash and Card</option>
                      </NativeSelect>
                    </FormControl>
                  </Box>
                  </div>
                )} */}

                <button className="create-post-btn-add" type="submit">
                  Add
                </button>
              </form>
              <Snackbar
                open={alertOpen}
                autoHideDuration={6000}
                onClose={HandleAlerClose}
              >
                <Alert
                  onClose={HandleAlerClose}
                  severity="success"
                  variant="filled"
                  sx={{ width: "100%" }}
                >
                  Succesfully added post!
                </Alert>
              </Snackbar>
            </>
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
