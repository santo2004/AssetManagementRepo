// src/pages/Home.jsx
import React from 'react';
import { useNavigate } from 'react-router-dom';

function Home() {
  const navigate = useNavigate();

  return (
    <div className="container text-center mt-5">
      <h2>Welcome to the Asset Management System</h2>
      <p>Please login to continue.</p>
      <button className="btn btn-primary mt-3" onClick={() => navigate('/login')}>Go to Login</button>
    </div>
  );
}

export default Home;
