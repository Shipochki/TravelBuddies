import { useNavigate } from "react-router-dom"
export const Logout = () => {
    const navigate = useNavigate();

    const clickHandler = () => {
        localStorage.clear();
        navigate('/');
    }

    return (
        <div>
            <button onClick={clickHandler}>Logout</button>
        </div>
    )
}