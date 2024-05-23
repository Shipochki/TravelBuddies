import { OnCreateMessageSubmit } from '../../services/MessageService';
import './CreateMessage.css';
import { useForm } from '../../utils/hooks/useForm';
import { Button } from '@mui/material';
import SendIcon from '@mui/icons-material/Send';
import { GetGroupById } from '../../services/GroupService';
import { useContext } from 'react';
import { GlobalContext } from '../../utils/contexts/GlobalContext';

const MessageFromKeys = {
    Text: 'text',
    GroupId: 'groupId'
}

export const CreateMessage = ({groupId}) => {
    const { OnSetGroup } = useContext(GlobalContext);

    const { values, changeHandler, onSubmit } = useForm({
        [MessageFromKeys.Text]: '',
        [MessageFromKeys.GroupId]: groupId
    }, OnCreateMessageSubmit)

    const onCreateSubmit = async (id) => {
        values[MessageFromKeys.GroupId] = id;
        await OnCreateMessageSubmit(values);

        values[MessageFromKeys.Text] = '';

        OnSetGroup(groupId);
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