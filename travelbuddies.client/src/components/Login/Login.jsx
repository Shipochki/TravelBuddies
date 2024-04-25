import { Link } from "react-router-dom";
import { useForm } from "../../hooks/useForm";
import { OnLoginSubmit } from "../../services/UserService";

const LoginFromKeys = {
    Email: 'email',
    Password: 'password'
};

export const Login = () => {
    const { values, changeHandler, onSubmit } = useForm({
        [LoginFromKeys.Email]: '',
        [LoginFromKeys.Password]: '',
    }, OnLoginSubmit)

    return (
        <div className="login-main">
            <div className="login-content">
                <form id="login" method="POST" onSubmit={onSubmit}>
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