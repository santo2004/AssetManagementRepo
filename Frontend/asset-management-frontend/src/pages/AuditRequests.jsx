import React from 'react';
import Navbar from '../components/Navbar';

function AuditRequests() {
  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h2>Audit requests</h2>
        <p>Manage audit from here.</p>
      </div>
    </>
  );
}

export default AuditRequests;
