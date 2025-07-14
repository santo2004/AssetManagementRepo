import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import Login from './pages/Login';
import ForgotPassword from './pages/ForgotPassword';
import ResetPassword from './pages/ResetPassword';
import Dashboard from './pages/Dashboard';
import User from './pages/Users/User';
import UserList from './pages/Users/UserList';
import UserForm from './pages/Users/UserForm';
import UserSearch from './pages/Users/UserSearch';
import Assets from './pages/Assets/Asset';
import AssetRequests from './pages/AssetRequests/AssetRequests';
import AssetRequestList from './pages/AssetRequests/AssetRequestList';
import AssetList from './pages/Assets/AssetList';
import AssetForm from './pages/Assets/AssetForm';
import AssetSearch from './pages/Assets/AssetSearch';
import AssetEdit from './pages/Assets/AssetEdit';
import AssetRequestForm from './pages/Assets/AssetRequestForm';
import EmployeeAssets from './pages/EmployeeAssets/EmployeeAssets';
import AllocationList from './pages/EmployeeAssets/AllocationList';
import ReturnAssetForm from './pages/EmployeeAssets/ReturnAssetFrom';
import ServiceRequests from './pages/ServiceRequests/ServiceRequests';
import ServiceRequest from './pages/ServiceRequests/ServiceRequest';
import ServiceRequestList from './pages/ServiceRequests/ServiceRequestList';
import AdminServiceRequestList from './pages/ServiceRequests/AdminServiceRequestList';
import Unauthorized from './pages/Unauthorized';
import Notfound from './pages/Notfound';
import ProtectedRoute from './routes/ProtectedRoute';
import Layout from './components/Layout';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/unauthorized" element={<Unauthorized />} />
        <Route path="/forgot-password" element={<ForgotPassword />} />
        <Route path="/reset-password" element={<ResetPassword />} />
        <Route element={<ProtectedRoute />}>
          <Route element={<Layout />}>
            <Route path="/dashboard" element={<Dashboard />} />
            <Route path="/users" element={<User />} />
            <Route path="/users/list" element={<UserList />} />
            <Route path="/users/create" element={<UserForm />} />
            <Route path="/users/search" element={<UserSearch />} />
            <Route path="/assets" element={<Assets />} />
            <Route path="/assets/list" element={<AssetList />} />
            <Route path="/assets/create" element={<AssetForm />} />
            <Route path="/assets/search" element={<AssetSearch />} />
            <Route path="/assets/edit/:id" element={<AssetEdit />} />
            <Route path="/assets/request" element={<AssetRequestForm />} />
            <Route path="/asset-requests" element={<AssetRequests />} />
            <Route path="/asset-requests/list" element={<AssetRequestList />} />
            <Route path="/employee-assets" element={<EmployeeAssets />} />
            <Route path="/employee-assets/list" element={<AllocationList />} />
            <Route path="/employee-assets/return" element={<ReturnAssetForm />} />
            <Route path="/service-requests" element={<ServiceRequests />} />
            <Route path="/service-request" element={<ServiceRequest />} />
            <Route path="/service-request" element={<ServiceRequest />} />
            <Route path="/my-requests" element={<ServiceRequestList />} />
            <Route path="/admin/requests" element={<AdminServiceRequestList />} />
          </Route>
        </Route>

        <Route path="*" element={<Notfound />} />
      </Routes>
    </Router>
  );
}

export default App;
