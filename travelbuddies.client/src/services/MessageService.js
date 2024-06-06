import { fetchWrapper } from "../utils/common/fetchWrapper";

const Url = 'https://localhost:7005/api/message';

export const OnCreateMessageSubmit = async (createMessageFromKeys) => {
    try {
        const options = {
          method: 'POST',
          body: JSON.stringify(createMessageFromKeys)
        }
        return await fetchWrapper(Url + '/create', options)
      } catch (error) {
        console.error('Error fetching create message:', error);
      }
}

export const OnUpdateMessageSubmit = async (updateMessageFromKeys) => {
    try {
        const options = {
          method: 'POST',
          body: JSON.stringify(updateMessageFromKeys)
        }
        return await fetchWrapper(Url + '/update', options)
      } catch (error) {
        console.error('Error fetching update message:', error);
      }
}

export const OnDeleteMessageSubmit = async (messageId) => {
    try {
        const options = {
          method: 'POST',
        }
        return await fetchWrapper(Url + `/delete/${messageId}`, options)
      } catch (error) {
        console.error('Error fetching delete message:', error);
      }
}