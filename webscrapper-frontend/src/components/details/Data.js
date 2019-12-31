import React, { useState, useEffect } from 'react';
import MaterialTable from 'material-table';
import Button from '@material-ui/core/Button';
import axios from 'axios';

function exportSingleElement(rowData) {
    delete rowData.tableData;
    console.log(rowData)
    axios.post('http://localhost:54985/api/etl/exportSingleToTxt', rowData, {
        headers: {'Content-Type': 'application/json'}
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
                        onClick: (rowData) => exportSingleElement(rowData)
                    }
                ]}
                components={{
                    Action: props => (
                        <Button
                            color="primary"
                            variant="contained"
                            style={{textTransform: 'none'}}
                            size="small"
                        >
                        Export
                    </Button>
                    ),
                }}
                editable={{
                    onRowAdd: newData =>
                      new Promise((resolve, reject) => {
                        setTimeout(() => {
                          {
                            const data = this.state.data;
                            data.push(newData);
                            this.setState({ data }, () => resolve());
                          }
                          resolve()
                        }, 1000)
                      }),
                    onRowUpdate: (newData, oldData) =>
                      new Promise((resolve, reject) => {
                        setTimeout(() => {
                          {
                            const data = this.state.data;
                            const index = data.indexOf(oldData);
                            data[index] = newData;
                            this.setState({ data }, () => resolve());
                          }
                          resolve()
                        }, 1000)
                      }),
                    onRowDelete: oldData =>
                      new Promise((resolve, reject) => {
                        setTimeout(() => {
                          {
                            let data = this.state.data;
                            const index = data.indexOf(oldData);
                            data.splice(index, 1);
                            this.setState({ data }, () => resolve());
                          }
                          resolve()
                        }, 1000)
                      }),
                  }}
            />
        </div>
    );
}