import { LazyLoadImage } from "react-lazy-load-image-component"
import './Group.css'
import { useState } from "react"
import { useForm } from "../../utils/hooks/useForm"
import { OnCreateMessageSubmit } from "../../services/MessageService"
import { useContext } from "react"
import { GlobalContext } from "../../utils/contexts/GlobalContext"
import { GetGroupById } from "../../services/GroupService"
import { useEffect } from "react"

const MessageFromKeys = {
    Text: 'text',
    GroupId: 'groupId'
}

export const Group = ({group}) => {
    const { OnSetGroup } = useContext(GlobalContext);

    const { values, changeHandler, onSubmit } = useForm({
        [MessageFromKeys.Text]: '',
        [MessageFromKeys.GroupId]: group.id
    }, OnCreateMessageSubmit)

    const [membersVisable, setMembersVisable] = useState(false);

    const onClickVisable = () => {
        setMembersVisable(!membersVisable);
    }

    const onCreateSubmit = async (id) => {
        await OnCreateMessageSubmit(values);

        values[MessageFromKeys.Text] = '';

        const result = await GetGroupById(id);

        OnSetGroup(result);
    }

    useEffect(() => {
        const interval = setInterval(async () => {
            const result = await GetGroupById(group.id);
            OnSetGroup(result);
        }, 60000);

        return () => clearInterval(interval);
    }, []);

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
                {group.messages.map((m) => (
                    <div className={`message ${m.creatorId == localStorage.userId && 'my-message'}`}>
                        <div className="message-content">
                            <div className="message-creator-info">
                                <LazyLoadImage 
                                    src={m.creatorProfileLink 
                                    ? m.creatorProfileLink 
                                    : 'https://sttravelbuddies001.blob.core.windows.net/web/blank-profile-picture-973460_960_720.png'}/>
                            </div>
                            <div className="message-text">
                                <p>{m.text}</p>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
            </div>
            <div className="message-form">
                <form method="POST" onSubmit={(e) => {
                    e.preventDefault();
                    onCreateSubmit(group.id)}}>
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