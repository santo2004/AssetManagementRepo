// src/pages/ServiceRequests/AdminServiceRequestList.jsx
import React, { useEffect, useState, useContext } from 'react';
import axios from '../../api/axiosInstance';
import Navbar from '../../components/Navbar';
import { AuthContext } from '../../context/AuthContext';

function AdminServiceRequestList() {
  const [requests, setRequests] = useState([]);
  const [editId, setEditId] = useState(null);
  const { auth } = useContext(AuthContext);

  const isAdmin = auth?.user?.role === 'Admin';

  useEffect(() => {
    if (isAdmin) {
      fetchAllRequests();
    }
  }, [isAdmin]);

  const fetchAllRequests = async () => {
    try {
      const res = await axios.get('/ServiceRequests/GetAllRequests');
      setRequests(res.data);
    } catch (err) {
      console.error('Fetch error:', err);
      alert('Could not load service requests');
    }
  };

  const deleteRequest = async (id) => {
    if (!window.confirm('Are you sure you want to delete this request?')) return;
    try {
      await axios.delete(`/ServiceRequests/Delete/${id}`);
      fetchAllRequests();
    } catch (err) {
      console.error('Delete error:', err);
      alert('Delete failed. Unauthorized or server error.');
    }
  };

  const markUnderService = async (id) => {
    try {
      await axios.post(`/ServiceRequests/MarkUnderService/${id}`);
      fetchAllRequests();
      setEditId(null);
    } catch (err) {
      console.error('MarkUnderService error:', err);
      alert('Action failed');
    }
  };

  const markReturned = async (id) => {
    try {
      await axios.post(`/ServiceRequests/MarkReturned/${id}`);
      fetchAllRequests();
      setEditId(null);
    } catch (err) {
      console.error('MarkReturned error:', err);
      alert('Action failed');
    }
  };

  const rejectRequest = async (id) => {
    const reason = prompt('Enter reason for rejection:');
    if (!reason) return;

    try {
      await axios.post(`/ServiceRequests/RejectRequest/${id}?reason=${encodeURIComponent(reason)}`);
      fetchAllRequests();
      setEditId(null);
    } catch (err) {
      console.error('Reject error:', err);
      alert('Action failed');
    }
  };

  if (!isAdmin) {
    return (
      <>
        <Navbar />
        <div className="container mt-4">
          <h4>Unauthorized - Admins only</h4>
        </div>
      </>
    );
  }

  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h2>Manage All Service Requests</h2>
        <table className="table table-bordered table-hover mt-3">
          <thead className="table-dark">
            <tr>
              <th>ID</th>
              <th>User</th>
              <th>Asset</th>
              <th>Issue</th>
              <th>Status</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {requests.map((r) => (
              <tr key={r.requestId}>
                <td>{r.requestId}</td>
                <td>{r.userId}</td>
                <td>{r.assetId}</td>
                <td>{r.issueType}</td>
                <td>{r.status}</td>
                <td>
                  {editId === r.requestId ? (
                    <>
                      <button className="btn btn-info btn-sm me-1" onClick={() => markUnderService(r.requestId)}>
                        Under Service
                      </button>
                      <button className="btn btn-success btn-sm me-1" onClick={() => markReturned(r.requestId)}>
                        Returned
                      </button>
                      <button className="btn btn-warning btn-sm me-1" onClick={() => rejectRequest(r.requestId)}>
                        Reject
                      </button>
                      <button className="btn btn-secondary btn-sm" onClick={() => setEditId(null)}>
                        Cancel
                      </button>
                    </>
                  ) : (
                    <>
                      <button className="btn btn-primary btn-sm me-2" onClick={() => setEditId(r.requestId)}>
                        Update
                      </button>
                      <button className="btn btn-danger btn-sm" onClick={() => deleteRequest(r.requestId)}>
                        Delete
                      </button>
                    </>
                  )}
                </td>
              </tr>
            ))}
            {requests.length === 0 && (
              <tr>
                <td colSpan="6" className="text-center">
                  No requests found.
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </>
  );
}

export default AdminServiceRequestList;
