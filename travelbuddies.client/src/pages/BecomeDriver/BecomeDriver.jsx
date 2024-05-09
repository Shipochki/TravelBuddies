import { Link } from 'react-router-dom'
import { OnBecomeDriverSubmit } from '../../services/UserService'
import { useForm } from '../../utils/hooks/useForm'
import './BecomeDriver.css'
import { useState } from 'react'

export const BecomeDriver = () => {
    const [frontFileName, setFrontFileName] = useState([]);
    const [backFileName, setBackFileName] = useState([]);

    // const [presonId, setPersonId] = useState('');
    // const [licenseNum, setLicenseNum] = useState('');

    // const OnSubmitMvr = async (e) => {
    //     e.preventDefault();

    //     const result = await fetch(`https://e-uslugi.mvr.bg/api/Obligations/AND?mode=1&obligedPersonIdent=${presonId}&drivingLicenceNumber=${licenseNum}`,
    //         {
    //             method: 'GET',
    //             headers: {
    //                 'Content-Type': 'application/json'
    //             }
    //         }
    //     )
    //     console.log(result);
    // }

    // const onChangePerson = (e) => {
    //     setPersonId(e.target.value);
    // }

    // const onChangeLicense = (e) => {
    //     setLicenseNum(e.target.value);
    // }

    const onChangeFrontFile = (e) => {
        const path = e.target.value.split('\\');
        const name = path[path.length - 1]

        setFrontFileName(name);
    }

    const onChangeBacktFile = (e) => {
        const path = e.target.value.split('\\');
        const name = path[path.length - 1]

        setBackFileName(name);
    }

    const { values, changeHandler, onSubmit } = useForm({   
    }, OnBecomeDriverSubmit)

    return (
        <div className="becomedriver-main">
            {localStorage.role == 'driver' ? (
                <div className='you-are-driver'>
                    <h3>You are allready a Driver</h3>
                    <div className='you-are-driver-links'>
                        <Link to={'/createPost'}>Add Post</Link>
                        <Link to={'/createVehicle'}>Add Vehicle</Link>
                    </div>
                </div>
            ) : (
                <div className="becomedriver-content">
                    <form className='becomedriver-form' onSubmit={onSubmit}>
                        <div className='license-upload'>
                            <label>Driver license Front
                            <input 
                            type="file"
                            onChange={onChangeFrontFile}
                            hidden
                            />
                            </label>
                            <span>{frontFileName}</span>
                        </div>
                        <div className='license-upload'>
                            <label>Driver license Back
                            <input 
                            type="file"
                            onChange={onChangeBacktFile}
                            hidden
                            />
                            </label>
                            <span>{backFileName}</span>
                        </div>

                        {/* <input
                            value={presonId}
                            onChange={onChangePerson}
                        />
                        <input
                            value={licenseNum}
                            onChange={onChangeLicense}
                        /> */}
                        <button>Submit</button>
                    </form>
                </div>
            )}
        </div>
    )
}