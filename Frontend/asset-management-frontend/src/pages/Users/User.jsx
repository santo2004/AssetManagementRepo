import React from 'react';
import Navbar from '../../components/Navbar';

function User() {
  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h2>Users</h2>
        <p>Manage users from here.</p>
      </div>
    </>
  );
}

export default User;
