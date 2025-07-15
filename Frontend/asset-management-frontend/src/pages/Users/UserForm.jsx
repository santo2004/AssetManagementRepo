// src/pages/Users/UserForm.jsx
import React, { useState } from 'react';
import axios from '../../api/axiosInstance';
import Navbar from '../../components/Navbar';

export default function UserForm() {
  const [formData, setFormData] = useState({
    username: '',
    email: '',
    roleId: '',
    password: '',
    fullName: '',
    phoneNumber: '',
    gender: '',
    address: ''
  });

  const handleChange = e => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async e => {
    e.preventDefault();

    // Validation: Ensure all fields are filled
    for (let key in formData) {
      if (formData[key].toString().trim() === '') {
        alert(`Please fill out the ${key} field.`);
        return;
      }
    }

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
      alert('User creation failed.');
    }
  };

  return (
    <>
      <Navbar />
      <div className="container mt-5">
        <div className="row justify-content-center">
          <div className="col-md-10">
            <div className="card shadow p-4">
              <h3 className="card-title mb-4 text-center">Create User</h3>
              <form onSubmit={handleSubmit}>
                <div className="row g-3">

                  <div className="col-md-6">
                    <label>Username</label>
                    <input
                      type="text"
                      className="form-control"
                      name="username"
                      value={formData.username}
                      onChange={handleChange}
                      required
                    />
                  </div>

                  <div className="col-md-6">
                    <label>Email</label>
                    <input
                      type="email"
                      className="form-control"
                      name="email"
                      value={formData.email}
                      onChange={handleChange}
                      required
                    />
                  </div>

                  <div className="col-md-6">
                    <label>Password</label>
                    <input
                      type="password"
                      className="form-control"
                      name="password"
                      value={formData.password}
                      onChange={handleChange}
                      required
                    />
                  </div>

                  <div className="col-md-6">
                    <label>Full Name</label>
                    <input
                      type="text"
                      className="form-control"
                      name="fullName"
                      value={formData.fullName}
                      onChange={handleChange}
                      required
                    />
                  </div>

                  <div className="col-md-6">
                    <label>Phone Number</label>
                    <input
                      type="tel"
                      className="form-control"
                      name="phoneNumber"
                      value={formData.phoneNumber}
                      onChange={handleChange}
                      required
                    />
                  </div>

                  <div className="col-md-6">
                    <label>Gender</label>
                    <select
                      className="form-control"
                      name="gender"
                      value={formData.gender}
                      onChange={handleChange}
                      required
                    >
                      <option value="">Select Gender</option>
                      <option value="Male">Male</option>
                      <option value="Female">Female</option>
                      <option value="Other">Other</option>
                    </select>
                  </div>

                  <div className="col-md-6">
                    <label>Role ID</label>
                    <input
                      type="number"
                      className="form-control"
                      name="roleId"
                      value={formData.roleId}
                      onChange={handleChange}
                      required
                    />
                  </div>

                  <div className="col-md-6">
                    <label>Address</label>
                    <input
                      type="text"
                      className="form-control"
                      name="address"
                      value={formData.address}
                      onChange={handleChange}
                      required
                    />
                  </div>
                </div>

                <div className="text-center mt-4">
                  <button type="submit" className="btn btn-success px-4">
                    Create User
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
