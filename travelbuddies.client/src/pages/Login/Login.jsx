import './Login.css'

import { Link } from "react-router-dom";
import { useForm } from "../../utils/hooks/useForm";
import { OnLoginSubmit } from "../../services/UserService";
import { useState } from 'react';
import { LazyLoadImage } from 'react-lazy-load-image-component';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEnvelope, faLock } from '@fortawesome/free-solid-svg-icons';

const LoginFromKeys = {
    Email: 'email',
    Password: 'password'
};

export const Login = () => {
    const [invalidLogin, setInvalidLogin] = useState(false);

    const { values, changeHandler, onSubmit } = useForm({
        [LoginFromKeys.Email]: '',
        [LoginFromKeys.Password]: '',
    }, OnLoginSubmit)

    return (
        <div className="login-main">
            <div id='container' className="login-content">
                <LazyLoadImage src={'https://sttravelbuddies001.blob.core.windows.net/web/anne-nygard-rTC5SF27jIc-unsplash.jpg'} alt="" />
                <div className='container-right'>
                    <h2>Log In</h2>
                    <form id="login" method="POST" onSubmit={onSubmit}>
                    <div className='input-field'>
                        <FontAwesomeIcon icon={faEnvelope}/>
                        <input
                            type="email"
                            id="email"
                            placeholder="example@gmail.com"
                            name={LoginFromKeys.Email}
                            value={values[LoginFromKeys.Email]}
                            onChange={changeHandler}
                            required
                        />
                    </div>
                    <div className='input-field'>
                        <FontAwesomeIcon icon={faLock}/>
                        <input
                            type="password"
                            id="password"
                            placeholder="********"
                            name={LoginFromKeys.Password}
                            value={values[LoginFromKeys.Password]}
                            onChange={changeHandler}
                            required
                        />
                    </div>
                    <button>LogIn</button>
                    </form>
                    <div className="signup-link">
                  Not a client? <Link to={"/register"}>SignUp now</Link>
                    </div>
                </div>
            </div>
        </div>
    )
}