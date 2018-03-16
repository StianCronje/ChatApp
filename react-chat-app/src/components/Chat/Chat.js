import React, { Component } from 'react';

import 'react-bootstrap';
import './Chat.css';


export default class Chat extends Component {


    constructor(props) {
        super(props)

        this.state = {
            username: '',
            messages: [],
            status: '',
            chatInput: ''
        }

        this.userChangeHandler = this.userChangeHandler.bind(this);
        this.chatChangeHandler = this.chatChangeHandler.bind(this);
        this.submitHandler = this.submitHandler.bind(this);
        this.deleteMessage = this.deleteMessage.bind(this);
    }

    
    componentDidMount() {
        console.log('Chat component mounted...');
        
        var socket = this.props.socket;

        // Set default status
        var statusDefault = this.state.status;

        var setStatus = (s) => {
            let newState = this.state;
            newState.status = s;
            this.setState(newState);

            if(s !== statusDefault){
                setTimeout(() => {
                    setStatus(statusDefault);
                }, 4000);
            }
        }

        // check connection
        if(socket !== undefined){
            console.log('Connected to socket...');

            // Handle output
            socket.on('output', (data) => {
                if(data.length){
                    for(let i = 0; i < data.length; i++){
                        this.printMessage(data[i]);
                    }
                }                
            });

            // Get Status
            socket.on('status', (data) => {
                setStatus((typeof data === 'object') ? data.message : data);

                if(data.clear){
                    let newState = this.state;
                    newState.chatInput = '';
                    this.setState(newState);
                }
            });
        }

        // Handle input
        const textArea = document.querySelector('#text-area');
        // const submitButton = document.querySelector('.submit')
        textArea.addEventListener('keydown', (event) => {
            if (event.keyCode === 13 && !event.shiftKey) {
                this.submitHandler();
                event.preventDefault();
            }
            // if (event.keyCode === 9) {
            //     event.preventDefault()
            //     this.setState({
            //         chatInput: event.target.value + "\t"
            //     })
            // }
        })

        // Handle deletion
        socket.on('deleted', (data) => {
            let newState = this.state;
            
            for(let i = newState.messages.length - 1; i >= 0; i--) {
                if(newState.messages[i]._id === data._id) {
                    newState.messages.splice(i, 1);
                    console.log('remove', i);
                }
                console.table(newState.messages[i]);
            }

            this.setState(newState);
        });
    }

    submitHandler(){
        console.log('submit --', this.state.username + ':', this.state.chatInput);

        this.props.socket.emit('input', JSON.stringify({
            name: this.state.username,
            message: this.state.chatInput
        }))
    }

    printMessage(message) {
        const oldList = this.state.messages
        const newList = [
            ...oldList,
            message
        ]

        let state = this.state
        state.messages = newList
        this.setState(state)

        // let lastMessage = document.querySelector('#messages:last-child')
        // lastMessage.scrollIntoView({behavior: "smooth", block: "end", inline: "nearest"})
    }

    userChangeHandler(event) {
        let state = this.state
        state.username = event.target.value
        this.setState(state)
    }

    chatChangeHandler(event) {
        let state = this.state
        state.chatInput = event.target.value
        this.setState(state)
    }

    deleteMessage(data){
        console.log('delete: ', data);

        this.props.socket.emit('delete', JSON.stringify(data));
    }

    render() {        
        return (
            <div className="chat">
                <div className="container">
                    <div className="row">
                        <div className="col-md-6 offset-md-3 col-sm-12">
                            <div id="chat">
                                <input type="text" id="username" className="form-control" placeholder="Enter Name..." onChange={this.userChangeHandler} value={this.state.username}/>
                                <br/>
                                <div className="card">
                                    <div id="messages" className="card-block">
                                        {
                                            this.state.messages.map((data, index) => {
                                                return <div className="chat-message" key={index}>
                                                    {data.name + ": " + data.message}
                                                    <button className="btn btn-sm btn-outline-danger float-right" onClick={() => this.deleteMessage(data)}>X</button>
                                                </div>
                                            })
                                        }
                                    </div>
                                </div>
                                <br/>
                                <div id="status">{this.state.status}</div>
                                <textarea id="text-area" 
                                        className="form-control" 
                                        placeholder="Enter Message" 
                                        onChange={this.chatChangeHandler} 
                                        value={this.state.chatInput}/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}