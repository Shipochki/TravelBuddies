const Url = 'https://localhost:7005/api/group';

export const GetAllGroupByUserId = async () => {
    try {
        const response = await fetch(Url + '/getusergroupsbyuserid', {
          method: 'GET',
          mode: "cors",
          headers: {
              'Authorization': `Bearer ${localStorage.accessToken}`,
              'Content-Type': 'application/json'
          },
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