import './CreateVehicle.css'
import { GetVehicleByOwnerId, OnCreateVehicleSubmit } from "../../services/VehicleService"
import { useForm } from "../../utils/hooks/useForm"
import { useContext, useState } from 'react'
import { Link } from 'react-router-dom'
import { NotDriver } from '../../components/NotDriver/NotDriver'
import { GlobalContext } from '../../utils/contexts/GlobalContext'

const VehicleFromKeys = {
    BrandName: 'brandname',
    ModelName: 'modelname',
    Fuel: 'fuel',
    SeatCount: 'seatcount',
    PictureLink: 'picturelink',
    ACSystem: 'acsystem',
}

export const CreateVehicle = ({vehicle}) => {
    const {OnSetVehicle} = useContext(GlobalContext);

    const {values, changeHandler, onSubmit} = useForm({
        [VehicleFromKeys.BrandName]: '',
        [VehicleFromKeys.ModelName]: '',
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
                <div className='vehicle-fuel'>
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
                </div>
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