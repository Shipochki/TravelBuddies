import './CreateVehicle.css'
import { OnCreateVehicleSubmit } from "../../services/VehicleService"
import { useForm } from "../../utils/hooks/useForm"
import { useState } from 'react'

const VehicleFromKeys = {
    BrandName: 'brandname',
    ModelName: 'modelname',
    Fuel: 'fuel',
    SeatCount: 'seatcount',
    PictureLink: 'picturelink',
    ACSystem: 'acsystem',
}

export const CreateVehicle = () => {
    const {values, changeHandler, onSubmit} = useForm({
        [VehicleFromKeys.BrandName]: '',
        [VehicleFromKeys.ModelName]: '',
        [VehicleFromKeys.Fuel]: 0,
        [VehicleFromKeys.SeatCount]: 0,
        [VehicleFromKeys.PictureLink]: null,
        [VehicleFromKeys.ACSystem]: false,
    }, OnCreateVehicleSubmit);

    const OnClickSubmit = (e) => {


        onSubmit(e);
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
            <div className='create-vehicle-content'>
                <h2>Add your Vehicle</h2>
                <form className="create-vehicle-form" onSubmit={OnClickSubmit}>
                <div className='vehicle-brandname'>
                    <input 
                        type='text'
                        id='brandname'
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
                        name={VehicleFromKeys.ModelName}
                        value={values[VehicleFromKeys.ModelName]}
                        onChange={changeHandler}
                        required
                        />
                </div>
                <div className='vehicle-fuel'>
                    <label for="fuel">Choose a Fuel:</label>
                    <select name="fuel" id="fuel">
                        <option value={0}>Diesel</option>
                        <option value={1}>Gasoline</option>
                        <option value={2}>Electric</option>
                    </select>
                </div>
                <div className='vehicle-seatcount'>
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
                <button className='vehicle-submit-button'>Submit</button>
                </form>
            </div>
        </div>
    )
}