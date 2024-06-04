import { errorHandler } from "../utils/common/errorHandler";
import { fetchWrapper } from "../utils/common/fetchWrapper";


const Url = 'https://localhost:7005/api/review';

export const GetAllReviewByReciverId = async () => {
  const params = new URLSearchParams(location.search);

    try {
        // const response = await fetch(Url + `/getallreviewbyreciverid?${params.toString()}`, {
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
        //     errorHandler(response.status);
        // }

        return await fetchWrapper(Url +`/getallreviewbyreciverid?${params.toString()}`);
      } catch (error) {
        console.error('Error fetching get all review by reciver id:', error);
      }
};

export const OnCreateReviewSubmit = async (createReviewFromKeys) => {
    try {
        // const response = await fetch(Url + '/create', {
        //   method: 'POST',
        //   mode: "cors",
        //   headers: {
        //       'Authorization': `Bearer ${localStorage.accessToken}`,
        //       'Content-Type': 'application/json'
        //   },
        //   body: JSON.stringify(createReviewFromKeys)
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
          body: JSON.stringify(createReviewFromKeys)
        }
        await fetchWrapper(Url + '/create', options)
      } catch (error) {
        console.error('Error fetching create review:', error);
      }
}

export const OnUpdateReviewSubmit = async (updateReviewFromKeys) => {
    try {
        // const response = await fetch(Url + '/update', {
        //   method: 'POST',
        //   mode: "cors",
        //   headers: {
        //       'Authorization': `Bearer ${localStorage.accessToken}`,
        //       'Content-Type': 'application/json'
        //   },
        //   body: JSON.stringify(updateReviewFromKeys)
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
          body: JSON.stringify(updateReviewFromKeys)
        }
        await fetchWrapper(Url + '/update', options)
      } catch (error) {
        console.error('Error fetching update review:', error);
      }
}

export const OnDeleteReviewSubmit = async (reviewId) => {
    try {
        // const response = await fetch(Url + `/delete/${reviewId}`, {
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
        //     errorHandler(response.status);
        // }
        const options = {
          method: 'POST',
        }
        await fetchWrapper(Url + `/delete/${reviewId}`, options)
      } catch (error) {
        console.error('Error fetching delete review:', error);
      }
}