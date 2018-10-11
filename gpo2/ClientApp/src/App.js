import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Login } from './components/Login';

import { Registration } from './components/Registration';

export default class App extends Component {
  displayName = App.name

  render() {
      return (
          <div>
              <Route exact path='/' component={Login} />
              <Route exact path='/reg' component={Registration} />     
            
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
