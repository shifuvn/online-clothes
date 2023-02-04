import React from "react";
import FallBackNoImage from "../../../resources/images/fallback-no-product-image.jpg";
import { useNavigate } from "react-router-dom";
import {
  Card,
  CardMedia,
  CardContent,
  Typography,
  CardActions,
  Button
} from "@material-ui/core";
import { toVnd } from "../../../utils";

const ProductItem = ({ sku, imageUrl, name, price, description }) => {
  const navigate = useNavigate();

  const handleClickDetail = () => {
    const path = `product/${sku}`;
    navigate(path);
  };

  return (
    <Card style={{ maxWidth: "100%" }}>
      <CardMedia
        style={{ paddingTop: "100%", objectFit: "contain" }}
        image={imageUrl || FallBackNoImage}
      />
      <CardContent>
        <Typography gutterBottom variant="h5" component="div">
          {name}
        </Typography>
        <Typography variant="body2" color="text.secondary">
          {toVnd(price)}
        </Typography>
      </CardContent>
      <CardActions>
        <Button size="small" onClick={handleClickDetail}>
          Detail
        </Button>
        <Button size="small">Add to cart</Button>
      </CardActions>
    </Card>
  );
};

export default ProductItem;
