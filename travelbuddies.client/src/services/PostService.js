const Url = 'https://localhost:7005/api/post';

export const OnSearchSubmit = async (searchFromKeys) => {
    try {
      const response = await fetch(Url + '/allpostbysearch', {
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
      console.error('Error fetching get all post by search:', error);
    }
  };

export const OnCreatePostSubmit = async (createPostFromKeys) => {
  try {
    const response = await fetch(Url + '/create', {
      method: 'POST',
      mode: 'cors',
      headers: {
          'Authorization': `Bearer ${localStorage.accessToken}`,
          'Content-Type': 'application/json'
      },
      body: JSON.stringify(createPostFromKeys)
    });

    if(response.ok){
      return await response.json();
    } else {
      console.error('Error:', response.statusText);
    }
  } catch (error){
    console.error('Error fetching create post', error)
  }
}

export const OnDeletePostSubmit = async (postId) => {
  try{
    const response = await fetch(Url + '/delete', {
      method: 'POST',
      mode: 'cors',
      headers: {
          'Authorization': `Bearer ${localStorage.accessToken}`,
          'Content-Type': 'application/json'
      },
      body: (postId)
    });

    if(response.ok){
      return response.json();
    } else {
      console.error('Error', response.statusText);
    }
  } catch (error){
    console.error('Error fetching delete post', error);
  }
}

export const OnUpdatePostSubmit = async (updatePostFromKeys) => {
  try {
    const response = await fetch(Url + '/update', {
      method: 'POST',
      mode: 'cors',
      headers: {
          'Authorization': `Bearer ${localStorage.accessToken}`,
          'Content-Type': 'application/json'
      },
      body: JSON.stringify(updatePostFromKeys)
    });

    if(response.ok){
      return response.json();
    } else {
      console.error('Error:', response.statusText);
    }
  } catch (error){
    console.error('Error fetching update post', error)
  }
}