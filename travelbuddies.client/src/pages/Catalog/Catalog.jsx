import './Catalog.css'

import { Post } from "../../components/Post/Post"
import { useEffect, useState } from 'react';
import { OnSearchSubmit } from '../../services/PostService';

export const Catalog = () => {
    const [posts, setPosts] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            const data = await OnSearchSubmit();
            setPosts(data);
        };
        fetchData();
    }, []);

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