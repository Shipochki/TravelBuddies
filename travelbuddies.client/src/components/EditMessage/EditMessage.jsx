import { useContext } from 'react';
import './EditMessage.css'
import { GlobalContext } from '../../utils/contexts/GlobalContext';
import { OnUpdateMessageSubmit } from '../../services/MessageService';
import { GetGroupById } from '../../services/GroupService';
import { useForm } from '../../utils/hooks/useForm';

const EditMessageFromKeys = {
    Id: 'id',
    Text: 'text',
    GroupId: 'groupId',
}

export const EditMessage = ({message}) => {
    const { OnSetGroup } = useContext(GlobalContext);

    const { values, changeHandler, onSubmit } = useForm({
        [EditMessageFromKeys.Id]: message.id,
        [EditMessageFromKeys.Text]: message.text,
        [EditMessageFromKeys.GroupId]: message.groupId,
    }, OnUpdateMessageSubmit)

    const OnEditSubmit = async (groupId) => {
        window.document.getElementById(`message-${message.id}`).style.display = 'none';

        await OnUpdateMessageSubmit(values);

        values[EditMessageFromKeys.Text] = message.text;

        const result = await GetGroupById(groupId);

        OnSetGroup(result);
    }

    return (
        <div id={`message-${message.id}`} className='edit-message-main'>
            <form method='POST' onSubmit={(e) => {
                e.preventDefault();
                OnEditSubmit(message.groupId)
            }}>
                 <input 
                        type="text"
                        placeholder="Your message here..."
                        name={EditMessageFromKeys.Text}
                        value={values[EditMessageFromKeys.Text]}
                        onChange={changeHandler}
                        required
                        />
                    <button>Edit</button>
            </form>
            <span className='close'
            onClick={() => {
                window.document.getElementById(`message-${message.id}`).style.display = 'none';
            }}
            >&times;</span>
        </div>
    )
}