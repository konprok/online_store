import React, { useEffect, useState, useCallback } from 'react';
import { useLocation } from 'react-router-dom';
import './ListProductAdmin.css';
import cross_icon from '../Assets/cart_cross_icon.png';

const ListProductAdmin = () => {
  const [allProducts, setAllProducts] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');
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
      await fetch(`http://localhost:5047/offers`).then((res) => res.json()).then((data) => {
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
    });
    await fetchInfo();
  }

  const handleSearchChange = (event) => {
    setSearchTerm(event.target.value);
  };

  const filteredProducts = allProducts.filter(product =>
    product.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
    product.category.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <div className='list-product'>
      <div className="list-product-container">
        <h1>My Products</h1>
        <input
          type="text"
          placeholder="Search by title or category"
          value={searchTerm}
          onChange={handleSearchChange}
          className="list-product-search"
        />
        <div className="listproduct-format-main">
          <p>Image</p>
          <p>Title</p>
          <p>Price</p>
          <p>Category</p>
          <p>Remove</p>
        </div>
        <div className="listproduct-allproducts">
          <hr />
          {filteredProducts.map((product, index) => (
            <div key={index} className="listproduct-allproducts listproduct-format">
              <img src={product.logo} alt="" className='listproduct-product-icon' />
              <p>{product.title}</p>
              <p>{product.price} zł</p>
              <p>{product.category}</p>
              <img onClick={() => { remove_product(product.id) }} className='listproduct-remove-icon' src={cross_icon} alt="Remove Product" />
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}

export default ListProductAdmin;
