import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { GetPostsById } from "../../services/PostService";
import { GetAllCities } from "../../services/CityService";

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
    const {post, setPost} = useState({});
    const {cities, setCities} = useState([]);
    const {id} = useParams();

    useEffect(() => {
        const fetchData = async () => {
            const postData = await GetPostsById(id);
            setPost(postData);

            const citiesData = await GetAllCities();
            setCities(citiesData);
        }
        fetchData();
    }, [id])

    return(
        <div className="edit-post-main">
            <div className="edit-post-content">
                
            </div>
        </div>
    )
}