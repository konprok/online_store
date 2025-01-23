import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './CSS/Login.css';
import axios from 'axios';

const Login = ({ setUserLoggedIn }) => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [errorMessage, setErrorMessage] = useState('');
  const navigate = useNavigate();

  const handleSignin = async () => {
    try {
      const userData = {
        userName: '',
        email: email,
        password: password,
      };

      const response = await axios.post('http://localhost:5284/users/login', userData);

      if (response.status === 200) {
        const userId = response.data.id;
        setUserLoggedIn(true);
        localStorage.setItem('userLoggedIn', 'true');
        localStorage.setItem('userId', userId);

        if (response.data.isAdmin) {
          navigate(`/admin/?userId=${userId}`);
        } else {
          navigate(`/?userId=${userId}`);
        }
      }

      console.log('Odpowiedź z serwera:', response.data);
    } catch (error) {
      console.error('Błąd podczas wysyłania żądania:', error);
      if (error.response && error.response.status === 400) {
        setErrorMessage('Niepoprawne dane wejściowe');
      }
    }
  };

  return (
    <div className='login'>
      <div className='login-container'>
        <h1>Login</h1>
        <div className='login-fields'>
          <input
            type='email'
            placeholder='Email Address'
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
          <input
            type='password'
            placeholder='Password'
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>
        <button onClick={handleSignin}>Login</button>
        {errorMessage && <p className='error-message'>{errorMessage}</p>}
      </div>
    </div>
  );
};

export default Login;
