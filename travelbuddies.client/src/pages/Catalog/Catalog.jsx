import { Post } from "../../components/Post/Post"

export const Catalog = ({posts}) => {
    return (
        <div className="catalog-main">
            {posts.map(post =>{
                <Post post={post}/>
            })}
        </div>
    )
}