const Url = 'https://localhost:7005/api/review';

export const GetAllReviewByReciverId = async (reciverId) => {
    try {
        const response = await fetch(Url + '/getallreviewbyreciverid', {
          method: 'POST',
          mode: "cors",
          headers: {
              'Authorization': `Bearer ${localStorage.accessToken}`,
              'Content-Type': 'application/json'
          },
          body: (reciverId)
        });
  
          if (response.ok) {
            // Handle successful response
            return response.json();
        }  else {
            // Handle other errors
            console.error('Error:', response.statusText);
        }
      } catch (error) {
        console.error('Error fetching get all review by reciver id:', error);
      }
};

export const OnCreateReviewSubmit = async (createReviewFromKeys) => {
    try {
        const response = await fetch(Url + '/create', {
          method: 'POST',
          mode: "cors",
          headers: {
              'Authorization': `Bearer ${localStorage.accessToken}`,
              'Content-Type': 'application/json'
          },
          body: JSON.stringify(createReviewFromKeys)
        });
  
          if (response.ok) {
            // Handle successful response
            return response.json();
        }  else {
            // Handle other errors
            console.error('Error:', response.statusText);
        }
      } catch (error) {
        console.error('Error fetching create review:', error);
      }
}

export const OnUpdateReviewSubmit = async (updateReviewFromKeys) => {
    try {
        const response = await fetch(Url + '/update', {
          method: 'POST',
          mode: "cors",
          headers: {
              'Authorization': `Bearer ${localStorage.accessToken}`,
              'Content-Type': 'application/json'
          },
          body: JSON.stringify(updateReviewFromKeys)
        });
  
          if (response.ok) {
            // Handle successful response
            return response.json();
        }  else {
            // Handle other errors
            console.error('Error:', response.statusText);
        }
      } catch (error) {
        console.error('Error fetching update review:', error);
      }
}

export const OnDeleteReviewSubmit = async (reviewId) => {
    try {
        const response = await fetch(Url + '/delete', {
          method: 'POST',
          mode: "cors",
          headers: {
              'Authorization': `Bearer ${localStorage.accessToken}`,
              'Content-Type': 'application/json'
          },
          body: (reviewId)
        });
  
          if (response.ok) {
            // Handle successful response
            return response.json();
        }  else {
            // Handle other errors
            console.error('Error:', response.statusText);
        }
      } catch (error) {
        console.error('Error fetching delete review:', error);
      }
}