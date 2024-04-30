import './Menu.css'
import { Link } from "react-router-dom";

import { useEffect, useRef, useState } from 'react';
import { LazyLoadImage } from 'react-lazy-load-image-component';

export const Menu = () => {
    const OnLogout = () => {
        localStorage.clear();

        window.location.assign('/')
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
                    {/* 👤⚬ ⚬ ⚬ */}
                <LazyLoadImage
                    onClick={!menuVisible ? toggleMenu : ''}
                    src={'https://sttravelbuddies001.blob.core.windows.net/web/blank-profile-picture-973460_960_720.png'}/>
            </div>
            <div className="menu-content">
                <div ref={menuRef} className={`menu ${menuVisible ? 'open' : ''}`}>
                    <div>
                        <div className='profile-info'>
                            <p>Name: {localStorage.fullname}</p>
                            <p>Mail: {localStorage.username}</p>
                            <p>Role: {localStorage.role}</p>
                        </div>
                        <div className='navLinks'>
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
                    </div>
                </div>
            </div>
        </div>
    )
}