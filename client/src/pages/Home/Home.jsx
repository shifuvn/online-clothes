import Header from "../../components/Header/Header";
import ListTypeProduct from "../../components/ListTypeProduct/ListTypeProduct";
import SlideShow from "../../components/SlideShow/SlideShow";
import "./Home.css";

const Home = () => {
  return (
    <>
      <Header />
      <SlideShow />
      <ListTypeProduct />
      <div className="product">
        <div className="itemProduct">
          <img src="https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/452408/item/goods_09_452408.jpg?width=1008&impolicy=quality_75"></img>
          <div className="infoProduct">Ten san pham</div>
          <div className="price">390,000</div>
        </div>
        <div className="itemProduct">
          <img src="https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/452408/item/goods_09_452408.jpg?width=1008&impolicy=quality_75"></img>
          <div className="infoProduct">Ten san pham</div>
          <div className="price">390,000</div>
        </div>{" "}
        <div className="itemProduct">
          <img src="https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/452408/item/goods_09_452408.jpg?width=1008&impolicy=quality_75"></img>
          <div className="infoProduct">Ten san pham</div>
          <div className="price">390,000</div>
        </div>{" "}
        <div className="itemProduct">
          <img src="https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/452408/item/goods_09_452408.jpg?width=1008&impolicy=quality_75"></img>
          <div className="infoProduct">Ten san pham</div>
          <div className="price">390,000</div>
        </div>{" "}
        <div className="itemProduct">
          <img src="https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/452408/item/goods_09_452408.jpg?width=1008&impolicy=quality_75"></img>
          <div className="infoProduct">Ten san pham</div>
          <div className="price">390,000</div>
        </div>
        <div className="itemProduct">
          <img src="https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/452408/item/goods_09_452408.jpg?width=1008&impolicy=quality_75"></img>
          <div className="infoProduct">Ten san pham</div>
          <div className="price">390,000</div>
        </div>
        <div className="itemProduct">
          <img src="https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/452408/item/goods_09_452408.jpg?width=1008&impolicy=quality_75"></img>
          <div className="infoProduct">Ten san pham</div>
          <div className="price">390,000</div>
        </div>
        <div className="itemProduct">
          <img src="https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/452408/item/goods_09_452408.jpg?width=1008&impolicy=quality_75"></img>
          <div className="infoProduct">Ten san pham</div>
          <div className="price">390,000</div>
        </div>
      </div>
      <div className="footer">
        <div>TP. Hồ Chí Minh</div>
        <div></div>
      </div>
    </>
  );
};
export default Home;
