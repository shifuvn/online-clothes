import "./Product.css";
import { Link } from "react-router-dom";
const Product = ({ product }) => {
  return (
    <>
      <Link to={`/products/${product.id}`} style={{ textDecoration: "none" }}>
        <div className="itemProduct">
          <img src={product.thumbnailUrl}></img>
          <div className="infoProduct">{product.name}</div>
          <div className="price">{product.price}</div>
        </div>
      </Link>
    </>
  );
};
export default Product;
