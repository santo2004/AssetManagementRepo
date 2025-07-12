// src/pages/Users/UserForm.jsx
import React, { useState } from 'react';
import axios from '../../api/axiosInstance';
import Navbar from '../../components/Navbar';

export default function UserForm() {
  const [formData, setFormData] = useState({ name: '', email: '', role: '', password: '' });
  const handleChange = e => setFormData({ ...formData, [e.target.name]: e.target.value });
  const handleSubmit = async e => {
    e.preventDefault();
    try {
      await axios.post('/Users/CreateUser', formData);
      alert('User added');
      setFormData({ name: '', email: '', role: '', password: '' });
    } catch {
      alert('Add failed');
    }
  };

  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h4>Add User</h4>
        <form onSubmit={handleSubmit}>
          <div className="row">
            {['name','email','role','password'].map((field, i) => (
              <div className="col-md-3 mb-3" key={i}>
                <input
                  type={field === 'password' ? 'password' : 'text'}
                  name={field}
                  placeholder={field.charAt(0).toUpperCase() + field.slice(1)}
                  className="form-control"
                  value={formData[field]}
                  onChange={handleChange}
                  required
                />
              </div>
            ))}
          </div>
          <button className="btn btn-success">Create</button>
        </form>
      </div>
    </>
  );
}
