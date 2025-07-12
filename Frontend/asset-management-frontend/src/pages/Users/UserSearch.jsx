import React, { useState } from 'react';
import axios from '../../api/axiosInstance';

function UserSearch() {
  const [query, setQuery] = useState('');
  const [results, setResults] = useState([]);

  const handleSearch = async () => {
    try {
      const res = await axios.get(`/Users/search?name=${query}`);
      setResults(res.data);
    } catch (err) {
      console.error('Search error:', err);
      alert('Search failed');
    }
  };

  return (
    <div className="mb-4">
      <h4>Search Users</h4>
      <div className="input-group">
        <input type="text" className="form-control" placeholder="Enter name" value={query} onChange={e => setQuery(e.target.value)} />
        <button className="btn btn-outline-secondary" onClick={handleSearch}>Search</button>
      </div>
      {results.length > 0 && (
        <ul className="list-group mt-3">
          {results.map(u => (
            <li key={u.userId} className="list-group-item">
              {u.name} - {u.email}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}

export default UserSearch;
