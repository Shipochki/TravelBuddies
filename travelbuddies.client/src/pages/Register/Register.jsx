import { useState } from 'react';
import { OnRegisterSubmit } from '../../services/UserService'
import { useForm } from '../../utils/hooks/useForm'
import './Register.css'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCarSide, faCity, faEnvelope, faGlobe, faLock, faPerson, faUpload, faUser } from '@fortawesome/free-solid-svg-icons';
import { useNavigate } from 'react-router-dom';

const RegisterFromKeys = {
    FirstName: 'firstname',
    LastName: 'lastname',
    Email: 'email',
    City: 'city',
    Country: 'country',
    Password: 'password',
    ProfilePicture: 'profilepicture'
};

export const Regiser = () => {
    const navigate = useNavigate();
    const [error, setError] = useState('');

    const { values, changeHandler, onSubmit } = useForm({
        [RegisterFromKeys.FirstName]: '',
        [RegisterFromKeys.LastName]: '',
        [RegisterFromKeys.Email]: '',
        [RegisterFromKeys.City]: null,
        [RegisterFromKeys.Country]: null,
        [RegisterFromKeys.Password]: '',
        [RegisterFromKeys.ProfilePicture]: null
    }, OnRegisterSubmit);

    const clickSubmit = async (e) => {

        if(values[RegisterFromKeys.Password] != repass){
            return;
        }
        e.preventDefault();
        const result = await OnRegisterSubmit(values);

        if(result){
            alert(result);
            setError(result);
        }else{
            navigate('/login')
        }
    }

    const [repass, setRepass] = useState('');

    const changeHandlerPassword = (e) => {
        setRepass(e.target.value)
    }

    const [nameFile, setNameFile] = useState('');

    const onChangeFile = (e) => {
        changeHandler(e);

        const path = e.target.value.split('\\');
        const name = path[path.length - 1]

        setNameFile(name);
    }

    return(
        <div className="register-main">
            <div className='register-background-content'>
                <div className='register-welcome'> 
                    <FontAwesomeIcon icon={faCarSide}/>
                    <h2>Welcome</h2>
                </div>
                <div className='register-content'>
                <h2>Register</h2>
                <form className="register-form" id='register' onSubmit={clickSubmit}>
                <div className='register-form-content'>
                <div className="register-label-input">
                    <FontAwesomeIcon icon={faUser}/>
                    <input 
                        type="text"
                        id='firstname'
                        placeholder='FirstName*'
                        name={RegisterFromKeys.FirstName}
                        value={values[RegisterFromKeys.FirstName]}
                        onChange={changeHandler}
                        required
                    />
                </div>
                <div className="register-label-input">
                    <FontAwesomeIcon icon={faUser}/>
                    <input 
                        type="text"
                        id='lastname'
                        placeholder='LastName*'
                        name={RegisterFromKeys.LastName}
                        value={values[RegisterFromKeys.LastName]}
                        onChange={changeHandler}
                        required
                    />
                </div>
                <div className="register-label-input">
                    <FontAwesomeIcon icon={faEnvelope}/>
                    <input 
                        type="email"
                        id='email'
                        placeholder='example@mail.com*'
                        name={RegisterFromKeys.Email}
                        value={values[RegisterFromKeys.Email]}
                        onChange={changeHandler}
                        required
                    />
                </div>
                <div className="register-label-input">
                    <FontAwesomeIcon icon={faCity}/>
                    <input 
                        type="text"
                        id='city'
                        placeholder='City'
                        name={RegisterFromKeys.City}
                        value={values[RegisterFromKeys.City]}
                        onChange={changeHandler}
                    />
                </div>
                <div className="register-label-input">
                    <FontAwesomeIcon icon={faGlobe}/>
                    <input 
                        type="text"
                        id='Country'
                        placeholder='Country'
                        name={RegisterFromKeys.Country}
                        value={values[RegisterFromKeys.Country]}
                        onChange={changeHandler}
                    />
                </div>
                <div className="register-label-input">
                    <FontAwesomeIcon icon={faLock}/>
                    <input 
                        type="password"
                        id='password'
                        placeholder='********'
                        name={RegisterFromKeys.Password}
                        value={values[RegisterFromKeys.Password]}
                        onChange={changeHandler}
                        required
                    />
                </div>
                <div className="register-label-input">
                    <FontAwesomeIcon icon={faLock}/>
                    <input 
                        type="password"
                        id='repassword'
                        placeholder='Confirm password'
                        name='repassword'
                        value={repass}
                        onChange={changeHandlerPassword}
                        required
                    />
                </div>
                <div className='register-upload-icon'>
                    <FontAwesomeIcon icon={faUpload}/>
                    <div className="register-upload">
                    <label>Upload Profile Img *
                    <input 
                        type="file"
                        id='profilepicture'
                        name={RegisterFromKeys.ProfilePicture}
                        value={values[RegisterFromKeys.ProfilePicture]}
                        onChange={onChangeFile}
                        required
                        hidden
                    />
                    </label>
                    <span>{nameFile}</span>
                    </div>
                </div>
                </div>
                <button>Submit</button>     
                </form>
            </div>
            </div>
            
        </div>
    )
}