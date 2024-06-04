import { errorHandler } from "../utils/common/errorHandler";
import { fetchWrapper } from "../utils/common/fetchWrapper";

const Url = "https://localhost:7005/api/vehicle";

export const OnCreateVehicleSubmit = async (createVehicleFromKeys) => {
  try {
    const formData = new FormData();
    formData.append("brandname", createVehicleFromKeys.brandName);
    formData.append("modelname", createVehicleFromKeys.modelName);
    formData.append("year", createVehicleFromKeys.year);
    formData.append("color", createVehicleFromKeys.color);
    formData.append("fuel", createVehicleFromKeys.fuel);
    formData.append("seatcount", createVehicleFromKeys.seatCount);
    formData.append("acsystem", createVehicleFromKeys.acSystem);
    formData.append("picturelink", createVehicleFromKeys.image);

    const response = await fetch(Url + "/create", {
      method: "POST",
      mode: "cors",
      headers: {
        Authorization: `Bearer ${localStorage.accessToken}`,
      },
      body: formData,
    });

    if (response.ok) {
      // Handle successful response
      return true;
    } else {
      // Handle other errors
      console.error("Error:", response.statusText);
      errorHandler(response.status);
    }
  } catch (error) {
    console.error("Error fetching create vehicle:", error);
  }
};

export const OnUpdateVehicleSubmit = async (updateVehicleFromKeys) => {
  try {
    const formData = new FormData();
    formData.append("id", updateVehicleFromKeys.id);
    formData.append("brandname", updateVehicleFromKeys.brandName);
    formData.append("modelname", updateVehicleFromKeys.modelName);
    formData.append("year", updateVehicleFromKeys.year);
    formData.append("color", updateVehicleFromKeys.color);
    formData.append("fuel", updateVehicleFromKeys.fuel);
    formData.append("seatcount", updateVehicleFromKeys.seatCount);
    formData.append("acsystem", updateVehicleFromKeys.acSystem);
    if (updateVehicleFromKeys.image) {
      formData.append("picturelink", createVehicleFromKeys.image);
    }

    const response = await fetch(Url + "/update", {
      method: "POST",
      mode: "cors",
      headers: {
        Authorization: `Bearer ${localStorage.accessToken}`,
      },
      body: formData,
    });

    if (response.ok) {
      // Handle successful response
      return true;
    } else {
      // Handle other errors
      console.error("Error:", response.statusText);
      //errorHandler(response.status);
    }
  } catch (error) {
    console.error("Error fetching update vehicle:", error);
  }
};

export const OnDeleteVehicleSubmit = async (vehicleId) => {
  try {
    // const response = await fetch(Url + "/create", {
    //   method: "POST",
    //   mode: "cors",
    //   headers: {
    //     Authorization: `Bearer ${localStorage.accessToken}`,
    //     "Content-Type": "application/json",
    //   },
    //   body: vehicleId,
    // });

    // if (response.ok) {
    //   // Handle successful response
    //   return response.json();
    // } else {
    //   // Handle other errors
    //   console.error("Error:", response.statusText);
    //   errorHandler(response.status);
    // }

    const options = {
      method: "POST",
    };
    await fetchWrapper(Url + `/delete/${vehicleId}`, options);
  } catch (error) {
    console.error("Error fetching create vehicle:", error);
  }
};

export const GetVehicleByOwnerId = async (ownerId) => {
  try {
    // const response = await fetch(Url + `/getvehiclebyownerid/${ownerId}`, {
    //   method: "GET",
    //   mode: "cors",
    //   headers: {
    //     Authorization: `Bearer ${localStorage.accessToken}`,
    //     "Content-Type": "application/json",
    //   },
    // });

    // if (response.ok) {
    //   // Handle successful response
    //   if(!response.headers.get('Content-Length')){
    //     return response.json();
    //   }else{
    //     return {};
    //   }
    // } else {
    //   // Handle other errors
    //   console.error("Error:", response.statusText);
    //   errorHandler(response.status);
    // }

    return await fetchWrapper(Url + `/getvehiclebyownerid/${ownerId}`);
  } catch (error) {
    console.error("Error fetching get vehicle by owner id:", error);
  }
};
