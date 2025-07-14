import React, { useState, useContext } from 'react';
import axios from '../../api/axiosInstance';
import Navbar from '../../components/Navbar';
import { AuthContext } from '../../context/AuthContext';

function ReturnAssetForm() {
  const [assetId, setAssetId] = useState('');
  const { auth } = useContext(AuthContext);

  const handleReturn = e => {
    e.preventDefault();
    if (!assetId || !auth?.user?.userId) return alert('Asset or user missing');

    axios.post(`/EmployeeAssets/ReturnAsset?assetId=${assetId}&userId=${auth.user.userId}`)
      .then(res => alert(res.data))
      .catch(err => {
        console.error(err);
        alert('Return failed');
      });
  };

  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h2>Return Asset</h2>
        <form onSubmit={handleReturn}>
          <div className="form-group mb-3">
            <label>Asset ID</label>
            <input
              className="form-control"
              value={assetId}
              onChange={e => setAssetId(e.target.value)}
              required
            />
          </div>
          <button className="btn btn-primary" type="submit">Return</button>
        </form>
      </div>
    </>
  );
}

export default ReturnAssetForm;
