const Url = 'https://localhost:7005/api/usergroup';

export const OnJoinGroupSubmit = async (groupId) => {
    try {
        const response = await fetch(Url + '/joingroup', {
          method: 'POST',
          mode: "cors",
          headers: {
              'Authorization': `Bearer ${localStorage.accessToken}`,
              'Content-Type': 'application/json'
          },
          body: (groupId)
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
      };
}

export const OnLeaveGroupSubmit = async (groupId) => {
    try {
        const response = await fetch(Url + '/leavegroup', {
          method: 'POST',
          mode: "cors",
          headers: {
              'Authorization': `Bearer ${localStorage.accessToken}`,
              'Content-Type': 'application/json'
          },
          body: (groupId)
        });
  
          if (response.ok) {
            // Handle successful response
            return response.json();
        }  else {
            // Handle other errors
            console.error('Error:', response.statusText);
        }
      } catch (error) {
        console.error('Error fetching leave group:', error);
      };
}

export const OnRemoveUserFromGroupSubmit = async (removeFromGroupKeys) => {
    try {
        const response = await fetch(Url + '/removeuserfromgroup', {
          method: 'POST',
          mode: "cors",
          headers: {
              'Authorization': `Bearer ${localStorage.accessToken}`,
              'Content-Type': 'application/json'
          },
          body: JSON.stringify(removeFromGroupKeys)
        });
  
          if (response.ok) {
            // Handle successful response
            return response.json();
        }  else {
            // Handle other errors
            console.error('Error:', response.statusText);
        }
      } catch (error) {
        console.error('Error fetching remove user from group:', error);
      }
}