// src/pages/ServiceRequests/ServiceRequest.jsx
import React, { useState, useContext } from 'react';
import axios from '../../api/axiosInstance';
import Navbar from '../../components/Navbar';
import { AuthContext } from '../../context/AuthContext';

function ServiceRequest() {
  const [assetId, setAssetId] = useState('');
  const [description, setDescription] = useState('');
  const [issueType, setIssueType] = useState('');
  const { auth } = useContext(AuthContext);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const userId = auth?.user?.userId;
    if (!userId) {
      alert('User ID missing');
      return;
    }

    // Basic validation
    if (!assetId || !issueType || !description) {
      alert('Please fill out all fields.');
      return;
    }

    try {
      await axios.post('/ServiceRequests/CreateRequest', {
        userId,
        assetId: parseInt(assetId),
        description,
        issueType,
        status: 'Requested',
        requestedDate: new Date().toISOString().split('T')[0]
      });
      alert('Service request created successfully!');
      setAssetId('');
      setDescription('');
      setIssueType('');
    } catch (err) {
      console.error('Backend Validation Error:', err.response?.data?.errors || err);
      alert('Failed to submit service request');
    }
  };

  return (
    <>
      <Navbar />
      <div className="container mt-5">
        <div className="row justify-content-center">
          <div className="col-md-8">
            <div className="card shadow p-4">
              <h3 className="card-title mb-4 text-center">Create Service Request</h3>
              <form onSubmit={handleSubmit}>
                <div className="mb-3">
                  <label className="form-label">Asset ID</label>
                  <input
                    className="form-control"
                    type="number"
                    value={assetId}
                    onChange={(e) => setAssetId(e.target.value)}
                    required
                  />
                </div>
                <div className="mb-3">
                  <label className="form-label">Issue Type</label>
                  <input
                    className="form-control"
                    type="text"
                    value={issueType}
                    onChange={(e) => setIssueType(e.target.value)}
                    required
                  />
                </div>
                <div className="mb-3">
                  <label className="form-label">Description</label>
                  <textarea
                    className="form-control"
                    rows="4"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                    required
                  ></textarea>
                </div>
                <div className="text-center">
                  <button className="btn btn-primary px-4" type="submit">
                    Submit Request
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default ServiceRequest;
