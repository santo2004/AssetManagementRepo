// src/pages/Unauthorized.jsx
import React from 'react';
import { useNavigate } from 'react-router-dom';

function Unauthorized() {
  const navigate = useNavigate();

  return (
    <div className="text-center mt-5">
      <h1 className="text-danger">403 - Unauthorized</h1>
      <p>You do not have permission to access this page.</p>
      <button className="btn btn-primary mt-3" onClick={() => navigate('/dashboard')}>
        Go to Dashboard
      </button>
    </div>
  );
}

export default Unauthorized;
