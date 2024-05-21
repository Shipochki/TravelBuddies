import React, { useState, useEffect, useContext } from 'react';
import './Search.css'
import Calendar from '../../components/Calendar/Calendar';
import { useForm } from '../../utils/hooks/useForm';
import { OnSearchSubmit } from '../../services/PostService';
import { GlobalContext } from '../../utils/contexts/GlobalContext'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowRight, faCalendar, faCalendarDays, faCarSide, faCheck, faSliders, faX } from '@fortawesome/free-solid-svg-icons';

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
  const {OnSetPosts} = useContext(GlobalContext);

  const { values, changeHandler, onSubmit } = useForm({
    [searchFromKeys.FromDestination]: '',
    [searchFromKeys.ToDestination]: '',
    [searchFromKeys.FromDate]: null,
    [searchFromKeys.ToDate]: null,
    [searchFromKeys.Baggage]: null,
    [searchFromKeys.Pets]: null,
    [searchFromKeys.PriceMin]: null,
    [searchFromKeys.PriceMax]: null,
  }, OnSearchSubmit);

  const clickSubmit = async (e) => {
    e.preventDefault();
    const fromdes = cities.filter(c => c.name == values[searchFromKeys.FromDestination])[0];
    const todes = cities.filter(c => c.name == values[searchFromKeys.ToDestination])[0];

    if(!fromdes || !todes){
      alert("Invalid cities")
    }else{
      values[searchFromKeys.FromDestination] = fromdes.id;
      values[searchFromKeys.ToDestination] = todes.id;
    
      const result = await OnSearchSubmit(values);

      OnSetPosts(result);
    }
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
      <div className='search-header'>
        <h2>You can search your travel here</h2>
      </div>
      <form id="search" method="POST" onSubmit={clickSubmit}>
        <div className='cities-inputs'>
        <div className='city-input'>
      <input
        type="text"
        name={searchFromKeys.FromDestination}
        value={values[searchFromKeys.FromDestination]}
        onChange={handleInputChange}
        placeholder="From destination..."
        autoComplete='off'
      />
      {filteredCities.length > 0 && (
        <ul>
          {filteredCities.map((city) => (
            <li key={city.id} onClick={() => handleSelectCity(city.name)}>
              {city.name}
            </li>
          )).slice(0, 8)}
        </ul>
      )}
        </div>
        {<FontAwesomeIcon icon={faArrowRight}/>}
        <div className='city-input'>
      <input
        type="text"
        name={searchFromKeys.ToDestination}
        value={values[searchFromKeys.ToDestination]}
        onChange={handleToDesChange}
        placeholder="To destination..."
        autoComplete='off'
      />
      {filteredToDesCities.length > 0 && (
        <ul>
          {filteredToDesCities.map((city) => (
            <li key={city.id} onClick={() => handleToDesSelectCity(city.name)}>
              {city.name}
            </li>
          )).slice(0, 8)}
        </ul>
      )}
        </div>
      </div>
      <div className='more-options'>
        <a className='more-options-a' onClick={toggleMoreOptions}>
            {<FontAwesomeIcon icon={faSliders}/>}
            <p>More options</p>
          </a>
        {moreOptionsVisible &&
        <div className='more-options-content'>
          <input 
          type="number"
          name={searchFromKeys.PriceMin}
          value={values[searchFromKeys.PriceMin]}
          onChange={changeHandler}
          placeholder='Min Price'
          className='more-option-price'
          />
          <input
          type='number'
          name={searchFromKeys.PriceMax}
          value={values[searchFromKeys.PriceMax]}
          onChange={changeHandler}
          placeholder='Max Price'
          className='more-option-price'
          />
        <div className='more-options-calendar'>
          <div className='options-calendar-input-button'>
            <input
            type='text'
            name={searchFromKeys.FromDate}
            value={values[searchFromKeys.FromDate]}
            placeholder='From Date: 05.21.2024'
            onChange={changeHandler}
            className='options-calendar-input'
            />
            <button type='button' onClick={toggleCalendar}><FontAwesomeIcon icon={!calendarVisible ? faCalendarDays : faCheck}/></button>
          </div>
          <div className='search-calendar'>
            {calendarVisible && <Calendar handle={handleFromDate} />}
          </div>
        </div>
        <div className='more-options-calendar'>
        <div className='options-calendar-input-button'>
          <input
            type='text'
            name={searchFromKeys.ToDate}
            value={values[searchFromKeys.ToDate]}
            placeholder='To Date: 05.29.2024'
            onChange={changeHandler}
            className='options-calendar-input'
            />
            <button type='button' onClick={toggleToDateCalendar}><FontAwesomeIcon icon={!calendarToDateVisible ? faCalendarDays : faCheck}/></button>
            </div>
            <div className='search-calendar'>
              {calendarToDateVisible && <Calendar handle={handleToDate} />}
            </div>
        </div>
        <div className='more-options-boolean'>
          <p>Baggage</p>
          <input
            type="checkbox"
            checked={isBaggage}
            onChange={handleIsBaggage}
          />
        </div>
        <div className='more-options-boolean'>
          <p>Pets</p>
          <input
            type="checkbox"
            checked={isPets}
            onChange={handleIsPets}
          />
        </div>
        </div>}
      </div>
        <div>
          <button className='submit-button' type='submit'>Search</button>
        </div>
      </form>
    </div>
  );
};
