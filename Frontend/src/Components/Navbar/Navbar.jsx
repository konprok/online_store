import React, { useState, useEffect, useContext } from 'react';
import './Navbar.css';
import logo from '../Assets/logo3.png';
import cart_icon from '../Assets/shopping-cart.png';
import account_icon from '../Assets/user.png';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { CartContext } from '../../CartContext'; // Importuj CartContext

const Navbar = ({ userLoggedIn, handleLogout }) => {
  const [menu, setMenu] = useState('shop');
  const [userId, setUserId] = useState('');
  const navigate = useNavigate();
  const location = useLocation();
  const { totalCartItems, setTotalCartItems } = useContext(CartContext); // Użyj CartContext
  const [accountMenuVisible, setAccountMenuVisible] = useState(false);

  useEffect(() => {
    const params = new URLSearchParams(location.search);
    const userIdFromQuery = params.get('userId');
    if (userIdFromQuery) {
      setUserId(userIdFromQuery);
    }
  }, [location]);

  useEffect(() => {
    const isGuid = /^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i.test(userId);
    if (isGuid) {
      fetch(`http://localhost:5252/cart/items?userId=${userId}`)
        .then(response => response.json())
        .then(data => {
          // Sprawdź, czy dane są liczbą
          if (typeof data === 'number') {
            setTotalCartItems(data);
          } else {
            console.error('Unexpected data format:', data);
          }
        })
        .catch((error) => {
          console.error('Error:', error);
        });
    }
  }, [userId]);
  

  const handleLogoutClick = () => {
    handleLogout();
    navigate('/');
  };

  return (
    <div className='navbar'>
      <div className='nav-logo'>
        <img src={logo} alt="" style={{ width: '130px', height: 'auto' }} />
      </div>
      <ul className='nav-menu'>
        <li>
          <Link to={`/?userId=${userId}`} className="nav-shop-button">Shop</Link>
        </li>
      </ul>
      <div className='nav-login-cart'>
        {userLoggedIn ? (
          <>
            <button onClick={handleLogoutClick}>Logout</button>
            <Link style={{ textDecoration: 'none' }} to={`/cart?userId=${userId}`}>
              <img src={cart_icon} alt="" style={{ width: '40px', height: 'auto' }} />
            </Link>
            <div className='nav-cart-count'>{totalCartItems}</div>
          </>
        ) : (
          <>
            <Link style={{ textDecoration: 'none' }} to='/signup'><button>Login</button></Link>
            <Link style={{ textDecoration: 'none' }} to='/cart'>
              <img src={cart_icon} alt="" style={{ width: '40px', height: 'auto' }} />
            </Link>
            <div className='nav-cart-count'>{totalCartItems}</div>
          </>
        )}
      </div>
      <div
        className='nav-account'
        onMouseEnter={() => setAccountMenuVisible(true)}
        onMouseLeave={() => setAccountMenuVisible(false)}
      >
        <img src={account_icon} alt="" style={{ width: '40px', height: 'auto', cursor: 'pointer', marginTop: '10px' }} />
        {accountMenuVisible && (
          <div className='account-menu'>
            <Link style={{ textDecoration: 'none' }} to={`/addproduct?userId=${userId}`}>Add Product</Link>
            <Link style={{ textDecoration: 'none' }} to={`/myproducts?userId=${userId}`}>My Products</Link>
            {/* <Link style={{ textDecoration: 'none' }} to={`/myproducts?userId=${userId}`}>My Products</Link> */}
            <button onClick={handleLogout} className='logout-button'>Logout</button>
          </div>
        )}
      </div>
    </div>
  );
}

export default Navbar;
