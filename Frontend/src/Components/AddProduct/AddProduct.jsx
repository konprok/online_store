import React, { useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import './AddProduct.css';
import upload_area from '../Assets/upload.png';

const AddProduct = () => {
  const [image, setImage] = useState(false);
  const [categories, setCategories] = useState([{ id: '', name: 'Category' }]);
  const [statusMessage, setStatusMessage] = useState('');
  const [productDetails, setProductDetails] = useState({
    name: '',
    image: '',
    category: '',
    price: '',
    description: '',
    userId: ''
  });

  const location = useLocation();

  useEffect(() => {
    const params = new URLSearchParams(location.search);
    const userIdFromQuery = params.get('userId');
    if (userIdFromQuery) {
      setProductDetails((prevDetails) => ({ ...prevDetails, userId: userIdFromQuery }));
    }
  }, [location]);

  useEffect(() => {
    fetch('http://localhost:5047/categories')
      .then((response) => response.json())
      .then((data) => setCategories([{ id: '', name: 'Category' }, ...data]))
      .catch((error) => {
        console.error('Błąd przy pobieraniu kategorii:', error);
      });
  }, []);

  useEffect(() => {
    if (statusMessage !== '') {
      const timer = setTimeout(() => {
        setStatusMessage('');
      }, 3000);

      return () => clearTimeout(timer);
    }
  }, [statusMessage]);

  const imageHandler = (e) => {
    setImage(e.target.files[0]);
  };

  const changeHandler = (e) => {
    setProductDetails({ ...productDetails, [e.target.name]: e.target.value });
  };

  const addProduct = async () => {
    if (productDetails.category === '') {
      setStatusMessage('Wystąpił błąd');
      return;
    }

    const offer = {
      Title: productDetails.name,
      Category: parseInt(productDetails.category, 10),
      Description: productDetails.description,
      Logo: productDetails.image,
      Price: parseFloat(productDetails.price)
    };

    try {
      const response = await fetch(
        `http://localhost:5047/users/${productDetails.userId}/offers`,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(offer)
        }
      );

      if (response.ok) {
        setStatusMessage('Dodano ofertę');
      } else {
        setStatusMessage('Wystąpił błąd');
      }
    } catch (error) {
      console.error('Błąd podczas dodawania produktu:', error);
      setStatusMessage('Wystąpił błąd');
    }
  };

  return (
    <div className='add-product'>
      <div className='add-product-container'>
        {/* Wyświetlanie komunikatów */}
        {statusMessage === 'Dodano ofertę' && (
          <div className='success-prompt'>{statusMessage}</div>
        )}
        {statusMessage === 'Wystąpił błąd' && (
          <div className='error-prompt'>{statusMessage}</div>
        )}

        <div className='addproduct-itemfield'>
          <p>Product title</p>
          <input
            value={productDetails.name}
            onChange={changeHandler}
            type='text'
            name='name'
            placeholder='Type here'
          />
        </div>

        <div className='addproduct-description'>
          <div className='addproduct-itemfield'>
            <p>Description</p>
            <input
              value={productDetails.description}
              onChange={changeHandler}
              type='text'
              name='description'
              placeholder='Type here'
            />
          </div>
        </div>

        <div className='addproduct-price'>
          <div className='addproduct-itemfield'>
            <p>Price</p>
            <input
              value={productDetails.price}
              onChange={changeHandler}
              type='text'
              name='price'
              placeholder='Type here'
            />
          </div>
        </div>

        <div className='addproduct-itemfield'>
          <p>Product Category</p>
          <select
            value={productDetails.category}
            onChange={changeHandler}
            name='category'
            className='addproduct-selector'
          >
            {categories.map((category, index) => (
              <option key={index} value={category.id}>
                {category.name}
              </option>
            ))}
          </select>
        </div>

        <div className='addproduct-itemuri'>
          <div className='addproduct-itemfield'>
            <p>Image</p>
            <input
              value={productDetails.image}
              onChange={changeHandler}
              type='text'
              name='image'
              placeholder='Paste here'
            />
          </div>
        </div>

        <div className='addproduct-itemfield'>
          <label htmlFor='file-input'>
            <img
              src={image ? URL.createObjectURL(image) : upload_area}
              className={
                image
                  ? 'addproduct-thumbnail-img-large'
                  : 'addproduct-thumbnail-img-small'
              }
              alt=''
              style={{ height: 'auto', opacity: image ? 1 : 0.6 }}
            />
          </label>
          <input onChange={imageHandler} type='file' name='image' id='file-input' hidden />
        </div>

        <button onClick={addProduct} className='addproduct-button'>
          ADD
        </button>
      </div>
    </div>
  );
};

export default AddProduct;
