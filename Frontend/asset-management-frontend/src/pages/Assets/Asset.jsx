import React from 'react';
import { useNavigate } from 'react-router-dom';
import Navbar from '../../components/Navbar';

export default function Asset() {
  const navigate = useNavigate();

  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h2>Asset Management</h2>
        <p>Select an option:</p>
        <div className="btn-group">
          <button className="btn btn-outline-primary" onClick={() => navigate('/assets/list')}>
            Assets List
          </button>
          <button className="btn btn-outline-success" onClick={() => navigate('/assets/create')}>
            Assets User
          </button>
          <button className="btn btn-outline-info" onClick={() => navigate('/assets/search')}>
            Assets User
          </button>
        </div>
      </div>
    </>
  );
}
