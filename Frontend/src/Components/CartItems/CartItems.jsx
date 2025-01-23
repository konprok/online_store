import React, { useContext, useEffect, useState } from 'react'
import { useLocation } from 'react-router-dom';
import './CartItems.css'
import { ShopContext } from '../../Context/ShopContext'
import { CartContext } from '../../CartContext'; // Importuj CartContext
import remove_icon from '../Assets/cart_cross_icon.png'

const CartItems = () => {
  const { removeFromCart } = useContext(ShopContext);
  const { totalCartItems, setTotalCartItems } = useContext(CartContext); // Użyj CartContext
  const [userId, setUserId] = useState(null);
  const [cartData, setCartData] = useState({ offers: {}, price: 0 });
  const location = useLocation();

  useEffect(() => {
    const params = new URLSearchParams(location.search);
    const userIdFromQuery = params.get('userId');
    if (userIdFromQuery) {
        setUserId(userIdFromQuery);
    }
  }, [location]);

  const fetchCartData = () => {
    if (userId) {
      fetch(`http://localhost:5252/cart/${userId}`)
        .then(response => response.json())
        .then(data => {
          // Transform the list of offers into an object
          const offersObj = data.offers.reduce((obj, offer) => {
            if (!obj[offer.id]) {
              obj[offer.id] = { ...offer, quantity: 1 };
            } else {
              obj[offer.id].quantity += 1;
            }
            return obj;
          }, {});
          setCartData({ offers: offersObj, price: data.price });
        });
    }
  };

  useEffect(fetchCartData, [userId]);

  const handleRemoveFromCart = (offerId) => {
    fetch(`http://localhost:5252/cart/${userId}?offerId=${offerId}`, {
      method: 'DELETE',
    })
    .then(() => {
      removeFromCart(offerId);
      fetchCartData(); // Refresh the cart data
      // Po usunięciu produktu z koszyka, wykonaj kolejne żądanie GET, aby zaktualizować liczbę produktów w koszyku
      fetch(`http://localhost:5252/cart/items/${userId}`)
        .then(response => response.json())
        .then(data => {
          // Zakładamy, że endpoint zwraca liczbę produktów w koszyku
          if (typeof data === 'number') {
            setTotalCartItems(data);
          } else {
            console.error('Unexpected data format:', data);
          }
        })
        .catch((error) => {
          console.error('Error:', error);
        });
    });
  };

  return (
    <div className='cartitems'>
      <div className="cartitems-format-main">
        <p>Products</p>
        <p>Title</p>
        <p>Price</p>
        <p>Quantity</p>
        <p>Total</p>
        <p>Remove</p>
      </div>
      <hr />
      {Object.values(cartData.offers).map((offer) => (
        <div key={offer.id}>
          <div className="cartitems-format cartitems-format-main">
            <img src={offer.logo} alt="" className='carticon-product-icon' />
            <p>{offer.title}</p>
            <p>{offer.price} zł</p>
            <p>{offer.quantity}</p>
            <p>{offer.price * offer.quantity} zł</p>
            <img className='cartitems-remove-icon' src={remove_icon} onClick={() => handleRemoveFromCart(offer.id)} alt="" />
          </div>
          <hr />
        </div>
      ))}
      
      <div className="cartitems-down">
        <div className="cartitems-total">
          <div>
            <div className="cartitems-total-item">
              <p>Subtotal</p>
              <p>{cartData.price} zł</p>
            </div>
            <hr />
            <div className="cartitems-total-item">
              <p>Shipping Fee</p>
              <p>Free</p>
            </div>
            <hr />
            <div className="cartitems-total-item">
              <h3>Total</h3>
              <p>{cartData.price} zł</p>
            </div>
            <button>PROCEED TO CHECKOUT</button>
          </div>
        </div>
      </div>
    </div>
  )
}

export default CartItems;
