import "./MyPosts.css";
import { NotDriver } from "../../components/NotDriver/NotDriver";
import { MyPost } from "../../components/MyPost/MyPost";
import { useEffect, useState } from "react";
import { GetPostsByOwnerId } from "../../services/PostService";
import { Loading } from "../Loading/Loading";

import backgroundImg from "../../utils/images/white-background-with-blue-geometric-and-white-line-pattern-free-vector.jpg";

export const MyPosts = () => {
  const [posts, setPosts] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      if (localStorage.role == "driver") {
        const data = await GetPostsByOwnerId(localStorage.userId);
        setPosts(data);
      }

      setLoading(false);
    };

    fetchData();
  }, []);

  if (loading) {
    return <Loading />;
  }

  return (
    <div className="myposts-main">
      <img className="demo-bg" src={backgroundImg} />
      {localStorage.role == "driver" ? (
        <div className="myposts-content">
          {posts.length > 0 ? (
            <>
              {posts.map((p) => (
                <MyPost post={p} setPosts={setPosts} />
              ))}
            </>
          ) : (
            <div>
              <h2>Looks like you haven't created any posts yet! Why not start now?</h2>
            </div>
          )}
        </div>
      ) : (
        <NotDriver />
      )}
    </div>
  );
};
