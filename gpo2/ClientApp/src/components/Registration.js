import React, { Component } from 'react';
import { Button }  from 'react-bootstrap';

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
            <form>
                <br />
                <div className="form-row">
                    <div className="col-md-4 mb-3">
                        <label >First name</label>
                        <input type="text" className="form-control" id="validationDefault01" placeholder="First name"  required />
                    </div>
                    <div className="col-md-4 mb-3">
                        <label >Last name</label>
                        <input type="text" className="form-control" id="validationDefault02" placeholder="Last name"  required />
                    </div>
                </div>
                <div className="form-row">
                    <div className="col-md-6 mb-3">
                        <label >City</label>
                        <input type="text" className="form-control" id="validationDefault03" placeholder="City" required />
                    </div>
                    <div className="col-md-3 mb-3">
                            <label >State</label>
                            <input type="text" className="form-control" id="validationDefault04" placeholder="State" required/>
                    </div>
                    <div className="col-md-3 mb-3">
                        <label >Zip</label>
                        <input type="text" className="form-control" id="validationDefault05" placeholder="Zip" required/>
                    </div>
                </div>
                <br/>
                <button className="btn btn-primary" type="submit">Submit form</button>
            </form>
        );
    }
}


