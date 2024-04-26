import { useNavigate } from "react-router-dom"
import { OnLogout } from "../../services/UserService"

export const Logout = () => {
    const navigate = useNavigate();

    const clickHandler = () => {
        localStorage.clear();
        window.location.reload();
        navigate('/');
    }

    return (
        <div>
            <button onClick={clickHandler}>Logout</button>
        </div>
    )
}