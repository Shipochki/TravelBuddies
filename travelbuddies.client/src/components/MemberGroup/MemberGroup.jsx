import { LazyLoadImage } from "react-lazy-load-image-component";
import './MemberGroup.css'
import { useNavigate } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faIdCard } from "@fortawesome/free-solid-svg-icons";
import { OnRemoveUserFromGroupSubmit } from "../../services/UserGroupService";

import personImgOffline from '../../utils/images/blank-profile-picture-973460_960_720.png'
import { Box, Popper } from "@mui/material";
import { useContext, useState } from "react";
import MoreVertIcon from '@mui/icons-material/MoreVert';
import { GlobalContext } from "../../utils/contexts/GlobalContext";
import { OnCreateUserBlackListSubmit } from "../../services/UserBlackListService";
import RemoveCircleOutlineIcon from '@mui/icons-material/RemoveCircleOutline';
import BlockIcon from '@mui/icons-material/Block';

export const MemberGroup = ({member, ownerId, groupId}) => {
    const {OnSetGroup} = useContext(GlobalContext);
    const navigate = useNavigate();
    const [anchorEl, setAnchorEl] = useState(null);

    const ConfirmDelete = async () => {
        const text = `Are you sure you want to kick this user:\n ${member.fullName}`
        if(confirm(text) == true){
            const kickMemberFromKeys = {
                groupId: groupId,
                userId: member.id
            }

            await OnRemoveUserFromGroupSubmit(kickMemberFromKeys);

            OnSetGroup(groupId);
        }
    }

    const ConfirmBan = async () => {
        const text = `Are you sure you want to ban this user:\n ${member.fullName}`
        if(confirm(text) == true){
            const createUserBlackListFromKeys = {
                groupId: groupId,
                userId: member.id
            }

            await OnCreateUserBlackListSubmit(createUserBlackListFromKeys);

            OnSetGroup(groupId);
        }
    }

    const handleClick = (event) => {
        setAnchorEl(anchorEl ? null : event.currentTarget)
    }

    const open = Boolean(anchorEl);
    const id = open ? 'simple-popper' : undefined;

    return(
        <div className="member">
            <LazyLoadImage 
                onClick={() => {
                    navigate(`/profile/${member.id}`)
                }}
                src={member.profilePictureLink 
                ? member.profilePictureLink 
                : personImgOffline}/>
            <p
                onClick={() => {
                    navigate(`/profile/${member.id}`)
                }}>{member.id == ownerId && <FontAwesomeIcon icon={faIdCard}/>} {member.fullName}</p>
                {(localStorage.userId == ownerId || localStorage.role == 'admin') && (
                <button onClick={handleClick} aria-describedby={id} className="button-member-options"><MoreVertIcon/></button>
            )}
                <Popper id={id} open={open} anchorEl={anchorEl}>
                    <Box sx={{ 
                        border: 1, 
                        p: 1, 
                        bgcolor: 'background.paper', 
                        display: 'flex', 
                        flexDirection: 'column',
                        width: '80px'
                        }}>
                        <button className="kick-user-btn" onClick={ConfirmDelete}>Kick<RemoveCircleOutlineIcon fontSize="8px"/></button>
                        <button className="ban-user-btn" onClick={ConfirmBan}>Ban<BlockIcon fontSize="8px"/></button>
                    </Box>
                </Popper>
            
        </div>
    )
}