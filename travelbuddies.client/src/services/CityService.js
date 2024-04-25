const Url = 'https://localhost:7005/api';

export const GetAllCities = async () => {
    try {
      const response = await fetch(Url + '/City/GetCities', {
        method: 'GET',
        mode: "cors",
        headers: {
            'Authorization': `Bearer ${localStorage.accessToken}`,
            'Content-Type': 'application/json'
        }});

        if (response.ok) {
          // Handle successful response
          return response.json();
      }  else {
          // Handle other errors
          console.error('Error:', response.statusText);
      }
    } catch (error) {
      console.error('Error fetching cities:', error);
    }
  };