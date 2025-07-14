// src/pages/Assets/AssetEdit.jsx
import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from '../../api/axiosInstance';
import Navbar from '../../components/Navbar';

function AssetEdit() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [asset, setAsset] = useState({
    assetName: '',
    status: 'Available',
    quantity: 1,
    imageUrl: ''
  });

  useEffect(() => {
    axios.get(`/Assets/GetAllById/${id}`)
      .then(res => setAsset(res.data))
      .catch(err => console.error('Error fetching asset:', err));
  }, [id]);

  const handleChange = e => setAsset({ ...asset, [e.target.name]: e.target.value });

  const handleSubmit = e => {
    e.preventDefault();
    axios.put(`/Assets/UpdateAsset/${id}`, asset)
      .then(() => {
        alert('Asset updated!');
        navigate('/assets/list');
      })
      .catch(err => console.error('Error updating asset:', err));
  };

  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h2>Edit Asset</h2>
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Asset Name</label>
            <input className="form-control" name="assetName" value={asset.assetName} onChange={handleChange} required />
          </div>
          <div className="form-group mt-2">
            <label>Status</label>
            <select className="form-control" name="status" value={asset.status} onChange={handleChange}>
              <option>Available</option>
              <option>Out of Stock</option>
            </select>
          </div>
          <div className="form-group mt-2">
            <label>Quantity</label>
            <input type="number" className="form-control" name="quantity" value={asset.quantity} onChange={handleChange} required />
          </div>
          <div className="form-group mt-2">
            <label>Image URL</label>
            <input className="form-control" name="imageUrl" value={asset.imageUrl} onChange={handleChange} />
          </div>
          <button className="btn btn-primary mt-3" type="submit">Update</button>
        </form>
      </div>
    </>
  );
}

export default AssetEdit;
