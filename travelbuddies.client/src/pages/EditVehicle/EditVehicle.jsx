import './EditVehicle.css'
import { useContext, useEffect, useState } from "react";
import { GetVehicleByOwnerId, OnUpdateVehicleSubmit } from "../../services/VehicleService";
import { useForm } from "../../utils/hooks/useForm";
import { Link } from "react-router-dom";
import { GlobalContext } from "../../utils/contexts/GlobalContext";
import { LazyLoadImage } from "react-lazy-load-image-component";
import { NoVehicle } from '../../components/NoVehicle/NoVehicle';

const EditVehicleFromKeys = {
    Id: 'id',
    BrandName: 'brandname',
    ModelName: 'modelname',
    Fuel: 'fuel',
    SeatCount: 'seatcount',
    PictureLink: 'picturelink',
    ACSystem: 'acsystem',
}

export const EditVehicle = ({vehicle}) => {
    const {OnSetVehicle} = useContext(GlobalContext);

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

    const OnClickSubmit = async (e) => {
        onSubmit(e);

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
        values[EditVehicleFromKeys.ACSystem] = !isACSystem;
        changeHandler;
    }

    return(
        <div className="create-vehicle-main">
            {vehicle.length != 0 ? (
                <div className='create-vehicle-content'>
                <div className='create-vehicle-header'>
                    <h2>Edit your Vehicle</h2>
                </div>
                <form className="create-vehicle-form" onSubmit={OnClickSubmit}>
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
                <div className='vehicle-fuel'>
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
                </div>
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
                    <LazyLoadImage src={
                        values[EditVehicleFromKeys.PictureLink] != null 
                        ? vehicle.pictureLink
                        : values[EditVehicleFromKeys.PictureLink]
                    }/>
                    <label>Edit Vehicle Img
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