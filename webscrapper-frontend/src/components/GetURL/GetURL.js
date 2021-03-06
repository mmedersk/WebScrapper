import React , { useState } from 'react';
import { TextField, Button, Fab, Snackbar, IconButton} from '@material-ui/core';
import { createStyles, makeStyles } from '@material-ui/core/styles';
// import DeleteIcon from '@material-ui/icons/Delete';
// import CloseIcon from '@material-ui/icons/Close';
import Transformed from '../transformed/Transformed';
import Extract from '../extract/Extract';
import Details from '../details/Details';
import Loader from '../loader/Loader';
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
      wrapper: {
          position: 'relative',
      },
      floating: {
          position: 'fixed',
          zIndex: '1000',
          right: '12px',
          bottom: '12px'
      },
      close: {
        padding: theme.spacing(0.5),
      },
    }),
);

export default function GetURL() {
    const [transfrom, setTransfrom] = useState([]);
    const [alert, setAlert] = useState(false);
    const [t, setT] = useState(true);
    const [l, setL] = useState(true);
    const [e, setE] = useState(true);
    const [showData, setShowData] = useState(false);
    const [loader, setLoader] = useState(false);
    const [extractStatus, setExtractStatus] = useState();
    const [details, setDetails] = useState();
    const [open, setOpen] = useState(false);
    const [message, setMessage] = useState();
    const classes = useStyles();

    function handleSubmit(e){
        e.preventDefault();
        const url = document.querySelector("#urlAdress").value;
        if(url.indexOf("otodom") > 0){
            postUrl(url);
            setAlert(false);
        } else {
            setAlert(true);
        }
    }
      
    function postUrl(url) {
        setExtractStatus(null);
        setShowData(false);
        setTransfrom(null);
        setT(true);
        setL(true);
        setLoader(true);
        axios.post('http://localhost:54985/api/etl/extract', {
            url_adress: url
        })
        .then((response) => {
            if(response.status === 200){
                setExtractStatus(true);
                setT(false);
                setLoader(false);
            }
        })
        .catch((error) => {
            console.log(error);
            setExtractStatus(false);
            setLoader(false);
        });
    }

    function getAfterTransform() {
        fetchData();
    }
      
    const fetchData = () => {
        setL(true);
        setLoader(true);
        axios.get('http://localhost:54985/api/etl/transform')
        .then(response => {
            if(response.status === 200){
                setTransfrom(response.data);
                setL(false);
                setLoader(false);
            }
        })
        .catch(error => {
            console.log(error);
            setLoader(false);
        })
    }

    function getAfterLoad() {
        setLoader(true);
        axios.get('http://localhost:54985/api/etl/load')
        .then(response => {
            if(response.status === 200){
                setDetails(response.data);
                setLoader(false);
                setE(false);
                setT(true);
                setL(true);
                setShowData(true);
            }
        })
        .catch(error => {
            console.log(error);
            setLoader(false);
        })
    }

    function fullETLProcess() {
        setExtractStatus(null);
        setTransfrom(null);
        setT(true);
        setL(true);
        setLoader(true);
        setShowData(false);
        axios.post('http://localhost:54985/api/etl/extract', {
            url_adress: document.querySelector("#urlAdress").value
        })
        .then((response) => {
            if(response.status === 200){
                setExtractStatus(true);
                axios.get('http://localhost:54985/api/etl/transform')
                .then(response => {
                    if(response.status === 200){
                        setL(false);
                        setTransfrom(response.data);
                        axios.get('http://localhost:54985/api/etl/load')
                        .then(response => {
                            if(response.status === 200){
                                setDetails(response.data);
                                setLoader(false);
                                setE(false);
                                setT(true);
                                setL(true);
                                setShowData(true);
                            }
                        })
                        .catch(error => {
                            console.log(error);
                            setLoader(false);
                        })
                    }
                })
                .catch(error => {
                    console.log(error);
                    setLoader(false);
                })
            }
        })
        .catch((error) => {
            console.log(error);
            setExtractStatus(false);
            setLoader(false);
        });
    }

    function exportToCsv() {
        debugger;
        axios.post('http://localhost:54985/api/etl/exportToCsv')
        .then((response) => {
            if(response.status === 200){
                setT(true);
                setL(true);
                setLoader(false);
            }
        })
        .catch((error) => {
            console.log(error);
            setLoader(false);
            handleClick();
            setMessage("Empty database, nothing to export.");
        });
    }

    function cleanDb() {
        setLoader(true);
        axios.get('http://localhost:54985/api/etl/cleanDb')
        .then(response => {
            console.log("response", response);
            if(response.status === 200){
                setLoader(false);
                handleClick();
                setMessage("Database cleaned.");
                setDetails([]);
            }
        })
        .catch(error => {
            console.log(error);
            setLoader(false);
            handleClick();
            setMessage("Database not cleaned, try again.");
        })
    }

    const handleClick = () => {
        setOpen(true);
    };

    const handleClose = (event, reason) => {
        if (reason === 'clickaway') {
            return;
        }

        setOpen(false);
    };

    return (
        <div>
            <p className={classes.header}>Type URL adress from <a className={classes.link} target="_blank" href="https://www.otodom.pl/" >Otodom.pl</a></p>
            <form className={classes.container} autoComplete="on" onSubmit={handleSubmit}>
                <div>
                    <TextField
                        required
                        id="urlAdress"
                        label="URL adress"
                        className={classes.textField}
                        margin="normal"
                    />
                <div className={"alert " + (alert ? 'invisible' : '')}>Url have to be from otodom.pl</div>
                </div>
                <div className={classes.buttonWrapper}>
                    <Button type="submit" variant="contained" color="primary" className={classes.button}>
                        Extract
                    </Button> 
                    <Button variant="contained" disabled={t}  color="primary" className={classes.button} onClick={getAfterTransform}>
                        Transform
                    </Button> 
                    <Button variant="contained" disabled={l} color="primary" className={classes.button} onClick={getAfterLoad}>
                        Load
                    </Button> 
                    <Button variant="contained" color="primary" className={classes.button} onClick={fullETLProcess}>
                        ETL
                    </Button> 
                    <Button variant="contained" color="primary" className={classes.button} onClick={exportToCsv}>
                        Export to .csv
                    </Button> 
                </div>
            </form>
            <div className={"realative-wrapper " + (loader ? 'opacity' : '')}>
                <Loader loader={loader}/>
                <Extract extract={extractStatus}/>
                <Transformed transformed={transfrom}/>
                <Details details={details} visible={showData}/>
                <Fab color="secondary" className={classes.floating} onClick={cleanDb}>
                    clean
                </Fab>
                <Snackbar
                    anchorOrigin={{
                        vertical: 'bottom',
                        horizontal: 'left',
                    }}
                    open={open}
                    autoHideDuration={6000}
                    onClose={handleClose}
                    ContentProps={{
                        'aria-describedby': 'message-id',
                    }}
                    message={<span id="message-id">{message}</span>}
                    action={[
                    <IconButton
                        key="close"
                        aria-label="close"
                        color="inherit"
                        className={classes.close}
                        onClick={handleClose}
                    >
                        ✖
                    </IconButton>,
                    ]}
                />
            </div>
        </div>
    );
}