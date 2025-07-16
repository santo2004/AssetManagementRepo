import React, { useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { AuthContext } from '../context/AuthContext';
import './Navbar.css';

function Navbar() {
  const navigate = useNavigate();
  const { auth, logout } = useContext(AuthContext);
  const role = auth?.user?.role;

  const isAdmin = role === 'Admin';
  const isUser = role === 'Employee' || role === 'Manager';

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark px-3 mt-3 rounded">
      <span className="navbar-brand">Asset Management</span>
      <ul className="navbar-nav ms-auto">
        <li className="nav-item">
          <button className="btn btn-link nav-link" onClick={() => navigate('/dashboard')}>
            Home
          </button>
        </li>

        {isAdmin && (
          <li className="nav-item dropdown">
            <div className="dropdown-hover nav-link">
              Users ▾
              <div className="dropdown-menu-custom">
                <button className="dropdown-item" onClick={() => navigate('/users/list')}>User List</button>
                <button className="dropdown-item" onClick={() => navigate('/users/create')}>User Form</button>
                <button className="dropdown-item" onClick={() => navigate('/users/search')}>User Search</button>
              </div>
            </div>
          </li>
        )}

        {(isAdmin || isUser) && (
          <li className="nav-item dropdown">
            <div className="dropdown-hover nav-link">
              Assets ▾
              <div className="dropdown-menu-custom">
                <button className="dropdown-item" onClick={() => navigate('/assets/list')}>Asset List</button>
                {isAdmin && (
                  <>
                    <button className="dropdown-item" onClick={() => navigate('/assets/create')}>Asset Form</button>
                  </>
                )}
                <button className="dropdown-item" onClick={() => navigate('/assets/search')}>Asset Search</button>
                {isUser && (
                  <button className="dropdown-item" onClick={() => navigate('/assets/request')}>Request Asset</button>
                )}
              </div>
            </div>
          </li>
        )}

        {isAdmin && (
          <li className="nav-item dropdown">
            <div className="dropdown-hover nav-link">
              Asset Requests ▾
              <div className="dropdown-menu-custom">
                <button className="dropdown-item" onClick={() => navigate('/asset-requests/list')}>View Requests</button>
              </div>
            </div>
          </li>
        )}

        {isUser && (
          <li className="nav-item dropdown">
            <div className="dropdown-hover nav-link">
              Employee Assets ▾
              <div className="dropdown-menu-custom">
                <button className="dropdown-item" onClick={() => navigate('/employee-assets/list')}>My Allocations</button>
                <button className="dropdown-item" onClick={() => navigate('/employee-assets/return')}>Return Asset</button>
              </div>
            </div>
          </li>
        )}

        {(isUser || isAdmin) && (
          <li className="nav-item dropdown">
            <div className="dropdown-hover nav-link">
              Service Requests ▾
              <div className="dropdown-menu-custom">
                {isUser && (
                  <>
                    <button className="dropdown-item" onClick={() => navigate('/service-request')}>Create Service Request</button>
                    <button className="dropdown-item" onClick={() => navigate('/my-requests')}>My Service Requests</button>
                  </>
                )}
                {isAdmin && (
                  <button className="dropdown-item" onClick={() => navigate('/admin/requests')}>Manage All Requests</button>
                )}
              </div>
            </div>
          </li>
        )}

        <li className="nav-item">
          <button
            className="btn btn-danger nav-link"
            onClick={() => {
              logout();
              navigate('/');
            }}
          >
            Logout
          </button>
        </li>
      </ul>
    </nav>
  );
}

export default Navbar;
