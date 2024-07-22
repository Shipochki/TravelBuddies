import React, { useState, useEffect } from "react";
import "./Search.css";
import { useForm } from "../../utils/hooks/useForm";
import { serializer } from "../../utils/common/serializer";
import { OnSearchSubmit } from "../../services/PostService";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faArrowRight,
  faSliders,
} from "@fortawesome/free-solid-svg-icons";
import { useNavigate } from "react-router-dom";
import { GetAllCities } from "../../services/CityService";
import {
  Box,
  Button,
  Slider,
  TextField,
  Tooltip,
} from "@mui/material";
import { Loading } from "../Loading/Loading";
import backgroundImg from "../../utils/images/white-background-with-blue-geometric-and-white-line-pattern-free-vector.jpg";

import { DemoContainer } from "@mui/x-date-pickers/internals/demo";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import InfoIcon from '@mui/icons-material/Info';
import dayjs from 'dayjs';


const searchFromKeys = {
  FromDestination: "fromDestinationCityId",
  ToDestination: "toDestinationCityId",
  PriceMin: "priceMin",
  PriceMax: "priceMax",
  FromDate: "fromDate",
  ToDate: "toDate",
  Baggage: "baggage",
  Pets: "pets",
};

const today = dayjs();
const tomorrow = dayjs().add(1, 'day');

function valuetext(value) {
  return `${value}`;
}

export const Search = () => {
  const navigate = useNavigate();
  const [cities, setCities] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      const data = await GetAllCities();
      setCities(data);

      setLoading(false);
    };
    fetchData();
  }, []);

  const { values, changeHandler, onSubmit } = useForm(
    {
      [searchFromKeys.FromDestination]: "",
      [searchFromKeys.ToDestination]: "",
      [searchFromKeys.FromDate]: null,
      [searchFromKeys.ToDate]: null,
      [searchFromKeys.Baggage]: null,
      [searchFromKeys.Pets]: null,
      [searchFromKeys.PriceMin]: null,
      [searchFromKeys.PriceMax]: null,
    },
    OnSearchSubmit
  );

  
  const clickSubmit = async (e) => {
    e.preventDefault();
    const fromdes = cities.filter(
      (c) => c.name == values[searchFromKeys.FromDestination]
    )[0];
    const todes = cities.filter(
      (c) => c.name == values[searchFromKeys.ToDestination]
    )[0];

    if (!fromdes || !todes) {
      alert("Invalid cities");
    } else {
      values[searchFromKeys.FromDestination] = fromdes.id;
      values[searchFromKeys.ToDestination] = todes.id;
      if (values[searchFromKeys.FromDate] != null) {
        let date = new Date(values[searchFromKeys.FromDate]);
        values[searchFromKeys.FromDate] = new Date(date.getTime() - (date.getTimezoneOffset() * 60000)).toISOString();
      }

      if (values[searchFromKeys.ToDate]) {
        let date = new Date(values[searchFromKeys.ToDate]);
        values[searchFromKeys.ToDate] = new Date(date.getTime() - (date.getTimezoneOffset() * 60000)).toISOString();
      }
      navigate(`/catalog?${serializer(values)}`);    
    }
  };

  const [filteredCities, setFilteredCities] = useState([]);
  const [filteredToDesCities, setFilteredToDesCities] = useState([]);

  const handleInputChange = (event) => {
    const value = event.target.value; // Convert input value to lowercase
    setFilteredCities(
      value
        ? cities.filter((city) =>
            city.name.toLowerCase().startsWith(value.toLowerCase())
          )
        : []
    );
    values[searchFromKeys.FromDestination] = value;
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
    values[searchFromKeys.ToDestination] = value;
  };

  const handleSelectCity = (city) => {
    values[searchFromKeys.FromDestination] = city;
    changeHandler;
    setFilteredCities([]);
  };

  const handleToDesSelectCity = (city) => {
    values[searchFromKeys.ToDestination] = city;
    changeHandler;
    setFilteredToDesCities([]);
  };

  const handleFromDate = (date) => {
    console.log(date["$d"]);
    values[searchFromKeys.FromDate] = date;
    changeHandler;
  };

  const handleToDate = (date) => {
    values[searchFromKeys.ToDate] = date;
    changeHandler;
  };

  const [moreOptionsVisible, setMoreOptionsVisible] = useState(false);

  const toggleMoreOptions = () => {
    setMoreOptionsVisible(!moreOptionsVisible);
  };

  const [isPets, setIsPets] = useState(false);

  const handleIsPets = () => {
    setIsPets(!isPets);
    values[searchFromKeys.Pets] = !isPets;
    changeHandler;
  };

  const [isBaggage, setIsBaggage] = useState(false);

  const handleIsBaggage = () => {
    setIsBaggage(!isBaggage);
    values[searchFromKeys.Baggage] = !isBaggage;
    changeHandler;
  };

  const [priceValues, setPriceValues] = useState([0, 100]);

  const handlePriceValues = (event, newPriceValue) => {
    setPriceValues(newPriceValue);
    values[searchFromKeys.PriceMin] = newPriceValue[0];
    values[searchFromKeys.PriceMax] = newPriceValue[1];
  };

  if (loading) {
    return <Loading />;
  }

  return (
    <div className="search-menu">
      <img className="demo-bg" src={backgroundImg} />
      <div className="search-menu-content">
        <div className="search-header">
          <h2>You can search your travel here</h2>
        </div>
        <form id="search" method="POST" onSubmit={(e) => {
          e.preventDefault()
          if(values[searchFromKeys.FromDate] > values[searchFromKeys.ToDate]){
            alert("The \"From\" date can't be later than the \"To\" date. Please ensure your date range is correctly selected.")
          }
          else{
            clickSubmit(e);
          }
        }}>
          <div className="cities-inputs">
            <div className="city-input">
              <TextField
                type="text"
                name={searchFromKeys.FromDestination}
                value={values[searchFromKeys.FromDestination]}
                onChange={handleInputChange}
                label="From destination..."
                autoComplete="off"
                sx={{
                  width: "14vw",
                }}
              />
              {filteredCities.length > 0 && (
                <ul>
                  {filteredCities
                    .map((city) => (
                      <li
                        key={city.id}
                        onClick={() => handleSelectCity(city.name)}
                      >
                        {city.name}
                      </li>
                    ))
                    .slice(0, 8)}
                </ul>
              )}
            </div>
            {<FontAwesomeIcon icon={faArrowRight} />}
            <div className="city-input">
              <TextField
                type="text"
                name={searchFromKeys.ToDestination}
                value={values[searchFromKeys.ToDestination]}
                onChange={handleToDesChange}
                label="To destination..."
                autoComplete="off"
                sx={{
                  width: "14vw",
                }}
              />
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
                    .slice(0, 8)}
                </ul>
              )}
            </div>
          </div>
          <div className="more-options">
            <Tooltip title="More options">
              <Button onClick={toggleMoreOptions}>
                <FontAwesomeIcon
                  className="more-options-icon"
                  icon={faSliders}
                />
              </Button>
            </Tooltip>
            {moreOptionsVisible && (
              <div id="search-more-options" className="more-options-content">
                <div className="more-opitons-calendars">
                  <LocalizationProvider dateAdapter={AdapterDayjs}>
                    <DemoContainer components={["DatePicker"]}>
                      <DatePicker
                        label="From date"
                        value={values[searchFromKeys.FromDate]}
                        onChange={(newValue) => handleFromDate(newValue)}
                        minDate={today}
                      />
                      <DatePicker
                        label="To date"
                        value={values[searchFromKeys.ToDate]}
                        onChange={(newValue) => handleToDate(newValue)}
                        minDate={today}
                      />
                    </DemoContainer>
                  </LocalizationProvider>
                </div>
                <div className="price-range-selector">
                  <label>Price range</label>
                  <Box
                    sx={{
                      width: "68%",
                      display: "flex",
                      columnGap: "20px",
                      alignItems: "center",
                    }}
                  >
                    <p>{priceValues[0]}</p>
                    <Slider
                      getAriaLabel={() => "Price range"}
                      value={priceValues}
                      onChange={handlePriceValues}
                      valueLabelDisplay="auto"
                      getAriaValueText={valuetext}
                    />
                    <p>{priceValues[1]}</p>
                  </Box>
                </div>
                <div className="more-options-bools">
                  <div className="more-options-boolean baggage-btn">
                  <Tooltip title="Do you bring baggage with you?" placement="top">
                      <InfoIcon/>
                    </Tooltip>
                    <p>Baggage</p>
                    <input
                      type="checkbox"
                      checked={isBaggage}
                      onChange={handleIsBaggage}
                    />
                  </div>
                  <div className="more-options-boolean pets-btn">
                  <Tooltip title="Do you bring pets with you?" placement="top">
                      <InfoIcon/>
                    </Tooltip>
                    <p>Pets</p>
                    <input
                      type="checkbox"
                      checked={isPets}
                      onChange={handleIsPets}
                    />
                  </div>
                </div>
              </div>
            )}
          </div>
          <div>
            <button className="submit-button" type="submit">
              Search
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};
