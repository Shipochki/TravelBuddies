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
        body: JSON.stringify(loginFromKeys),
      });

      if (!response.ok) {
        if (response.status === 400) {
          console.log('Invalid username or password.');
        } else {
          console.log('An error occurred while logging in.');
        }
        return;
      }

      const { token: accessToken } = await response.json();

      const { nameId: userId, sub: username, role: role } = parseJwt(accessToken);

      localStorage.setItem('accessToken', accessToken);
      localStorage.setItem('username', username);
      localStorage.setItem('userId', userId);
      localStorage.setItem('role', role)

      window.location.reload();
    } catch (error) {
      console.log("Error with login");
    }
  }

export const OnRegisterSubmit = async (registerFromKeys) => {
    try {
      const response = await fetch(Url + `/register`, {
        method: "POST", // GET, POST, PUT, DELETE, etc.
        mode: "cors", // no-cors,cors, same-origin
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(registerFromKeys),
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