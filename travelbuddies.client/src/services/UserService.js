import { useNavigate } from "react-router-dom";
import { parseJwt } from "../common/parsers";

const Url = 'https://localhost:7005/api';

export const OnLoginSubmit = async (loginFromKeys) => {
    try {
      const response = await fetch(Url + `/user/login`, {
        method: "POST", // GET, POST, PUT, DELETE, etc.
        mode: "cors", // no-cors,cors, same-origin
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(loginFromKeys),
      });

      const { token: accessToken } = await response.json();

      const { nameId: userId, sub: username } = parseJwt(accessToken);

      localStorage.setItem('accessToken', accessToken);
      localStorage.setItem('username', username);
      localStorage.setItem('userId', userId);

    } catch (error) {
      console.log("Error with login");
    }
  }

export const OnRegisterSubmit = async (registerFromKeys) => {
    try {
      const response = await fetch(Url + `/user/register`, {
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

export const OnLogout = () => {
    localStorage.clear();
    navigate('/');
  }