import { Slide } from "react-slideshow-image";
import "react-slideshow-image/dist/styles.css";
import "./SlideShow.css";
const SlideShow = () => {
  const images = [
    "https://upload.wikimedia.org/wikipedia/en/b/bd/Doraemon_character.png",
    "https://doraemon2112shop.com/wp-content/uploads/2020/08/59875579_634137807060090_2576955986480726016_n.jpg",
    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcShNPuH2nc8pfTXq_YYNRGrm6Ah5gwtF1PT8Q&usqp=CAU",
  ];
  const proprietes = {
    duration: 2000,
    transitionDuration: 500,
    infinite: true,
    indicators: true,
    arrows: true,
  };
  return (
    <div className="slideShow">
      <div className="containerSlide">
        <Slide {...proprietes}>
          <div className="each-slide">
            <div>
              <img src={images[0]} alt="img1" />
            </div>
          </div>
          <div className="each-slide">
            <div>
              <img src={images[1]} alt="img2" />
            </div>
          </div>
          <div className="each-slide">
            <div>
              <img src={images[2]} alt="img3" />
            </div>
          </div>
        </Slide>
      </div>
    </div>
  );
};
export default SlideShow;
