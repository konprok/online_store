import React, { useState, useEffect, useContext } from 'react';
import { useParams, useLocation } from 'react-router-dom';
import './CSS/OfferDetails.css';
import { CartContext } from '../CartContext'; // Importuj CartContext

const OfferDetails = () => {
  const { offerId } = useParams();
  const location = useLocation();
  const params = new URLSearchParams(location.search);
  const userId = params.get('userId');
  const [offer, setOffer] = useState(null);
  const [seller, setSeller] = useState(null);
  const [ratings, setRatings] = useState([]); // Stan dla recenzji
  const [reviewForm, setReviewForm] = useState({
    title: '',
    description: '',
    rate: 1,
  }); // Stan formularza recenzji
  const { totalCartItems, setTotalCartItems } = useContext(CartContext);

  useEffect(() => {
    // Pobierz dane oferty i informacje o użytkowniku
    const fetchOfferAndSeller = async () => {
      try {
        const response = await fetch(`http://localhost:5047/offers/${offerId}`);
        const data = await response.json();
        setOffer(data);

        const userResponse = await fetch(`http://localhost:5284/users/${data.createdBy}`);
        const userData = await userResponse.json();
        setSeller(userData.name);
      } catch (error) {
        console.error('Error fetching offer or seller:', error);
      }
    };

    fetchOfferAndSeller();
  }, [offerId]);

  useEffect(() => {
    // Pobierz recenzje dla danej oferty
    const fetchRatings = async () => {
      try {
        const response = await fetch(`http://localhost:5047/ratings/${offerId}`);
        const data = await response.json();
        setRatings(data);
      } catch (error) {
        console.error('Error fetching ratings:', error);
      }
    };

    fetchRatings();
  }, [offerId]);

  const handleAddReview = async () => {
    try {
      const newReview = {
        title: reviewForm.title,
        description: reviewForm.description,
        createdBy: userId, // Zakładamy, że userId jest w query params
        rate: parseInt(reviewForm.rate),
      };

      const response = await fetch(`http://localhost:5047/ratings/${offerId}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(newReview),
      });

      if (response.ok) {
        const addedReview = await response.json();
        setRatings((prevRatings) => [...prevRatings, addedReview]); // Dodaj nową recenzję do listy
        setReviewForm({ title: '', description: '', rate: 1 }); // Zresetuj formularz
      } else {
        console.error('Failed to add review');
      }
    } catch (error) {
      console.error('Error adding review:', error);
    }
  };

  const addToCart = () => {
    fetch(`http://localhost:5252/cart/${userId}?offerId=${offerId}`, {
      method: 'PATCH',
    })
      .then(response => response.json())
      .then(data => {
        console.log(data);
        fetch(`http://localhost:5252/cart/items/${userId}`)
          .then(response => response.json())
          .then(data => {
            if (typeof data === 'number') {
              setTotalCartItems(data);
            } else {
              console.error('Unexpected data format:', data);
            }
          })
          .catch((error) => {
            console.error('Error:', error);
          });
      })
      .catch((error) => {
        console.error('Error:', error);
      });
  };

  return (
    <div className="offer-details-container">
      {offer ? (
        <>
          <div className="offer-main">
            <img src={offer.logo} alt={offer.title} />
            <h2>{offer.title}</h2>
            <p className="product-description">{offer.description}</p>

            <div className="product-details-container">
              <table className="product-details-table">
                <tbody>
                  <tr>
                    <td className="detail-label">Price:</td>
                    <td className="detail-value">{offer.price} zł</td>
                  </tr>
                  <tr>
                    <td className="detail-label">Author:</td>
                    <td className="detail-value">{seller}</td>
                  </tr>
                  <tr>
                    <td className="detail-label">Creation:</td>
                    <td className="detail-value">
                      {new Date(offer.createdAt).toLocaleDateString()}
                    </td>
                  </tr>
                </tbody>
              </table>
              <button className="addToCart-button">Add to cart</button>
            </div>
          </div>

          <div className="review-column">
            <div className="add-review-section">
              <h3>Add a Review:</h3>
              <input
                type="text"
                placeholder="Review title"
                value={reviewForm.title}
                onChange={(e) => setReviewForm({ ...reviewForm, title: e.target.value })}
              />
              <textarea
                placeholder="Review description"
                value={reviewForm.description}
                onChange={(e) => setReviewForm({ ...reviewForm, description: e.target.value })}
              ></textarea>
              <select
                value={reviewForm.rate}
                onChange={(e) => setReviewForm({ ...reviewForm, rate: e.target.value })}
              >
                {[1, 2, 3, 4, 5].map((rate) => (
                  <option key={rate} value={rate}>
                    {rate}
                  </option>
                ))}
              </select>
              <button className="add-review-button" onClick={handleAddReview}>
                Add Review
              </button>
            </div>

            <div className="ratings-section">
              <h3>Reviews:</h3>
              {ratings.length > 0 ? (
                <div className="ratings-list">
                  {ratings.map((rating, index) => (
                    <div className="rating-item" key={index}>
                      <div className="rating-header">
                        <strong>{rating.title}</strong>
                        <span className="rating-score">{rating.rating}/5</span>
                      </div>
                      <p className="rating-description">{rating.description}</p>
                      <p className="rating-author">
                        Reviewed by: <span>{rating.username}</span>
                      </p>
                      <p className="rating-date">
                        Date: {new Date(rating.modifiedAt || rating.createdAt).toLocaleDateString()}
                      </p>
                    </div>
                  ))}
                </div>
              ) : (
                <p>No reviews yet for this offer.</p>
              )}
            </div>
          </div>
        </>
      ) : (
        <p>Loading...</p>
      )}
    </div>
  );
};

export default OfferDetails;
