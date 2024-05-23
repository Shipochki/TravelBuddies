import { errorHandler } from "../utils/common/errorHandler";

const Url = 'https://localhost:7005/api/userblacklist';

export const OnCreateUserBlackListSubmit = async (userBlackListFromKeys) => {
    try {
        const response = await fetch(Url + '/create', {
          method: 'POST',
          mode: "cors",
          headers: {
              'Authorization': `Bearer ${localStorage.accessToken}`,
              'Content-Type': 'application/json'
          },
          body: JSON.stringify(userBlackListFromKeys)
        });
  
          if (response.ok) {
            // Handle successful response
            return response.json();
        }  else {
            // Handle other errors
            console.error('Error:', response.statusText);
            errorHandler(response.status);
        }
      } catch (error) {
        console.error('Error fetching create user black list:', error);
      }
}