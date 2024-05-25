import './Catalog.css'

import { Post } from "../../components/Post/Post"
import { useEffect, useState } from 'react';
import { OnSearchSubmit } from '../../services/PostService';
import { Loading } from '../Loading/Loading';

export const Catalog = () => {
    const [posts, setPosts] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchData = async () => {
            const data = await OnSearchSubmit();
            setPosts(data);
            setLoading(false);
        };
        fetchData();
    }, []);

    if(loading){
        return <Loading/>
    }

    return (
        <div className="catalog-main">
            <div className='catalog-center'>
                {posts.map((p, i) => (
                    <div>
                        <Post key={i} post={p}/>
                    </div>
                ))}
            </div>
        </div>
    )
}