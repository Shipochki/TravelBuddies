import { errorHandler } from "../utils/common/errorHandler";
import { fetchWrapper } from "../utils/common/fetchWrapper";

const Url = 'https://localhost:7005/api/userblacklist';

export const OnCreateUserBlackListSubmit = async (userBlackListFromKeys) => {
    try {
        // const response = await fetch(Url + '/create', {
        //   method: 'POST',
        //   mode: "cors",
        //   headers: {
        //       'Authorization': `Bearer ${localStorage.accessToken}`,
        //       'Content-Type': 'application/json'
        //   },
        //   body: JSON.stringify(userBlackListFromKeys)
        // });
  
        //   if (response.ok) {
        //     // Handle successful response
        // }  else {
        //     // Handle other errors
        //     console.error('Error:', response.statusText);
        //     errorHandler(response.status);
        // }
        const options = {
          method: 'POST',
          body: JSON.stringify(userBlackListFromKeys)
        }
        await fetchWrapper(Url + '/create', options)
      } catch (error) {
        console.error('Error fetching create user black list:', error);
      }
}