import React from 'react';
import { createStyles, makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles(theme =>
    createStyles({
       capitalize: {
        textTransform: 'capitalize',
        textAlign: 'center'
      },
    }),
);
export default function Data(props) {
    const classes = useStyles();
    return (
        <div className={classes.capitalize}>
            {props.data}
        </div>
    );
}