import { LazyLoadImage } from "react-lazy-load-image-component";
import { GetUserById } from "../../services/UserService";
import { useContext } from "react";
import { GlobalContext } from "../../utils/contexts/GlobalContext";
import './MemberGroup.css'

export const MemberGroup = ({member}) => {
    const { OnSetUser } = useContext(GlobalContext);
    
    const LoadProfile = async (id) => {
        const result = await GetUserById(id);
        OnSetUser(result);
    }
    return(
        <div className="member" 
            onClick={async (e) => {
                e.preventDefault();
                LoadProfile(member.id);
            }}>
            <LazyLoadImage 
                src={member.profilePictureLink 
                ? member.profilePictureLink 
                : 'https://sttravelbuddies001.blob.core.windows.net/web/blank-profile-picture-973460_960_720.png'}/>
            <p>{member.fullName}</p>
        </div>
    )
}