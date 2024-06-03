import "./PostMenu.css";
import DynamicFeedIcon from "@mui/icons-material/DynamicFeed";
import PostAddIcon from "@mui/icons-material/PostAdd";
import { Link } from "react-router-dom";
import backgroundImg from "../../utils/images/white-background-with-blue-geometric-and-white-line-pattern-free-vector.jpg";
import { useEffect, useRef, useState } from "react";
import { Loading } from "../Loading/Loading";

export const PostMenu = () => {
  const [loading, setLoading] = useState(true);

  const intervalRef = useRef(null);

  useEffect(() => {
    setTimeout(() => {
      setLoading(false); // Set loading to false after data is fetched
    }, 500);
    return () => clearInterval(intervalRef.current);
  }, []);

  if (loading) {
    return <Loading />;
  }

  return (
    <div className="post-menu">
      <img className="demo-bg" src={backgroundImg} />
      <div className="post-menu-content">
        <div className="post-menu-header">
          <h2>Post Menu</h2>
        </div>
        <div className="post-menu-links">
          <Link to={"/myPosts"} className="post-menu-my">
            <h3>My</h3>
            <DynamicFeedIcon />
          </Link>
          <Link to={"/createPost"} className="post-menu-add">
            <h3>Add</h3>
            <PostAddIcon />
          </Link>
        </div>
      </div>
    </div>
  );
};
