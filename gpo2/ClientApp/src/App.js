import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Login } from './components/Login';

export default class App extends Component {
  displayName = App.name

  render() {
      return (
          <Login />
          // TODO: Router?
      /*<Layout> // меню
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetchdata' component={FetchData} />
      </Layout> */
    );
  }
}
