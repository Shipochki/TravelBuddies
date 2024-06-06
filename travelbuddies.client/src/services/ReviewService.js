import { fetchWrapper } from "../utils/common/fetchWrapper";


const Url = 'https://localhost:7005/api/review';

export const GetAllReviewByReciverId = async () => {
  const params = new URLSearchParams(location.search);

    try {
        return await fetchWrapper(Url +`/getallreviewbyreciverid?${params.toString()}`);
      } catch (error) {
        console.error('Error fetching get all review by reciver id:', error);
      }
};

export const OnCreateReviewSubmit = async (createReviewFromKeys) => {
    try {
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
        const options = {
          method: 'POST',
        }
        await fetchWrapper(Url + `/delete/${reviewId}`, options)
      } catch (error) {
        console.error('Error fetching delete review:', error);
      }
}