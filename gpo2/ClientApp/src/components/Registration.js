import React, { Component } from 'react';
import './NavMenu.css';

export class Registration  extends Component {
    displayName = Registration.name

    constructor(props) {
        super(props);
        this.state =
            {
            name: "",
            password: "",
            login: "",
            result: "",
            email: ""
            };
    }

    RegistrationPost = () => {
        console.log(this.state.login + this.state.password);
        fetch('api/Auth/Login', {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                login: this.state.login,
                password: this.state.password,
                name: this.state.name,
                email: this.state.email
            })
        });
    }

    render() {
        return (
            <form className="form-signup  form">
                <h1 className="h3 mb-3 font-weight-normal">Please sign up</h1>
                        <label >Name</label>
                <input type="text" className="form-control" id="validationDefault01" placeholder="First name" required
                    onChange={e => this.setState({ name: e.target.value })}
                />
                        <label >Username</label>
                <input type="text" className="form-control" id="validationDefault02" placeholder="Username" required
                    onChange={e => this.setState({ login: e.target.value })}
                />
                        <label >Email address</label>
                <input type="email" className="form-control" id="validationDefault03" placeholder="Email address" required
                    onChange={e => this.setState({ email: e.target.value })}
                />
                        <label >Password</label>
                <input type="password" className="form-control" id="validationDefault04" placeholder="Password" required
                    onChange={e => this.setState({ password: e.target.value })}
                />
                        <label >Password</label>
                        <input type="password" className="form-control" id="validationDefault05" placeholder="Password" required/>
                    
                
                <br/>
                <button className="btn btn-primary btn-block btn-lg" type="button" onClick={this.RegistrationPost}>Submit form</button>
            </form>
        );
    }
}


