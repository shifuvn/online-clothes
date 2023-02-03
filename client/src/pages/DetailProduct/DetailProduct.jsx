import Color from "../../components/Color/Color";
import Header from "../../components/Header/Header";
import { useParams } from "react-router-dom";
import "./DetailProduct.css";
import { useContext, useEffect, useState } from "react";
import axios from "axios";
import { cartContext } from "../../Context/cartContext";
import { Navigate, useNavigate } from "react-router-dom";
const DetailProduct = () => {
  const navigate = useNavigate();
  const handleAddtoCart = (e) => {
    e.preventDefault();


    navigate('/Cart');


  }
  const { id } = useParams();
  console.log(id);
  const [img, setImg] = useState('');
  const [price, setPrice] = useState(0);
  const [color, setColor] = useState([]);
  const [title, setTitle] = useState('');
  const [skuSelected, setSkuSelected] = useState(false);
  const [stock, setStock] = useState(false);
  const { cart, setCart } = useContext(cartContext);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const res = await axios.get(`https://localhost:9443/api/v1/Products/${id}`)
        setImg(res.data.data.thumbnailUrl)
        setPrice(res.data.data.price)
        setColor(res.data.data.skus)
        setTitle(res.data.data.name)
      } catch (err) {
        console.error(err);
      }
    };
    fetchData();

  }, [])
  return (
    <>
      <Header />
      <div className="container">
        <div className="img">
          <img src={img}></img>
        </div>
        <div className="info">
          <div className="title">{title}</div>
          <div className="sku">Sku:   {id}</div>
          <div className="pricee">{price}</div>
          <div className="color">Màu sắc:</div>
          <div className="flex-color">
            {color.map((item) => {
              return <Color stock={stock} setStock={setStock} key={item.id} id={item.id} setSkuSelected={setSkuSelected} skuSelected={skuSelected} setImg={setImg} setPrice={setPrice} />;
            })}
          </div>
          {stock ? <div>{`san pham con ${stock}`}</div> : null}
          <div className="addcart">
            <button onClick={handleAddtoCart}>Thêm vào giỏ hàng</button>
          </div>
        </div>

      </div>
    </>
  );
};
export default DetailProduct;
