import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';


export default class App extends React.Component {
    render() {
        return (
           /* <Router>
                <div>
                    <main>
                        <Switch>
                            <Route path="/about" component={About} />
                            <Route path="/" component={Blog} />
                        </Switch>
                    </main>
                </div>
            </Router>*/
            <h1>Если ты это видишь то react работает</h1>
        );
    }
}