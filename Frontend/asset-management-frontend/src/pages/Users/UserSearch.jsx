// src/pages/Users/UserSearch.jsx
import React, { useState } from 'react';
import axios from '../../api/axiosInstance';
import Navbar from '../../components/Navbar';

export default function UserSearch() {
  const [type, setType] = useState('id');
  const [query, setQuery] = useState('');
  const [results, setResults] = useState([]);

  const handleSearch = async () => {
    try {
      const url = type === 'id'
        ? `/Users/GetUserById/${query}`
        : `/Users/GetUserByRoleId/${query}`;
      const res = await axios.get(url);
      setResults(Array.isArray(res.data) ? res.data : [res.data]);
    } catch (err) {
      console.error(err);
      alert('Search failed');
    }
  };

  return (
    <>
      <Navbar />
      <div className="container mt-4">
        <h4>Search Users</h4>
        <div className="row g-2 align-items-end">
          <div className="col-md-2">
            <select className="form-select" value={type} onChange={e => setType(e.target.value)}>
              <option value="id">By ID</option>
              <option value="role">By Role ID</option>
            </select>
          </div>
          <div className="col-md-4">
            <input
              type="text"
              className="form-control"
              placeholder={`Enter ${type}`}
              value={query}
              onChange={e => setQuery(e.target.value)}
            />
          </div>
          <div className="col-md-2">
            <button className="btn btn-outline-secondary" onClick={handleSearch}>
              Search
            </button>
          </div>
        </div>

        {results.length > 0 && (
          <ul className="list-group mt-3">
            {results.map(u => (
              <li key={u.userId} className="list-group-item">
                <strong>{u.username}</strong> | Email: {u.email} | Role: {u.roleName}
              </li>
            ))}
          </ul>
        )}
      </div>
    </>
  );
}
