import React from 'react';

import "./Customers.scss"

import customerData from '../../../helpers/data/customerData';

import SingleCustomer from '../../shared/SingleCustomer/SingleCustomer';

class Customers extends React.Component {
  state = {
    customers: []
  }

  componentDidMount() {
    customerData.getAllCustomers()
      .then((response) => {
        const customers = response.data;
          this.setState({
            customers
        });
    }).catch();
  }

  render() {
    const {customers} = this.state;
    const buildCustomerList = customers.map((customer) => {
      <SingleCustomer key={customer.id} customer={customer} />
    })

    return (
      <>
        {buildCustomerList}
      </>
    )
  }
}

export default Customers;
