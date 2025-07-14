import React, { useEffect, useState, useContext } from 'react';
import axios from '../../api/axiosInstance';
import Navbar from '../../components/Navbar';
import { AuthContext } from '../../context/AuthContext';

function ServiceRequestList() {
  const [requests, setRequests] = useState([]);
  const { auth } = useContext(AuthContext);

  useEffect(() => {
    if (!auth?.user?.userId) return;
    axios.get(`/ServiceRequests/GetRequestByUserId/${auth.user.userId}`)
      .then(res => setRequests(res.data))
      .catch(console.error);
  }, [auth]);

  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h2>My Service Requests</h2>
        <table className="table">
          <thead>
            <tr>
              <th>ID</th><th>Asset</th><th>Issue</th><th>Status</th>
            </tr>
          </thead>
          <tbody>
            {requests.map(r => (
              <tr key={r.requestId}>
                <td>{r.requestId}</td>
                <td>{r.assetId}</td>
                <td>{r.issueType}</td>
                <td>{r.status}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </>
  );
}

export default ServiceRequestList;
