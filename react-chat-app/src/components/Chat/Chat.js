import React, { Component } from 'react';
import PropTypes from 'prop-types';

import 'react-bootstrap';
import './Chat.css';

export default class Chat extends Component {

    
    componentDidMount() {
        console.log('Chat component mounted...');
    }
    

    render() {
        return (
            <div className="container">
                <div className="row">
                    <div className="col-md-6 offset-md-3 col-sm-12">
                        <div className="status"></div>
                        <div id="chat">
                            <input type="text" id="username" className="form-control" placeholder="Enter Name..."/>
                            <br/>
                            <div className="card">
                                <div id="messages" className="card-block"></div>
                            </div>
                            <br/>
                            <textarea id="text-area" className="form-control" placeholder="Enter Message"></textarea>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}