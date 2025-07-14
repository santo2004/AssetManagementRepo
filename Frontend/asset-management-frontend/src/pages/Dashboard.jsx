// src/pages/Dashboard.jsx
import React from 'react';
import Navbar from '../components/Navbar';

function Dashboard() {
  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h2>Home page</h2>
        <p>Welcome to the Asset Management home page.</p>
      </div>
    </>
  );
}

export default Dashboard;
