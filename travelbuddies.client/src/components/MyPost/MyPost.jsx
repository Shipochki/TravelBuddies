import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import './MyPost.css'
import { faArrowRight } from '@fortawesome/free-solid-svg-icons'
import { Link } from 'react-router-dom'
import EditIcon from '@mui/icons-material/Edit';
import DeleteForeverOutlinedIcon from '@mui/icons-material/DeleteForeverOutlined';
import CheckCircleOutlineOutlinedIcon from '@mui/icons-material/CheckCircleOutlineOutlined';
import { GetPostsByOwnerId, OnCompletePostById, OnDeletePostSubmit } from '../../services/PostService';

const PayTypes = {
    0: 'Cash',
    1: 'Card',
    2: 'Cash and Card'
}

export const MyPost = ({post, setPosts}) => {

    const onDeletePost = async (e) => {
        const text = `Are you sure you want to delete post: \n 
            ${post.fromDestinationName} -> ${post.toDestinationName}
            ${post.dateAndTime}`
        if(confirm(text) == true){
            e.preventDefault();

            await OnDeletePostSubmit(post.id);

            const data = await GetPostsByOwnerId(localStorage.userId);

            setPosts(data);
        }
    }

    const onCompletePost = async (e) => {
        const text = `Are you sure you want to complete post: \n 
            ${post.fromDestinationName} -> ${post.toDestinationName}
            ${post.dateAndTime}`
        if(confirm(text) == true){
            e.preventDefault();

            await OnCompletePostById(post.id);

            const data = await GetPostsByOwnerId(localStorage.userId);

            setPosts(data);
        }
    }

    return(
        <div className='mypost-main'>
            <div className='mypost-content'>
                <div className='mypost-destin'>
                    <p>{post.fromDestinationName}</p>
                    <FontAwesomeIcon icon={faArrowRight}/>
                    <p>{post.toDestinationName}</p>
                </div>
                <p className='mypost-date-time'>{post.dateAndTime}</p>
                <p className='mypost-desc'>{post.description}</p>
                <p className='post-paymentType'>Payment type: {PayTypes[post.paymentType]}</p>
                <div className='mypost-info'>
                    <p className='mypost-seats'>Avalible seats: {post.freeSeats}</p>
                    <p className='mypost-pricePerSeat'>Price per seats: {post.pricePerSeat} {post.currency}</p>
                    <p className='mypost-baggage'>Baggage: {post.baggage ? 'Yes' : 'No'}</p>
                    <p className='mypost-pets'>Pets: {post.pets ? 'Yes': 'No'}</p>
                </div>
            </div>
            <div className='mypost-navigation'>
                <Link className='edit-post-btn' to={`/editPost/${post.id}`}><EditIcon fontSize='16px'/> Edit</Link>
                <Link className='complete-post-btn'onClick={onCompletePost} ><CheckCircleOutlineOutlinedIcon fontSize='16px'/> Complete</Link>
                <Link className='delete-post-btn' onClick={onDeletePost} ><DeleteForeverOutlinedIcon fontSize='16px'/> Delete</Link>
            </div>
        </div>
    )
}