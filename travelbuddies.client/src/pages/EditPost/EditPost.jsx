import './EditPost.css';
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { GetPostById } from "../../services/PostService";
import { GetAllCities } from "../../services/CityService";
import { TextField } from "@mui/material";
import { useForm } from "../../utils/hooks/useForm";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowRight } from "@fortawesome/free-solid-svg-icons";

const EditPostFromKeys = {
    Id: 'id',
    FromDestination: 'fromDestinationCityId',
    ToDestination: 'toDestinationCityId',
    Description: 'description',
    PricePerSeat: 'pricePerSeat',
    FreeSeats: 'freeSeats',
    Baggage: 'baggage',
    Pets: 'pets',
    Date: 'date', 
    Time: 'time',
    PaymentType: 'payType'
}

export const EditPost = () => {
    const [post, setPost] = useState({});
    const [cities, setCities] = useState([]);
    const [filteredCities, setFilteredCities] = useState([]);
    const [filteredToDesCities, setFilteredToDesCities] = useState([]);
    const {id} = useParams();

    const { values, changeHandler, onSubmit } = useForm({
        [EditPostFromKeys.Id]: post.id,
        [EditPostFromKeys.FromDestination]: post.fromDestinationName,
        [EditPostFromKeys.ToDestination]: post.toDestinationName,
        [EditPostFromKeys.Description]: post.description,
        [EditPostFromKeys.PricePerSeat]: post.pricePerSeat,
        [EditPostFromKeys.FreeSeats]: post.freeSeats,
        [EditPostFromKeys.Baggage]: post.baggage,
        [EditPostFromKeys.Pets]: post.pets,
        [EditPostFromKeys.Date]: '',
        [EditPostFromKeys.Time]: '',
        [EditPostFromKeys.PaymentType]: 0,
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
            values[EditPostFromKeys.Date] = postData.dateAndTime.split().slice(0, 1);
            values[EditPostFromKeys.Time] = postData.dateAndTime.split().slice(1, 1);
        }
        fetchData();
    }, [id])

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

    return(
        <div className="edit-post-main">
            <div className="edit-post-header">
                <h2>Edit Post</h2>
            </div>
            <div className="edit-post-content">
                <form className="edit-post-form" id="edit-post">
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
                </form>
            </div>
        </div>
    )
}