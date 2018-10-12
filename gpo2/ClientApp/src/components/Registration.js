import React, { Component } from 'react';
import './NavMenu.css';

export class Registration  extends Component {
    displayName = Registration.name

    constructor(props) {
        super(props);
        this.state =
            {
            };
    }

    render() {
        return (
            <form className="form-signup  form">
                <h1 className="h3 mb-3 font-weight-normal">Please sign up</h1>
                        <label >Name</label>
                        <input type="text" className="form-control" id="validationDefault01" placeholder="First name"  required />
                        <label >Username</label>
                        <input type="text" className="form-control" id="validationDefault02" placeholder="Last name"  required />
                        <label >Email address</label>
                        <input type="email" className="form-control" id="validationDefault03" placeholder="Username" required />
                        <label >Password</label>
                        <input type="password" className="form-control" id="validationDefault04" placeholder="Password" required/>
                        <label >Password</label>
                        <input type="password" className="form-control" id="validationDefault05" placeholder="Password" required/>
                    
                
                <br/>
                <button className="btn btn-primary btn-block btn-lg" type="button">Submit form</button>
            </form>
        );
    }
}


