import './Group.css'
import { useState } from "react"
import { useContext } from "react"
import { GlobalContext } from "../../utils/contexts/GlobalContext"
import { GetGroupById } from "../../services/GroupService"
import { useEffect } from "react"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faPeopleGroup } from "@fortawesome/free-solid-svg-icons"
import { MemberGroup } from "../../components/MemberGroup/MemberGroup"
import { Message } from "../../components/Message/Message"
import { CreateMessage } from "../../components/CreateMessage/CreateMessage"

export const Group = ({group}) => {
    const { OnSetGroup } = useContext(GlobalContext);

    const [membersVisable, setMembersVisable] = useState(false);

    const onClickVisable = () => {
        setMembersVisable(!membersVisable);
    }

    useEffect(() => {
        const interval = setInterval(async () => {
            const result = await GetGroupById(group.id);
            OnSetGroup(result);
        }, 60000);

        return () => clearInterval(interval);
    }, []);

    return (
        <div className="group-main">
            <div className="group-content">
                <div className="group-info">
                    <p onClick={onClickVisable}><FontAwesomeIcon icon={faPeopleGroup}/> Members</p>
                    {membersVisable && group.members.map((m) => (
                       <MemberGroup member={m}/>
                    ))}
            </div>
            <div className="group-messages">
                {group.messages.map((m, i) => (
                    <Message message={m} i={i}/>
                ))}
            </div>
            </div>
            <CreateMessage group={group}/>
        </div>
    )
}