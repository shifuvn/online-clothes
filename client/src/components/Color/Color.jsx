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
            src="data:image/jpeg;base64,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"
          ></img>
        </div>
        <div className="text-color">den</div>
      </div>

    </div>
  );
};
export default Color;
