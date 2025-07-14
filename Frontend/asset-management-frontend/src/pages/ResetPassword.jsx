import { useState } from 'react';
import axios from '../api/axiosInstance';
import { useNavigate } from 'react-router-dom';

function ResetPassword() {
  const [email, setEmail] = useState('');
  const [token, setToken] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [msg, setMsg] = useState('');
  const [success, setSuccess] = useState(false);
  const navigate = useNavigate();

  const handleReset = async (e) => {
    e.preventDefault();
    try {
      await axios.post('/Login/reset-password', { email, token, newPassword });
      setMsg('Password reset successfully! You can now login.');
      setSuccess(true);
    } catch {
      setMsg('Invalid token or email or expired token.');
      setSuccess(false);
    }
  };

  return (
    <div className="d-flex justify-content-center align-items-center vh-100 bg-light">
      <div className="card shadow p-4" style={{ width: '100%', maxWidth: '400px' }}>
        <div className="card-body">
          <h4 className="mb-3 text-center">Reset Password</h4>
          <form onSubmit={handleReset}>
            <input
              type="email"
              className="form-control mb-3"
              placeholder="Email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
            <input
              type="text"
              className="form-control mb-3"
              placeholder="Reset Token"
              value={token}
              onChange={(e) => setToken(e.target.value)}
              required
            />
            <input
              type="password"
              className="form-control mb-3"
              placeholder="New Password"
              value={newPassword}
              onChange={(e) => setNewPassword(e.target.value)}
              required
            />
            <button className="btn btn-success w-100">Reset Password</button>
          </form>

          {msg && <div className="mt-3 text-center text-muted">{msg}</div>}

          {success && (
            <div className="text-center mt-3">
              <button className="btn btn-primary" onClick={() => navigate('/login')}>
                Go to Login
              </button>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

export default ResetPassword;
