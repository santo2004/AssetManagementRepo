import React from 'react';
import { Outlet } from 'react-router-dom';
import Navbar from './Navbar';

function Layout() {
  return (
    <>
      <div className="container mt-4">
        <Outlet />
      </div>
    </>
  );
}

export default Layout;
