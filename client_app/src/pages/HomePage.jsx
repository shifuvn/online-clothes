import React, { Fragment } from "react";
import { Navbar, ProductContainer, Footer } from "../components";

const HomePage = () => {
  return (
    <Fragment>
      <Navbar />
      <ProductContainer />
      <Footer />
    </Fragment>
  );
};

export default HomePage;
