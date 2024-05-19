import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { solidStar } from "../StarSelector/solidStar"

export const StarGenerator = ({num}) => {

    return(
        <div className="stars">
            {[...Array(num)].map(i => (
                <span><FontAwesomeIcon icon={solidStar}/></span>
            ))}
        </div>
    )
}