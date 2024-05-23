import './Group.css'
import { useContext, useState } from "react"
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

    useEffect(() => {
        if(!group.id){
            const { id } = useParams();
            OnSetGroup(id);
        }
        // const fetchData = async () => {
        //     const data = await GetGroupById(id);
        //     setGroup(data);
        // fetchData();
        // }
    }, []);

    const [membersVisable, setMembersVisable] = useState(false);

    const onClickVisable = () => {
        setMembersVisable(!membersVisable);
    }

    useEffect(() => {
        const interval = setInterval(async () => {
            const { id } = useParams();
            OnSetGroup(id);
        }, 60000);

        return () => clearInterval(interval);
    }, []);

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