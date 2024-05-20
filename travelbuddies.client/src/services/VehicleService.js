const Url = 'https://localhost:7005/api/vehicle';

export const OnCreateVehicleSubmit = async (createVehicleFromKeys) => {
    try {
      const formData = new FormData();
      formData.append('brandname', createVehicleFromKeys.brandname);
      formData.append('modelname', createVehicleFromKeys.modelname);
      formData.append('fuel', createVehicleFromKeys.fuel);
      formData.append('seatcount', createVehicleFromKeys.seatcount);
      formData.append('acsystem', createVehicleFromKeys.acsystem);
      formData.append('picturelink', document.querySelector("#picturelink").files[0])

        const response = await fetch(Url + '/create', {
          method: 'POST',
          mode: "cors",
          headers: {
              'Authorization': `Bearer ${localStorage.accessToken}`,
          },
          body: formData
        });
  
          if (response.ok) {
            // Handle successful response
        }  else {
            // Handle other errors
            console.error('Error:', response.statusText);
        }
      } catch (error) {
        console.error('Error fetching create vehicle:', error);
      }
}

export const OnUpdateVehicleSubmit = async (updateVehicleFromKeys) => {
    try {
      const formData = new FormData();
      formData.append('id', updateVehicleFromKeys.id);
      formData.append('brandname', updateVehicleFromKeys.brandname);
      formData.append('modelname', updateVehicleFromKeys.modelname);
      formData.append('fuel', updateVehicleFromKeys.fuel);
      formData.append('seatcount', updateVehicleFromKeys.seatcount);
      formData.append('acsystem', updateVehicleFromKeys.acsystem);
      formData.append('picturelink', document.querySelector("#picturelink").files[0])

        const response = await fetch(Url + '/update', {
          method: 'POST',
          mode: "cors",
          headers: {
              'Authorization': `Bearer ${localStorage.accessToken}`,
          },
          body: formData
        });
  
          if (response.ok) {
            // Handle successful response
        }  else {
            // Handle other errors
            console.error('Error:', response.statusText);
        }
      } catch (error) {
        console.error('Error fetching update vehicle:', error);
      }
}

export const OnDeleteVehicleSubmit = async (vehicleId) => {
    try {
        const response = await fetch(Url + '/create', {
          method: 'POST',
          mode: "cors",
          headers: {
              'Authorization': `Bearer ${localStorage.accessToken}`,
              'Content-Type': 'application/json'
          },
          body: (vehicleId)
        });
  
          if (response.ok) {
            // Handle successful response
            return response.json();
        }  else {
            // Handle other errors
            console.error('Error:', response.statusText);
        }
      } catch (error) {
        console.error('Error fetching create vehicle:', error);
      }
}

export const GetVehicleByOwnerId = async (ownerId) => {
  try {
    const response = await fetch(Url + `/getvehiclebyownerid/${ownerId}`, {
      method: 'GET',
      mode: "cors",
      headers: {
          'Authorization': `Bearer ${localStorage.accessToken}`,
          'Content-Type': 'application/json'
      },
    });

      if (response.ok) {
        // Handle successful response
        return response.json();
    }  else {
        // Handle other errors
        console.error('Error:', response.statusText);
    }
  } catch (error) {
    console.error('Error fetching get vehicle by owner id:', error);
  };
}