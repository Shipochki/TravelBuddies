const Url = 'https://localhost:7005/api/message';

export const OnCreateMessageSubmit = async (createMessageFromKeys) => {
    try {
        const response = await fetch(Url + '/create', {
          method: 'POST',
          mode: "cors",
          headers: {
              'Authorization': `Bearer ${localStorage.accessToken}`,
              'Content-Type': 'application/json'
          },
          body: JSON.stringify(createMessageFromKeys)
        });
  
          if (response.ok) {
            // Handle successful response
            return response.json();
        }  else {
            // Handle other errors
            console.error('Error:', response.statusText);
        }
      } catch (error) {
        console.error('Error fetching create message:', error);
      }
}

export const OnUpdateMessageSubmit = async (updateMessageFromKeys) => {
    try {
        const response = await fetch(Url + '/update', {
          method: 'POST',
          mode: "cors",
          headers: {
              'Authorization': `Bearer ${localStorage.accessToken}`,
              'Content-Type': 'application/json'
          },
          body: JSON.stringify(updateMessageFromKeys)
        });
  
          if (response.ok) {
            // Handle successful response
            return response.json();
        }  else {
            // Handle other errors
            console.error('Error:', response.statusText);
        }
      } catch (error) {
        console.error('Error fetching update message:', error);
      }
}

export const OnDeleteMessageSubmit = async (messageId) => {
    try {
        const response = await fetch(Url + '/delete', {
          method: 'POST',
          mode: "cors",
          headers: {
              'Authorization': `Bearer ${localStorage.accessToken}`,
              'Content-Type': 'application/json'
          },
          body: (messageId)
        });
  
          if (response.ok) {
            // Handle successful response
            return response.json();
        }  else {
            // Handle other errors
            console.error('Error:', response.statusText);
        }
      } catch (error) {
        console.error('Error fetching delete message:', error);
      }
}

export const GetAllMessageByGroupId = async (groupId) => {
    try {
        const response = await fetch(Url + `/getmessagesbygroupid/${groupId}`, {
          method: 'GET',
          mode: "cors",
          headers: {
              'Authorization': `Bearer ${localStorage.accessToken}`,
              'Content-Type': 'application/json'
          }
        });
  
          if (response.ok) {
            // Handle successful response
            return response.json();
        }  else {
            // Handle other errors
            console.error('Error:', response.statusText);
        }
      } catch (error) {
        console.error('Error fetching join group:', error);
      }
}