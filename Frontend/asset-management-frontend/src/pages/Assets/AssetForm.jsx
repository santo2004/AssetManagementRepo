// src/pages/Assets/AssetForm.jsx
import React, { useState } from 'react';
import axios from '../../api/axiosInstance';
import Navbar from '../../components/Navbar';

function AssetForm() {
  const [asset, setAsset] = useState({
    assetName: '',
    status: 'Available',
    quantity: 1,
    imageUrl: ''
  });

  const handleChange = e => setAsset({ ...asset, [e.target.name]: e.target.value });

  const handleSubmit = e => {
    e.preventDefault();
    axios.post('/Assets/CreateAsset', asset)
      .then(() => alert('Asset Created!'))
      .catch(err => console.error(err));
  };

  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h2>Create Asset</h2>
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Asset Name</label>
            <input className="form-control" name="assetName" onChange={handleChange} required />
          </div>
          <div className="form-group mt-2">
            <label>Status</label>
            <select className="form-control" name="status" onChange={handleChange}>
              <option>Available</option>
              <option>Out of Stock</option>
            </select>
          </div>
          <div className="form-group mt-2">
            <label>Quantity</label>
            <input type="number" className="form-control" name="quantity" onChange={handleChange} required />
          </div>
          <div className="form-group mt-2">
            <label>Image URL</label>
            <input className="form-control" name="imageUrl" onChange={handleChange} />
          </div>
          <button className="btn btn-success mt-3" type="submit">Create</button>
        </form>
      </div>
    </>
  );
}

export default AssetForm;
