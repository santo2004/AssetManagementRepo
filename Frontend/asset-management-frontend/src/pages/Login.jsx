import { useContext, useState } from 'react';
import { useNavigate, Navigate } from 'react-router-dom';
import { AuthContext } from '../context/AuthContext';
import axios from '../api/axiosInstance';

const Login = () => {
  const { auth, login } = useContext(AuthContext);
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();

    try {
      const res = await axios.post('/Login/login', { username, password });

      const { token, user } = res.data;

      if (!token || !user) {
        alert('Login failed: Missing token or user info');
        return;
      }

      login({ token, user });
      navigate('/dashboard');
    } catch (error) {
      console.error('Login error:', error);
      alert('Login failed: Invalid credentials or server error');
    }
  };

  if (auth && auth.token) {
    return <Navigate to="/dashboard" />;
  }

  return (
    <div className="container mt-5">
      <h2>Login</h2>
      <form onSubmit={handleLogin}>
        <input
          type="text"
          className="form-control mb-3"
          placeholder="Username"
          value={username}
          onChange={e => setUsername(e.target.value)}
          required
        />
        <input
          type="password"
          className="form-control mb-3"
          placeholder="Password"
          value={password}
          onChange={e => setPassword(e.target.value)}
          required
        />
        <button type="submit" className="btn btn-primary">Login</button>
      </form>
    </div>
  );
};

export default Login; // ✅ REQUIRED
