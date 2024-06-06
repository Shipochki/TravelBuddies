import { fetchWrapper } from "../utils/common/fetchWrapper";

const Url = 'https://localhost:7005/api/userblacklist';

export const OnCreateUserBlackListSubmit = async (userBlackListFromKeys) => {
    try {
        const options = {
          method: 'POST',
          body: JSON.stringify(userBlackListFromKeys)
        }
        await fetchWrapper(Url + '/create', options)
      } catch (error) {
        console.error('Error fetching create user black list:', error);
      }
}