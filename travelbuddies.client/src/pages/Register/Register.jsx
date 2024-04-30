import { useState } from 'react';
import { OnRegisterSubmit } from '../../services/UserService'
import { useForm } from '../../utils/hooks/useForm'
import './Register.css'

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

    const { values, changeHandler, onSubmit } = useForm({
        [RegisterFromKeys.FirstName]: '',
        [RegisterFromKeys.LastName]: '',
        [RegisterFromKeys.Email]: '',
        [RegisterFromKeys.City]: null,
        [RegisterFromKeys.Country]: null,
        [RegisterFromKeys.Password]: '',
        [RegisterFromKeys.ProfilePicture]: null
    }, OnRegisterSubmit);

    const clickSubmit = (e) => {

        if(values[RegisterFromKeys.Password] != repass){
            return;
        }

        onSubmit(e);
        window.location.assign('/login')
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
            <div className='register-content'>
            <h2>Register</h2>
            <form className="register-form" id='register' onSubmit={clickSubmit}>
                <div className='register-form-content'>
                <div className="register-label-input">
                    <label>First Name</label>
                    <input 
                        type="text"
                        id='firstname'
                        placeholder='James'
                        name={RegisterFromKeys.FirstName}
                        value={values[RegisterFromKeys.FirstName]}
                        onChange={changeHandler}
                        required
                    />
                </div>
                <div className="register-label-input">
                    <label>Last Name</label>
                    <input 
                        type="text"
                        id='lastname'
                        placeholder='Fill'
                        name={RegisterFromKeys.LastName}
                        value={values[RegisterFromKeys.LastName]}
                        onChange={changeHandler}
                        required
                    />
                </div>
                <div className="register-label-input">
                    <label>Email</label>
                    <input 
                        type="email"
                        id='email'
                        placeholder='example@mail.com'
                        name={RegisterFromKeys.Email}
                        value={values[RegisterFromKeys.Email]}
                        onChange={changeHandler}
                        required
                    />
                </div>
                <div className="register-label-input">
                    <label>City</label>
                    <input 
                        type="text"
                        id='city'
                        placeholder='Sofia'
                        name={RegisterFromKeys.City}
                        value={values[RegisterFromKeys.City]}
                        onChange={changeHandler}
                    />
                </div>
                <div className="register-label-input">
                    <label>Country</label>
                    <input 
                        type="text"
                        id='Country'
                        placeholder='Bulgaria'
                        name={RegisterFromKeys.Country}
                        value={values[RegisterFromKeys.Country]}
                        onChange={changeHandler}
                    />
                </div>
                <div className="register-label-input">
                    <label>Password</label>
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
                    <label>Confirm Password</label>
                    <input 
                        type="password"
                        id='repassword'
                        placeholder='********'
                        name='repassword'
                        value={repass}
                        onChange={changeHandlerPassword}
                        required
                    />
                </div>
                <div className="register-upload">
                    <label>Upload Img
                    <input 
                        type="file"
                        id='profilepicture'
                        name={RegisterFromKeys.ProfilePicture}
                        value={values[RegisterFromKeys.ProfilePicture]}
                        onChange={onChangeFile}
                        hidden
                    />
                    <span>{nameFile}</span>
                    </label>
                </div>
                </div>
                <button>Submit</button>     
        </form>
        </div>
    </div>
    )
}