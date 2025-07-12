import React, { useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { AuthContext } from '../context/AuthContext';

function Navbar() {
  const navigate = useNavigate();
  const { logout } = useContext(AuthContext);

  const handleLogout = () => {
    logout();
    navigate('/');
  };

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark px-3 mt-3 rounded">
      <span className="navbar-brand">Asset Management</span>
      <ul className="navbar-nav ms-auto">
        <li className="nav-item"><button className="btn btn-link nav-link" onClick={() => navigate('/dashboard')}>Dashboard</button></li>
        <li className="nav-item"><button className="btn btn-link nav-link" onClick={() => navigate('/users')}>Users</button></li>
        <li className="nav-item"><button className="btn btn-link nav-link" onClick={() => navigate('/assets')}>Assets</button></li>
        <li className="nav-item"><button className="btn btn-link nav-link" onClick={() => navigate('/asset-requests')}>Asset Requests</button></li>
        <li className="nav-item"><button className="btn btn-link nav-link" onClick={() => navigate('/employee-assets')}>Employee Assets</button></li>
        <li className="nav-item"><button className="btn btn-link nav-link" onClick={() => navigate('/service-requests')}>Service Requests</button></li>
        <li className="nav-item"><button className="btn btn-link nav-link" onClick={() => navigate('/audit-requests')}>Audit Requests</button></li>
        <li className="nav-item"><button className="btn btn-danger nav-link" onClick={handleLogout}>Logout</button></li>
      </ul>
    </nav>
  );
}

export default Navbar;
