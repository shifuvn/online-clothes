import axios from "axios";
import "./Color.css";
const Color = ({ skuSelected, setSkuSelected, id, setImg, setPrice, setStock, stock }) => {
  const handleOnclick = async () => {
    const res = await axios.get(`https://localhost:9443/api/v1/Skus/${id}`);
    setImg(res.data.data.imageUrl);
    setPrice(res.data.data.name)
    setSkuSelected(res.data.data.sku);
    setStock(res.data.data.inStock);
  }

  console.log(stock);
  return (
    <div>
      <div className={`color-container ${(skuSelected == id) ? 'red' : ''}`} onClick={handleOnclick}>
        <div>
          <img
            className="img-color"
            src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR0TX5B2D7LpGvedp23RRrPX-DVR62oZDqO-Q&usqp=CAU"
          ></img>
        </div>
        <div className="text-color">Check</div>
      </div>

    </div>
  );
};
export default Color;
