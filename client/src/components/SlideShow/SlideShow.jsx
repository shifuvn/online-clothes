import { Slide } from "react-slideshow-image";
import "react-slideshow-image/dist/styles.css";
import "./SlideShow.css";
const SlideShow = () => {
  const images = [
    //"https://upload.wikimedia.org/wikipedia/en/b/bd/Doraemon_character.png",
    "https://img.cdn.vncdn.io/cdn-pos/556e88-134541/bn/20221017_H1V8ft5tHMMRPqWiPJowfRfB.png",
    "https://img.cdn.vncdn.io/cdn-pos/556e88-134541/bn/20221017_Djad5my0bhW1MTBHzWa62jC1.png",
    "https://img.cdn.vncdn.io/cdn-pos/556e88-134541/bn/20221017_Cfh2S6fkB2j5E8ECbg5x72CO.png",
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
