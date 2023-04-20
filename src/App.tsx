import React from "react";
import './App.css';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Dashboard from "./components/Dashboard";
import Profile from "./components/Profile";
import VideoReview from "./components/VideoReview";
import Home from "./components/Home";
import Upload from "./components/Upload";
import Settings from "./components/Settings";
import Insights from "./components/Insights";

const App = () => {
  return (
    <BrowserRouter>
      <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/dashboard" element={<Dashboard />} />
      <Route path="/profile" element={<Profile />} />
      <Route path="/video" element={<VideoReview/>}/>
      <Route path="/upload" element={<Upload />} />
      <Route path="/settings" element={<Settings />} />
      <Route path="/insights" element={<Insights/>} />
      </Routes>
    </BrowserRouter>
  );
};

export default App;


