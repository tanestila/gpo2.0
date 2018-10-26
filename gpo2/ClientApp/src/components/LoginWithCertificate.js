import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './StyleForForms.css';

export class LoginWithCertificate extends Component {



    constructor(props) {
        super(props);
        this.state =
            {
                
            };
    }

    componentDidMount() {
       
    }
    //style={{ width: 100 %; resize: none; border: 0; }}

    renderCertificateList = props => <ul>rtr </ul>

    render() {

        return (
            
            <div>
                <script language="javascript" src="(/Scripts/cadesplugin_api.js)"></script>
            <script language="javascript" src="(/Scripts/Code.js)" ></script >
                <p id="info_msg" name="CertificateTitle">Сертификат:</p>
                <div id="item_border" name="CertListBoxToHide">
                    <select size="4" name="CertListBox" id="CertListBox" > </select>
                </div>

                <div id="boxdiv" >
                <span id="errorarea">
                    У вас отсутствуют личные сертификаты. Вы можете получить сертификат от тестового УЦ, предварительно установив корневой сертификат тестового УЦ в доверенные.
                            </span>
            </div>


            <div id="cert_info">
                <h3 id="cert_txt" >Информация о сертификате</h3>
                    <p className="info_field" id="subject"></p>
                    <p className="info_field" id="issuer"></p>
                    <p className="info_field" id="from"></p>
                    <p className="info_field" id="till"></p>
                    <p className="info_field" id="provname"></p>
                    <p className="info_field" id="algorithm"></p>
                    <p className="info_field" id="status"></p>
                </div>
                <script language="javascript">
                    CheckForPlugIn('isPlugInEnabled');
                            </script>
                </div>
        );
    }
}