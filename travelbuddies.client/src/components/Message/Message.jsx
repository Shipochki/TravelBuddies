import { useContext } from 'react';
import './Message.css';
import { GlobalContext } from '../../utils/contexts/GlobalContext';
import { GetUserById } from '../../services/UserService';
import { Link } from 'react-router-dom';
import { LazyLoadImage } from 'react-lazy-load-image-component';
import { EditMessage } from '../EditMessage/EditMessage';
import { OnDeleteMessageSubmit } from '../../services/MessageService';
import { GetGroupById } from '../../services/GroupService';

export const Message = ({message, i}) => {
    const { OnSetUser, OnSetGroup } = useContext(GlobalContext);
    
    const LoadProfile = async (id) => {
        const result = await GetUserById(id);
        OnSetUser(result);
    }

    const ConfirmDelete = async (messageId) => {
        const text = `Are you sure you want to delete this message:\n ${message.text}`
        if(confirm(text) == true){
            await OnDeleteMessageSubmit(messageId);

            const result = await GetGroupById(groupId);

            OnSetGroup(result);
        }
    }

    return (
        <div key={i} id={i} className={`message ${message.creatorId == localStorage.userId && 'my-message'}`}>
                        <div className="message-content">
                            {message.creatorId == localStorage.userId && (
                                <div className="message-buttons">
                                    <div className='message-edit-menu'>
                                        <button className="message-buttons-edit"
                                        onClick={() => {
                                            window.document.getElementById(`message-${message.id}`).style.display = 'flex';
                                        }}
                                        >Edit</button>
                                        <EditMessage message={message}/>
                                    </div>
                                    <button onClick={ConfirmDelete} className="message-buttons-delete">Delete</button>
                                </div>
                            )}
                            <div className="message-creator-info">
                                {message.creatorId != localStorage.userId && (
                                    <Link onClick={async (e) => {
                                        e.preventDefault();
                                        LoadProfile(message.creatorId);
                                        }}>
                                    <LazyLoadImage
                                        src={message.creatorProfileLink 
                                        ? message.creatorProfileLink 
                                        : 'https://sttravelbuddies001.blob.core.windows.net/web/blank-profile-picture-973460_960_720.png'}/> 
                                    </Link>)}
                            </div>
                            <div className="message-text">
                                <p>{message.text}</p>
                            </div>
                        </div>
                    </div>
    )
}