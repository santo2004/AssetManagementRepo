import React, { useState } from 'react';
import axios from '../../api/axiosInstance';

function UserForm() {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    role: '',
    password: ''
  });

  const handleChange = e => setFormData({ ...formData, [e.target.name]: e.target.value });

  const handleSubmit = async e => {
    e.preventDefault();
    try {
      await axios.post('/Users', formData);
      alert('User added!');
      setFormData({ name: '', email: '', role: '', password: '' });
    } catch (err) {
      console.error('Error:', err);
      alert('Failed to add user');
    }
  };

  return (
    <form className="mb-4" onSubmit={handleSubmit}>
      <h4>Add User</h4>
      <div className="row">
        <div className="col-md-3 mb-3">
          <input type="text" name="name" placeholder="Name" className="form-control" value={formData.name} onChange={handleChange} required />
        </div>
        <div className="col-md-3 mb-3">
          <input type="email" name="email" placeholder="Email" className="form-control" value={formData.email} onChange={handleChange} required />
        </div>
        <div className="col-md-3 mb-3">
          <input type="text" name="role" placeholder="Role" className="form-control" value={formData.role} onChange={handleChange} required />
        </div>
        <div className="col-md-3 mb-3">
          <input type="password" name="password" placeholder="Password" className="form-control" value={formData.password} onChange={handleChange} required />
        </div>
      </div>
      <button className="btn btn-primary">Create User</button>
    </form>
  );
}

export default UserForm;
