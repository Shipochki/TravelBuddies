import { OnCreateMessageSubmit } from '../../services/MessageService';
import './CreateMessage.css';
import { useForm } from '../../utils/hooks/useForm';
import { useNavigate } from 'react-router-dom';

const MessageFromKeys = {
    Text: 'text',
    GroupId: 'groupId'
}

export const CreateMessage = ({group}) => {
    const navigate = useNavigate();

    const { values, changeHandler, onSubmit } = useForm({
        [MessageFromKeys.Text]: '',
        [MessageFromKeys.GroupId]: group.id
    }, OnCreateMessageSubmit)

    const onCreateSubmit = async (id) => {
        await OnCreateMessageSubmit(values);

        values[MessageFromKeys.Text] = '';

        navigate(`/group/${id}`);
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