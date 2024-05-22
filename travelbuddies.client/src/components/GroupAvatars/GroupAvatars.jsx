import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import AvatarGroup from '@mui/material/AvatarGroup';

export const GroupAvatars = ({members}) => {

    return(
        <AvatarGroup max={4}>
            {members.map((m) => {
                <Avatar src={m.profilePictureLink}/>
            })}
        </AvatarGroup>
    )
}