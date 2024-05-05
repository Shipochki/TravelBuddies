import { useContext } from "react";
import { GetGroupById } from "../../services/GroupService"
import { GlobalContext } from "../../utils/contexts/GlobalContext";
import './Groups.css'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPeopleGroup } from "@fortawesome/free-solid-svg-icons";
import { LazyLoadImage } from "react-lazy-load-image-component";

export const Groups = ({groups}) => {
    const { OnSetGroup } = useContext(GlobalContext);

    const LoadGroup = async (id) => {
        const result = await GetGroupById(id);

        OnSetGroup(result);
    }

    return(
        <div className="groups-main">
            <h3>{<FontAwesomeIcon icon={faPeopleGroup}/>} My Groups</h3>
            {groups.length == 0 && (
                <p>You don't have any groups</p>
            )}
            {groups.map((g, i) => (
                <div key={i} id={g.id} onClick={(e) => {
                    e.preventDefault();
                    LoadGroup(g.id);
                }} className="groups-main-group">
                    <div className="picture-group">
                        <LazyLoadImage src={g.creatorProifileLink ? g.creatorProifileLink : 'https://sttravelbuddies001.blob.core.windows.net/web/blank-profile-picture-973460_960_720.png'}/>
                    </div>
                    <div>  
                        <p>{g.name}</p>
                        <p>{g.date}</p>
                    </div>
                </div>
            ))}
        </div>
    )
}