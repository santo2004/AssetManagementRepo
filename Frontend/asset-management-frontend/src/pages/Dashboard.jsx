import React from 'react';
import { useNavigate } from 'react-router-dom';
import './Dashboard.css';

function Dashboard() {
  const navigate = useNavigate();
  const userRole = 'admin'; // Replace with actual user role if available

  const handleNavigation = (path) => {
    navigate(path);
  };

  return (
    <div className="dashboard-container">
      <nav className="navbar">
        <div className="navbar-left">
          <h1>Asset Management</h1>
        </div>
        <ul className="nav-links">
          {userRole === 'admin' && (
            <li onClick={() => handleNavigation('/users')}>Users</li>
          )}
          <li className="dropdown">
            <span>Assets</span>
            <ul className="dropdown-content">
              <li onClick={() => handleNavigation('/assets')}>Assets</li>
              <li onClick={() => handleNavigation('/asset-requests')}>Asset Requests</li>
            </ul>
          </li>
          <li onClick={() => handleNavigation('/employee-assets')}>Employee Assets</li>
          <li onClick={() => handleNavigation('/service-requests')}>Service Requests</li>
          <li onClick={() => handleNavigation('/audit-requests')}>Audit Requests</li>
        </ul>
      </nav>

      <div className="description-section">
        <p>
          Welcome to the Asset Management Dashboard. This portal allows you to
          view and manage company assets, service requests, and audit reports
          efficiently.
        </p>
      </div>
    </div>
  );
}

export default Dashboard;
