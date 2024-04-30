import { parseJwt } from "../utils/common/parsers";

const Url = 'https://localhost:7005/api/user';

export const OnLoginSubmit = async (loginFromKeys) => {
    try {
      const response = await fetch(Url + `/login`, {
        method: "POST", // GET, POST, PUT, DELETE, etc.
        mode: "cors", // no-cors,cors, same-origin
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(loginFromKeys)
      });

      const { token: accessToken } = await response.json();

      const { 
        nameId: userId, 
        sub: username, 
        role: role,
        fullname: fullname
      } = parseJwt(accessToken);

      localStorage.setItem('accessToken', accessToken);
      localStorage.setItem('username', username);
      localStorage.setItem('userId', userId);
      localStorage.setItem('role', role);
      localStorage.setItem('fullname', fullname);

      window.location.assign('/search')
    } catch (error) {
      console.log("Error with login");
    }
  }

export const OnRegisterSubmit = async (registerFromKeys) => {
  console.log(document.querySelector("#profilepicture"));
    const formData = new FormData();
    formData.append('firstname', registerFromKeys.firstname);
    formData.append('lastname', registerFromKeys.lastname);
    formData.append('email', registerFromKeys.email);
    formData.append('city', registerFromKeys.city);
    formData.append('country', registerFromKeys.country);
    formData.append('password', registerFromKeys.password);
    formData.append('profilepicture', document.querySelector("#profilepicture").files[0])

    try {
      const response = await fetch(Url + `/register`, {
        method: "POST", // GET, POST, PUT, DELETE, etc.
        mode: "cors", // no-cors,cors, same-origin
        body: formData,

      });

      const result = await response.json();
    } catch (error) {
      console.log("Error with register")
    }
  }

export const OnBecomeDriverSubmit = async () => {
  try{
    const response = await fetch(Url + `/becomedriver`, {
      method: "POST", // GET, POST, PUT, DELETE, etc.
      mode: "cors", // no-cors,cors, same-origin
      headers: {
        'Authorization': `Bearer ${localStorage.accessToken}`,
        'Content-Type': 'application/json'
      },
    });
  }
  catch(error){
    console.log("Error with become driver")
  }
}

export const OnLogout = () => {
    localStorage.clear();
  }