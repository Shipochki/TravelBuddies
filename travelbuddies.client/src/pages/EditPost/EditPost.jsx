import "./EditPost.css";
import { useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { GetPostById, OnUpdatePostSubmit } from "../../services/PostService";
import { GetAllCities } from "../../services/CityService";
import {
  Box,
  FormControl,
  InputLabel,
  NativeSelect,
  TextField,
  Tooltip,
} from "@mui/material";
import { useForm } from "../../utils/hooks/useForm";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faArrowRight,
  faCalendarDays,
  faCheck,
} from "@fortawesome/free-solid-svg-icons";
import Calendar from "../../components/Calendar/Calendar";
import { Forbidden } from "../Forbidden/Forbidden";
import { GlobalContext } from "../../utils/contexts/GlobalContext";
import { Loading } from "../Loading/Loading";

const EditPostFromKeys = {
  Id: "id",
  FromDestination: "fromDestinationCityId",
  ToDestination: "toDestinationCityId",
  Description: "description",
  PricePerSeat: "pricePerSeat",
  FreeSeats: "freeSeats",
  Baggage: "baggage",
  Pets: "pets",
  Date: "date",
  Time: "time",
  PaymentType: "payType",
  Currency: "currency",
};

export const EditPost = () => {
  const navigate = useNavigate();
  const [post, setPost] = useState({});
  const [cities, setCities] = useState([]);
  const [filteredCities, setFilteredCities] = useState([]);
  const [filteredToDesCities, setFilteredToDesCities] = useState([]);
  const [loading, setLoading] = useState(true);
  const { id } = useParams();

  const { values, changeHandler, onSubmit } = useForm({
    [EditPostFromKeys.Id]: post.id,
    [EditPostFromKeys.FromDestination]: "",
    [EditPostFromKeys.ToDestination]: "",
    [EditPostFromKeys.Description]: post.description,
    [EditPostFromKeys.PricePerSeat]: post.pricePerSeat,
    [EditPostFromKeys.FreeSeats]: post.freeSeats,
    [EditPostFromKeys.Baggage]: post.baggage,
    [EditPostFromKeys.Pets]: post.pets,
    [EditPostFromKeys.Date]: "",
    [EditPostFromKeys.Time]: "",
    [EditPostFromKeys.PaymentType]: post.paymentType,
    [EditPostFromKeys.Currency]: post.currency,
  });

  useEffect(() => {
    const fetchData = async () => {
      const postData = await GetPostById(id);
      setPost(postData);

      const citiesData = await GetAllCities();
      setCities(citiesData);

      values[EditPostFromKeys.Id] = postData.id;
      values[EditPostFromKeys.FromDestination] = postData.fromDestinationName;
      values[EditPostFromKeys.ToDestination] = postData.toDestinationName;
      values[EditPostFromKeys.Description] = postData.description;
      values[EditPostFromKeys.PricePerSeat] = postData.pricePerSeat;
      values[EditPostFromKeys.FreeSeats] = postData.freeSeats;
      values[EditPostFromKeys.Baggage] = postData.baggage;
      values[EditPostFromKeys.Pets] = postData.pets;
      values[EditPostFromKeys.Date] = postData.dateAndTime.split(" ")[0];
      values[EditPostFromKeys.Time] = postData.dateAndTime.split(" ")[1];
      values[EditPostFromKeys.PaymentType] = postData.paymentType;
      values[EditPostFromKeys.Currency] = postData.currency;
      setIsBaggage(postData.baggage);
      setIsPets(postData.pets);

      setLoading(false);
    };
    fetchData();
  }, [id]);

  const OnEditSubmit = async (e) => {
    e.preventDefault();

    const fromdes = cities.filter(
      (c) => c.name == values[EditPostFromKeys.FromDestination]
    )[0];
    const todes = cities.filter(
      (c) => c.name == values[EditPostFromKeys.ToDestination]
    )[0];

    values[EditPostFromKeys.FromDestination] = fromdes.id;
    values[EditPostFromKeys.ToDestination] = todes.id;
    values[EditPostFromKeys.FreeSeats] = Number(
      values[EditPostFromKeys.FreeSeats]
    );
    values[EditPostFromKeys.PricePerSeat] = Number(
      values[EditPostFromKeys.PricePerSeat]
    );

    await OnUpdatePostSubmit(values);

    navigate("/myPosts");
  };

  const handleFromDesChange = (event) => {
    const value = event.target.value; // Convert input value to lowercase
    setFilteredCities(
      value
        ? cities.filter((city) =>
            city.name.toLowerCase().startsWith(value.toLowerCase())
          )
        : []
    );
    values[EditPostFromKeys.FromDestination] = value;
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
    values[EditPostFromKeys.ToDestination] = value;
  };

  const handleFromDesSelectCity = (city) => {
    values[EditPostFromKeys.FromDestination] = city;
    changeHandler;
    setFilteredCities([]);
  };

  const handleToDesSelectCity = (city) => {
    values[EditPostFromKeys.ToDestination] = city;
    changeHandler;
    setFilteredToDesCities([]);
  };

  const handleDate = (date) => {
    console.log(date);
    values[EditPostFromKeys.Date] = date;
    changeHandler;
  };

  const [calendarVisible, setCalendarVisible] = useState(false);

  const toggleCalendar = () => {
    setCalendarVisible(!calendarVisible);
  };

  const [isPets, setIsPets] = useState(false);

  const handleIsPets = () => {
    setIsPets(!isPets);
    values[EditPostFromKeys.Pets] = !isPets;
    changeHandler;
  };

  const [isBaggage, setIsBaggage] = useState(false);

  const handleIsBaggage = () => {
    setIsBaggage(!isBaggage);
    values[EditPostFromKeys.Baggage] = !isBaggage;
    changeHandler;
  };

  if (loading) {
    return <Loading />;
  }

  return (
    <>
      {localStorage.role == "driver" &&
      localStorage.userId == post.creatorId ? (
        <div className="edit-post-main">
          <div className="edit-post-header">
            <h2>Edit Post</h2>
          </div>
          <div className="edit-post-content">
            <form
              className="edit-post-form"
              id="edit-post"
              onSubmit={OnEditSubmit}
            >
              <div className="cities-inputs">
                <div className="city-input">
                  <TextField
                    type="text"
                    name={EditPostFromKeys.FromDestination}
                    value={values[EditPostFromKeys.FromDestination]}
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
                    name={EditPostFromKeys.ToDestination}
                    value={values[EditPostFromKeys.ToDestination]}
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
                className="edit-post-description"
                name={EditPostFromKeys.Description}
                value={values[EditPostFromKeys.Description]}
                onChange={changeHandler}
                placeholder="Description..."
              />
              <div className="create-post-date-time">
                <div className="create-post-date-time-info">
                  <input
                    type="text"
                    id="date"
                    className="create-post-date"
                    value={values[EditPostFromKeys.Date.slice(0, 17)]}
                    placeholder="Choose date"
                    disabled
                  />
                  <input
                    type="text"
                    id="time"
                    className="create-post-date"
                    value={values[EditPostFromKeys.Time]}
                    placeholder="Choose time"
                    disabled
                  />
                </div>
                {calendarVisible && (
                  <div className="calendar-time">
                    <Calendar handle={handleDate} />
                    <input
                      type="time"
                      className="create-post-time-input"
                      name={EditPostFromKeys.Time}
                      value={values[EditPostFromKeys.Time]}
                      onChange={changeHandler}
                      required
                    />
                  </div>
                )}
                <div className="btn-calendar-container">
                  <Tooltip title="Date and Time" placement="top">
                    <button
                      className="create-post-btn-calendar"
                      type="button"
                      onClick={toggleCalendar}
                    >
                      <FontAwesomeIcon
                        icon={!calendarVisible ? faCalendarDays : faCheck}
                      />
                    </button>
                  </Tooltip>
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
                  <label>Price per seat</label>
                  <label>
                    <input
                      type="number"
                      name={EditPostFromKeys.PricePerSeat}
                      value={values[EditPostFromKeys.PricePerSeat]}
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
                    name={EditPostFromKeys.FreeSeats}
                    value={values[EditPostFromKeys.FreeSeats]}
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
                    <InputLabel variant="standard" htmlFor="payType">
                      Choose a Payment type
                    </InputLabel>
                    <NativeSelect
                      value={values[EditPostFromKeys.PaymentType]}
                      onChange={changeHandler}
                      inputProps={{
                        name: "payType",
                        id: "payType",
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
                      value={values[EditPostFromKeys.Currency]}
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
              <button className="create-post-btn-add" type="submit">
                Edit
              </button>
            </form>
          </div>
        </div>
      ) : (
        <Forbidden />
      )}
    </>
  );
};
