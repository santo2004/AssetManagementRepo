import React, { useContext, useState, useRef, useEffect } from 'react';
import { AuthContext } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';
import Navbar from '../components/Navbar';
import './Dashboard.css';

function Dashboard() {
  const { auth } = useContext(AuthContext);
  const role = auth?.user?.role;
  const navigate = useNavigate();

  const isAdmin = role === 'Admin';
  const isUser = role === 'Employee' || role === 'Manager';

  return (
    <>
      <Navbar />
      <div className="container mt-4" style={{ overflow: 'visible' }}>
        <div className="row mt-5" style={{ overflow: 'visible' }}>
          {isAdmin && (
            <>
              <DashboardCard
                title="Users"
                options={[
                  { label: 'User List', path: '/users/list' },
                  { label: 'User Form', path: '/users/create' },
                  { label: 'User Search', path: '/users/search' },
                ]}
                navigate={navigate}
              />
              <DashboardCard
                title="Assets"
                options={[
                  { label: 'Asset List', path: '/assets/list' },
                  { label: 'Asset Form', path: '/assets/create' },
                  { label: 'Asset Search', path: '/assets/search' },
                ]}
                navigate={navigate}
              />
              <DashboardCard
                title="Asset Requests"
                options={[{ label: 'View Requests', path: '/asset-requests/list' }]}
                navigate={navigate}
              />
              <DashboardCard
                title="Service Requests"
                options={[{ label: 'Manage All Requests', path: '/admin/requests' }]}
                navigate={navigate}
              />
            </>
          )}

          {isUser && (
            <>
              <DashboardCard
                title="Assets"
                options={[
                  { label: 'Asset List', path: '/assets/list' },
                  { label: 'Asset Search', path: '/assets/search' },
                  { label: 'Request Asset', path: '/assets/request' },
                ]}
                navigate={navigate}
              />
              <DashboardCard
                title="Employee Assets"
                options={[
                  { label: 'My Allocations', path: '/employee-assets/list' },
                  { label: 'Return Asset', path: '/employee-assets/return' },
                ]}
                navigate={navigate}
              />
              <DashboardCard
                title="Service Requests"
                options={[
                  { label: 'Create Service Request', path: '/service-request' },
                  { label: 'My Service Requests', path: '/my-requests' },
                ]}
                navigate={navigate}
              />
            </>
          )}
        </div>
      </div>
    </>
  );
}

function DashboardCard({ title, options, navigate }) {
  const [open, setOpen] = useState(false);
  const wrapperRef = useRef(null);

  // Close dropdown when clicking outside
  useEffect(() => {
    const handleClickOutside = (event) => {
      if (wrapperRef.current && !wrapperRef.current.contains(event.target)) {
        setOpen(false);
      }
    };
    document.addEventListener('mousedown', handleClickOutside);
    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, []);

  return (
    <div className="col-md-4 mb-5 position-relative" style={{ overflow: 'visible', zIndex: 1 }}>
      <div className="card h-100 shadow-sm p-3">
        <h5 className="card-title text-center">{title}</h5>
        <div className="dropdown-wrapper" ref={wrapperRef}>
          <button className="dropdown-toggle-card" onClick={() => setOpen(!open)}>Open</button>
          {open && (
            <div className="dropdown-card-menu show">
              {options.map((opt, i) => (
                <div
                  key={i}
                  className="dropdown-item"
                  onClick={() => {
                    navigate(opt.path);
                    setOpen(false);
                  }}
                >
                  {opt.label}
                </div>
              ))}
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

export default Dashboard;
