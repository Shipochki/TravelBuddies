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
import { Link, useParams, redirect, Navigate, useNavigate } from 'react-router-dom'
import { GlobalContext } from '../../utils/contexts/GlobalContext'
import GroupsIcon from '@mui/icons-material/Groups';
import SettingsIcon from '@mui/icons-material/Settings';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import LogoutIcon from '@mui/icons-material/Logout';
import { OnLeaveGroupSubmit } from '../../services/UserGroupService'
import { Tooltip } from '@mui/material'
import { Loading } from '../Loading/Loading'

export const Group = ({group}) => {
    const navigate = useNavigate();
    const { OnSetGroup, OnSetGroups } = useContext(GlobalContext);
    const intervalRef = useRef(null);
    const [loading, setLoading] = useState(true);

    const { id } = useParams();

    useEffect(() => {
        if(!group.id || group.id != id){
            OnSetGroup(id);
        }

        intervalRef.current = setInterval(() => {
            OnSetGroup(id);
        }, 60000);

        setTimeout(() => {
            setLoading(false); // Set loading to false after data is fetched
          }, 500);

        return () => clearInterval(intervalRef.current);
    }, [id]);

    // useEffect(() => {
    //     const handlePopState = () => {
    //         // When user navigates back, trigger data reload
    //         OnSetGroup(id);
    //       };
      
    //       // Add event listener for browser navigation back
    //       window.addEventListener('popstate', handlePopState);
      
    //       // Cleanup: remove event listener when component unmounts
    //       return () => {
    //         window.removeEventListener('popstate', handlePopState);
    //       };
    // }, [id])

    const [membersVisable, setMembersVisable] = useState(false);

    const onClickVisable = () => {
        setMembersVisable(!membersVisable);
    }

    const [timeVisable, setTimeVisable] = useState(false);

    const onTimeVisable = () => {
        setTimeVisable(!timeVisable);
    }

    const [settingsVisable, setSettingsVisable] = useState(false);

    const onSettingVisa = () => {
        setSettingsVisable(!settingsVisable);
    }

    const onLeaveGroup = async (e) => {
        const text = `Are you sure you want to leave group:\n Group of ${group.name}`
        if(confirm(text) == true){
            e.preventDefault();

            await OnLeaveGroupSubmit(group.id);

            OnSetGroups();
            navigate('/');
        }
    }

    if(loading){
        return <Loading/>
    }

    return (
        <div className="group-main">
            <div className="group-content">
                <div className="group-info-container">
                    <div className='group-info'>
                        <h3>Group of {group.name}</h3>
                        <div className='group-info-options'>    
                            {/* <p className='group-info-members' onClick={onClickVisable}><FontAwesomeIcon icon={faPeopleGroup}/></p> */}
                            <Tooltip title='Members'>
                                <GroupsIcon className='group-info-members' onClick={onClickVisable}/>
                            </Tooltip>
                            <Tooltip title='Date and Time'>
                                <AccessTimeIcon className='group-time-btn' onClick={onTimeVisable}/>
                            </Tooltip>
                            <Tooltip title='Settings'>
                                <SettingsIcon className='group-settings-btn' onClick={onSettingVisa}/>
                            </Tooltip>
                        </div>
                    </div>
                    {settingsVisable && (
                        <div className='group-settings-menu'>
                            <p>Settings menu</p>
                            <button onClick={onLeaveGroup} className='leave-group-btn'><LogoutIcon/>Leave Group</button>
                        </div>
                    )}
                    {timeVisable && (
                        <div className='group-time'>
                            <p>Date and time</p>
                            <p>{group.date}</p>
                        </div>
                    )}
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