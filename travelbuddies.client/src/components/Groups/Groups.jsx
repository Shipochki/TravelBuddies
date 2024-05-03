import { useContext } from "react";
import { GetAllMessageByGroupId } from "../../services/MessageService"
import { GlobalContext } from "../../utils/contexts/GlobalContext";
import './Groups.css'

export const Groups = ({groups}) => {
    const { OnSetMessages } = useContext(GlobalContext);


    const onClick = async (e) => {
        e.preventDefault();
        const result = await GetAllMessageByGroupId(e.target.id);

        OnSetMessages(result);
    }

    return(
        <div className="groups-main">
            <h3>My Groups</h3>
            {groups.length == 0 && (
                <p>You don't have any groups</p>
            )}
            {groups.map((g, i) => (
                <div key={i} id={g.Id} onClick={onClick} className="groups-main-group">
                    <p>{g.name}</p>
                    <p>{g.date}</p>
                </div>
            ))}
        </div>
    )
}