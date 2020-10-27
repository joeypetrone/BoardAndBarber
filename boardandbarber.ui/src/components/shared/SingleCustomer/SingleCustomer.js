import React from 'react';

import "./SingleCustomer.scss"

class SingleCustomer extends React.Component {
  render() {

    return (
      <>
        <strong>{this.props.name}</strong>
          <ul>
            <li>Id: {this.props.id}</li>
            <li>Birthday: {this.props.birthday}</li>
            <li>Favorite Barber: {this.props.Name}</li>
            <li>Notes: {this.props.Name}</li>
          </ul>
      </>
    )
  }
}

export default SingleCustomer;
