import React, { Component } from 'react';
import { Route } from 'react-router';
import { Login } from './components/Login';
import { LoginWithCertificate } from './components/LoginWithCertificate';
import { Registration } from './components/Registration';

export default class App extends Component {
  displayName = App.name

  render() {
      return (
          <div>
              <Route exact path='/' component={Login} />
              <Route exact path='/reg' component={Registration} />     
              <Route exact path='/logincert' component={LoginWithCertificate} />
           </div>
          // TODO: Router?
      /*<Layout> // меню
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetchdata' component={FetchData} />
      </Layout> */
    );
  }
}
