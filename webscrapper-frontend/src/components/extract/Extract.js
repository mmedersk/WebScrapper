import React, { useState, useEffect } from 'react';
import { createStyles, makeStyles } from '@material-ui/core/styles';
import Data from './Data';

const useStyles = makeStyles(theme =>
    createStyles({
      header: {
        margin: '0 0 24px 0',
        fontSize: '24px',
        fontWeight: '400',
        textDecoration: 'underline',
        textAlign: 'center',
      },
      container: {
          marginTop: '24px',
          minHeight: '100px'
      }
    }),
);

export default function Extract(props) {
    const [data, setData] = useState(null);
    const classes = useStyles();

    useEffect(() => {
        if ( props.extract === true ) {
            setData(true)
        } else if( props.extract === false ) {
            setData(false)
        } else {
            setData(null)
        }
    }, [props.extract]);

    console.log("data extract", data);

    return (
        <div className={classes.container}>
            <h4 className={classes.header}>Extract detail:</h4>
            <Data data={data}/>
        </div>
    );
}