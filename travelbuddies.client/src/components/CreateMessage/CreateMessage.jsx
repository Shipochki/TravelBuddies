import { useContext } from 'react';
import { GetGroupById } from '../../services/GroupService';
import { OnCreateMessageSubmit } from '../../services/MessageService';
import './CreateMessage.css';
import { GlobalContext } from '../../utils/contexts/GlobalContext';
import { useForm } from '../../utils/hooks/useForm';

const MessageFromKeys = {
    Text: 'text',
    GroupId: 'groupId'
}

export const CreateMessage = ({group}) => {
    const { OnSetGroup } = useContext(GlobalContext);

    const { values, changeHandler, onSubmit } = useForm({
        [MessageFromKeys.Text]: '',
        [MessageFromKeys.GroupId]: group.id
    }, OnCreateMessageSubmit)

    const onCreateSubmit = async (id) => {
        await OnCreateMessageSubmit(values);

        values[MessageFromKeys.Text] = '';

        const result = await GetGroupById(id);

        OnSetGroup(result);
    }

    return(
        <div className="message-form">
                <form method="POST" onSubmit={(e) => {
                    e.preventDefault();
                    onCreateSubmit(group.id)}}>
                    <input 
                        type="text"
                        placeholder="Your message here..."
                        name={MessageFromKeys.Text}
                        value={values[MessageFromKeys.Text]}
                        onChange={changeHandler}
                        required
                        />
                    <button>Send</button>
                </form>
            </div>
    )
}