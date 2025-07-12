import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';

import Login from './pages/Login';
import Dashboard from './pages/Dashboard';
import User from './pages/Users/User';
import Assets from './pages/Assets';
import AssetRequests from './pages/AssetRequests';
import EmployeeAssets from './pages/EmployeeAssets';
import ServiceRequests from './pages/ServiceRequests';
import AuditRequests from './pages/AuditRequests';
import Unauthorized from './pages/Unauthorized';
import Notfound from './pages/Notfound';

import ProtectedRoute from './routes/ProtectedRoute';
import Layout from './components/Layout';
import Home from './pages/Home';

function App() {
  return (
    <Router>
      <Routes>
        {/* Public Routes */}
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/unauthorized" element={<Unauthorized />} />

        {/* Protected Routes - user must be logged in */}
        <Route element={<ProtectedRoute />}>
          <Route element={<Layout />}>
            <Route path="/dashboard" element={<Dashboard />} />
            <Route path="/users" element={<User />} />
            <Route path="/assets" element={<Assets />} />
            <Route path="/asset-requests" element={<AssetRequests />} />
            <Route path="/employee-assets" element={<EmployeeAssets />} />
            <Route path="/service-requests" element={<ServiceRequests />} />
            <Route path="/audit-requests" element={<AuditRequests />} />
          </Route>
        </Route>

        {/* Redirect root to login */}
        <Route path="/" element={<Navigate to="/login" />} />

        {/* 404 fallback */}
        <Route path="*" element={<Notfound />} />
      </Routes>
    </Router>
  );
}

export default App;
