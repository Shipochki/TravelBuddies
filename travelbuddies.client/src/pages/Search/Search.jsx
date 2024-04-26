import React, { useState, useEffect, useContext } from 'react';
import './Search.css'
import Calendar from './Calendar/Calendar';
import { useForm } from '../../utils/hooks/useForm';
import { OnSearchSubmit } from '../../services/PostService';
import { useNavigate } from 'react-router-dom';
import { GlobalContext } from '../../utils/contexts/GlobalContext'

const searchFromKeys = {
  FromDestination: 'fromDestinationCityId',
  ToDestination: 'toDestinationCityId',
  PriceMin: 'priceMin',
  PriceMax: 'priceMax',
  FromDate: 'fromDate',
  ToDate: 'toDate',
  Baggage: 'baggage',
  Pets: 'pets',
}

export const Search = ({cities}) => {
  const navigate = useNavigate();
  const { setPosts } = useContext(GlobalContext);

  const { values, changeHandler, onSubmit } = useForm({
    [searchFromKeys.FromDestination]: '',
    [searchFromKeys.ToDestination]: '',
    [searchFromKeys.FromDate]: null,
    [searchFromKeys.ToDate]: null,
    [searchFromKeys.Baggage]: null,
    [searchFromKeys.Pets]: null,
    [searchFromKeys.PriceMin]: null,
    [searchFromKeys.PriceMax]: null,
  });

  const clickHandler = () => {
    const fromdes = cities.filter(c => c.name == values[searchFromKeys.FromDestination])[0];
    const todes = cities.filter(c => c.name == values[searchFromKeys.ToDestination])[0];

    values[searchFromKeys.FromDestination] = fromdes.id;
    values[searchFromKeys.ToDestination] = todes.id;
    
    setPosts(OnSearchSubmit(values));

    navigate('/catalog')
  }

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
    const value = event.target.value // Convert input value to lowercase
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
  }

  const handleFromDate = (date) => {
    console.log(date);
    values[searchFromKeys.FromDate] = date;
    changeHandler;
  }

  const handleToDate = (date) => {
    values[searchFromKeys.ToDate] = date;
    changeHandler;
  }

  const [calendarVisible, setCalendarVisible] = useState(false);

  const toggleCalendar = () => {
    setCalendarVisible(!calendarVisible);
  };

  const [calendarToDateVisible, setCalendarToDateVisible] = useState(false);

  const toggleToDateCalendar = () => {
    setCalendarToDateVisible(!calendarToDateVisible);
  };

  const [moreOptionsVisible, setMoreOptionsVisible] = useState(false);

  const toggleMoreOptions = () => {
    setMoreOptionsVisible(!moreOptionsVisible);
  }

  const [isPets, setIsPets] = useState(false);

  const handleIsPets = () => {
    setIsPets(!isPets);
    values[searchFromKeys.Pets] = !isPets;
    changeHandler;
  }

  const [isBaggage, setIsBaggage] = useState(false);

  const handleIsBaggage = () => {
    setIsBaggage(!isBaggage);
    values[searchFromKeys.Baggage] = !isBaggage;
    changeHandler;
  }

  return (
    <div className='search-menu'>
      <form id="search" method="POST" onSubmit={clickHandler}>
        <div className='cities-inputs'>
        <div className='city-input'>
      <input
        type="text"
        name={searchFromKeys.FromDestination}
        value={values[searchFromKeys.FromDestination]}
        onChange={handleInputChange}
        placeholder="From destination..."
      />
      {filteredCities.length > 0 && (
        <ul>
          {filteredCities.map((city) => (
            <li key={city.id} onClick={() => handleSelectCity(city.name)}>
              {city.name}
            </li>
          )).slice(0, 10)}
        </ul>
      )}
      </div>
      <div className='city-input'>
      <input
        type="text"
        name={searchFromKeys.ToDestination}
        value={values[searchFromKeys.ToDestination]}
        onChange={handleToDesChange}
        placeholder="To destination..."
      />
      {filteredToDesCities.length > 0 && (
        <ul>
          {filteredToDesCities.map((city) => (
            <li key={city.id} onClick={() => handleToDesSelectCity(city.name)}>
              {city.name}
            </li>
          )).slice(0, 10)}
        </ul>
      )}
      </div>
      </div>
      <div>
        <a onClick={toggleMoreOptions}>More options</a>
        {moreOptionsVisible &&
        <div>
          <input 
          type="number"
          name={searchFromKeys.PriceMin}
          value={values[searchFromKeys.PriceMin]}
          onChange={changeHandler}
          placeholder='Min Price'
          />
          <input
          type='number'
          name={searchFromKeys.PriceMax}
          value={values[searchFromKeys.PriceMax]}
          onChange={changeHandler}
          placeholder='Max Price'
          />
        <div>
          <button type='button' onClick={toggleCalendar}>From Date</button>
          {calendarVisible && <Calendar handle={handleFromDate} />}
        </div>
        <div>
          <button type='button' onClick={toggleToDateCalendar}>To Date</button>
          {calendarToDateVisible && <Calendar handle={handleToDate} />}
        </div>
        <div>
          <p>Baggage</p>
          <input
            type="checkbox"
            checked={isBaggage}
            onChange={handleIsBaggage}
          />
          <label>{isBaggage ? 'Checked' : 'Unchecked'}</label>
        </div>
        <div>
          <p>Pets</p>
          <input
            type="checkbox"
            checked={isPets}
            onChange={handleIsPets}
          />
          <label>{isPets ? 'Checked' : 'Unchecked'}</label>
        </div>
        </div>}
      </div>
        <div>
          <button type='submit'>Submit</button>
        </div>
      </form>
    </div>
  );
};
