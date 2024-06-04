import { errorHandler } from "../utils/common/errorHandler";
import { fetchWrapper } from "../utils/common/fetchWrapper";

const Url = 'https://localhost:7005/api/city';

export const GetAllCities = async () => {
    try {
      // const response = await fetch(Url + '/getcities', {
      //   method: 'GET',
      //   mode: "cors",
      //   headers: {
      //       'Authorization': `Bearer ${localStorage.accessToken}`,
      //       'Content-Type': 'application/json'
      //   }});

      //   if (response.ok) {
      //     // Handle successful response
      //     return await response.json();
      // }  else {
      //     // Handle other errors
      //     console.error('Error:', response.statusText);
      //     //errorHandler(response.status);
      // }

      return await fetchWrapper(Url + '/getcities')
    } catch (error) {
      console.error('Error fetching cities:', error);
    }
  };