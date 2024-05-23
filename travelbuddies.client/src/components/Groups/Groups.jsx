import { useContext, useEffect, useState } from "react";
import { GetAllGroupByUserId, GetGroupById } from "../../services/GroupService"
import './Groups.css'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPeopleGroup } from "@fortawesome/free-solid-svg-icons";
import { LazyLoadImage } from "react-lazy-load-image-component";
import { useNavigate } from "react-router-dom";

import personImgOffline from '../../utils/images/blank-profile-picture-973460_960_720.png'
import { GlobalContext } from "../../utils/contexts/GlobalContext";

export const Groups = () => {
    const { OnSetGroup } = useContext(GlobalContext);
    const [groups, setGroups] = useState([]);
    const navigate = useNavigate();

    const OnGetGroup = async (id) => {
        //const data = await GetGroupById(id);
        OnSetGroup(id);
        // navigate(`../group/${id}`, { replace: true });
    }

    useEffect(() => {
        const fetchData = async () => {
            const data = await GetAllGroupByUserId(localStorage.userId);
            setGroups(data);
        };
        fetchData();
    }, []);

    
    return(
        <div className="groups-main">
            <h3>{<FontAwesomeIcon icon={faPeopleGroup}/>} My Groups</h3>
            {groups.length == 0 && (
                <p>You don't have any groups</p>
            )}
            {groups.map((g, i) => (
                <div key={i} id={g.id} onClick={() => {
                    OnGetGroup(g.id);
                    // window.location.assign(`/group/${g.id}`);
                }} className="groups-main-group">
                    <div className="picture-group">
                        <LazyLoadImage src={g.creatorProfileLink ? g.creatorProfileLink : personImgOffline}/>
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