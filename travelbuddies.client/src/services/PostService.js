const Url = 'https://localhost:7005/api';

export const OnSearchSubmit = async (searchFromKeys) => {
    try {
      const response = await fetch(Url + '/Post/AllPostBySearch', {
        method: 'POST',
        mode: "cors",
        headers: {
            'Authorization': `Bearer ${localStorage.accessToken}`,
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(searchFromKeys)
      });

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