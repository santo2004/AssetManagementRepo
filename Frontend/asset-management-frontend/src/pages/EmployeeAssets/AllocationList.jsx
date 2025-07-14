import React, { useState, useEffect, useContext } from 'react';
import axios from '../../api/axiosInstance';
import Navbar from '../../components/Navbar';
import { AuthContext } from '../../context/AuthContext';

function AllocationList() {
  const [allocs, setAllocs] = useState([]);
  const [loading, setLoading] = useState(true);
  const { auth } = useContext(AuthContext);

  useEffect(() => {
    const fetchAllocations = async () => {
      try {
        const userId = auth?.user?.userId;
        if (!userId) {
          console.warn('User ID not found in auth context');
          return;
        }

        const response = await axios.get(`/EmployeeAssets/GetAllocationByUserId/${userId}`);
        setAllocs(response.data);
      } catch (err) {
        console.error('Error fetching allocations:', err);
      } finally {
        setLoading(false);
      }
    };

    fetchAllocations();
  }, [auth]);

  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h2>Your Allocated Assets</h2>

        {loading ? (
          <p>Loading...</p>
        ) : allocs.length === 0 ? (
          <p>No assets allocated.</p>
        ) : (
          <table className="table table-bordered">
            <thead>
              <tr>
                <th>Allocation ID</th>
                <th>Asset ID</th>
                <th>Assigned Date</th>
                <th>Return Date</th>
                <th>Status</th>
              </tr>
            </thead>
            <tbody>
              {allocs.map(a => (
                <tr key={a.allocationId}>
                  <td>{a.allocationId}</td>
                  <td>{a.assetId}</td>
                  <td>{a.assignedDate}</td>
                  <td>{a.returnDate || '-'}</td>
                  <td>{a.status}</td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </>
  );
}

export default AllocationList;
