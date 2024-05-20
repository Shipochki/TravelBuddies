import { useState } from "react";
import { useForm } from "../../utils/hooks/useForm";
import { OnCreatePostSubmit } from "../../services/PostService";
import { Link } from "react-router-dom";
import './CreatePost.css'
import Calendar from "../../components/Calendar/Calendar";
import { NotDriver } from "../../components/NotDriver/NotDriver";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowRight, faCalendarDays, faCheck } from "@fortawesome/free-solid-svg-icons";
import { TimePicker } from "../../components/TimePicker/TimePicker";

const CreatePostFromKeys = {
    FromDestination: 'fromDestinationCityId',
    ToDestination: 'toDestinationCityId',
    Description: 'description',
    PricePerSeat: 'pricePerSeat',
    FreeSeats: 'freeSeats',
    Baggage: 'baggage',
    Pets: 'pets',
    Date: 'date', 
    Time: 'time'
}

export const CreatePost = ({cities}) => {
    const {values, changeHandler, onSubmit } = useForm({
        [CreatePostFromKeys.FromDestination]: '',
        [CreatePostFromKeys.ToDestination]: '',
        [CreatePostFromKeys.Description]: '',
        [CreatePostFromKeys.PricePerSeat]: '',
        [CreatePostFromKeys.FreeSeats]: '',
        [CreatePostFromKeys.Baggage]: '',
        [CreatePostFromKeys.Pets]: '',
        [CreatePostFromKeys.Date]: '',
        [CreatePostFromKeys.Time]: ''
    })

    const clickHandler = () => {
        const fromdes = cities.filter(c => c.name == values[CreatePostFromKeys.FromDestination])[0];
        const todes = cities.filter(c => c.name == values[CreatePostFromKeys.ToDestination])[0];
    
        if(fromdes == '' || todes == ''
            || values[CreatePostFromKeys.Description] == ''
            || values[CreatePostFromKeys.PricePerSeat] == ''
            || values[CreatePostFromKeys.FreeSeats] == ''
            || values[CreatePostFromKeys.FreeSeats] <= 0
            || values[CreatePostFromKeys.Baggage] == ''
            || values[CreatePostFromKeys.Pets] == ''
            || values[CreatePostFromKeys.Date] == ''
            || values[CreatePostFromKeys.Time] == ''){
          return;
        }
    
        values[CreatePostFromKeys.FromDestination] = fromdes.id;
        values[CreatePostFromKeys.ToDestination] = todes.id;
        values[CreatePostFromKeys.FreeSeats] = Number(values[CreatePostFromKeys.FreeSeats]);
        values[CreatePostFromKeys.PricePerSeat] = Number(values[CreatePostFromKeys.PricePerSeat]);

        OnCreatePostSubmit(values);
    
        navigate('/driverHome');
      }

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
    const value = event.target.value // Convert input value to lowercase
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
      }

    const [calendarVisible, setCalendarVisible] = useState(false);

    const toggleCalendar = () => {
      setCalendarVisible(!calendarVisible);
    };

    const [isPets, setIsPets] = useState(false);

  const handleIsPets = () => {
    setIsPets(!isPets);
    values[CreatePostFromKeys.Pets] = !isPets;
    changeHandler;
  }

  const [isBaggage, setIsBaggage] = useState(false);

  const handleIsBaggage = () => {
    setIsBaggage(!isBaggage);
    values[CreatePostFromKeys.Baggage] = !isBaggage;
    changeHandler;
  }

    return(
        <div className="create-post-main">
            {localStorage.role == 'driver' ? (
                <>
                <div className="create-post-header">
                    <h2>Create Post</h2>
                </div>
                <form id="create-post" method="POST" onSubmit={clickHandler}>
                <div className='cities-inputs'>
                    <div className='city-input'>
                        <input
                            type="text"
                            name={CreatePostFromKeys.FromDestination}
                            value={values[CreatePostFromKeys.FromDestination]}
                            onChange={handleFromDesChange}
                            placeholder="From destination..."
                            autoComplete="off"
                        />
                        {filteredCities.length > 0 && (
                            <ul>
                                {filteredCities.map((city) => (
                                    <li key={city.id} onClick={() => handleFromDesSelectCity(city.name)}>
                                        {city.name}
                                    </li>
                                )).slice(0, 10)}
                            </ul>
                        )}
                    </div>
                    <FontAwesomeIcon icon={faArrowRight}/>
                    <div className='city-input'>
                        <input
                            type="text"
                            name={CreatePostFromKeys.ToDestination}
                            value={values[CreatePostFromKeys.ToDestination]}
                            onChange={handleToDesChange}
                            placeholder="To destination..."
                            autoComplete="off"
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
                <textarea 
                    type='text'
                    className="createpost-description"
                    name={CreatePostFromKeys.Description}
                    value={values[CreatePostFromKeys.Description]}
                    onChange={changeHandler}
                    placeholder="Description..."
                />
                <div className="create-post-date-time">
                    <input 
                        type="text"
                        id="date"
                        className="create-post-date"
                        value={values[CreatePostFromKeys.Date.slice(0, 17)]}
                        placeholder="Choose date"
                        disabled
                    />
                    <input
                        type="text"
                        id="time"
                        className="create-post-date"
                        value={values[CreatePostFromKeys.Time]}
                        placeholder="Choose time"
                        disabled
                    />
                <button type='button' onClick={toggleCalendar}><FontAwesomeIcon icon={!calendarVisible ? faCalendarDays : faCheck}/></button>
                    {calendarVisible && 
                        <div className="calendar-time">
                            <Calendar handle={handleDate}/>
                            <input 
                                type="time"
                                className="create-post-time-input"
                                name={CreatePostFromKeys.Time}
                                value={values[CreatePostFromKeys.Time]}
                                onChange={changeHandler}
                            />
                        </div>}
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
                <div>
                    <label>Price per seat</label>
                    <input
                        type="number"
                        name={CreatePostFromKeys.PricePerSeat}
                        value={values[CreatePostFromKeys.PricePerSeat]}
                        onChange={changeHandler}
                        placeholder="10$"
                    />
                </div>
                <div>
                    <label>Free seats</label>
                    <input
                        type="number"
                        name={CreatePostFromKeys.FreeSeats}
                        value={values[CreatePostFromKeys.FreeSeats]}
                        onChange={changeHandler}
                        placeholder="3"
                    />
                </div>
                <button type="submit">Add</button>
                </form>
                </>
            ) : (
                <NotDriver />
            )}
            
        </div>
    )
}