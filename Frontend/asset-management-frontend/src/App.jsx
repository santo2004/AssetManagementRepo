import React from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import Navbar from './components/Navbar';
import Login from './pages/Login';
import Dashboard from './pages/Dashboard';
import Users from './pages/Users';
import Assets from './pages/Assets';
import AssetRequests from './pages/AssetRequests';
import EmployeeAssets from './pages/EmployeeAssets';
import ServiceRequests from './pages/ServiceRequests';
import AuditRequests from './pages/AuditRequests';

function App() {
  const isLoggedIn = true; // Replace with your login logic

  return (
    <>
      {isLoggedIn && <Navbar />} {/* Navbar shown only when logged in */}

      <Routes>
        <Route path="/" element={isLoggedIn ? <Navigate to="/dashboard" /> : <Login />} />
        <Route path="/login" element={<Login />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/users" element={<Users />} />
        <Route path="/assets" element={<Assets />} />
        <Route path="/asset-requests" element={<AssetRequests />} />
        <Route path="/employee-assets" element={<EmployeeAssets />} />
        <Route path="/service-requests" element={<ServiceRequests />} />
        <Route path="/audit-requests" element={<AuditRequests />} />
        <Route path="*" element={<h1 className="text-center mt-5">404 - Page Not Found</h1>} />
      </Routes>
    </>
  );
}

export default App;
