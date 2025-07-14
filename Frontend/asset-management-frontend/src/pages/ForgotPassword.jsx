import { useState } from 'react';
import axios from '../api/axiosInstance';
import { useNavigate } from 'react-router-dom';

function ForgotPassword() {
  const [email, setEmail] = useState('');
  const [message, setMessage] = useState('');
  const [token, setToken] = useState('');
  const [copied, setCopied] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post('/Login/forgot-password', { email });

      // ✅ Check if token is returned as response.data.token
      if (response.data?.token) {
        setToken(response.data.token);
        setMessage('Reset token generated. You can copy it below.');
      } else {
        setMessage('Token not returned from server.');
        setToken('');
      }

    } catch (err) {
      setMessage('Failed to send token. Make sure the email is correct.');
      setToken('');
      console.error(err.message);
    }
  };

  const handleCopy = () => {
    navigator.clipboard.writeText(token)
      .then(() => {
        setCopied(true);
        setTimeout(() => setCopied(false), 2000);
      })
      .catch(() => {
        alert("Copy failed. Please copy manually.");
      });
  };

  return (
    <div className="d-flex justify-content-center align-items-center vh-100 bg-light">
      <div className="card shadow p-4" style={{ width: '100%', maxWidth: '400px' }}>
        <div className="card-body">
          <h4 className="mb-3 text-center">Forgot Password</h4>
          <form onSubmit={handleSubmit}>
            <input
              type="email"
              className="form-control mb-3"
              placeholder="Enter registered email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
            <button className="btn btn-warning w-100" type="submit">
              Generate Reset Token
            </button>
          </form>

          {message && <div className="mt-3 text-center text-muted">{message}</div>}

          {token && (
            <div className="mt-4">
              <label htmlFor="token" className="form-label">Your Reset Token</label>
              <textarea
                id="token"
                className="form-control mb-2"
                rows={2}
                value={token}
                readOnly
                onClick={(e) => e.target.select()}
              />
              <button className="btn btn-outline-secondary w-100 mb-2" onClick={handleCopy}>
                {copied ? 'Copied!' : 'Copy Token'}
              </button>
              <button className="btn btn-primary w-100" onClick={() => navigate('/reset-password')}>
                Go to Reset Password
              </button>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

export default ForgotPassword;
