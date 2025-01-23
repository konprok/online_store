import React, { useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import CartItems from '../Components/CartItems/CartItems';

const Cart = () => {
  const location = useLocation();
  let userId;

  useEffect(() => {
    const params = new URLSearchParams(location.search);
    userId = params.get('userId');
  }, [location]);

  return (
    <div>
      <CartItems/>
    </div>
  );
}

export default Cart;
