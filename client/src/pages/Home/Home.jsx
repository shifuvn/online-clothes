import Header from "../../components/Header/Header";
import ListTypeProduct from "../../components/ListTypeProduct/ListTypeProduct";
import SlideShow from "../../components/SlideShow/SlideShow";
import "./Home.css";
import Footer from "../../components/Footer/Footer";
import Product from "../../components/Product/Product"
const Home = () => {
  return (
    <>
      <Header />
      <SlideShow />
      <ListTypeProduct />
      <div className="product">
        <div className="itemProduct">
          <img src="https://localhouze.com/wp-content/uploads/2022/11/Quan-Jogger-Nam-Nu-Khaki-nhieu-tui-Den-4.jpg"></img>
          <div className="infoProduct">Quần Jogger túi hộp Nam Đen</div>
          <div className="price">320,000$</div>
        </div>
        <div className="itemProduct">
          <img src="https://localhouze.com/wp-content/uploads/2022/11/quanjogger-5.jpg"></img>
          <div className="infoProduct">Quần Jogger nhiều túi Nam Đen</div>
          <div className="price">350,000$</div>
        </div>{" "}
        <div className="itemProduct">
          <img src="https://localhouze.com/wp-content/uploads/2022/11/quanjogger-3.jpg"></img>
          <div className="infoProduct">Quần Jogger Nam, Jogger pants, quần jogger kaki</div>
          <div className="price">290,000$</div>
        </div>{" "}
        <div className="itemProduct">
          <img src="https://hidanz.com/wp-content/uploads/2021/01/quan-kaki-nam-cong-so.jpg"></img>
          <div className="infoProduct">Quần kaki nam công sở</div>
          <div className="price">390,000$</div>
        </div>{" "}
        <div className="itemProduct">
          <img src="https://sakurafashion.vn/upload/a/2749-ao-hoodie-ao-thun-tay-dai-3587.jpg"></img>
          <div className="infoProduct">Áo hoodie nữ</div>
          <div className="price">250,000$</div>
        </div>
        <div className="itemProduct">
          <img src="https://laforce.vn/wp-content/uploads/2022/07/phoi-do-voi-ao-so-mi-nam.jpg"></img>
          <div className="infoProduct">Sơ mi Nam</div>
          <div className="price">590,000$</div>
        </div>
        <div className="itemProduct">
          <img src="https://muaaothun.vn/wp-content/uploads/2022/07/ao-polo.jpg"></img>
          <div className="infoProduct">Áo Polo Nam</div>
          <div className="price">390,000</div>
        </div>
        <div className="itemProduct">
          <img src="https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/452408/item/goods_09_452408.jpg?width=1008&impolicy=quality_75"></img>
          <div className="infoProduct">Ten san pham</div>
          <div className="price">390,000</div>
        </div>
      </div>

      <Footer />
    </>
  );
};
export default Home;
