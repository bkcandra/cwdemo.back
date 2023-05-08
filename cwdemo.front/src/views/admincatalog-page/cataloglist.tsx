import * as React from 'react';
import { useState, useContext, useEffect, useCallback } from 'react';
import Button from '@mui/material/Button';
import Title from '../shared-components/Title';
import { MainTitle } from '../main-page/components/maintitle';
import { Grid, Paper } from '@mui/material';
import { ICatalog, ICreateCatalog } from '../../utilities/api/interfaces/ICatalogModel';
import { BackendAPI, useBackendApi } from '../../utilities/api/backendapi';
import DataTable from '../shared-components/table';
import { CatalogType } from '../../utilities/Enums/catalogTypeEnum';
import CreateCatalog from './catalogcreate';
import { Link, RouterProvider } from 'react-router-dom';
import { DataGrid, GridColDef, GridRowId, GridValueGetterParams } from '@mui/x-data-grid';

function preventDefault(event: React.MouseEvent) {
  event.preventDefault();
}


const CatalogList = (): React.ReactElement => {
  const { setTitle } = useContext(MainTitle);


  const { request: requestGetCatalog, response: responseGetCatalog } = useBackendApi<ICatalog[]>(
    BackendAPI.catalog.get
  );
  const { request: requestDeleteCatalog } = useBackendApi<void>(
    BackendAPI.catalog.delete, 'DELETE'
  );



  const handleDeleteCatalog = async (e: any, row: any) => {
    await requestDeleteCatalog('', false, row.id);
    requestGetCatalog();
  };



  const headers = ['id', 'name', 'description', 'storeName'];
  const columns: GridColDef[] = [

    ...headers.map((header) => ({
      headerName: header,
      field: header,
      width: 200,
    })),
  ];
  columns.push({
    headerName: 'catalogType',
    field: 'type',
    width: 200,
    renderCell: (params) => {
      if (params.row.type === 1) {
        return 'Pizza';
      } else {
        return 'Topping';
      }
    }
  });

  columns.push({
    field: "editButton",
    headerName: "Actions",
    description: "Actions column.",
    sortable: false,
    width: 160,
    type: 'singleSelect',
    renderCell: (params) => {
      return (
        <>
          <Button variant="contained" component={Link} to={`/admin/catalog/update/${params.row.id}`}>
            Edit
          </Button>
          &nbsp;
          <Button color='error' variant="contained" onClick={(e) => handleDeleteCatalog(e, params.row)}>
            Delete
          </Button>
        </>
      );
    }
  });

  useEffect(() => {
    setTitle('Catalog List');
    requestGetCatalog()
  }, []);


  return (
    <React.Fragment>
      <Grid container spacing={3}>
        {/* Chart */}
        <Grid item lg={12}>
          <Paper
            sx={{
              p: 2,
              display: 'flex',
              flexDirection: 'column',
            }}
          >
            <Title>Catalog List</Title>
            <Grid container justifyContent="space-between" alignItems="left">
              <Button variant="contained" component={Link} to="/admin/catalog/create">
                Create Catalog
              </Button>
            </Grid>
            <DataGrid
              rows={responseGetCatalog?.content || []}
              columns={columns}
            />
          </Paper>
        </Grid>
      </Grid>

    </React.Fragment>
  );
};

export default CatalogList;
