import { fetchWrapper } from "../utils/common/fetchWrapper";

const Url = 'https://localhost:7005/api/usergroup';

export const OnJoinGroupSubmit = async (groupId) => {
    try {

        const options = {
          method: 'POST',
        }
        await fetchWrapper(Url + `/joingroup/${groupId}`, options)
      } catch (error) {
        console.error('Error fetching join group:', error);
      };
}

export const OnLeaveGroupSubmit = async (groupId) => {
    try {
        const options = {
          method: 'POST',
        }
        await fetchWrapper(Url + `/leavegroup/${groupId}`, options)
      } catch (error) {
        console.error('Error fetching leave group:', error);
      };
}

export const OnRemoveUserFromGroupSubmit = async (removeFromGroupKeys) => {
    try {
        const options = {
          method: 'POST',
          body: JSON.stringify(removeFromGroupKeys)
        }
        await fetchWrapper(Url + `/removeuserfromgroup`, options)
      } catch (error) {
        console.error('Error fetching remove user from group:', error);
      }
}