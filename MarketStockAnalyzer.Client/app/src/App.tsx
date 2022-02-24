import React, { useState } from "react";
import "./App.css";
import Chart from "./Chart";
import "react-datepicker/dist/react-datepicker.css";
import DatePicker from "react-datepicker";
import { Link, Route } from "react-router-dom";

function App() {
  const [startDate, setStartDate] = useState(new Date("2019.12.01"));
  const [endDate, setEndDate] = useState(new Date("2020.12.01"));

  const selectStartDateHandler = (date: Date) => {
    setStartDate(date);
  };

  const selectEndDateHandler = (date: Date) => {
    setEndDate(date);
  };
  return (
    <div className="App">
        <DatePicker selected={startDate} onChange={selectStartDateHandler} />
        <DatePicker selected={endDate} onChange={selectEndDateHandler} />
        <Chart startDate={startDate} endDate={endDate} />
    </div>
  );
}

export default App;
