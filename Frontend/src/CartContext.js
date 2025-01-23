import React, { useState } from 'react';

export const CartContext = React.createContext();

export const CartProvider = ({ children }) => {
  const [totalCartItems, setTotalCartItems] = useState(0);

  return (
    <CartContext.Provider value={{ totalCartItems, setTotalCartItems }}>
      {children}
    </CartContext.Provider>
  );
};
