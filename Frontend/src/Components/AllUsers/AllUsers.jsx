import React, { useEffect, useState } from 'react';
import './AllUsers.css';
import { useLocation } from 'react-router-dom';
import cross_icon from '../Assets/cart_cross_icon.png';

const AllUsers = () => {
  const [users, setUsers] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');
  const location = useLocation();

  const loadUsers = async () => {
    const queryParams = new URLSearchParams(location.search);
    const userId = queryParams.get('userId');
    if (userId) {
      try {
        const response = await fetch(`http://localhost:5284/getAllUsers?userId=${userId}`);
        const data = await response.json();
        setUsers(data);
      } catch (error) {
        console.error('Error:', error);
      }
    }
  };

  useEffect(() => {
    loadUsers();
  }, [location]);

  const remove_user = async (userId) => {
    const response = await fetch(`http://localhost:5284/${userId}/deleteUser`, {
      method: 'DELETE',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
    });
    if (response.ok) {
      loadUsers();
    }
  };

  const handleSearchChange = (event) => {
    setSearchTerm(event.target.value);
  };

  const filteredUsers = users.filter(user =>
    user.email.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <div className='all-users'>
      <div className="all-users-container">
        <h1>All Users List</h1>
        <input
          type="text"
          placeholder="Search by email"
          value={searchTerm}
          onChange={handleSearchChange}
          className="all-users-search"
        />
        <div className="allusers-format-main">
          <p>Username</p>
          <p>Email</p>
          <p>Action</p>
        </div>
        <div className="allusers-listallusers">
          <hr />
          {filteredUsers.map((user, index) => (
            <div key={index} className="allusers-format">
              <p>{user.name}</p>
              <p>{user.email}</p>
              <img onClick={() => remove_user(user.id)} className='remove-user-button-remove-icon' src={cross_icon} alt="Remove User" />
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}

export default AllUsers;
