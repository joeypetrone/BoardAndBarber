import axios from 'axios';
import {baseUrl} from '../data/constants.json';

const getAllCustomers = () => new Promise((resolve,reject) => {

  axios.get(`${baseUrl}/Customers`)
    .then(response => resolve(response.data))
    .catch(reject);
});

export default {getAllCustomers};
