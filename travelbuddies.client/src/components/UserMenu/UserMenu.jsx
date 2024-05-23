import './UserMenu.css'
import { Link } from "react-router-dom";

import { useEffect, useRef, useState } from 'react';
import { LazyLoadImage } from 'react-lazy-load-image-component';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPencil, faSignOut, faUser } from '@fortawesome/free-solid-svg-icons';
import { Button, Tooltip } from '@mui/material';

export const UserMenu = () => {
    const OnLogout = () => {
        localStorage.clear();

        window.location.assign('/')
    }

    const [menuVisible, setMenuVisible] = useState(false);

    const menuRef = useRef(null);

    useEffect(() => {
      const handleOutsideClick = (event) => {
        if (menuRef.current 
            && !menuRef.current.contains(event.target) 
            && event.target != document.getElementsByClassName('clicked')[0]) {
            setMenuVisible(false);
        } else if (event.target == document.getElementsByClassName('clicked')[0] && menuVisible == true){
            setMenuVisible(false);
        } else if (event.target == document.getElementsByClassName('clicked')[0] && menuVisible == false){
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
            {menuVisible ? (
                <Tooltip title="User settings">
                    <Button sx={{
                        borderRadius: '32px'
                    }}>
                        <LazyLoadImage
                            className='menuLines'
                            src={localStorage.profilePictureLink !== 'undefined' ? localStorage.profilePictureLink : 'https://lh3.googleusercontent.com/d/1jzzGHsTZWHo57Mhria1n_MIm4kzxe-tD=s220?authuser=0'}/>
                    </Button>
                </Tooltip>
            ): (
                <Tooltip title="User settings">
                    <Button sx={{
                        borderRadius: '32px'
                    }}>
                        <LazyLoadImage
                            className='menuLines clicked'
                            src={localStorage.profilePictureLink !== 'undefined' ? localStorage.profilePictureLink : 'https://lh3.googleusercontent.com/d/1jzzGHsTZWHo57Mhria1n_MIm4kzxe-tD=s220?authuser=0'}/>
                    </Button>
                </Tooltip>
            )}
            
            <div className="menu-content">
                <div ref={menuRef} className={`menu ${menuVisible ? 'open' : ''}`}>
                    <div>
                        <div className='profile-info'>
                            <LazyLoadImage
                            src={localStorage.profilePictureLink !== 'undefined' ? localStorage.profilePictureLink : 'https://lh3.googleusercontent.com/d/1jzzGHsTZWHo57Mhria1n_MIm4kzxe-tD=s220?authuser=0'}/>
                            <p>{localStorage.fullname}</p>
                        </div>
                        <div className='navLinks'>
                            <Link to={`/profile/${localStorage.userId}`}>{<FontAwesomeIcon icon={faUser}/>} Profile</Link>
                            <Link to={'/edit'}>{<FontAwesomeIcon icon={faPencil}/>} Edit</Link>
                            {/* {localStorage.role == 'client' ? (  
                                <Link to={'/becomeDriver'}>{<FontAwesomeIcon icon={faDriversLicense}/>}Become Driver</Link>
                            ) : ''}
                            {localStorage.role == 'driver' ? (
                                <Link to={'/createPost'}>{<FontAwesomeIcon icon={faPlus}/>}Add Post</Link>
                            ) : ''} */}
                            <a onClick={() => {OnLogout()}}><FontAwesomeIcon icon={faSignOut}/> Logout</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}