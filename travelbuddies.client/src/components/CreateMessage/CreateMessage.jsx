import { OnCreateMessageSubmit } from '../../services/MessageService';
import './CreateMessage.css';
import { useForm } from '../../utils/hooks/useForm';
import { useNavigate } from 'react-router-dom';

const MessageFromKeys = {
    Text: 'text',
    GroupId: 'groupId'
}

export const CreateMessage = ({groupId}) => {
    const navigate = useNavigate();

    const { values, changeHandler, onSubmit } = useForm({
        [MessageFromKeys.Text]: '',
        [MessageFromKeys.GroupId]: groupId
    }, OnCreateMessageSubmit)

    const onCreateSubmit = async (id) => {
        values[MessageFromKeys.GroupId] = id;
        await OnCreateMessageSubmit(values);

        values[MessageFromKeys.Text] = '';

        window.location.reload();
        navigate(`/group/${id}`);
    }

    return(
        <div className="message-form">
                <form method="POST" onSubmit={(e) => {
                    e.preventDefault();
                    onCreateSubmit(groupId)}}>
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