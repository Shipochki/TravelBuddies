import './StarSelector.css'
import {solidStar} from './solidStar';
import {regularStar} from './regularStar';

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useState } from "react";

export const StarSelector = ({ totalStars = 5, onSelect }) => {
    const [selectedStars, setSelectedStars] = useState(0);
  
    const handleStarClick = (star) => {
      setSelectedStars(star);
      if (onSelect) {
        onSelect(star);
      }
    };
  
    return (
      <div className='starselector'>
        {[...Array(totalStars)].map((_, index) => (
          <Star
            key={index}
            filled={index < selectedStars}
            onClick={() => handleStarClick(index + 1)}
          />
        ))}
      </div>
    );
  };
  
  const Star = ({ filled, onClick }) => {
    return (
      <span
        style={{ cursor: 'pointer' }}
        onClick={onClick}
      >
        <FontAwesomeIcon icon={filled ? solidStar : regularStar}/>
      </span>
    );
  };