import React , { useState } from 'react';
import { TextField, Button } from '@material-ui/core';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import axios from 'axios';

const useStyles = makeStyles(theme =>
    createStyles({
      container: {
        display: 'flex',
        flexWrap: 'wrap',
        justifyContent: 'center',
      },
      textField: {
        marginLeft: theme.spacing(1),
        marginRight: theme.spacing(1),
        width: 500,
  
      },
      header: {
          textAlign: 'center',
          fontSize: '24px',
      },
      link: {
          textDecoration: 'none',
          color: 'green'
      },
      button: {
          margin: theme.spacing(1),
      },
      buttonWrapper: {
          width: '100%',
          display: 'flex',
          margin: '24px 0',
          justifyContent: 'center'
      },
    }),
);

export default function GetURL() {
    const [transfrom, setTransfrom] = useState([]);
    const classes = useStyles();

   
    function handleSubmit(e){
        e.preventDefault();
        const url = document.querySelector("#urlAdress").value;
        postUrl(url)
    }
      
    function postUrl(url) {
        axios.post('http://localhost:54985/api/etl/extract', {
            url_adress: url
        })
        .then((response) => {
            console.log(response);
        })
        .catch((error) => {
            console.log(error);
        });
    }

    function getAfterTransform() {
        fetchData();
    }
      
    const fetchData = () => {
        axios.get('http://localhost:54985/api/etl/transform')
        .then(response => {
            console.log(response.data);
            setTransfrom(response.data)
        })
        .catch(error => {
            console.log(error);
        })
    }

    return (
        <div>
            <p className={classes.header}>Prosze podać adress URL domeny <a className={classes.link} target="_blank" href="https://www.otodom.pl/" >Otodom.pl</a></p>
            <form className={classes.container} autoComplete="on" onSubmit={handleSubmit}>
                <div>
                    <TextField
                        required
                        id="urlAdress"
                        label="URL adress"
                        className={classes.textField}
                        margin="normal"
                    />
                </div>
                <div className={classes.buttonWrapper}>
                    <Button type="submit" variant="contained" color="primary" className={classes.button}>
                        Extract
                    </Button> 
                    <Button variant="contained" color="primary" className={classes.button} onClick={getAfterTransform}>
                        Transform
                    </Button> 
                    <Button variant="contained" disabled color="primary" className={classes.button}>
                        Load
                    </Button> 
                    <Button variant="contained" disabled color="primary" className={classes.button}>
                        ETL
                    </Button> 
                    <Button variant="contained" disabled color="primary" className={classes.button}>
                        Export to .csv
                    </Button> 
                </div>
            </form> 
        </div>
    );
}