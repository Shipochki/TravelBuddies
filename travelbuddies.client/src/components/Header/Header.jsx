import { Link } from "react-router-dom"
import { UserMenu } from "../UserMenu/UserMenu"
import './Header.css'
import imgLogo from '../../utils/images/logo-no-background.png'
import carLogo from '../../utils/images/auto-car-logo-design-icon-vector-illustration-auto-car-logo-design-icon-vector-illustration-symbol-service-automobile-silhouette-157364282.jpg'

export const Header = () => {
    return (
        <div className="header">
            <img 
                className="header-img"
                src={carLogo}
            />
            <div className="logo-content">
                
                <Link to={'/'} className="logo">
                    <img src={imgLogo} alt="logo" /></Link>
           
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