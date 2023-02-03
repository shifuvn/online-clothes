import { useState } from "react";
import "./CartItem.css"
import { RiDeleteBin6Line } from "react-icons/ri";

const CartItem = () => {
    const items = [
        {
            price: 390000,
            name: "ao thun",
            inStock: 3,
            img: "https://salt.tikicdn.com/cache/w78/ts/product/aa/b6/eb/be11ef24004ae85e0f0964d0f03e4462.jpg.webp",
        },
    ];
    const handlePlus = () => {
        setNumber(number + 1);
        setTotal(items[0].price * (number + 1));
    };
    const handleSubtract = () => {
        if (number > 0) {
            setNumber(number - 1);
            setTotal(items[0].price * (number - 1));
        }
    };
    const [total, setTotal] = useState(0);
    const [number, setNumber] = useState(0);
    return (
        <div className="item">
            <div>
                <img
                    className="img-cart-item"
                    src="https://hidanz.com/wp-content/uploads/2021/01/quan-kaki-nam-cong-so.jpg"
                />
            </div>
            <div>Quần Kaki công sở nam</div>
            <div>390000</div>
            <div className="quantity-cart">
                <img
                    onClick={handleSubtract}
                    className="icon-cart-item "
                    src="https://frontend.tikicdn.com/_desktop-next/static/img/icons/decrease.svg"
                />
                <input
                    style={{ width: "28px", height: "19px", textAlign: "center" }}
                    readOnly
                    type="text"
                    value={number}
                />
                <img
                    onClick={handlePlus}
                    className="icon-cart-item "
                    src="https://frontend.tikicdn.com/_desktop-next/static/img/icons/increase.svg"
                ></img>
            </div>
            <div> {total}</div>
            <div>
                <RiDeleteBin6Line style={{ cursor: "pointer" }} />
            </div>
        </div>)

}
export default CartItem;