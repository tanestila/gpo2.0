﻿import React, { Component } from 'react';
// TODO: Добавить bootstrap по-человечески
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
    // Post query, выдает 404 FIXIT
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
            <form >
                <div className="form-group">
                    <input type="text" placeholder="Username" className="form-control" />
                    <input type="password" placeholder="Password" className="form-control" />
                </div>
                <button type="button" className="btn btn-primary" onClick={this.Login}>Submit</button>
            </form >
        );
    }
}


