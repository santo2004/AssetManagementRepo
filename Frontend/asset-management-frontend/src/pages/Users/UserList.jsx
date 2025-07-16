import React, { useEffect, useState } from 'react';
import axios from '../../api/axiosInstance';
import Navbar from '../../components/Navbar';

export default function UserList() {
  const [users, setUsers] = useState([]);

  const fetchUsers = async () => {
    try {
      const res = await axios.get('/Users/GetAllUser'); 
      setUsers(res.data);
      console.log(res);
    } catch (err) {
      console.error(err);
      alert('Could not fetch users');
    }
  };

  const handleDelete = async (id) => {
  if (window.confirm('Confirm soft delete?')) {
    try {
      await axios.put(`/Users/DeleteUser/${id}`);
      alert('User soft-deleted');
      fetchUsers();
    } catch (err) {
      console.error(err);
      alert('Delete failed');
    }
  }
};


  useEffect(() => {
    fetchUsers();
  }, []);

  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h4>User List</h4>
        {users.length === 0 ? (
          <p>No users found.</p>
        ) : (
          <table className="table table-bordered table-striped">
            <thead className="table-dark">
              <tr>
                <th>ID</th><th>Username</th><th>Email</th><th>Phone</th><th>Role</th><th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {users.map(u => (
                <tr key={u.userId}>
                  <td>{u.userId}</td>
                  <td>{u.username}</td>
                  <td>{u.email}</td>
                  <td>{u.phoneNumber}</td>
                  <td>{u.roleName}</td>
                  <td>
                    <button className="btn btn-sm btn-danger" onClick={() => handleDelete(u.userId)}>
                      Delete
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </>
  );
}
