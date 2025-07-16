import React, { useEffect, useState } from 'react';
import axios from '../../api/axiosInstance';
import { useNavigate } from 'react-router-dom';
import Navbar from '../../components/Navbar';
import './Asset.css';

function AssetList() {
  const [assets, setAssets] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    fetchAssets();
  }, []);

  const fetchAssets = () => {
    axios.get('/Assets/GetAll')
      .then(res => setAssets(res.data))
      .catch(err => console.error(err));
  };

  const handleDelete = (id) => {
    if (window.confirm('Are you sure you want to delete this asset?')) {
      axios.delete(`/Assets/DeleteAsset/${id}`)
        .then(() => fetchAssets())
        .catch(err => console.error(err));
    }
  };

  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h2>Assets</h2>
        <button className="btn btn-success mb-3" onClick={() => navigate('/assets/create')}>Add New Asset</button>
        <div className="asset-card-container">
          {assets.map(asset => (
            <div className="asset-card" key={asset.assetId}>
              <img
                src={asset.imageUrl && asset.imageUrl.trim() !== '' ? asset.imageUrl : 'https://via.placeholder.com/300x180?text=No+Image'}
                alt={asset.assetName}
              />
              <div className="card-body">
                <h5>{asset.assetName}</h5>
                <p>Status: {asset.status}</p>
                <p>Quantity: {asset.quantity}</p>
                <div className="mt-2 d-flex justify-content-between">
                  <button className="btn btn-primary btn-sm" onClick={() => navigate(`/assets/edit/${asset.assetId}`)}>Edit</button>
                  <button className="btn btn-danger btn-sm" onClick={() => handleDelete(asset.assetId)}>Delete</button>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </>
  );
}

export default AssetList;
