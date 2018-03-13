import React, { Component } from 'react';
import logo from './../../logo.svg';
import './App.css';

import Chat from './../Chat/Chat';

import io from 'socket.io-client';

const socket = io.connect('http://stiancronje.com:4000');


class App extends Component {
  render() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <h1 className="App-title">Totally not CodeChat</h1>
        </header>
          <br/>
        <Chat {...this.props} socket={socket}/>
      </div>
    );
  }
}

export default App;
