import { Link } from "react-router-dom"
import { UserMenu } from "../UserMenu/UserMenu"
import './Header.css'

export const Header = () => {
    return (
        <div className="header">
            <div className="logo-content">
                
                <Link to={'/'} className="logo">
                    <img src="https://sttravelbuddies001.blob.core.windows.net/web/logo-no-background.png" alt="" /></Link>
           
            </div>
            <div className="navigation">
                <ul>
                    <li>
                        <Link to={'/'}>Home</Link>
                    </li>
                    {!localStorage.accessToken && (
                        <li>
                            <Link to={'/about'}>About</Link>
                        </li>
                    )}
                    <li>
                        {localStorage.accessToken ? (
                            <UserMenu/>
                        ) : (
                            <Link to={'/login'}>👤 Log In</Link>
                        )}
                    </li>
                </ul>
            </div>
        </div>
    )
}