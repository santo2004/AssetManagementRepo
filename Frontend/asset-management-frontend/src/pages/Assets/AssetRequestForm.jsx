// src/pages/Assets/AssetRequestForm.jsx
import React, { useState, useEffect, useContext } from 'react';
import axios from '../../api/axiosInstance';
import Navbar from '../../components/Navbar';
import { AuthContext } from '../../context/AuthContext';

function AssetRequestForm() {
  const [assets, setAssets] = useState([]);
  const [assetId, setAssetId] = useState('');
  const { auth } = useContext(AuthContext);

  useEffect(() => {
    axios.get('/Assets/GetAll')
      .then(res => setAssets(res.data))
      .catch(err => console.error('Failed to load assets', err));
  }, []);

  const handleRequest = (e) => {
    e.preventDefault();
    if (!assetId) {
      alert('Please select an asset.');
      return;
    }

    const userId = auth?.user?.userId;
    if (!userId) {
      alert('User ID is missing from context.');
      return;
    }

    axios
      .post(`/Assets/RequestAsset?assetId=${assetId}&userId=${userId}`)
      .then(res => alert(res.data))
      .catch(err => {
        console.error('Failed to request asset:', err);
        alert('Asset request failed.');
      });
  };

  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h2>Request Asset</h2>
        <form onSubmit={handleRequest}>
          <div className="form-group mb-3">
            <label>Select Asset</label>
            <select
              className="form-control"
              value={assetId}
              onChange={e => setAssetId(e.target.value)}
              required
            >
              <option value="">-- Select Asset --</option>
              {assets.map(a => (
                <option key={a.assetId} value={a.assetId}>
                  {a.assetName} ({a.status}, Qty: {a.quantity})
                </option>
              ))}
            </select>
          </div>

          <button className="btn btn-primary" type="submit">Request Asset</button>
        </form>
      </div>
    </>
  );
}

export default AssetRequestForm;
