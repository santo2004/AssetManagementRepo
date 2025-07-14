// src/components/Navbar.jsx
import React, { useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { AuthContext } from '../context/AuthContext';
import './Navbar.css';

function Navbar() {
  const navigate = useNavigate();
  const { logout } = useContext(AuthContext);

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark px-3 mt-3 rounded">
      <span className="navbar-brand">Asset Management</span>
      <ul className="navbar-nav ms-auto">
        <li className="nav-item">
          <button className="btn btn-link nav-link" onClick={() => navigate('/dashboard')}>
            Home
          </button>
        </li>
        <li className="nav-item dropdown">
          <div className="dropdown-hover nav-link">
            Users ▾
            <div className="dropdown-menu-custom">
              <button className="dropdown-item" onClick={() => navigate('/users/list')}>
                User List
              </button>
              <button className="dropdown-item" onClick={() => navigate('/users/create')}>
                User Form
              </button>
              <button className="dropdown-item" onClick={() => navigate('/users/search')}>
                User Search
              </button>
            </div>
          </div>
        </li>
        <li className="nav-item dropdown">
          <div className="dropdown-hover nav-link">
            Assets ▾
            <div className="dropdown-menu-custom">
              <button className="dropdown-item" onClick={() => navigate('/assets/list')}>
                Asset List
              </button>
              <button className="dropdown-item" onClick={() => navigate('/assets/create')}>
                Asset Form
              </button>
              <button className="dropdown-item" onClick={() => navigate('/assets/search')}>
                Asset Search
              </button>
              <button className="dropdown-item" onClick={() => navigate('/assets/request')}>
                Request Asset
              </button>
            </div>
          </div>
        </li>
        <li className="nav-item"><button className="btn btn-link nav-link" onClick={() => navigate('/asset-requests')}>Asset Requests</button></li>
        <li className="nav-item"><button className="btn btn-link nav-link" onClick={() => navigate('/employee-assets')}>Employee Assets</button></li>
        <li className="nav-item"><button className="btn btn-link nav-link" onClick={() => navigate('/service-requests')}>Service Requests</button></li>
        <li className="nav-item"><button className="btn btn-link nav-link" onClick={() => navigate('/audit-requests')}>Audit Requests</button></li>
        <li className="nav-item">
          <button
            className="btn btn-danger nav-link"
            onClick={() => {
              logout();
              navigate('/');
            }}>
            Logout
          </button>
        </li>
      </ul>
    </nav>
  );
}

export default Navbar;
