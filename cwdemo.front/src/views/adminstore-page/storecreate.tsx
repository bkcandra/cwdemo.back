import React, { useContext } from 'react';
import { Grid, Paper } from '@mui/material';
import { MainTitle } from '../main-page/components/maintitle';
import Title from '../shared-components/Title';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import TextField from '@mui/material/TextField';
import Checkbox from '@mui/material/Checkbox';
import FormControlLabel from '@mui/material/FormControlLabel';
import Button from '@mui/material/Button';
import { useNavigate } from 'react-router-dom';

import { ICreateStore } from '../../utilities/api/interfaces/IStoreModel';
import { useBackendApi, BackendAPI } from '../../utilities/api/backendapi';
import { ICreateCatalog } from '../../utilities/api/interfaces/ICatalogModel';

const initialValues: ICreateStore = {
    name: '',
    location: '',
    active: true,
};

const validationSchema = Yup.object({
    name: Yup.string().required('Name is required'),
    location: Yup.string().required('Location is required'),
});

const CreateStore = (): React.ReactElement => {
    const { setTitle } = useContext(MainTitle);
    const navigate = useNavigate();



    const { request: requestCreateStore } = useBackendApi<ICreateStore>(BackendAPI.store.create, 'POST');



    const handleSubmit = async (values: ICreateStore) => {
        //console.log(values);
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


    const formik = useFormik({
        initialValues,
        validationSchema,
        onSubmit: handleSubmit,
    });

    const handleReset = () => {
        formik.resetForm();
    };

    React.useEffect(() => {
        setTitle('Create Store');
    }, [setTitle]);

    return (
        <React.Fragment>
            <Grid container spacing={3}>
                <Grid item lg={12}>
                    <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
                        <Title>Create Store</Title>
                        <form onSubmit={formik.handleSubmit}>
                            <TextField
                                fullWidth
                                id="name"
                                name="name"
                                label="Name"
                                value={formik.values.name}
                                onChange={formik.handleChange}
                                error={formik.touched.name && Boolean(formik.errors.name)}
                                helperText={formik.touched.name && formik.errors.name}
                                sx={{ mb: 2 }}
                            />
                            <TextField
                                fullWidth
                                id="location"
                                name="location"
                                label="Location"
                                value={formik.values.location}
                                onChange={formik.handleChange}
                                error={formik.touched.location && Boolean(formik.errors.location)}
                                helperText={formik.touched.location && formik.errors.location}
                                sx={{ mb: 2 }}
                            />
                            <FormControlLabel
                                control={
                                    <Checkbox
                                        checked={formik.values.active}
                                        onChange={formik.handleChange}
                                        name="active"
                                    />
                                }
                                label="Active"
                            />
                            <br></br>
                            <Button type="submit" variant="contained" sx={{ mr: 2, mt: 2 }}>
                                Create
                            </Button>
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

export default CreateStore;
