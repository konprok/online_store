import React, { useContext } from 'react'
import './ProductDisplay.css'
import { ShopContext } from '../../Context/ShopContext';



const ProductDisplay = (props) => {
    const {product} = props;
const {addToCart} = useContext(ShopContext);
  return (
    <div className='productdisplay'>
      <div className="productdisplay-left">
        <div className="productdisplay-img-list">
            <img src={product.image} alt="" />
            <img src={product.image} alt="" />
            <img src={product.image} alt="" />
            <img src={product.image} alt="" />
        </div>
        <div className="productdisplay-img">
        <img className="productdisplay-main-img" src={product.image} alt="" />
        </div>
      </div>
      <div className="productdisplay-right">
        <h1>{product.name}</h1>
      <div className="productdisplay-right-price">
        ${product.price}
      </div>
      <div className="productdisplay-right-description">
      Cupcake ipsum dolor sit amet. Jelly caramels wafer cheesecake tootsie roll wafer. Gummi bears pudding sesame snaps liquorice tart lollipop tiramisu bear claw dessert. 
      </div>
      <button onClick={()=>{addToCart(product.id)}}>ADD TO CART</button>
      
      </div>
    </div>

    
    
  )
}

export default ProductDisplay
