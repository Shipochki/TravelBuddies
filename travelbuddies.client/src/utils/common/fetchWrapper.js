import { errorHandler } from "./errorHandler";

export const fetchWrapper = (url, options = {}) => {
    const token = localStorage.getItem('accessToken'); 
    const defaultHeaders = {
      'Content-Type': 'application/json',
      Authorization: token ? `Bearer ${token}` : undefined,
    };
  
    options.headers = { ...defaultHeaders, ...options.headers };
    options.mode = 'cors'
  
    return fetch(url, options)
      .then(async response => {
        if (!response.ok) {
          // const error = await response.json();
          // return Promise.reject(error);
          console.error('Error:', response.statusText);
          errorHandler(response.status);
        }
        return response.json();
      })
      .catch(error => {
        console.error('Fetch error:', error);
        return Promise.reject(error);
      });
  };