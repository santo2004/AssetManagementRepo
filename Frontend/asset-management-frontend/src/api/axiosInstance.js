// src/api/axiosInstance.js
import axios from 'axios';

const instance = axios.create({
  baseURL: 'http://localhost:5190/api',
  withCredentials: true,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Request Interceptor
instance.interceptors.request.use(
  (config) => {
    const auth = JSON.parse(localStorage.getItem('auth'));
    if (auth?.token) {
      config.headers.Authorization = `Bearer ${auth.token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

export default instance;
