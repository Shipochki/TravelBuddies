import './Catalog.css'

import { Post } from "../../components/Post/Post"

export const Catalog = ({posts}) => {
    return (
        <div className="catalog-main">
            <div className='catalog-center'>
                {posts.map((x, i) => (
                    <div>
                        <Post key={i} {...x}/>
                    </div>
                ))}
            </div>
        </div>
    )
}