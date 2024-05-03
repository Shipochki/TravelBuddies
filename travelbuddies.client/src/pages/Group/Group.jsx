import { LazyLoadImage } from "react-lazy-load-image-component"
import './Group.css'
import { useState } from "react"
import { useForm } from "../../utils/hooks/useForm"
import { OnCreateMessageSubmit } from "../../services/MessageService"

const MessageFromKeys = {
    Text: 'text',
    GroupId: 'groupId'
}

export const Group = ({group}) => {
    const { values, changeHandler, onSubmit } = useForm({
        [MessageFromKeys.Text]: '',
        [MessageFromKeys.GroupId]: group.id
    }, OnCreateMessageSubmit)

    const [membersVisable, setMembersVisable] = useState(false);

    const onClickVisable = () => {
        setMembersVisable(!membersVisable);
    }

    return (
        <div className="group-main">
            <div className="group-content">
                <div className="group-info">
                    <p onClick={onClickVisable}>Members</p>
                    {membersVisable && group.members.map((m) => (
                        <div  className="member">
                            <LazyLoadImage 
                            src={m.profilePictureLink 
                            ? m.profilePictureLink 
                            : 'https://sttravelbuddies001.blob.core.windows.net/web/blank-profile-picture-973460_960_720.png'}/>
                            <p>{m.fullName}</p>
                        </div>
                    ))}
            </div>
            <div className="group-messages">
                {group.messages.map((m) => {
                    <div className="message">
                        <div className="message-creator-info">
                            <LazyLoadImage 
                                src={m.creatorProfileLink 
                                ? m.creatorProfileLink 
                                : 'https://sttravelbuddies001.blob.core.windows.net/web/blank-profile-picture-973460_960_720.png'}/>
                            <p>{m.creatorName}</p>
                        </div>
                        <div className="message-content">
                            <p>{m.text}</p>
                        </div>
                    </div>
                })}
            </div>
            </div>
            <div className="message-form">
                <form method="POST" onSubmit={onSubmit}>
                    <input 
                        type="text"
                        placeholder="Your message here..."
                        name={MessageFromKeys.Text}
                        value={values[MessageFromKeys.Text]}
                        onChange={changeHandler}
                        required
                        />
                    <button>Send</button>
                </form>
            </div>
        </div>
    )
}