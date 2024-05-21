import './MyPosts.css'
import { NotDriver } from "../../components/NotDriver/NotDriver"
import { MyPost } from "../../components/MyPost/MyPost"
import { useEffect, useState } from "react"
import { GetPostsByOwnerId } from "../../services/PostService"

export const MyPosts = () => {
    const [posts, setPosts] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            const data = await GetPostsByOwnerId(localStorage.userId);
            setPosts(data);
        }

        fetchData();
    }, []);
    
    return(
        <div className="myposts-main">
            {localStorage.role == 'driver' ? (
                <div className="myposts-content">
                    {posts.length > 0 ? (
                        <>
                            {posts.map((p) => (
                              <MyPost post={p}/>
                            ))}
                        </>
                    ): (
                        <div>
                            <h2>You don't have created Posts</h2>
                        </div>
                    )}
                </div>
            ) : (
                <NotDriver/>
            )}
        </div>
    )
}