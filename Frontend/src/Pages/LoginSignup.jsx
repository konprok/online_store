import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './CSS/LoginSignup.css';
import axios from 'axios';

const LoginSignup = ({setUserLoggedIn}) => {
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [errorMessage, setErrorMessage] = useState('');
  const navigate = useNavigate();

  const redirectToShop = async (userId) => {
    // Wywołanie endpointu po pomyślnej rejestracji
    await fetch(`http://localhost:5252/cart/${userId}`, {
      method: 'POST',
    });
    navigate(`/?userId=${userId}`);
    setUserLoggedIn(true); // Ustawienie stanu zalogowania na true
  };

  const handleLoginClick = () => {
    navigate('/login');
  };

  const handleSignup = async () => {

    try {
      const userData = {
        userName: username,
        email: email,
        password: password
      };
  
      //POST do endpointu Rejestracji na localhost:5284/Register
      const response = await axios.post('http://localhost:5284/users/register', userData);
  
      if (response.status === 200) {
        const userId = response.data.id;
        redirectToShop(userId);
        setUserLoggedIn(true);
      }
    } catch (error) {
      console.error('Błąd podczas wysyłania żądania:', error);
      if (error.response && error.response.status === 400) {
        setErrorMessage('Niepoprawne dane wejściowe');
      }
    }
  };
  

  return (
    <div className='loginsignup'>
      <div className="loginsignup-container">
        <h1>Sign Up</h1>
        <div className="loginsignup-fields">
          <input type='text' placeholder='Your Name' value={username} onChange={(e) => setUsername(e.target.value)} />
          <input type='email' placeholder='Email Address' value={email} onChange={(e) => setEmail(e.target.value)} />
          <input type='password' placeholder='Password' value={password} onChange={(e) => setPassword(e.target.value)} />
        </div>
        {errorMessage && <p className="error-message">{errorMessage}</p>} {/* error message */}
        <button onClick={handleSignup}>Continue</button> {}
        <p className="loginsignup-login">Already have an account? <span onClick={handleLoginClick}>Login here</span></p>
        <div className="loginsignup-agree">
          <input type="checkbox" name='' id='' />
          <p>By continuing, I accept the terms of use & privacy policy.</p>
        </div>
      </div>
    </div>
  );
}

export default LoginSignup;
