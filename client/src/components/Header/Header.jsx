import { FiSearch } from "react-icons/fi";
import { Link } from "react-router-dom";
import { AiOutlineShoppingCart } from "react-icons/ai";
import BadgeIcon from '@mui/icons-material/Badge';
import "./Header.css";

export default function Header() {
  return (
    <div className="header">
      <div className="leftHeader">
        <Link to="/" className="itemLeftHeader">
          Trang chủ
        </Link>
        <Link to="/products" className="itemLeftHeader">
          Sản phẩm
        </Link>
        <Link to="/cart" className="itemLeftHeader">
          Giỏ hàng
        </Link>
        <Link to="/profile" className="itemLeftHeader">
          Profile
        </Link>
        <div className="logo">
          Online Clothes
        </div>
      </div>

      <Link to="/login" className="rightHeader"> Log In</Link>

      {/*<div className="iconCart">
          <AiOutlineShoppingCart className="itemRightHeader icon" style={{ fontSize: "25px", marginLeft: "5px", padding: "3 5px" }}
          />
          <link to="/cart" />
          <div className="quantity">3</div>
  </div>
  </div>*/}
    </div>
  );
}
