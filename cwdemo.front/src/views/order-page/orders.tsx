import * as React from 'react';
import { useState, useContext, useEffect, useCallback, ReactNode } from 'react';
import Button from '@mui/material/Button';
import Title from '../shared-components/Title';
import { MainTitle } from '../main-page/components/maintitle';
import { Grid, Paper, MenuItem, Select, FormControl, InputLabel, IconButton, List, ListItem, ListItemSecondaryAction, ListItemText, Typography } from '@mui/material';
import { ICatalog } from '../../utilities/api/interfaces/ICatalogModel';
import { BackendAPI, useBackendApi } from '../../utilities/api/backendapi';
import { SelectChangeEvent } from '@mui/material/Select/Select';
import { IStore } from '../../utilities/api/interfaces/IStoreModel';
import DeleteIcon from '@mui/icons-material/Delete';


interface CatalogListProps {
  items: ICatalog[];
  onAddToCart: (item: ICatalog) => void;
}
const CatalogItem = ({ items, onAddToCart }: CatalogListProps): React.ReactElement => {
  const handleAddToCart = (item: ICatalog) => {
    console.log(`Added ${item.name} to cart`);
    onAddToCart(item);
  };


  return (
    <Grid container spacing={3}>
      {items.map(item => (
        <Grid item xs={4} lg={3} key={item.id}>
          <Paper
            sx={{
              p: 1,
              display: 'flex',
              flexDirection: 'row',
              alignItems: 'center',
              height: '200px'
            }}
          >
            <Grid container spacing={2}>
              <Grid item xs={12}>
                <div>{item.name} - {item.description}</div>
              </Grid>
              <Grid item xs={12}>
                <div>$ {item.price}</div>
              </Grid>
              <Grid item xs={12}>
                <Button variant="contained" onClick={() => handleAddToCart(item)}>
                  Add to Cart
                </Button>
              </Grid>
            </Grid>
          </Paper>
        </Grid>
      ))}
    </Grid>
  );
};


const OrderPage = (): React.ReactElement => {
  const { setTitle } = useContext(MainTitle);

  const [storeId, setStoreId] = useState<number>(0);
  const [catalogItems, setCatalogItems] = useState<ICatalog[]>([]);
  const [orderItems, setOrderItems] = useState<ICatalog[]>([]);

  const { request, response: responseGetCatalog } = useBackendApi<ICatalog[]>(
    BackendAPI.catalog.get
  );

  const { request: requestGetStore, response: responseGetStore } = useBackendApi<IStore[]>(
    BackendAPI.store.get
  );

  useEffect(() => {
    setTitle('Catalog List');
    request()
  }, [setTitle, request]);

  useEffect(() => {
    if (responseGetCatalog?.content) {
      const filteredCatalogItems = storeId !== 0 ? responseGetCatalog.content.filter(item => item.storeId === storeId) : responseGetCatalog.content;
      setCatalogItems(filteredCatalogItems);
    }
  }, [responseGetCatalog, storeId]);

  const handleStoreChange = (event: SelectChangeEvent<number>, child: ReactNode) => {
    const storeId = event.target.value as number;
    setStoreId(storeId);
  };

  const handleAddToCart = useCallback((item: ICatalog) => {

    setOrderItems((prevItems) => [...prevItems, item]);
  }, []);

  const calculateTotalPrice = () => {
    return orderItems.reduce((total, item) => total + item.price, 0);
  }

  const handleRemoveItem = (index: number) => {
    const updatedItems = [...orderItems];
    updatedItems.splice(index, 1);
    setOrderItems(updatedItems);
  };

  return (
    <React.Fragment>
      <Grid container spacing={3}>

        <Grid item spacing={3} xs={12} lg={8}>
          {/* Filter */}
          <Grid item lg={12}>
            <Paper
              sx={{
                p: 2,
                display: 'flex',
                flexDirection: 'row',
                alignItems: 'center'
              }}
            >
              <FormControl sx={{ m: 1, minWidth: 120 }}>
                <InputLabel id="store-select-label">Store</InputLabel>
                <Select
                  labelId="store-select-label"
                  id="store-select"
                  value={storeId}
                  label="Store"
                  onChange={handleStoreChange}
                >
                  <MenuItem value={0}>All Stores</MenuItem>
                  {responseGetStore && responseGetStore.content && responseGetStore?.content.map(store => (
                    <MenuItem key={store.id} value={store.id}>{store.name}</MenuItem>
                  ))}
                </Select>

              </FormControl>
            </Paper>
          </Grid>
          {/* Catalog */}
          <Grid item lg={12}>
            <Paper
              sx={{
                p: 2,
                display: 'flex',
                flexDirection: 'column',
              }}
            >
              <CatalogItem
                items={catalogItems}
                onAddToCart={handleAddToCart}
              />
            </Paper>
          </Grid>
        </Grid>
        <Grid item spacing={3} xs={12} lg={4}>

          <Paper
            sx={{
              p: 2,
              display: 'flex',
              flexDirection: 'column',
              alignItems: 'center',
            }}
          >
            <Title>Order</Title>
            {/* Order Items */}
            {orderItems.length > 0 ? (
              <List>
                {orderItems.map((item, index) => (
                  <ListItem key={index}>
                    <ListItemText
                      primary={item.name}
                      secondary={`$${item.description}`}
                    />
                    <ListItemText
                      secondary={`$${item.price}`}
                    />
                    <ListItemSecondaryAction>
                      <IconButton
                        edge="end"
                        aria-label="delete"
                        onClick={() => handleRemoveItem(index)}
                      >
                        <DeleteIcon />
                      </IconButton>
                    </ListItemSecondaryAction>
                  </ListItem>
                ))}
              </List>
            ) : (
              <Typography variant="body1">No items added to cart.</Typography>
            )}
            {/* Total */}
            {orderItems.length > 0 && (
              <Typography variant="h6" sx={{ mt: 2 }}>
                Total: ${calculateTotalPrice()}
              </Typography>
            )}
          </Paper>
        </Grid>
      </Grid>
    </React.Fragment>
  );
};

export default OrderPage;
