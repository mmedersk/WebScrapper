import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import CircularProgress from '@material-ui/core/CircularProgress';

const useStyles = makeStyles(theme => ({
  root: {
    display: 'flex',
    '& > * + *': {
      marginLeft: theme.spacing(2),
    },
    position: 'absolute',
    justifyContent: 'center',
    width: '100%',
    marginTop: '50px',
    zIndex: '9'
  },
}));

export default function Loader(props) {
  const classes = useStyles();
  if(props.loader){
    return (
      <div className={classes.root}>
        <CircularProgress />
      </div>
    );
  } else {
    return (<div></div>)
  }
}