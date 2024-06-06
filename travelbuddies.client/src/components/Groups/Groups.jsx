import { useContext, useEffect } from "react";
import "./Groups.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPeopleGroup } from "@fortawesome/free-solid-svg-icons";
import { LazyLoadImage } from "react-lazy-load-image-component";

import personImgOffline from "../../utils/images/blank-profile-picture-973460_960_720.png";
import { GlobalContext } from "../../utils/contexts/GlobalContext";
import { Button } from "@mui/material";

export const Groups = ({ groups }) => {
  const { OnSetGroup, OnSetGroups } = useContext(GlobalContext);

  const OnGetGroup = async (id) => {
    OnSetGroup(id);
  };

  useEffect(() => {
    if (!groups.length) {
      OnSetGroups();
    }
  }, []);

  return (
    <div className="groups-menu-main">
      <div id="groups-menu-main" className="groups-main">
        <h3>{<FontAwesomeIcon icon={faPeopleGroup} />} My Groups</h3>
        {groups.length == 0 && <p>You don't participate in any groups</p>}
        {groups.map((g, i) => (
          <Button
            sx={{
              color: "black",
            }}
            variant="text"
          >
            <div
              key={i}
              id={g.id}
              onClick={() => {
                OnGetGroup(g.id);
              }}
              className="groups-main-group"
            >
              <div className="picture-group">
                <LazyLoadImage
                  src={
                    g.creatorProfileLink
                      ? g.creatorProfileLink
                      : personImgOffline
                  }
                />
              </div>
              <div className="groups-group">
                <p>{g.name}</p>
                <p className="groups-group-date">{g.date}</p>
              </div>
            </div>
          </Button>
        ))}
      </div>
    </div>
  );
};
