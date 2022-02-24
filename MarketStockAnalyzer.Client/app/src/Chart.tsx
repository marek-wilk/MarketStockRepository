import moment from "moment";
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
  }, [startDate, endDate]);

  const fetchTicks = async () => {
    await fetch("https://localhost:44367/Ticker", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        startDate,
        endDate, // Use your own property name / key
      }),
    })
      .then((res) => res.json())
      .then((result) => setTicks(result))
      .catch((err) => console.log("error"));
  };

  const formatTicker = (tickItem: Date) => {
    return moment(tickItem).format('DD MM YYYY')
  }

  return (
    <div>
      <h1 className="text-heading">Google Stock Market Chart</h1>
      <ResponsiveContainer width="100%" aspect={3}>
        <LineChart data={ticks} margin={{ right: 300 }}>
          <CartesianGrid />
          <XAxis dataKey="date" interval={"preserveStartEnd"} tickFormatter={formatTicker}/>
          <YAxis interval={"preserveStartEnd"}></YAxis>
          <Legend />
          <Tooltip />
          <Line dataKey="price" stroke="black" activeDot={{ r: 8 }} />
        </LineChart>
      </ResponsiveContainer>
    </div>
  );
};

export default Chart;
