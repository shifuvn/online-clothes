import "./ListTypeProduct.css";
const ListTypeProduct = () => {
  return (
    <div className="listTypeProduct">
      <div className="itemListProduct">
        <img src="https://image.uniqlo.com/UQ/ST3/AsianCommon/imagesgoods/452408/item/goods_09_452408.jpg?width=1008&impolicy=quality_75"></img>
        <div className="text">áo</div>
      </div>
      <div className="itemListProduct">
        <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTjEioK8QLVKQ8OEcSR8wxiQvcRP4jm2p5Z4g&usqp=CAU"></img>
        <div className="text">quần </div>
      </div>
      <div className="itemListProduct">
        <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRUJOdAlGHWHaCM2IsdGSDvAt8uM59BT3ns1Q&usqp=CAU"></img>
        <div className="text">giày</div>
      </div>
    </div>
  );
};

export default ListTypeProduct;
