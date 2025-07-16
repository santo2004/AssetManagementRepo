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

  const handleChange = e => {
    setAsset({ ...asset, [e.target.name]: e.target.value });
  };

  const handleSubmit = e => {
    e.preventDefault();
    axios.post('/Assets/CreateAsset', asset)
      .then(() => alert('Asset Created!'))
      .catch(err => console.error(err));
  };

  return (
    <>
      <Navbar />
      <div className="container mt-5">
        <div className="row justify-content-center">
          <div className="col-md-8">
            <div className="card shadow p-4">
              <h3 className="card-title mb-4 text-center">Create Asset</h3>
              <form onSubmit={handleSubmit}>
                <div className="form-group mb-3">
                  <label htmlFor="assetName">Asset Name</label>
                  <input
                    type="text"
                    className="form-control"
                    id="assetName"
                    name="assetName"
                    value={asset.assetName}
                    onChange={handleChange}
                    required
                  />
                </div>

                <div className="form-group mb-3">
                  <label htmlFor="status">Status</label>
                  <select
                    className="form-control"
                    id="status"
                    name="status"
                    value={asset.status}
                    onChange={handleChange}
                  >
                    <option>Available</option>
                    <option>Out of Stock</option>
                  </select>
                </div>

                <div className="form-group mb-3">
                  <label htmlFor="quantity">Quantity</label>
                  <input
                    type="number"
                    className="form-control"
                    id="quantity"
                    name="quantity"
                    value={asset.quantity}
                    onChange={handleChange}
                    min="1"
                    required
                  />
                </div>

                <div className="form-group mb-3">
                  <label htmlFor="imageUrl">Image URL</label>
                  <input
                    type="text"
                    className="form-control"
                    id="imageUrl"
                    name="imageUrl"
                    value={asset.imageUrl}
                    onChange={handleChange}
                  />
                </div>

                <div className="text-center">
                  <button type="submit" className="btn btn-success px-4">
                    Create Asset
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

export default AssetForm;
