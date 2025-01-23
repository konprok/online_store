import React, { useState, useEffect, useContext, useMemo } from 'react';
import { Link, useLocation } from 'react-router-dom';
import './Offers.css';
import { CartContext } from '../../CartContext';

const Offers = () => {
  const [categories, setCategories] = useState([{ id: '', name: 'All Categories' }]);
  const [selectedCategory, setSelectedCategory] = useState('');
  const [offers, setOffers] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [sortOption, setSortOption] = useState('');

  const location = useLocation();
  const userId = new URLSearchParams(location.search).get('userId');
  const { setTotalCartItems } = useContext(CartContext);

  useEffect(() => {
    fetch('http://localhost:5047/categories')
      .then((response) => response.json())
      .then((data) => setCategories([{ id: '', name: 'All Categories' }, ...data]))
      .catch((error) => console.error('Error fetching categories:', error));
  }, []);

  useEffect(() => {
    const endpoint = selectedCategory
      ? `http://localhost:5047/category/${selectedCategory}/offers`
      : 'http://localhost:5047/offers';

    fetch(endpoint)
      .then((response) => response.json())
      .then((data) => setOffers(data))
      .catch((error) => console.error('Error fetching offers:', error));
  }, [selectedCategory]);

  const filteredOffers = useMemo(() => {
    return offers.filter((offer) => {
      const lowerTitle = offer.title.toLowerCase();
      const lowerDesc = offer.description.toLowerCase();
      const lowerSearch = searchTerm.toLowerCase();

      return (
        lowerTitle.includes(lowerSearch) ||
        lowerDesc.includes(lowerSearch)
      );
    });
  }, [offers, searchTerm]);

  const sortedOffers = useMemo(() => {
    const sorted = [...filteredOffers];
    switch (sortOption) {
      case 'priceAsc':
        sorted.sort((a, b) => a.price - b.price);
        break;
      case 'priceDesc':
        sorted.sort((a, b) => b.price - a.price);
        break;
      case 'alphaAsc':
        sorted.sort((a, b) => a.title.localeCompare(b.title));
        break;
      case 'alphaDesc':
        sorted.sort((a, b) => b.title.localeCompare(a.title));
        break;
      default:
        break;
    }
    return sorted;
  }, [filteredOffers, sortOption]);

  const changeHandler = (e) => {
    setSelectedCategory(e.target.value);
  };

  const handleSearchChange = (event) => {
    setSearchTerm(event.target.value);
  };

  const handleSortChange = (event) => {
    setSortOption(event.target.value);
  };

  const addToCart = (event, offerId) => {
    event.stopPropagation();
    fetch(`http://localhost:5252/cart/${userId}?offerId=${offerId}`, {
      method: 'PATCH',
    })
      .then(() => {
        fetch(`http://localhost:5252/cart/items/${userId}`)
          .then((response) => response.json())
          .then((data) => setTotalCartItems(data))
          .catch((error) => console.error('Error fetching cart items:', error));
      })
      .catch((error) => console.error('Error adding to cart:', error));
  };

  return (
    <div className='offers'>
      <div className='offers-search'>
        {/* Pole wyszukiwania */}
        <input
          type='text'
          placeholder='Search by title or description'
          value={searchTerm}
          onChange={handleSearchChange}
          className='offers-search-input'
        />

        {/* Wybór kategorii */}
        <div className='addproduct-itemfield'>
          <select
            value={selectedCategory}
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

        {/* Wybór opcji sortowania */}
        <div className='addproduct-itemfield'>
          <select
            value={sortOption}
            onChange={handleSortChange}
            name='sort'
            className='addproduct-selector'
          >
            <option value=''>Sort by</option>
            <option value='priceAsc'>Price Asc</option>
            <option value='priceDesc'>Price Desc</option>
            <option value='alphaAsc'>A - Z</option>
            <option value='alphaDesc'>Z - A</option>
          </select>
        </div>
      </div>

      <h2>Offers</h2>

      <div className='offers-item'>
        {sortedOffers.map((offer, index) => (
          <div key={index} className='offer-card'>
            <Link to={`/offers/${offer.id}?userId=${userId}`} className='offer-link'>
              <img src={offer.logo} alt={offer.title} className='offer-image' />
              <div className='offer-details'>
                <h3>{offer.title}</h3>
                <p className='offer-description'>
                  {offer.description.slice(0, 50)}
                  {offer.description.length > 50 ? '...' : ''}
                </p>
              </div>
            </Link>
            <div className='offer-footer'>
              <p className='price'>{offer.price} zł</p>
              <button
                className='addToCartMain-button'
                onClick={(event) => addToCart(event, offer.id)}
              >
                Add to cart
              </button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Offers;
