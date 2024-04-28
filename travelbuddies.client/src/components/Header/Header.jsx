import { Link } from "react-router-dom"
import { Menu } from "./Menu/Menu"
import './Header.css'

import mainLogo from '../../utils/images/pngegg.png'

export const Header = () => {
    return (
        <div className="header">
            <div className="logo-content">
                
                <Link to={'/'} className="logo"><img src={mainLogo} alt="logo" />Buddies</Link>
           
            </div>
            <div className="navigation">
                <ul>
                    <li>
                        <Link to={'/'}>Home</Link>
                    </li>
                    <li>
                        <Link to={'/about'}>About</Link>
                    </li>
                    <li>
                        {localStorage.accessToken ? (
                            <Menu/>
                        ) : (
                            <Link to={'/login'}>👤 Log In</Link>
                        )}
                    </li>
                </ul>
            </div>
        </div>
    )
}