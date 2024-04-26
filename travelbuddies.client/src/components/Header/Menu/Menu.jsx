import { Link } from "react-router-dom"

export const Menu = () => {
    return (
        <div>
            <Link to={'/'}>Home</Link>
            <Link to={'/aboutus'}>About us</Link>
            {localStorage.accessToken ? (
                <>
                    {localStorage.role == 'client' ? (
                        <Link to={'/becomeDriver'}>Become Driver</Link>
                    ): (<></>)}
                    <Link to={'/logout'}>Logout</Link>
                </>
            ) : (
                <>
                    <Link to={'/login'}>Login</Link>
                    <Link to={'/register'}>Regiser</Link>
                </>
            )}
        </div>
    )
}