import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './StyleForForms.css';

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
    componentDidMount() {
        const script = document.createElement("script");
        script.src = "./Scripts/cadesplugin_api.js";
        script.language = "javascript";
        document.body.appendChild(script);
        const script1 = document.createElement("script");
        script1.src = "./Scripts/code.js";
        script1.language = "javascript";
        document.body.appendChild(script1);
        const script2 = document.createElement("script");
        script2.innerHTML = "CheckForPlugIn('isPlugInEnabled');";
        script2.language = "javascript";
        document.body.appendChild(script2);

    }

    //Test
    //handleClick() {
    //    fetch('api/Auth/Login')
    //        .then(response => response.json())
    //        .then(data => {
    //            console.log(data);
    //        });
    //}
    // Post query
    LoginPost = () => {
        console.log(this.state.login + this.state.password);
        fetch('api/Auth/Login', {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                login: this.state.login,
                password: this.state.password
            })
        });
    }
    ScriptCheck = () => {
        render() {
            return {

            }
        }

    }

    render() {

        return (
            <div>
            <form className="form-signin  form">

                <h1 className="h3 mb-3 font-weight-normal">Please sign in</h1>  

                <input type="email" id="inputEmail" className="form-control" placeholder="Email address" required autoFocus
                    onChange={e => this.setState({ login: e.target.value })}
                />              
                
                <input type="password" id="inputPassword" className="form-control" placeholder="Password" required
                    onChange={e => this.setState({ password: e.target.value })}
                />
                <div className="checkbox mb-3">
                    <label>
                        <input type="checkbox" value="remember-me"/> Remember me 
                    </label>
                </div>
                <button className="btn btn-lg btn-primary btn-block" type="button" onClick={this.LoginPost}>Sign in</button>
                <Link to={'/reg'} className="checkbox">Sign up</Link>
                <Link to={'/logincert'} className="checkbox">Sign up with certificate</Link>
            </form>
            <div>
                
                    <p id="info_msg" name="CertificateTitle">Сертификат:</p>
                    <div id="item_border" name="CertListBoxToHide">
                        <select size="4" name="CertListBox" id="CertListBox" className="select"></select>
                    </div>

                    <div id="boxdiv" className="disnone">
                        <span id="errorarea">
                            У вас отсутствуют личные сертификаты. Вы можете получить сертификат от тестового УЦ, предварительно установив корневой сертификат тестового УЦ в доверенные.
        </span>
                    </div>
                    

                    <div id="cert_info">
                        <h3 id="cert_txt" className="vishidden">Информация о сертификате</h3>
                        <p className="info_field" id="subject"></p>
                        <p className="info_field" id="issuer"></p>
                        <p className="info_field" id="from"></p>
                        <p className="info_field" id="till"></p>
                        <p className="info_field" id="provname"></p>
                        <p className="info_field" id="algorithm"></p>
                        <p className="info_field" id="status"></p>
                    </div>

                    
                    
                   
                    
                </div>
            </div>

        );
    }
}


