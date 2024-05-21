import { Link } from "react-router-dom"
import './MyPosts.css'
import { Post } from "../../components/Post/Post"
import { NotDriver } from "../../components/NotDriver/NotDriver"
import { MyPost } from "../../components/MyPost/MyPost"

export const MyPosts = ({posts}) => {

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