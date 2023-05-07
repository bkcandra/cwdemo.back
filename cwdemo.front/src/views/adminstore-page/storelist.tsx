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



const AdminStoreList = (): React.ReactElement => {
  const { setTitle } = useContext(MainTitle);




  const { request: requestGetStore, response: responseGetStore } = useBackendApi<IStore[]>(
    BackendAPI.store.get
  );

  const { request: requestDeleteStore } = useBackendApi<void>(
    BackendAPI.store.delete
  );

  useEffect(() => {
    setTitle('Administration - Store');
    requestGetStore()
  }, [setTitle, requestGetStore]);


  const handleDeleteStore = useCallback((storeId: number) => {
    console.log(storeId)
    // requestDeleteStore(storeId).then(() => {
    //   requestGetStore();
    // });

  }, [requestDeleteStore, requestGetStore]);

  


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

            <DataTable
              headers={['ID', 'Name', 'Description']}
              data={responseGetStore?.content || []}
              config={{
                'ID': (store: IStore) => store.id,
                'Name': (store: IStore) => store.name,
                'Description': (store: IStore) => store.location,
                'Actions': (store: IStore) => (
                  <Box display="flex" flexDirection="row" alignItems="center">
                    <Button variant="contained" component={Link} to={`/admin/store/update`}>
                      Edit
                    </Button>
                    <Button
                      variant="contained"
                      color="error"
                      onClick={() => handleDeleteStore(store.id)}
                      sx={{ ml: 1 }}
                    >
                      Delete
                    </Button>
                  </Box>
                ),
              }}
            />

          </Paper>
        </Grid>
      </Grid>
    </React.Fragment>
  );
};

export default AdminStoreList;


