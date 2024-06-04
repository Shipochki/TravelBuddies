import { errorHandler } from "../utils/common/errorHandler";
import { fetchWrapper } from "../utils/common/fetchWrapper";

const Url = 'https://localhost:7005/api/post';

export const OnSearchSubmit = async () => {
    try {
      const params = new URLSearchParams(location.search);

      // const response = await fetch(Url + `/allpostbysearch?${params.toString()}`, {
      //   method: 'GET',
      //   mode: "cors",
      //   headers: {
      //       'Authorization': `Bearer ${localStorage.accessToken}`,
      //   }
      // });
      
      // if(response.ok){
      //   return await response.json();
      // }else{
      //   console.log(response.statusText);
      //   errorHandler(response.status);
      // }

      return await fetchWrapper(Url + `/allpostbysearch?${params.toString()}`)
    } catch (error) {
      console.error('Error fetching get all post by search:', error);
    }
  };

export const OnCreatePostSubmit = async (createPostFromKeys) => {
  try {
    // const response = await fetch(Url + '/create', {
    //   method: 'POST',
    //   mode: 'cors',
    //   headers: {
    //       'Authorization': `Bearer ${localStorage.accessToken}`,
    //       'Content-Type': 'application/json'
    //   },
    //   body: JSON.stringify(createPostFromKeys)
    // });

    // if(response.ok){
    //   //return await response.json();
    //   return true;
    // } else {
    //   const result = await response.json();
    //   alert(result.detail);
    //   console.error('Error:', response.statusText);
    //   //errorHandler(response.status);
    //   return false;
    // }

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
    // const response = await fetch(Url + `/delete/${postId}`, {
    //   method: 'POST',
    //   mode: 'cors',
    //   headers: {
    //       'Authorization': `Bearer ${localStorage.accessToken}`,
    //       'Content-Type': 'application/json'
    //   },
    // });

    // if(response.ok){
    // } else {
    //   console.error('Error', response.statusText);
    //   errorHandler(response.status);
    // }

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
    // const response = await fetch(Url + '/update', {
    //   method: 'POST',
    //   mode: 'cors',
    //   headers: {
    //       'Authorization': `Bearer ${localStorage.accessToken}`,
    //       'Content-Type': 'application/json'
    //   },
    //   body: JSON.stringify(updatePostFromKeys)
    // });

    // if(response.ok){
    // } else {
    //   console.error('Error:', response.statusText);
    //   errorHandler(response.status);
    // }

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
    // const response = await fetch(Url + `/getpostsbyownerid/${ownerId}`, {
    //   method: 'GET',
    //   mode: "cors",
    //   headers: {
    //       'Authorization': `Bearer ${localStorage.accessToken}`,
    //       'Content-Type': 'application/json'
    //   },
    // });
    
    // if(response.ok){
    //   return response.json();
    // } else {
    //   console.error('Error:', response.statusText);
    //   errorHandler(response.status);
    // }

    return await fetchWrapper(Url + `/getpostsbyownerid/${ownerId}`);
  } catch (error) {
    console.error('Error fetching get post by owner id:', error);
  }
};

export const GetPostById = async (postId) => {
  try {
    // const response = await fetch(Url + `/getpostbyid/${postId}`, {
    //   method: 'GET',
    //   mode: "cors",
    //   headers: {
    //       'Authorization': `Bearer ${localStorage.accessToken}`,
    //       'Content-Type': 'application/json'
    //   },
    // });
    
    // if(response.ok){
    //   return response.json();
    // } else {
    //   console.error('Error:', response.statusText);
    //   errorHandler(response.status);
    // }

    return await fetchWrapper(Url + `/getpostbyid/${postId}`);
  } catch (error) {
    console.error('Error fetching get post by id:', error);
  }
};

export const OnCompletePostById = async (postId) => {
  try {
    // const response = await fetch(Url + `/complete/${postId}`, {
    //   method: 'POST',
    //   mode: "cors",
    //   headers: {
    //       'Authorization': `Bearer ${localStorage.accessToken}`,
    //       'Content-Type': 'application/json'
    //   },
    // });
    
    // if(response.ok){

    // } else {
    //   console.error('Error:', response.statusText);
    //   errorHandler(response.status);
    // }

    const options = {
      method: 'POST'
    }
    await fetchWrapper(Url + `/complete/${postId}`, options)
  } catch (error) {
    console.error('Error fetching complete post by id:', error);
  }
};