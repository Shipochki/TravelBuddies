const Url = 'https://localhost:7005/api/city';

export const GetAllCities = async () => {
    try {
      const response = await fetch(Url + '/getcities', {
        method: 'GET',
        mode: "cors",
        headers: {
            'Authorization': `Bearer ${localStorage.accessToken}`,
            'Content-Type': 'application/json'
        }});

        if (response.ok) {
          // Handle successful response
          return await response.json();
      }  else {
          // Handle other errors
          console.error('Error:', response.statusText);
      }
    } catch (error) {
      console.error('Error fetching cities:', error);
    }
  };