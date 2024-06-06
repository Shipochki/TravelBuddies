import React, { useState } from 'react';
import './Calendar.css';

const Calendar = (props) => {
  const [date, setDate] = useState(new Date());
  const { handle } = props;

  const monthNames = [
    "January", "February", "March",
    "April", "May", "June", "July",
    "August", "September", "October",
    "November", "December"
  ];

  const daysInMonth = (month, year) => {
    return new Date(year, month + 1, 0).getDate();
  };

  const firstDayOfMonth = () => {
    const firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
    return firstDay.getDay();
  };

  const handlePrevMonth = () => {
    setDate(new Date(date.getFullYear(), date.getMonth() - 1, 1));
  };

  const handleNextMonth = () => {
    setDate(new Date(date.getFullYear(), date.getMonth() + 1, 1));
  };

  const renderDays = () => {
    const days = [];
    const daysCount = daysInMonth(date.getMonth(), date.getFullYear());
    const startingDay = firstDayOfMonth();

    // Add empty cells for the days before the start of the month
    for (let i = 0; i < startingDay; i++) {
      days.push(<div key={`empty-${i}`} className="empty-cell"></div>);
    }

    // Add the days of the month
    for (let i = 1; i <= daysCount; i++) {
      days.push(<div onClick={setReturnDate} key={`day-${i}`} className="day-cell">{i}</div>);
    }

    return days;
  };

  const setReturnDate = (event) => {
    const value = event.target.textContent;
    handle(new Date(date.getFullYear(), date.getMonth(), Number(value)));
    event.target.className += ' clickedCell';
  };

  return (
    <div className="calendar">
      <div className="calendar-header">
        <button type='button' className="nav-btn" onClick={handlePrevMonth}>&lt;</button>
        <h2>{monthNames[date.getMonth()]} {date.getFullYear()}</h2>
        <button type='button' className="nav-btn" onClick={handleNextMonth}>&gt;</button>
      </div>
      <div className="calendar-body">
        <div className="weekdays">
          <div>Sun</div>
          <div>Mon</div>
          <div>Tue</div>
          <div>Wed</div>
          <div>Thu</div>
          <div>Fri</div>
          <div>Sat</div>
        </div>
        <div className="days">
          {renderDays()}
        </div>
      </div>
    </div>
  );
};

export default Calendar;
