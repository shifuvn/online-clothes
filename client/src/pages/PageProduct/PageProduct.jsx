import Header from "../../components/Header/Header";
import Product from "../../components/Product/Product";
import React, { useEffect, useState } from "react";
import ReactPaginate from "react-paginate";
import axios from "axios";
import "./PageProduct.css";

const PageProduct = () => {
  const PageSize = 4;
  const [items, setItems] = useState([]);
  const [currentPage, setCurrentPage] = useState(0);
  const [totalPages, setTotalPages] = useState(
    0);
  useEffect(() => {
    const fetchData = async () => {
      try {
        console.log(currentPage);
        const res = await axios.get(`https://localhost:9443/api/v1/Products?OrderBy=desc&PageIndex=${currentPage + 1}&PageSize=${PageSize}`, {
        });
        console.log(res);
        setItems(res.data.data.items);
        setTotalPages(res.data.data.pages);
      } catch (err) {
        console.error(err);
      }
    };
    fetchData();
  }, [currentPage]);


  const startIndex = currentPage * PageSize;
  const endIndex = startIndex + PageSize;

  return (
    <>
      <div>
        <Header />
        <div className="container-page-product">
          <div className="product">
            {items.map((item) => {
              return <Product product={item} />;
            })}</div>
          <div className="container_button"> <button
            disabled={currentPage === 0}
            onClick={() => setCurrentPage(currentPage - 1)}
          >
            Prev
          </button>
            {Array.from({ length: totalPages }, (_, index) => (
              <button
                key={index}
                onClick={() => setCurrentPage(index)}
                style={{
                  backgroundColor: index === currentPage ? "lightblue" : "white",
                  color: index === currentPage ? "white" : "black",
                }}
              >
                {index + 1}
              </button>
            ))}
            <button
              disabled={endIndex >= (totalPages * PageSize)}
              onClick={() => setCurrentPage(currentPage + 1)}
            >
              Next
            </button>
          </div>
        </div>
      </div>
    </>
  );
};
export default PageProduct;
