import { useEffect, useState } from "react";
import { GetAllGroupByUserId } from "../../services/GroupService"
import './Groups.css'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPeopleGroup } from "@fortawesome/free-solid-svg-icons";
import { LazyLoadImage } from "react-lazy-load-image-component";
import { useNavigate } from "react-router-dom";

export const Groups = () => {
    const [groups, setGroups] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            const data = await GetAllGroupByUserId(localStorage.userId);
            setGroups(data);
        };
        fetchData();
    }, []);

    const navigate = useNavigate();

    return(
        <div className="groups-main">
            <h3>{<FontAwesomeIcon icon={faPeopleGroup}/>} My Groups</h3>
            {groups.length == 0 && (
                <p>You don't have any groups</p>
            )}
            {groups.map((g, i) => (
                <div key={i} id={g.id} onClick={() => {
                    window.location.assign(`/group/${g.id}`);
                }} className="groups-main-group">
                    <div className="picture-group">
                        <LazyLoadImage src={g.creatorProfileLink ? g.creatorProfileLink : 'https://lh3.googleusercontent.com/d/1jzzGHsTZWHo57Mhria1n_MIm4kzxe-tD=s220?authuser=0'}/>
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