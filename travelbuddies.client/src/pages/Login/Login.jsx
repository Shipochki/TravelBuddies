import './Login.css'

import { Link, useNavigate } from "react-router-dom";
import { useForm } from "../../utils/hooks/useForm";
import { OnLoginSubmit } from "../../services/UserService";

const LoginFromKeys = {
    Email: 'email',
    Password: 'password'
};

export const Login = () => {
    const navigate = useNavigate();

    const { values, changeHandler, onSubmit } = useForm({
        [LoginFromKeys.Email]: '',
        [LoginFromKeys.Password]: '',
    })

    const onClick = () => {
        OnLoginSubmit(values);

        navigate('/');
    }

    return (
        <div className="login-main">
            <div className="login-content">
                <form id="login" method="POST" onSubmit={onClick}>
                    <input
                        type="email"
                        id="email"
                        placeholder="Email..."
                        name={LoginFromKeys.Email}
                        value={values[LoginFromKeys.Email]}
                        onChange={changeHandler}
                        required
                    />
                    <input
                        type="password"
                        id="password"
                        placeholder="********"
                        name={LoginFromKeys.Password}
                        value={values[LoginFromKeys.Password]}
                        onChange={changeHandler}
                        required
                    />
                    <button>Submit</button>
                </form>
                <div className="signup-link">
                  Not a client <Link to={"/register"}>SignUp now</Link>
                </div>
            </div>
        </div>
    )
}