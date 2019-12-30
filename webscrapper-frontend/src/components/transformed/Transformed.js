import React, { useState, useEffect } from 'react';
import Data from './Data';
import { createStyles, makeStyles } from '@material-ui/core/styles';

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

export default function Transformed(props) {
    const [data, setData] = useState(null);
    const classes = useStyles();

    useEffect(() => {
        setData(props.transformed)
    }, [props.transformed]);

    console.log('data transformed', data)

    return (
        <div className={classes.container}>
            <h4 className={classes.header}>Transform detail:</h4>
            <Data data={data}/>
        </div>
    );
}