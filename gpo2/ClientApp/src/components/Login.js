import React, { Component } from 'react';

export class Login extends Component {
    displayName = Login.name

    render() {
        return (
            <form role="form">
                <div className="form-group">
                    <input type="text" placeholder="Username" />
                    <input type="password"  placeholder="Password" />
                </div>
                <button type="submit">Submit</button>
            </form >
        );
    }
}


