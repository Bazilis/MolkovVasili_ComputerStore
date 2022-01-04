import React from "react";
import Grid from "@mui/material/Grid";
import { Container } from "@mui/material";

const HomePage = () => {
  return (
    <Container component="main" maxWidth="xl">
      <Grid container spacing={2}>
        <Grid item xs={6} md={8}>
          <div>xs=6 md=8</div>
        </Grid>
        <Grid item xs={6} md={4}>
          <div>xs=6 md=4</div>
        </Grid>
        <Grid item xs={6} md={4}>
          <div>xs=6 md=4</div>
        </Grid>
        <Grid item xs={6} md={8}>
          <div>xs=6 md=8</div>
        </Grid>
      </Grid>
    </Container>
  );
};

export default HomePage;
