import './Menu.css'
import { Link } from "react-router-dom";

import { useEffect, useRef, useState } from 'react';
import { LazyLoadImage } from 'react-lazy-load-image-component';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faDriversLicense, faPlus, faSearch, faSignOut, faUser } from '@fortawesome/free-solid-svg-icons';

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
        if (menuRef.current 
            && !menuRef.current.contains(event.target) 
            && event.target != document.getElementsByClassName('menuLines')[0]) {
            setMenuVisible(false);
        } else if (event.target == document.getElementsByClassName('menuLines')[0] && menuVisible == true){
            setMenuVisible(false);
        } else if (event.target == document.getElementsByClassName('menuLines')[0] && menuVisible == false){
            setMenuVisible(true);
        }
      };

      document.addEventListener('mousedown', handleOutsideClick);
  
      return () => {
        document.removeEventListener('mousedown', handleOutsideClick);
      };
    }, []);

    return (
        <div>
            <LazyLoadImage
                className='menuLines'
                src={'https://sttravelbuddies001.blob.core.windows.net/web/blank-profile-picture-973460_960_720.png'}/>
            
            <div className="menu-content">
                <div ref={menuRef} className={`menu ${menuVisible ? 'open' : ''}`}>
                    <div>
                        <div className='profile-info'>
                            <p>Name: {localStorage.fullname}</p>
                            <p>Mail: {localStorage.username}</p>
                            <p>Role: {localStorage.role}</p>
                        </div>
                        <div className='navLinks'>
                            <Link to={'/profile'}>{<FontAwesomeIcon icon={faUser}/>} Profile</Link>
                            <Link to={'/'}>{<FontAwesomeIcon icon={faSearch}/>} Search</Link>
                            {localStorage.role == 'client' ? (
                                <Link to={'/becomeDriver'}>{<FontAwesomeIcon icon={faDriversLicense}/>}Become Driver</Link>
                            ) : ''}
                            {localStorage.role == 'driver' ? (
                                <Link to={'/createPost'}>{<FontAwesomeIcon icon={faPlus}/>}Add Post</Link>
                            ) : ''}
                            <a onClick={() => {OnLogout()}}><FontAwesomeIcon icon={faSignOut}/> Logout</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}