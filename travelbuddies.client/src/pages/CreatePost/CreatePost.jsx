import { useState } from "react";
import { useForm } from "../../utils/hooks/useForm";
import { OnCreatePostSubmit } from "../../services/PostService";
import Calendar from "../../components/Calendar/Calendar";

const createPostFromKeys = {
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
        [createPostFromKeys.FromDestination]: '',
        [createPostFromKeys.ToDestination]: '',
        [createPostFromKeys.Description]: '',
        [createPostFromKeys.PricePerSeat]: '',
        [createPostFromKeys.FreeSeats]: '',
        [createPostFromKeys.Baggage]: '',
        [createPostFromKeys.Pets]: '',
        [createPostFromKeys.Date]: '',
        [createPostFromKeys.Time]: ''
    })

    const clickHandler = () => {
        const fromdes = cities.filter(c => c.name == values[createPostFromKeys.FromDestination])[0];
        const todes = cities.filter(c => c.name == values[createPostFromKeys.ToDestination])[0];
    
        if(fromdes == '' || todes == ''
            || [createPostFromKeys.Description] == ''
            || [createPostFromKeys.PricePerSeat] == ''
            || [createPostFromKeys.FreeSeats] == ''
            || [createPostFromKeys.FreeSeats] <= 0
            || [createPostFromKeys.Baggage] == ''
            || [createPostFromKeys.Pets] == ''
            || [createPostFromKeys.DataAndTime] == ''){
          return;
        }
    
        values[createPostFromKeys.FromDestination] = fromdes.id;
        values[createPostFromKeys.ToDestination] = todes.id;
        
        setPosts(OnCreatePostSubmit(values));
    
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
        values[createPostFromKeys.FromDestination] = value;
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
        values[createPostFromKeys.ToDestination] = value;
    };

    const handleFromDesSelectCity = (city) => {
        values[createPostFromKeys.FromDestination] = city;
        changeHandler;
        setFilteredCities([]);
    };

    const handleToDesSelectCity = (city) => {
    values[createPostFromKeys.ToDestination] = city;
    changeHandler;
    setFilteredToDesCities([]);
    }; 

    const handleDate = (date) => {
        console.log(date);
        values[createPostFromKeys.Date] = date;
        changeHandler;
      }

    const [calendarVisible, setCalendarVisible] = useState(false);

    const toggleCalendar = () => {
      setCalendarVisible(!calendarVisible);
    };

    const [isPets, setIsPets] = useState(false);

  const handleIsPets = () => {
    setIsPets(!isPets);
    values[createPostFromKeys.Pets] = !isPets;
    changeHandler;
  }

  const [isBaggage, setIsBaggage] = useState(false);

  const handleIsBaggage = () => {
    setIsBaggage(!isBaggage);
    values[createPostFromKeys.Baggage] = !isBaggage;
    changeHandler;
  }

    return(
        <div className="createPost-main">
            <form id="search" method="POST" onSubmit={clickHandler}>
                <div className='cities-inputs'>
                    <div className='city-input'>
                        <input
                            type="text"
                            name={createPostFromKeys.FromDestination}
                            value={values[createPostFromKeys.FromDestination]}
                            onChange={handleFromDesChange}
                            placeholder="From destination..."
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
                    <div className='city-input'>
                        <input
                            type="text"
                            name={createPostFromKeys.ToDestination}
                            value={values[createPostFromKeys.ToDestination]}
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
                <input 
                    type="textbox"
                    name={createPostFromKeys.Description}
                    value={values[createPostFromKeys.Description]}
                    onChange={changeHandler}
                    placeholder="Description..."
                />
                <div>
                    <button type='button' onClick={toggleCalendar}>Date</button>
                    {calendarVisible && 
                        <div>
                            <Calendar handle={handleDate}/>
                            <input 
                                type="time"
                                name={createPostFromKeys.Time}
                                value={values[createPostFromKeys.Time]}
                                onChange={changeHandler}
                                placeholder="Time..."
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
                        name={createPostFromKeys.PricePerSeat}
                        value={values[createPostFromKeys.PricePerSeat]}
                        onChange={changeHandler}
                        placeholder="10$"
                    />
                </div>
                <div>
                    <label>Free seats</label>
                    <input
                        type="number"
                        name={createPostFromKeys.FreeSeats}
                        value={values[createPostFromKeys.FreeSeats]}
                        onChange={changeHandler}
                        placeholder="3"
                    />
                </div>
            </form>
        </div>
    )
}