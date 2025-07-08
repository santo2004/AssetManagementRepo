import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Login from 'asset-management-frontend\src\pages\Login.jsx';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Login />} />
      </Routes>
    </Router>
  );
}

export default App;
