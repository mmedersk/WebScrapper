import React, { useState, useEffect } from 'react';
import MaterialTable from 'material-table';
import Button from '@material-ui/core/Button';
import axios from 'axios';

function exportSingleElement(event, rowData) {
    delete rowData.tableData;
    const data = JSON.stringify(rowData);
    axios.get('http://localhost:54985/api/etl/exportSingleToTxt', {
        headers: {'Content-Type':'application/json'},
        data
    })
    .then((response) => {
        console.log("Succes")
    })
    .catch((error) => {
        console.log(error);
    });
}

export default function Data(props) {
    const [header, setHeader] = useState([
            { title: 'Title', field: 'Title' },
            { title: 'Rooms', field: 'Rooms' },
            { title: 'Area', field: 'Area'},
            { title: 'Price', field: 'Price'},
            { title: 'Bond', field: 'Bond'},
            { title: 'BuildingType', field: 'BuildingType'},
            { title: 'FloorsInBuilding', field: 'FloorsInBuilding'},
            { title: 'Windows', field: 'Windows'},
            { title: 'HeatingType', field: 'HeatingType'},
            { title: 'Materials', field: 'Materials'},
            { title: 'Floor', field: 'Floor'},
            { title: 'Address', field: 'Address'},
    ]);

    return (
        <div id="main-table">
            <MaterialTable
                title="Adverts"
                columns={header}
                data={props.data}
                actions={[
                    {
                        onClick: (event, rowData) => exportSingleElement(event, rowData)
                    }
                ]}
                components={{
                    Action: props => (
                        <Button
                            onClick={(event) => props.action.onClick(event, props.data)}
                            color="primary"
                            variant="contained"
                            style={{textTransform: 'none'}}
                            size="small"
                        >
                        Export
                    </Button>
                    ),
                }}
            />
        </div>
    );
}