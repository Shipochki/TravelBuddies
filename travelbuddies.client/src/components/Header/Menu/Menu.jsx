import './Menu.css'
import { Link, useNavigate } from "react-router-dom";

import { useContext, useEffect, useRef, useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faX } from '@fortawesome/free-solid-svg-icons';

export const Menu = () => {
    const navigate = useNavigate();

    const OnLogout = () => {
        localStorage.clear();

        window.location.reload()

        navigate('/');
    }

    const [menuVisible, setMenuVisible] = useState(false);
    
    const toggleMenu = () => {
        setMenuVisible(!menuVisible);
    };

    const menuRef = useRef(null);

    useEffect(() => {
      const handleOutsideClick = (event) => {
        if (menuRef.current && !menuRef.current.contains(event.target)) {
            setMenuVisible(false);
        }
      };

      document.addEventListener('mousedown', handleOutsideClick);
  
      return () => {
        document.removeEventListener('mousedown', handleOutsideClick);
      };
    }, []);

    return (
        <div>
            <div className={`menuLines`}>
                {menuVisible ? (
                    <div className='icon'>
                        <FontAwesomeIcon icon={faX}/>
                    </div>
                ) : (
                <div onClick={toggleMenu}>
                    👤
                </div>)}
            </div>
            <div className="menu-content">
                <div ref={menuRef} className={`menu ${menuVisible ? 'open' : ''}`}>
                <Link to={'/aboutUs'}>About Us</Link>
                 {localStorage.accessToken ? (
                <div>
                    <Link to={'/profile'}>My Profile</Link>
                    <Link to={'/search'}>Search</Link>
                    {localStorage.role == 'client' ? (
                    <Link to={'/becomeDriver'}>Become Driver</Link>
                    ) : ''}
                    {localStorage.role == 'driver' ? (
                        <Link to={'/createPost'}>Add Post</Link>
                    ) : ''}
                    <a onClick={() => {OnLogout()}}>Logout</a>
                </div>
            ): (
                <div>
                    <Link to={'/login'}>Login</Link>
                    <Link to={'/register'}>Register</Link>
                </div>
            )}
                </div>
            </div>
        </div>
    )
}