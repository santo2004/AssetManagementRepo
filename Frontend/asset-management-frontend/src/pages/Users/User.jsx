// src/pages/Users/User.jsx
import React from 'react';
import { useNavigate } from 'react-router-dom';
import Navbar from '../../components/Navbar';

export default function User() {
  const navigate = useNavigate();
  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h2 className="mb-3">User Management</h2>
        <div className="btn-group">
          <button className="btn btn-outline-primary" onClick={() => navigate('/users/list')}>User List</button>
          <button className="btn btn-outline-success" onClick={() => navigate('/users/create')}>Add User</button>
          <button className="btn btn-outline-info" onClick={() => navigate('/users/search')}>Search User</button>
        </div>
      </div>
    </>
  );
}
