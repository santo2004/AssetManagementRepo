// src/api/axiosInstance.js
import axios from 'axios';

const instance = axios.create({
  baseURL: 'https://localhost:7275/api',
  headers: {
    'Content-Type': 'application/json'
  }
});

export default instance;
