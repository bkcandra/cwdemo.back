import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Button from '@mui/material/Button';
import Title from '../shared-components/Title';
import { useContext, useEffect, useCallback } from 'react';
import { MainTitle } from '../main-page/components/maintitle';
import { Box, Grid, Paper } from '@mui/material';
import { IStore } from '../../utilities/api/interfaces/IStoreModel'
import { BackendAPI, useBackendApi } from '../../utilities/api/backendapi';
import ListTable from '../shared-components/table';
import DataTable from '../shared-components/table';
import { Link } from 'react-router-dom';
import { DataGrid, GridColDef } from '@mui/x-data-grid';


const AdminStoreList = (): React.ReactElement => {
  const { setTitle } = useContext(MainTitle);




  const { request: requestGetStore, response: responseGetStore } = useBackendApi<IStore[]>(
    BackendAPI.store.get
  );

  const { request: requestDeleteStore } = useBackendApi<void>(
    BackendAPI.store.delete, 'DELETE'
  );



  useEffect(() => {
    setTitle('Administration - Store');
    requestGetStore()
  }, [ ]);




  const handleDeleteStore = async (e: any, row: any) => {
    await requestDeleteStore('', false, row.id);
    requestGetStore();
  };

  const headers = ['id', 'name', 'location'];
  const columns: GridColDef[] = headers.map((header) => ({
    headerName: header,
    field: header,
    width: 200
  }));

  columns.push({
    field: "editButton",
    headerName: "Actions",
    description: "Actions column.",
    sortable: false,
    width: 160,
    renderCell: (params) => {
      return (
        <>
          <Button
            variant="contained" component={Link} to={`/admin/store/update/${params.row.id}`}
          >
            Edit
          </Button>
          &nbsp;
          <Button color='error'
            variant="contained"
            onClick={(e) => handleDeleteStore(e, params.row)}
          >
            delete
          </Button>
        </>


      );
    }
  });



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
            <Title>Store List</Title>
            <Grid container justifyContent="space-between" alignItems="flex-start">
              <Button variant="contained" component={Link} to="/admin/store/create">
                Create store
              </Button>
            </Grid>
            <DataGrid
              rows={responseGetStore?.content || []}
              columns={columns}
            />
          </Paper>
        </Grid>
      </Grid>
    </React.Fragment>
  );
};

export default AdminStoreList;


