import { fetchWrapper } from "../utils/common/fetchWrapper";

const Url = 'https://localhost:7005/api/group';

export const GetAllGroupByUserId = async () => {
    try {
        const options = {
          'headers': {
            'method': 'GET'
          }
        }
        return await fetchWrapper(Url + '/getusergroupsbyuserid');
      } catch (error) {
        console.error('Error fetching get all group by user id:', error);
      };
}


export const GetGroupById = async (id) => {
  try {
      const options = {
        'headers': {
          'method': 'GET'
        }
      }
      return await fetchWrapper(Url + `/getgroupbyid/${id}`)
    } catch (error) {
      console.error('Error fetching get all group by user id:', error);
    };
}