import { Post } from "../../components/Post/Post"

export const Catalog = ({posts}) => {
    return (
        <div className="catalog-main">
            {posts.map((x) => (
                <Post key={x.id} {...x}/>
            ))}
        </div>
    )
}