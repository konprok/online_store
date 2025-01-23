import React, { useState, useEffect } from 'react';
import './CSS/Admin.css';
import Sidebar from '../Components/Sidebar/Sidebar';
import { Routes, Route } from 'react-router-dom';
import AddProduct from '../Components/AddProduct/AddProduct';
import ListProduct from '../Components/ListProduct/ListProduct';
import AllUsers from '../Components/AllUsers/AllUsers';
import { useLocation } from 'react-router-dom';

const Admin = () => {
  const [userId, setUserId] = useState(null);
  const location = useLocation();

  useEffect(() => {
    const params = new URLSearchParams(location.search);
    setUserId(params.get('userId'));
  }, [location]);

  return (
    <div className='admin'>
      {userId ? <Sidebar userId={userId} /> : <div>Loading...</div>}
      {userId ? (
        <Routes>
          <Route path='/addproduct' element={<AddProduct userId={userId} />} />
          <Route path='/allproducts' element={<ListProduct userId={userId} />} />
          <Route path='/users' element={<AllUsers userId={userId} />} />
        </Routes>
      ) : (
        <div>Loading...</div>  // Możesz tu dodać loader
      )}
    </div>
  );
};

export default Admin;
