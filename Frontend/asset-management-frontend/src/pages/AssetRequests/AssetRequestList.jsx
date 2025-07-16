import React, { useEffect, useState } from 'react';
import axios from '../../api/axiosInstance';
import Navbar from '../../components/Navbar';

function AssetRequestList() {
  const [requests, setRequests] = useState([]);
  const [statusFilter, setStatusFilter] = useState('All');

  useEffect(() => {
    fetchRequests();
  }, []);

  const fetchRequests = () => {
    axios.get('/AssetRequests/All')
      .then(res => setRequests(res.data))
      .catch(err => console.error('Failed to fetch requests', err));
  };

  const handleApprove = (id) => {
    axios.post(`/AssetRequests/Approve/${id}`)
      .then(res => {
        alert(res.data);
        fetchRequests();
      })
      .catch(err => alert('Approval failed'));
  };

  const handleReject = (id) => {
    const comment = prompt('Enter rejection reason:');
    if (!comment) return;
    axios.post(`/AssetRequests/Reject/${id}?comments=${encodeURIComponent(comment)}`)
      .then(res => {
        alert(res.data);
        fetchRequests();
      })
      .catch(err => alert('Rejection failed'));
  };

  const filteredRequests = statusFilter === 'All'
    ? requests
    : requests.filter(req => req.status === statusFilter);

  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h2>Asset Requests</h2>

        <div className="mb-3">
          <label>Filter by Status: </label>
          <select className="form-select" value={statusFilter} onChange={(e) => setStatusFilter(e.target.value)}>
            <option>All</option>
            <option>Requested</option>
            <option>Assigned</option>
            <option>Rejected</option>
          </select>
        </div>

        <table className="table table-bordered">
          <thead>
            <tr>
              <th>Request ID</th>
              <th>Asset ID</th>
              <th>User ID</th>
              <th>Status</th>
              <th>Requested On</th>
              <th>Response On</th>
              <th>Action</th>
            </tr>
          </thead>
          <tbody>
            {filteredRequests.map(req => (
              <tr key={req.assetRequestId}>
                <td>{req.assetRequestId}</td>
                <td>{req.assetId}</td>
                <td>{req.userId}</td>
                <td>{req.status}</td>
                <td>{req.requestDate}</td>
                <td>{req.responseDate || '-'}</td>
                <td>
                  {req.status === 'Requested' ? (
                    <>
                      <button className="btn btn-success btn-sm me-2" onClick={() => handleApprove(req.assetRequestId)}>Approve</button>
                      <button className="btn btn-danger btn-sm" onClick={() => handleReject(req.assetRequestId)}>Reject</button>
                    </>
                  ) : (
                    'Processed'
                  )}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </>
  );
}

export default AssetRequestList;
