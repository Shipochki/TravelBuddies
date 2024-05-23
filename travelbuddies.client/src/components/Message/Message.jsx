import './Message.css';
import { Link, useNavigate } from 'react-router-dom';
import { LazyLoadImage } from 'react-lazy-load-image-component';
import { OnDeleteMessageSubmit } from '../../services/MessageService';
import { GetGroupById } from '../../services/GroupService';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPencil, faTrash } from '@fortawesome/free-solid-svg-icons';
import { useContext, useState } from 'react';

import personImgOffline from '../../utils/images/blank-profile-picture-973460_960_720.png'
import { GlobalContext } from '../../utils/contexts/GlobalContext';

export const Message = ({message, i, ownerId}) => {
    const {OnSetGroup} = useContext(GlobalContext);
    const [vissable, setVissable] = useState(false); 

    const ConfirmDelete = async (messageId) => {
        const text = `Are you sure you want to delete this message:\n ${message.text}`
        if(confirm(text) == true){
            await OnDeleteMessageSubmit(messageId);

            OnSetGroup(message.groupId)
        }
    }

    return (
        <div key={i} id={i} className={`message ${message.creatorId == localStorage.userId && 'my-message'}`}>
            <div className="message-content">
                {(message.creatorId == localStorage.userId 
                    || localStorage.role == 'admin'
                    || ownerId == localStorage.userId) && (
                    <div className="message-buttons">
                        <div>
                            {message.creatorId == localStorage.userId &&
                                <div className='message-edit-menu'>
                                        <button className="message-buttons-edit"
                                        onClick={() => {
                                            window.document.getElementById(`message-${message.id}`).style.display = 'flex';
                                        }}
                                    ><FontAwesomeIcon icon={faPencil}/> Edit</button>
                                </div>
                                }
                                <button onClick={(e) => {
                                e.preventDefault();
                                ConfirmDelete(message.id);
                                }} className="message-buttons-delete"><FontAwesomeIcon icon={faTrash}/> Delete</button>
                        </div>
                    </div>
                )}
                <div className="message-creator-info">
                    {message.creatorId != localStorage.userId && (
                    <Link to={`/profile/${message.creatorId}`}>
                        <LazyLoadImage
                        src={message.creatorProfileLink 
                        ? message.creatorProfileLink 
                        : personImgOffline}/> 
                    </Link>)}
                </div>
                <div className="message-text">
                    <p>{message.text}</p>  
                    <span>Sent: {message.createdOn}</span>  
                </div>
            </div>
        </div>
    )
}