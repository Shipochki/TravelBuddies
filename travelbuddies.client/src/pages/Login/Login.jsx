import './Login.css'

import { Link, useNavigate } from "react-router-dom";
import { useForm } from "../../utils/hooks/useForm";
import { OnLoginSubmit } from "../../services/UserService";
import { useState } from 'react';

const LoginFromKeys = {
    Email: 'email',
    Password: 'password'
};

export const Login = () => {
    const navigate = useNavigate();

    const [invalidLogin, setInvalidLogin] = useState(false);

    const { values, changeHandler, onSubmit } = useForm({
        [LoginFromKeys.Email]: '',
        [LoginFromKeys.Password]: '',
    }, OnLoginSubmit)

    return (
        <div className="login-main">
            <div id='container' className="login-content">
                <h2>Log In</h2>
                <form id="login" method="POST" onSubmit={onSubmit}>
                    <div className='input-field'>
                        <label>Email</label>
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
                    <label>Password</label>
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
                    <button>Submit</button>
                </form>
                <div className="signup-link">
                  Not a client? <Link to={"/register"}>SignUp now</Link>
                </div>
            </div>
        </div>
    )
}