// src/pages/Assets/AssetSearch.jsx
import React, { useState } from 'react';
import axios from '../../api/axiosInstance';
import Navbar from '../../components/Navbar';
import './Asset.css';

function AssetSearch() {
  const [status, setStatus] = useState('');
  const [results, setResults] = useState([]);

  const handleSearch = () => {
    axios.get(`/Assets/GetAllByStatus/${status}`)
      .then(res => setResults(res.data))
      .catch(err => console.error(err));
  };

  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h2>Search Assets by Status</h2>
        <div className="form-inline mb-3">
          <select className="form-select me-2" onChange={e => setStatus(e.target.value)}>
            <option value="">Select Status</option>
            <option>Available</option>
            <option>Out of Stock</option>
          </select>
          <button className="btn btn-primary" onClick={handleSearch}>Search</button>
        </div>

        <div className="asset-card-container">
          {results.map(asset => (
            <div className="asset-card" key={asset.assetId}>
              <img src={asset.imageUrl} alt={asset.assetName} />
              <div className="card-body">
                <h5>{asset.assetName}</h5>
                <p>Status: {asset.status}</p>
                <p>Quantity: {asset.quantity}</p>
              </div>
            </div>
          ))}
        </div>
      </div>
    </>
  );
}

export default AssetSearch;
