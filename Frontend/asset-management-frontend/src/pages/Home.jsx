import React from 'react';
import { useNavigate } from 'react-router-dom';

function Home() {
  const navigate = useNavigate();

  return (
    <div className="d-flex justify-content-center align-items-center vh-100 bg-light">
      <div className="card shadow-lg p-4" style={{ width: '100%', maxWidth: '500px' }}>
        <div className="card-body text-center">
          <h2 className="card-title mb-3">Welcome to Asset Management System</h2>
          <p className="card-text">To access your dashboard and manage assets, please log in.</p>
          <button className="btn btn-primary mt-3" onClick={() => navigate('/login')}>
            Go to Login
          </button>
        </div>
      </div>
    </div>
  );
}

export default Home;
