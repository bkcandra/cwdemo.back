
import { Button, Grid, MenuItem, Paper, TextField } from '@mui/material';
import { Field, Form, Formik } from 'formik';
import React, { useContext, useEffect } from 'react';
import * as Yup from 'yup';
import { ICreateCatalog } from '../../utilities/api/interfaces/ICatalogModel';
import { MainTitle } from '../main-page/components/maintitle';
import Title from '../shared-components/Title';
import { useFormik } from 'formik';
import { useNavigate } from 'react-router-dom';
import { BackendAPI, useBackendApi } from '../../utilities/api/backendapi';


const CreateCatalog = (): React.ReactElement => {
    const { setTitle } = useContext(MainTitle);
    const navigate = useNavigate();
    const { request: requestCreateStore } = useBackendApi<ICreateCatalog>(BackendAPI.catalog.create, 'POST');



    useEffect(() => {
        setTitle('Create Catalog');
    }, [setTitle]);

    const handleSubmit = async (values: ICreateCatalog) => {
console.log(values)
        requestCreateStore(
            values,
            true,
            '',
            (response) => {
                if (response.valid) {
                    navigate(-1); // Navigate back one step in the browser history
                }
            }
        );
    };

    const formik = useFormik<ICreateCatalog>({
        initialValues: {
            name: '',
            description: '',
            price: 0,
            type: 1,
            storeId: 1
        },
        validationSchema: Yup.object({
            name: Yup.string().required('Name is required'),
            description: Yup.string(),
            price: Yup.number().min(0, 'Price must be greater than or equal to 1').required('Price is required'),
            type: Yup.number().oneOf([1, 2], 'Invalid type').required('Type is required'),
            storeId: Yup.number().required('Store ID is required'),
        }),
        onSubmit: handleSubmit
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
                        <Title>Create Catalog</Title>
                        <form onSubmit={formik.handleSubmit}>
                            <TextField
                                fullWidth
                                id="name"
                                name="name"
                                label="Name"
                                variant="outlined"
                                value={formik.values.name}
                                onChange={formik.handleChange}
                                error={formik.touched.name && Boolean(formik.errors.name)}
                                helperText={formik.touched.name && formik.errors.name}
                                sx={{ mt: 2 }}
                            />
                            <br />
                            <TextField
                                fullWidth
                                id="description"
                                name="description"
                                label="Description"
                                variant="outlined"
                                value={formik.values.description}
                                onChange={formik.handleChange}
                                error={formik.touched.description && Boolean(formik.errors.description)}
                                helperText={formik.touched.description && formik.errors.description}
                                sx={{ mt: 2 }}
                            />
                            <br />
                            <TextField
                                fullWidth
                                id="price"
                                name="price"
                                label="Price"
                                variant="outlined"
                                type="number"
                                value={formik.values.price}
                                onChange={formik.handleChange}
                                error={formik.touched.price && Boolean(formik.errors.price)}
                                helperText={formik.touched.price && formik.errors.price}
                                sx={{ mt: 2 }}
                            />
                            <br />
                            <TextField
                                fullWidth
                                id="type"
                                name="type"
                                label="Type"
                                variant="outlined"
                                select
                                value={formik.values.type}
                                onChange={formik.handleChange}
                                error={formik.touched.type && Boolean(formik.errors.type)}
                                helperText={formik.touched.type && formik.errors.type}
                                sx={{ mt: 2 }}
                            >
                                <MenuItem value="1">Pizza</MenuItem>
                                <MenuItem value="2">Toppings</MenuItem>
                            </TextField>
                            <br />
                            <TextField
                                fullWidth
                                id="storeId"
                                name="storeId"
                                label="Store ID"
                                variant="outlined"
                                type="number"
                                value={formik.values.storeId}
                                onChange={formik.handleChange}
                                error={formik.touched.storeId && Boolean(formik.errors.storeId)}
                                helperText={formik.touched.storeId && formik.errors.storeId}
                                sx={{ mt: 2 }}
                            />
                            <br /><br />
                            <Button variant="contained" type="submit">Create</Button>

                            <Button type="button" variant="outlined" onClick={() => navigate(-1)} sx={{ mt: 2 }}>
                                Back
                            </Button>
                        </form>


                    </Paper>
                </Grid>
            </Grid>
        </React.Fragment>
    );
};

export default CreateCatalog;
