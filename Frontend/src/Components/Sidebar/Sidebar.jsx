import React, { useState, useEffect, useContext } from 'react';
import './Sidebar.css'
import { Link, useNavigate, useLocation } from 'react-router-dom';
import add_product_icon from '../Assets/addproduct-icon.png'
import list_product_icon from '../Assets/allproducts-icon.png'
import users_icon from '../Assets/users-icon.png'

const Sidebar = () => {
  const [userId, setUserId] = useState('');
  const location = useLocation();

  useEffect(() => {
    const params = new URLSearchParams(location.search);
    const userIdFromQuery = params.get('userId');
    if (userIdFromQuery) {
      setUserId(userIdFromQuery);
    }
  }, [location]);
  return (
    <div className='sidebar'>
      {/* <Link to={`/addproducts?userId=${userId}`} style={{textDecoration:"none", color: 'black', opacity: 0.7 }}>
        <div className="sidebar-item">
            <img src={add_product_icon} alt="" style={{ width: '60px', height: 'auto' }} />
            <p>Add Product</p>
        </div>
      </Link> */}
      <Link to={`/listofproductsadmin?userId=${userId}`} style={{textDecoration:"none",  color: 'black', opacity: 0.7}}>
        <div className="sidebar-list">
            <img src={list_product_icon} alt="" style={{ width: '60px', height: 'auto'}} />
            <p>Offers</p>
        </div>
      </Link>
      <Link to={`/users?userId=${userId}`} style={{textDecoration:"none",  color: 'black', opacity: 0.7}}>
        <div className="sidebar-users">
            <img src={users_icon} alt="" style={{ width: '60px', height: 'auto'}} />
            <p>Users</p>
        </div>
      </Link>
    </div>
  )
}

export default Sidebar
