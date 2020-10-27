import axios from 'axios';
import {baseUrl} from '../data/constants.json';

const getAllCustomers = () => new Promise((resolve,reject) => {

  axios.get(`${baseUrl}/Customers`)
    .then(response => {
      const customers = response.data;
      resolve(customers);
    })
    .catch(reject);
});

export default {getAllCustomers};
