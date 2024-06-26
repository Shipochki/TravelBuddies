import { errorHandler } from "../utils/common/errorHandler";
import { parseJwt } from "../utils/common/parsers";
import { fetchWrapper } from "../utils/common/fetchWrapper";

const Url = "https://localhost:7005/api/user";

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
      const errorResponse = await response.json(); // Parse JSON response body
      if (Array.isArray(response.detail))
        window.alert(
          errorResponse.detail.join("\n")
        ); // Access ErrorMessage property
      else window.alert(errorResponse.detail);
    }

    const { token: accessToken } = await response.json();

    const {
      nameId: userId,
      sub: username,
      role: role,
      fullname: fullname,
      exp: exp,
      profilePictureLink: profilePictureLink,
    } = parseJwt(accessToken);

    localStorage.setItem("accessToken", accessToken);
    localStorage.setItem("username", username);
    localStorage.setItem("userId", userId);
    localStorage.setItem("role", role);
    localStorage.setItem("fullname", fullname);
    localStorage.setItem("exp", exp);
    localStorage.setItem("profilePictureLink", profilePictureLink);

    window.location.assign("/");
  } catch (error) {
    console.log("Error with login", error);
  }
};

export const OnRegisterSubmit = async (registerFromKeys) => {
  const formData = new FormData();
  formData.append("firstname", registerFromKeys.firstname);
  formData.append("lastname", registerFromKeys.lastname);
  formData.append("email", registerFromKeys.email);
  formData.append("city", registerFromKeys.city);
  formData.append("country", registerFromKeys.country);
  formData.append("password", registerFromKeys.password);
  formData.append("profilepicture", registerFromKeys.image);

  try {
    const response = await fetch(Url + `/register`, {
      method: "POST", // GET, POST, PUT, DELETE, etc.
      mode: "cors", // no-cors,cors, same-origin
      body: formData,
    });

    if (response.ok) {
      const result = await response.json();
    } else {
      const result = await response.json();
      console.log(result.detail);
      return result.detail;
    }
  } catch (error) {
    console.log("Error with register");
  }
};

export const OnBecomeDriverSubmit = async () => {
  try {
    const options = {
      method: "POST",
    };
    await fetchWrapper(Url + `/becomedriver`, options);
  } catch (error) {
    console.log("Error with become driver");
  }
};

export const OnUpdateProfilePicutreSubmit = async () => {
  try {
    const formData = new FormData();
    formData.append(
      "profilepicture",
      document.querySelector("#profilepicture").files[0]
    );

    const response = await fetch(Url + `/updateprofilepicture`, {
      method: "POST", // GET, POST, PUT, DELETE, etc.
      mode: "cors", // no-cors,cors, same-origin
      headers: {
        Authorization: `Bearer ${localStorage.accessToken}`,
      },
      body: formData,
    });

    if (!response.ok) {
      console.log(response.statusText);
      errorHandler(response.status);
    }
  } catch (error) {
    console.log("Error with update profile picture");
  }
};

export const OnUpdateProfileSubmit = async (editProfileFromKeys) => {
  try {
    const options = {
      method: "POST",
      body: JSON.stringify(editProfileFromKeys),
    };
    await fetchWrapper(Url + `/update`, options);
  } catch (error) {
    console.log("Error with update profile");
  }
};

export const OnLogout = () => {
  localStorage.clear();
};

export const GetUserById = async (id) => {
  try {
    return await fetchWrapper(Url + `/getuserbyid/${id}`);
  } catch (error) {
    console.error("Error fetching get user by id:", error);
  }
};

export const GetOnlyUserById = async (id) => {
  try {
    return await fetchWrapper(Url + `/getonlyuserbyid/${id}`);
  } catch (error) {
    console.error("Error fetching get only user by id:", error);
  }
};
