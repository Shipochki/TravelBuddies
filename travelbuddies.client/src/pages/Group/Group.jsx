import './Group.css'
import { useContext, useRef, useState } from "react"
import { GetGroupById } from "../../services/GroupService"
import { useEffect } from "react"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faPeopleGroup } from "@fortawesome/free-solid-svg-icons"
import { MemberGroup } from "../../components/MemberGroup/MemberGroup"
import { Message } from "../../components/Message/Message"
import { CreateMessage } from "../../components/CreateMessage/CreateMessage"
import { EditMessage } from '../../components/EditMessage/EditMessage'
import { Link, useParams, redirect, Navigate } from 'react-router-dom'
import { GlobalContext } from '../../utils/contexts/GlobalContext'

export const Group = ({group}) => {
    const { OnSetGroup } = useContext(GlobalContext);
    const intervalRef = useRef(null);

    const { id } = useParams();

    useEffect(() => {
        if(!group.id){
            OnSetGroup(id);
        }
    }, [id]);

    useEffect(() => {
        const handlePopState = () => {
            // When user navigates back, trigger data reload
            OnSetGroup(id);
          };
      
          // Add event listener for browser navigation back
          window.addEventListener('popstate', handlePopState);
      
          // Cleanup: remove event listener when component unmounts
          return () => {
            window.removeEventListener('popstate', handlePopState);
          };
    }, [id])

    useEffect(() => {
        intervalRef.current = setInterval(() => {
            OnSetGroup(id);
        }, 5000);

        return () => clearInterval(intervalRef.current);
    }, [id]);

    const [membersVisable, setMembersVisable] = useState(false);

    const onClickVisable = () => {
        setMembersVisable(!membersVisable);
    }

    return (
        <div className="group-main">
            <div className="group-content">
                <div className="group-info">
                    <p className='group-info-members' onClick={onClickVisable}><FontAwesomeIcon icon={faPeopleGroup}/> Members</p>
                    {membersVisable && group.members.map((m) => (
                       <MemberGroup key={`member-key-${m.id}`} member={m} ownerId={group.creator.id} groupId={group.id}/>
                    ))}
            </div>
            <div className="group-messages">
                    {group.messages && group.messages.map((m, i) => (
                        <div key={`message-container-key${i}`}>
                            <Message key={`message-key-${m.id}`} message={m} i={i} ownerId={group.creator.id}/>
                            <EditMessage key={`edit-message-key-${m.id}`} message={m}/>
                        </div>
                    ))}
            </div>
            </div>
            <CreateMessage groupId={group.id}/>
        </div>
    )
}