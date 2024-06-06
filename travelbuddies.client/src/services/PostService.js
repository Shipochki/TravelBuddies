import { fetchWrapper } from "../utils/common/fetchWrapper";

const Url = 'https://localhost:7005/api/post';

export const OnSearchSubmit = async () => {
    try {
      const params = new URLSearchParams(location.search);

      return await fetchWrapper(Url + `/allpostbysearch?${params.toString()}`)
    } catch (error) {
      console.error('Error fetching get all post by search:', error);
    }
  };

export const OnCreatePostSubmit = async (createPostFromKeys) => {
  try {
    const options = {
      method: 'POST',
      body: JSON.stringify(createPostFromKeys)
    }

    const result = await fetchWrapper(Url + '/create', options);

    return result ? true : false;
  } catch (error){
    console.error('Error fetching create post', error)
  }
}

export const OnDeletePostSubmit = async (postId) => {
  try{
    const options = {
      method: 'POST'
    }
    await fetchWrapper(Url + `/delete/${postId}`, options);
  } catch (error){
    console.error('Error fetching delete post', error);
  }
}

export const OnUpdatePostSubmit = async (updatePostFromKeys) => {
  try {
    const options = {
      method: 'POST',
      body: JSON.stringify(updatePostFromKeys)
    }
    await fetchWrapper(Url + '/update', options);
  } catch (error){
    console.error('Error fetching update post', error)
  }
}

export const GetPostsByOwnerId = async (ownerId) => {
  try {
    return await fetchWrapper(Url + `/getpostsbyownerid/${ownerId}`);
  } catch (error) {
    console.error('Error fetching get post by owner id:', error);
  }
};

export const GetPostById = async (postId) => {
  try {
    return await fetchWrapper(Url + `/getpostbyid/${postId}`);
  } catch (error) {
    console.error('Error fetching get post by id:', error);
  }
};

export const OnCompletePostById = async (postId) => {
  try {
    const options = {
      method: 'POST'
    }
    await fetchWrapper(Url + `/complete/${postId}`, options)
  } catch (error) {
    console.error('Error fetching complete post by id:', error);
  }
};