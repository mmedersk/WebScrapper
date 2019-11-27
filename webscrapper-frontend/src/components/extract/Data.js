import React from 'react';
import { createStyles, makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles(theme =>
    createStyles({
        success: {
            color: 'green'
        },
        failed: {
            color: 'red'
        },
    }),
);

export default function Data(props) {
    const classes = useStyles();
    if (props.data === true ){
        return (
            <p className={classes.success}>
                Success - Data extracted !!!
            </p>
        );
    } else if (props.data === false) {
        return (
            <div className={classes.failed}>
                Invalid URL adress - please type correct adress
            </div>
        );
    } else {
        return (
            <div>
                Please type URL adress
            </div>
        );
    }
}