// src/pages/Users/UserForm.jsx
import React, { useState } from 'react';
import axios from '../../api/axiosInstance';
import Navbar from '../../components/Navbar';

export default function UserForm() {
  const [formData, setFormData] = useState({
    username: '',
    email: '',
    roleId: '', // integer
    password: '',
    fullName: '',
    phoneNumber: '',
    gender: '',
    address: ''
  });

  const handleChange = e => 
    setFormData({ ...formData, [e.target.name]: e.target.value });

  const handleSubmit = async e => {
    e.preventDefault();
    try {
      const res = await axios.post('/Users/CreateUser', formData);
      alert(res.data);
      setFormData({
        username: '',
        email: '',
        roleId: '',
        password: '',
        fullName: '',
        phoneNumber: '',
        gender: '',
        address: ''
      });
    } catch (err) {
      console.error(err);
      alert('Add failed');
    }
  };

  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h4>Add User</h4>
        <form onSubmit={handleSubmit}>
          <div className="row g-3">
            {['username','email','roleId','password','fullName','phoneNumber','gender','address'].map((field, i) => (
              <div className="col-md-3" key={i}>
                <input
                  type={field === 'password' ? 'password' : 'text'}
                  name={field}
                  placeholder={field}
                  className="form-control"
                  value={formData[field]}
                  onChange={handleChange}
                  required
                />
              </div>
            ))}
          </div>
          <button className="btn btn-success mt-3">Create User</button>
        </form>
      </div>
    </>
  );
}
