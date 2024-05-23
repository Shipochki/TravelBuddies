import './CreateVehicle.css'
import { GetVehicleByOwnerId, OnCreateVehicleSubmit } from "../../services/VehicleService"
import { useForm } from "../../utils/hooks/useForm"
import { useContext, useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import { NotDriver } from '../../components/NotDriver/NotDriver'
import { GlobalContext } from '../../utils/contexts/GlobalContext'
import { Box, FormControl, InputLabel, NativeSelect } from '@mui/material'

const VehicleFromKeys = {
    BrandName: 'brandname',
    ModelName: 'modelname',
    Year: 'year',
    Color: 'color',
    Fuel: 'fuel',
    SeatCount: 'seatcount',
    PictureLink: 'picturelink',
    ACSystem: 'acsystem',
}

export const CreateVehicle = () => {
    const [vehicle, setVehicle] = useState({});

    useEffect(() => {
        const fetchData = async () => {
            const data = await GetVehicleByOwnerId(localStorage.userId);
            setVehicle(data);
        }
        fetchData();
    }, []);

    const {OnSetVehicle} = useContext(GlobalContext);

    const {values, changeHandler, onSubmit} = useForm({
        [VehicleFromKeys.BrandName]: '',
        [VehicleFromKeys.ModelName]: '',
        [VehicleFromKeys.Year]: 0,
        [VehicleFromKeys.Color]: '',
        [VehicleFromKeys.Fuel]: 0,
        [VehicleFromKeys.SeatCount]: 0,
        [VehicleFromKeys.PictureLink]: null,
        [VehicleFromKeys.ACSystem]: false,
    }, OnCreateVehicleSubmit);

    const OnClickSubmit = async (e) => {
        e.preventDefault();

        await OnCreateVehicleSubmit(values);

        const result = await GetVehicleByOwnerId(localStorage.userId);

        OnSetVehicle(result);
    }

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
        values[VehicleFromKeys.ACSystem] = !isACSystem;
        changeHandler;
    }

    return(
        <div className="create-vehicle-main">
            {localStorage.role == 'driver' ? (
                <>
                {vehicle.length == 0 ? (
                <div className='create-vehicle-content'>
                <div className='create-vehicle-header'>
                    <h2>Add your Vehicle</h2>
                </div>
                <form className="create-vehicle-form" onSubmit={OnClickSubmit}>
                <div className='vehicle-brandname'>
                    <input 
                        type='text'
                        id='brandname'
                        placeholder='BrandName'
                        className='inputModel'
                        name={VehicleFromKeys.BrandName}
                        value={values[VehicleFromKeys.BrandName]}
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
                        name={VehicleFromKeys.ModelName}
                        value={values[VehicleFromKeys.ModelName]}
                        onChange={changeHandler}
                        required
                        />
                </div>
                <div className='vehicle-year'>
                    <label>Year</label>
                    <input
                        type='number'
                        id='year'
                        className='inputModel'
                        name={VehicleFromKeys.Year}
                        value={values[VehicleFromKeys.Year]}
                        onChange={changeHandler}
                        required/>
                </div>
                <div className='vehicle-color'>
                    <input
                        type='text'
                        id='color'
                        className='inputModel'
                        placeholder='Color'
                        name={VehicleFromKeys.Color}
                        value={values[VehicleFromKeys.Color]}
                        onChange={changeHandler}
                        required/>
                </div>
                {/* <div className='vehicle-fuel'>
                    <label for="fuel">Choose a Fuel:</label>
                    <select 
                        value={values[VehicleFromKeys.Fuel]} 
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
                            value={values[VehicleFromKeys.Fuel]}
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
                        name={VehicleFromKeys.SeatCount}
                        value={values[VehicleFromKeys.SeatCount]}
                        onChange={changeHandler}
                        required
                    />
                </div>
                <div className='vehicle-acsystme'>
                    <label>ACSystem</label>
                    <input
                        type="checkbox"
                        checked={values[VehicleFromKeys.ACSystem]}
                        onChange={handleIsACSystem}
                    />
                </div>
                <div className="vehicle-upload">
                    <label>Upload Vehicle Img
                    <input 
                        type="file"
                        id='picturelink'
                        name={VehicleFromKeys.PictureLink}
                        value={values[VehicleFromKeys.PictureLink]}
                        onChange={onChangeFile}
                        required
                        hidden
                    />
                    </label>
                    <span>{nameFile}</span>
                </div>
                <button className='vehicle-submit-button'>Add</button>
                </form>
            </div>
                ) : (
                <div className='allready-have-vehicle'>
                    <h3>You allready have added vehicle</h3>
                    <div className='allready-vehicle-links'>
                        <Link to={'/myVehicle'}>My Vehicle</Link>
                        <Link to={'/editVehicle'}>Edit Vehicle</Link>
                    </div>
                </div>
                )}
                </>
            ) : (
                <NotDriver/>
            )}
        </div>
    )
}