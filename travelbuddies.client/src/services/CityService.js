import { fetchWrapper } from "../utils/common/fetchWrapper";

const Url = 'https://localhost:7005/api/city';

export const GetAllCities = async () => {
    try {
      return await fetchWrapper(Url + '/getcities')
    } catch (error) {
      console.error('Error fetching cities:', error);
    }
  };