// src/pages/Notfound.jsx
import React from 'react';
import { useNavigate } from 'react-router-dom';

function Notfound() {
  const navigate = useNavigate();

  return (
    <div className="text-center mt-5">
      <h1 className="text-danger">404 - Page Not Found</h1>
      <p>The page you're looking for doesn't exist.</p>
      <button className="btn btn-secondary mt-3" onClick={() => navigate('/')}>
        Go to Home
      </button>
    </div>
  );
}

export default Notfound;
