import React, { useState } from 'react';
import MaterialTable from 'material-table';
import axios from 'axios';

function exportSingleElement(rowData) {
    const data = JSON.parse(JSON.stringify(rowData));
    delete data.tableData;
    axios.post('http://localhost:54985/api/etl/exportSingleToTxt', data, {
        headers: {'Content-Type': 'application/json'}
    })
    .then((response) => {
        console.log("Succes save")
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
                    icon: 'save',
                    tooltip: 'Save',
                    onClick: (event, rowData) => exportSingleElement(rowData)
                  }
                ]}
            />
        </div>
    );
}