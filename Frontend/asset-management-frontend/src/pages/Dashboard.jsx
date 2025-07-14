import React, { useContext } from 'react';
import Navbar from '../components/Navbar';
import { AuthContext } from '../context/AuthContext';

function Dashboard() {
  const { auth } = useContext(AuthContext);
  const role = auth?.user?.role;

  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h2>Dashboard</h2>
        <p>
          Welcome, <strong>{auth?.user?.username}</strong>!<br />
          You are logged in as <strong>{role}</strong>.
        </p>
      </div>
    </>
  );
}

export default Dashboard;
