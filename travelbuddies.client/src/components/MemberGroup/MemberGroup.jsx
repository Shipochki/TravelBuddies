import { LazyLoadImage } from "react-lazy-load-image-component";
import './MemberGroup.css'
import { useNavigate } from "react-router-dom";

export const MemberGroup = ({member}) => {
    const navigate = useNavigate();
    return(
        <div className="member" 
            onClick={() => {
                navigate(`/profile/${member.id}`)
            }}>
            <LazyLoadImage 
                src={member.profilePictureLink 
                ? member.profilePictureLink 
                : 'https://sttravelbuddies001.blob.core.windows.net/web/blank-profile-picture-973460_960_720.png'}/>
            <p>{member.fullName}</p>
        </div>
    )
}