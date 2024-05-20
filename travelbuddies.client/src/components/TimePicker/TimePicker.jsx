import { useState } from "react"
import './TimePicker.css';

export const TimePicker = ({OnChangeTime}) => {
    const [hours, serHours] = useState('');
    const [minutes, setMinutes] = useState('');

    const setTime = () => {
        if (isValidTime(hours, minutes)) {
            alert(`Selected time: ${formatTime(hours)}:${formatTime(minutes)}`);
          } else {
            alert('Please enter a valid time.');
          }
    };

    const isValidTime = (hours, minutes) => {
        return hours !== '' && minutes !== '' &&
               parseInt(hours) >= 0 && parseInt(hours) <= 23 &&
               parseInt(minutes) >= 0 && parseInt(minutes) <= 59;
        };

    const formatTime = (time) => {
        return time.toString().padStart(2, '0');
    };

    return (
        <div className="time-picker">
          <input
            type="number"
            value={hours}
            onChange={(e) => setHours(e.target.value)}
            min="0"
            max="23"
            placeholder="HH"
            className="time-input"
          />
          <span>:</span>
          <input
            type="number"
            value={minutes}
            onChange={(e) => setMinutes(e.target.value)}
            min="0"
            max="59"
            placeholder="MM"
            className="time-input"
          />
          <button onClick={setTime} className="set-button">Set</button>
        </div>
      );
}