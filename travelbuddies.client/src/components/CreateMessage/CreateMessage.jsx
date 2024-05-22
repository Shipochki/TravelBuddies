import { OnCreateMessageSubmit } from '../../services/MessageService';
import './CreateMessage.css';
import { useForm } from '../../utils/hooks/useForm';
import { Button } from '@mui/material';
import SendIcon from '@mui/icons-material/Send';
import { GetGroupById } from '../../services/GroupService';

const MessageFromKeys = {
    Text: 'text',
    GroupId: 'groupId'
}

export const CreateMessage = ({groupId, setGroup}) => {
    const { values, changeHandler, onSubmit } = useForm({
        [MessageFromKeys.Text]: '',
        [MessageFromKeys.GroupId]: groupId
    }, OnCreateMessageSubmit)

    const onCreateSubmit = async (id) => {
        values[MessageFromKeys.GroupId] = id;
        await OnCreateMessageSubmit(values);

        values[MessageFromKeys.Text] = '';

        const data = await GetGroupById(groupId);
        setGroup(data);
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
                    <Button type='submit'
                    variant="contained" endIcon={<SendIcon />}>
                        Send
                    </Button>
                </form>
            </div>
    )
}