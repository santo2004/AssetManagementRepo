import { useContext, useState } from 'react';
import { useNavigate, Navigate, Link } from 'react-router-dom';
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
    <div className="d-flex justify-content-center align-items-center vh-100 bg-light">
      <div className="card shadow-lg p-4" style={{ width: '100%', maxWidth: '400px' }}>
        <div className="card-body">
          <h2 className="card-title text-center mb-4">Login</h2>
          <form onSubmit={handleLogin}>
            <input
              type="text"
              className="form-control mb-3"
              placeholder="Username"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              required
            />
            <input
              type="password"
              className="form-control mb-2"
              placeholder="Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />

            {/* ✅ Forgot Password Link */}
            <div className="text-end mb-3">
              <Link to="/forgot-password" className="text-decoration-none small text-primary">
                Forgot Password?
              </Link>
            </div>

            <button type="submit" className="btn btn-primary w-100">Login</button>
          </form>
        </div>
      </div>
    </div>
  );
};

export default Login;
