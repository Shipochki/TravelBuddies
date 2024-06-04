import { errorHandler } from "../utils/common/errorHandler";
import { fetchWrapper } from "../utils/common/fetchWrapper";

const Url = 'https://localhost:7005/api/message';

export const OnCreateMessageSubmit = async (createMessageFromKeys) => {
    try {
        // const response = await fetch(Url + '/create', {
        //   method: 'POST',
        //   mode: "cors",
        //   headers: {
        //       'Authorization': `Bearer ${localStorage.accessToken}`,
        //       'Content-Type': 'application/json'
        //   },
        //   body: JSON.stringify(createMessageFromKeys)
        // });
  
        //   if (response.ok) {
        //     // Handle successful response
        //     return await response.json();
        // }  else {
        //     // Handle other errors
        //     alert(await response.json());
        //     console.error('Error:', response.statusText);
        //     //errorHandler(response.status);
        // }

        const options = {
          method: 'POST',
          body: JSON.stringify(createMessageFromKeys)
        }
        return await fetchWrapper(Url + '/create', options)
      } catch (error) {
        console.error('Error fetching create message:', error);
      }
}

export const OnUpdateMessageSubmit = async (updateMessageFromKeys) => {
    try {
        // const response = await fetch(Url + '/update', {
        //   method: 'POST',
        //   mode: "cors",
        //   headers: {
        //       'Authorization': `Bearer ${localStorage.accessToken}`,
        //       'Content-Type': 'application/json'
        //   },
        //   body: JSON.stringify(updateMessageFromKeys)
        // });
  
        //   if (response.ok) {
        //     // Handle successful response
        // }  else {
        //     // Handle other errors
        //     alert(await response.json());
        //     console.error('Error:', response.statusText);
        //     // errorHandler(response.status);
        // }
        const options = {
          method: 'POST',
          body: JSON.stringify(updateMessageFromKeys)
        }
        return await fetchWrapper(Url + '/update', options)
      } catch (error) {
        console.error('Error fetching update message:', error);
      }
}

export const OnDeleteMessageSubmit = async (messageId) => {
    try {
        // const response = await fetch(Url + `/delete/${messageId}`, {
        //   method: 'POST',
        //   mode: "cors",
        //   headers: {
        //       'Authorization': `Bearer ${localStorage.accessToken}`,
        //       'Content-Type': 'application/json'
        //   },
        // });
  
        //   if (response.ok) {
        //     // Handle successful response
        // }  else {
        //     // Handle other errors
        //     console.error('Error:', response.statusText);
        //     errorHandler(response.status)
        // }

        const options = {
          method: 'POST',
        }
        return await fetchWrapper(Url + `/delete/${messageId}`, options)
      } catch (error) {
        console.error('Error fetching delete message:', error);
      }
}

//not used
export const GetAllMessageByGroupId = async (groupId) => {
    try {
        // const response = await fetch(Url + `/getmessagesbygroupid/${groupId}`, {
        //   method: 'GET',
        //   mode: "cors",
        //   headers: {
        //       'Authorization': `Bearer ${localStorage.accessToken}`,
        //       'Content-Type': 'application/json'
        //   }
        // });
  
        //   if (response.ok) {
        //     // Handle successful response
        //     return response.json();
        // }  else {
        //     // Handle other errors
        //     console.error('Error:', response.statusText);
        //     errorHandler(response.status)
        // }
        
      } catch (error) {
        console.error('Error fetching join group:', error);
      }
}