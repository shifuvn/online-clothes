import React from "react";
import { Typography, Grid, Button } from "@material-ui/core";
import { Link } from "react-router-dom";

const FilterSkuOptions = ({ skus }) => {
  const handleClickSkuOption = (e) => {
    console.log(e.target.innerHTML);
  };

  return (
    <div style={{ padding: "24px 0" }}>
      <Typography gutterBottom variant="h5" component="div">
        Others
      </Typography>
      <Grid container spacing={8}>
        {skus &&
          skus.map((item, idx) => {
            return (
              <Grid key={idx} item xs={3}>
                <Link
                  to={`/product/${item.sku}`}
                  style={{
                    padding: "8px 24px",
                    backgroundColor: "#d5d5d5",
                    textDecoration: "none"
                  }}
                  onClick={handleClickSkuOption}
                >
                  {item.sku}
                </Link>
              </Grid>
            );
          })}
      </Grid>
    </div>
  );
};

export default FilterSkuOptions;
