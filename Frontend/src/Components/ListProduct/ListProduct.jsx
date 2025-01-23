import React, { useEffect, useState, useCallback } from 'react';
import { useLocation } from 'react-router-dom';
import './ListProduct.css';
import cross_icon from '../Assets/cart_cross_icon.png';

const ListProduct = () => {
  const [allproducts, setAllProducts] = useState([]);
  const location = useLocation();
  const [userId, setUserId] = useState('');

  useEffect(() => {
    const params = new URLSearchParams(location.search);
    const userIdFromQuery = params.get('userId');
    if (userIdFromQuery) {
      setUserId(userIdFromQuery);
    }
  }, [location]);

  const fetchInfo = useCallback(async () => {
    if (userId) {
      await fetch(`http://localhost:5047/users/${userId}/offer`).then((res) => res.json()).then((data) => {
        setAllProducts(data);
      });
    }
  }, [userId]);

  useEffect(() => {
    fetchInfo();
  }, [fetchInfo, userId]);

  const remove_product = async (id) => {
    await fetch(`http://localhost:5047/users/${userId}/offer/${id}`, {
      method: 'DELETE',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
    })
    await fetchInfo();
  }

  return (
    <div className='list-product'>
      <div className="list-product-container">
        <h1>My Products</h1>
        <div className="listproduct-format-main">
          <p>Image</p>
          <p>Title</p>
          <p>Price</p>
          <p>Category</p>
          <p>Remove</p>
        </div>
        <div className="listproduct-allproducts">
          <hr />
          {allproducts.map((product, index) => {
            return <div key={index} className="listproduct-allproducts listproduct-format">
              <img src={product.logo} alt="" className='listproduct-product-icon' />
              <p>{product.title}</p>
              <p>{product.price} zł</p>
              <p>{product.category}</p>
              <img onClick={() => { remove_product(product.id) }} className='listproduct-remove-icon' src={cross_icon} alt="" />
            </div>
          })}
        </div>
      </div>
    </div>
  );
}

export default ListProduct;
