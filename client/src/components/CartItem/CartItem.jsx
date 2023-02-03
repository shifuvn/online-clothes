import "./CartItem.css"
const CartItem = () => {
    return (
        <div className="cart-item-container">
            <div ><img className="img-cart" src="https://dosi-in.com/images/detailed/42/CDL10_1.jpg" /></div>
            <div className="name-cart">tEN SAN PHAM</div>
            <div className="quantity-cart">so luong</div>
            <div className="price-cart">don gia</div>
            <div className="icon-delete-cart">icon xoa san pham</div>
        </div>)

}
export default CartItem;