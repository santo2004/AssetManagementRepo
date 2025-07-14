// src/api/axiosInstance.js
import axios from 'axios';

const instance = axios.create({
  baseURL: 'http://localhost:5190/api',
  withCredentials: true, // only needed if using cookies
  headers: {
    'Content-Type': 'application/json',
  }
});

// Add a request interceptor to inject token
instance.interceptors.request.use(
  config => {
    const auth = JSON.parse(localStorage.getItem('auth'));
    if (auth && auth.token) {
      config.headers['Authorization'] = `Bearer ${auth.token}`;
    }
    return config;
  },
  error => Promise.reject(error)
);

export default instance;
