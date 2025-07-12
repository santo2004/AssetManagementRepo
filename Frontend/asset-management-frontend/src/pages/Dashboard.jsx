// src/pages/Dashboard.jsx
import React from 'react';
import Navbar from '../components/Navbar';

function Dashboard() {
  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h2>Dashboard</h2>
        <p>Welcome to the Asset Management Dashboard.</p>
      </div>
    </>
  );
}

export default Dashboard;
