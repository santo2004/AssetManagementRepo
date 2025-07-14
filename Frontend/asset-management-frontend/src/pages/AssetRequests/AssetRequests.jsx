import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

function AssetRequests() {
  const navigate = useNavigate();

  useEffect(() => {
    navigate('/asset-requests/list');
  }, [navigate]);

  return null;
}

export default AssetRequests;
