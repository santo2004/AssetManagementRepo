import React from 'react';
import { useNavigate } from 'react-router-dom';
import './Navbar.css'; // Add this line to apply hover CSS

function Navbar() {
  const navigate = useNavigate();
  const userRole = 'admin'; // Replace this with dynamic role logic if needed

  const handleNavigation = (path) => {
    navigate(path);
  };

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark px-3 rounded mt-3">
      <span className="navbar-brand">Asset Management</span>
      <div className="collapse navbar-collapse">
        <ul className="navbar-nav ms-auto">
          {userRole === 'admin' && (
            <li className="nav-item">
              <button className="btn btn-link nav-link" onClick={() => handleNavigation('/users')}>
                Users
              </button>
            </li>
          )}
          <li className="nav-item dropdown hover-dropdown">
            <span className="btn btn-link nav-link dropdown-toggle">
              Assets
            </span>
            <ul className="dropdown-menu">
              <li>
                <button className="dropdown-item" onClick={() => handleNavigation('/assets')}>
                  Assets
                </button>
              </li>
              <li>
                <button className="dropdown-item" onClick={() => handleNavigation('/asset-requests')}>
                  Asset Requests
                </button>
              </li>
            </ul>
          </li>
          <li className="nav-item">
            <button className="btn btn-link nav-link" onClick={() => handleNavigation('/employee-assets')}>
              Employee Assets
            </button>
          </li>
          <li className="nav-item">
            <button className="btn btn-link nav-link" onClick={() => handleNavigation('/service-requests')}>
              Service Requests
            </button>
          </li>
          <li className="nav-item">
            <button className="btn btn-link nav-link" onClick={() => handleNavigation('/audit-requests')}>
              Audit Requests
            </button>
          </li>
        </ul>
      </div>
    </nav>
  );
}

export default Navbar;
