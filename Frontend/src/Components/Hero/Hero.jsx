import React from 'react'
import './Hero.css'
import shop_img from '../Assets/fleamarket2.jpg'
import { Link } from 'react-router-dom'

const Hero = () => {
  return (
    <div className='hero'>
        <div className="hero-left">
            <h2>JOIN US</h2>
            <p>Give your items a second chance!</p>
            <p>Join us, sell what you don't need anymore, and let someone else cherish it.</p>
            <p>Inspire and breathe new life into your belongings!</p>
        </div>
        <div className='hero-login-btn'>
        <Link style={{ textDecoration: 'none'}} to='/signup'><button>Create Your Account!</button></Link>
        </div>
       
        <div className="hero-right">
        <img src={shop_img} alt='' style={{ width: '590px', height: 'auto' }} />   
        </div>
        <div class="bottom-box"></div>

    </div>
  )
}

export default Hero
