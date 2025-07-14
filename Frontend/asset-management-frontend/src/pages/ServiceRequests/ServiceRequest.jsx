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
    if (!userId) return alert('User ID missing');

    try {
      await axios.post('/ServiceRequests/CreateRequest', {
        userId,
        assetId: parseInt(assetId),
        description,
        issueType,
        status: 'Requested',
        requestedDate: new Date().toISOString().split('T')[0]
      });
      alert('Service request created');
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
      <div className="container mt-4">
        <h2>Create Service Request</h2>
        <form onSubmit={handleSubmit}>
          <div className="mb-3">
            <label>Asset ID</label>
            <input
              className="form-control"
              type="number"
              value={assetId}
              onChange={e => setAssetId(e.target.value)}
              required
            />
          </div>
          <div className="mb-3">
            <label>Issue Type</label>
            <input
              className="form-control"
              value={issueType}
              onChange={e => setIssueType(e.target.value)}
              required
            />
          </div>
          <div className="mb-3">
            <label>Description</label>
            <textarea
              className="form-control"
              value={description}
              onChange={e => setDescription(e.target.value)}
              required
            />
          </div>
          <button className="btn btn-primary">Submit Request</button>
        </form>
      </div>
    </>
  );
}

export default ServiceRequest;
