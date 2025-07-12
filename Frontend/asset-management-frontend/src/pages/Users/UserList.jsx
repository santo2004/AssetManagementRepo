// src/pages/Users/UserList.jsx
import React, { useEffect, useState } from 'react';
import axios from '../../api/axiosInstance';
import Navbar from '../../components/Navbar';

export default function UserList() {
  const [users, setUsers] = useState([]);
  const fetchUsers = async () => {
    try {
      const res = await axios.get('/Users/GetAllUser');
      setUsers(res.data);
    } catch {
      alert('Could not fetch users');
    }
  };
  const handleDelete = async (id) => {
    if (window.confirm('Confirm delete?')) {
      try {
        await axios.delete(`/Users/DeteletUser/${id}`);
        alert('Deleted');
        fetchUsers();
      } catch {
        alert('Delete failed');
      }
    }
  };
  useEffect(fetchUsers, []);

  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h4>User List</h4>
        <table className="table table-bordered table-striped">
          <thead className="table-dark">
            <tr><th>ID</th><th>Name</th><th>Email</th><th>Role</th><th>Actions</th></tr>
          </thead>
          <tbody>
            {users.map(u => (
              <tr key={u.userId}>
                <td>{u.userId}</td><td>{u.name}</td><td>{u.email}</td><td>{u.role}</td>
                <td><button className="btn btn-sm btn-danger" onClick={() => handleDelete(u.userId)}>Delete</button></td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </>
  );
}
