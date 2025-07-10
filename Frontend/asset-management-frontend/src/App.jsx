import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Login from './pages/Login';
import Dashboard from './pages/Dashboard';
import Users from './pages/Users';
import Assets from './pages/Assets';
import AssetRequests from './pages/AssetRequests';
import EmployeeAssets from './pages/EmployeeAssets';
import ServiceRequests from './pages/ServiceRequests';
import AuditRequests from './pages/AuditRequests';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/users" element={<Users />} />
        <Route path="/assets" element={<Assets />} />
        <Route path="/asset-requests" element={<AssetRequests />} />
        <Route path="/employee-assets" element={<EmployeeAssets />} />
        <Route path="/service-requests" element={<ServiceRequests />} />
        <Route path="/audit-requests" element={<AuditRequests />} />
      </Routes>
    </Router>
  );
}

export default App;
