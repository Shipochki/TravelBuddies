import './EditVehicle.css'
import { useEffect, useState } from "react";
import { GetVehicleByOwnerId, OnUpdateVehicleSubmit } from "../../services/VehicleService";
import { useForm } from "../../utils/hooks/useForm";
import { NoVehicle } from '../../components/NoVehicle/NoVehicle';
import { Box, FormControl, InputLabel, NativeSelect } from '@mui/material';

const EditVehicleFromKeys = {
    Id: 'id',
    BrandName: 'brandname',
    ModelName: 'modelname',
    Fuel: 'fuel',
    SeatCount: 'seatcount',
    PictureLink: 'picturelink',
    ACSystem: 'acsystem',
}

export const EditVehicle = () => {
    const [vehicle, setVehicle] = useState({});

    useEffect(() => {
        const fetchData = async () => {
            const data = await GetVehicleByOwnerId(localStorage.userId);
            setVehicle(data);

            values[EditVehicleFromKeys.Id] = data.id;
            values[EditVehicleFromKeys.BrandName] = data.brandName;
            values[EditVehicleFromKeys.ModelName] = data.modelName;
            values[EditVehicleFromKeys.Fuel] = fuel[data.fuel];
            values[EditVehicleFromKeys.SeatCount] = data.seatCount;
            values[EditVehicleFromKeys.ACSystem] = data.acSystem;
        }
        fetchData();
    }, []);

    const fuel = {
        "Diesel": 0,
        "Gasoline": 1,
        "Electric": 2,
    }

    const {values, changeHandler, onSubmit} = useForm({
        [EditVehicleFromKeys.Id]: vehicle.id,
        [EditVehicleFromKeys.BrandName]: vehicle.brandName,
        [EditVehicleFromKeys.ModelName]: vehicle.modelName,
        [EditVehicleFromKeys.Fuel]: fuel[vehicle.fuel],
        [EditVehicleFromKeys.SeatCount]: vehicle.seatCount,
        [EditVehicleFromKeys.PictureLink]: null,
        [EditVehicleFromKeys.ACSystem]: vehicle.acSystem,
    }, OnUpdateVehicleSubmit);

    const [nameFile, setNameFile] = useState('');

    const onChangeFile = (e) => {
        changeHandler(e);

        const path = e.target.value.split('\\');
        const name = path[path.length - 1]

        setNameFile(name);
    }

    const [isACSystem, setIsACSystem] = useState(false);

    const handleIsACSystem = () => {
        setIsACSystem(!isACSystem);
        values[EditVehicleFromKeys.ACSystem] = !isACSystem;
        changeHandler;
    }

    return(
        <div className="create-vehicle-main">
            {vehicle.id ? (
                <div className='create-vehicle-content'>
                <div className='create-vehicle-header'>
                    <h2>Edit your Vehicle</h2>
                </div>
                <form className="create-vehicle-form" onSubmit={onSubmit}>
                <div className='vehicle-brandname'>
                    <input 
                        type='text'
                        id='brandname'
                        placeholder='BrandName'
                        className='inputModel'
                        name={EditVehicleFromKeys.BrandName}
                        value={values[EditVehicleFromKeys.BrandName]}
                        onChange={changeHandler}
                        required
                        />
                </div>
                <div className='vehicle-modelname'>
                    <input
                        type='text'
                        id='modelname'
                        placeholder='ModelName'
                        className='inputModel'
                        name={EditVehicleFromKeys.ModelName}
                        value={values[EditVehicleFromKeys.ModelName]}
                        onChange={changeHandler}
                        required
                        />
                </div>
                {/* <div className='vehicle-fuel'>             
                    <label for="fuel">Choose a Fuel:</label>
                    <select 
                        value={values[EditVehicleFromKeys.Fuel]} 
                        onChange={changeHandler} 
                        name="fuel" 
                        id="fuel">
                        <option value={0}>Diesel</option>
                        <option value={1}>Gasoline</option>
                        <option value={2}>Electric</option>
                    </select>
                </div> */}
                <Box sx={{ minWidth: 120 , gridArea:'fuel'}}>
                    <FormControl fullWidth>
                        <InputLabel variant="standard" htmlFor="fuel">
                            Choose a Fuel
                        </InputLabel>
                        <NativeSelect
                            value={values[EditVehicleFromKeys.Fuel]}
                            onChange={changeHandler}
                            inputProps={{
                                name: 'fuel',
                                id: 'fuel',
                            }}
                            >
                            <option value={0}>Diesel</option>
                            <option value={1}>Gasoline</option>
                            <option value={2}>Electric</option>
                        </NativeSelect>
                    </FormControl>
                </Box>
                <div className='vehicle-seatcount'>
                    <label>SeatCount</label>
                    <input
                        type='number'
                        id='seatcount'
                        name={EditVehicleFromKeys.SeatCount}
                        value={values[EditVehicleFromKeys.SeatCount]}
                        onChange={changeHandler}
                        required
                    />
                </div>
                <div className='vehicle-acsystme'>
                    <label>ACSystem</label>
                    <input
                        type="checkbox"
                        checked={values[EditVehicleFromKeys.ACSystem]}
                        onChange={handleIsACSystem}
                    />
                </div>
                <div className="vehicle-upload">
                    
                    <label>Upload new vehicle img
                    <input 
                        type="file"
                        id='picturelink'
                        name={EditVehicleFromKeys.PictureLink}
                        value={values[EditVehicleFromKeys.PictureLink]}
                        onChange={onChangeFile}
                        hidden
                    />
                    </label>
                    <span>{nameFile}</span>
                </div>
                <button className='vehicle-submit-button'>Edit</button>
                </form>
            </div>
            ) : (
                <NoVehicle/>
            )}
            
        </div>
    )
}