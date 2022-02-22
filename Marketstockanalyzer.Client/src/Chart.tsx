import { useEffect, useState } from "react";
import {
  LineChart,
  ResponsiveContainer,
  Legend,
  Tooltip,
  Line,
  XAxis,
  YAxis,
  CartesianGrid,
} from "recharts";
import IPickedDates from './IPickedDates'
import ITick from './ITick'

const Chart = ({startDate, endDate} : IPickedDates) => {
  const [ticks, setTicks] = useState<ITick[]>([]);

  useEffect(() => {
    fetchTicks();
  });

  const fetchTicks = () => {
    fetch("https://localhost:7203/ticker", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        startDate: startDate,
        endDate: endDate, // Use your own property name / key
      }),
    })
      .then((res) => res.json())
      .then((result) => setTicks(result))
      .catch((err) => console.log("error"));
  };

  return (
    <div>
      <h1 className="text-heading">Line Chart Using Rechart</h1>
      <ResponsiveContainer width="100%" aspect={3}>
        <LineChart data={ticks} margin={{ right: 300 }}>
          <CartesianGrid />
          <XAxis dataKey="date" interval={"preserveStartEnd"} />
          <YAxis></YAxis>
          <Legend />
          <Tooltip />
          <Line dataKey="price" stroke="black" activeDot={{ r: 8 }} />
        </LineChart>
      </ResponsiveContainer>
    </div>
  );
};

export default Chart;
