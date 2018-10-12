import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export class Login extends Component {
    displayName = Login.name

    constructor(props) {
        super(props);
        this.state =
        {
            password: "",
            login: "",
            result: ""
        };
    }

    //Test
    handleClick() {
        fetch('api/Auth/LoginTest')
            .then(response => response.json())
            .then(data => {
                console.log(data);
            });
    }
    // Post query
    Login = () => {
        fetch('api/Auth/Login', {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                firstParam: 'yourValue'
            })
        });
    }

    render() {
        return (
            <form className="form-signin  form">
                <h1 className="h3 mb-3 font-weight-normal">Please sign in</h1>  
                
                <input type="email" id="inputEmail" className="form-control" placeholder="Email address" required autoFocus />              
                
                <input type="password" id="inputPassword" className="form-control" placeholder="Password" required />
                <div className="checkbox mb-3">
                    <label>
                        <input type="checkbox" value="remember-me"/> Remember me 
                    </label>
                </div>
                <button className="btn btn-lg btn-primary btn-block" type="button" onClick={this.Login}>Sign in</button>
                <Link to={'/reg'} className="checkbox">Sign up</Link>
                <Link to={'/reg'} className="checkbox">Sign up with certificate</Link>
            </form>

        );
    }
}


