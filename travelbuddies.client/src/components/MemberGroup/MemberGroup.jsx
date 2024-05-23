import { LazyLoadImage } from "react-lazy-load-image-component";
import './MemberGroup.css'
import { useNavigate } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faIdCard, faXmark } from "@fortawesome/free-solid-svg-icons";
import { OnRemoveUserFromGroupSubmit } from "../../services/UserGroupService";

export const MemberGroup = ({member, ownerId, groupId, setGroup}) => {
    const navigate = useNavigate();

    const ConfirmDelete = async () => {
        const text = `Are you sure you want to kick this user:\n ${member.fullName}`
        if(confirm(text) == true){
            const kickMemberFromKeys = {
                groupId: groupId,
                userId: member.id
            }

            await OnRemoveUserFromGroupSubmit(kickMemberFromKeys);

            const data = await GetGroupById(groupId);

            setGroup(data);
        }
    }
    return(
        <div className="member">
            <LazyLoadImage 
                onClick={() => {
                    navigate(`/profile/${member.id}`)
                }}
                src={member.profilePictureLink 
                ? member.profilePictureLink 
                : 'https://lh3.googleusercontent.com/d/1jzzGHsTZWHo57Mhria1n_MIm4kzxe-tD=s220?authuser=0'}/>
            <p
                onClick={() => {
                    navigate(`/profile/${member.id}`)
                }}>{member.id == ownerId && <FontAwesomeIcon icon={faIdCard}/>} {member.fullName}</p>
            {(localStorage.userId == ownerId || localStorage.role == 'admin') && (
                <button onClick={(e) => {
                    e.preventDefault();
                    ConfirmDelete();
                }} className="button-kick"><FontAwesomeIcon icon={faXmark}/></button>
            )}
        </div>
    )
}