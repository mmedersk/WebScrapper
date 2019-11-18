import React from 'react';
import './App.css';
import {Container, Grid } from '@material-ui/core';
import Content from './components/content/Content';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <h1>WebScrapper</h1>
      </header>
      <Container maxWidth="lg">
        <Grid container spacing={3}>
          <Grid item xs={12}>
            <Content />
          </Grid>
        </Grid>
      </Container>
    </div>
  );
}

export default App;
