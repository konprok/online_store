import React from 'react';
import { useLocation } from 'react-router-dom';
import Hero from '../Components/Hero/Hero';
import Offers from '../Components/Offers/Offers';

const Shop = ({ userLoggedIn }) => {
  const location = useLocation();
  const queryParams = new URLSearchParams(location.search);
  const userId = queryParams.get('userId');

  return (
    <div>
      {!userLoggedIn && <Hero />}
      <Offers userId={userId} /> {/* Przekazujemy userId do Offers */}
    </div>
  );
}

export default Shop;
