import React from "react";
import { makeStyles } from "@mui/styles";
import {
  IconButton,
  ImageList,
  ImageListItem,
  ImageListItemBar
} from "@mui/material";
import { Button, useRecordContext } from "react-admin";
import { HttpApiProvider } from "../../../services/ApiWrapper";

const useStyles = makeStyles((theme) => ({
  root: {
    display: "flex",
    flexWrap: "wrap",
    //justifyContent: "space-around",
    overflow: "hidden",
    backgroundColor: theme.palette.background.paper
  },
  imageList: {
    flexWrap: "nowrap",
    // Promote the list into his own layer on Chrome. This cost memory but helps keeping high FPS.
    transform: "translateZ(0)"
  },
  title: {
    color: "#fff"
  },
  titleBar: {
    background:
      "linear-gradient(to top, rgba(0,0,0,0.7) 0%, rgba(0,0,0,0.3) 70%, rgba(0,0,0,0) 100%)"
  }
}));

export default function SingleLineImageList(props) {
  const classes = useStyles();
  const record = useRecordContext();
  const [images, setImages] = React.useState();

  React.useEffect(() => {
    const fetchData = async (id) => {
      const result = await HttpApiProvider.get(`products/${id}/images`);
      setImages(result.data?.data);
    };

    fetchData(record.id);
  }, [record.id]);

  const handlePromoteImg = async (e) => {
    const imageId = e.target.value;
    const payload = {
      productId: record.id,
      imageId: imageId
    };
    const result = await HttpApiProvider.put(
      `products/thumbnail/promote`,
      payload
    );
    console.log(result.data);
  };

  return (
    <div className={classes.root}>
      <ImageList sx={{ width: 500, height: 450 }} cols={3} rowHeight={164}>
        {images &&
          images.map((item, idx) => (
            <ImageListItem key={idx}>
              <img src={item.url} alt={`${item.id} img`} />
              <ImageListItemBar
                classes={{
                  root: classes.titleBar,
                  title: classes.title
                }}
                actionIcon={
                  <IconButton aria-label={`star ${item.title}`}>
                    <Button
                      onClick={handlePromoteImg}
                      value={item.id}
                      name="Promote"
                      label="Promote thumbnail"
                      className={classes.title}
                    />
                  </IconButton>
                }
              />
            </ImageListItem>
          ))}
      </ImageList>
    </div>
  );
}
