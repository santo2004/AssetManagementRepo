// Example: src/pages/Users.jsx
import React from 'react';
import Navbar from '../../components/Navbar';

function ServiceRequests() {
  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h2>service requests</h2>
        <p>Manage service requests from here.</p>
      </div>
    </>
  );
}

export default ServiceRequests;
