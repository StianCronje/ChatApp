const mongo = require('mongodb').MongoClient;
const io = require('socket.io').listen(4000).sockets;

const url = 'mongodb://localhost:27017';
const dbName = 'chat';

//Connect to DB
mongo.connect(url, function(err, client){
    if(err){
        throw err;
    }

    let db = client.db(dbName);

    console.log('MongoDB Connected...')

    //Connect to Socket.io
    io.on('connection', function(socket){
        let chat = db.collection('chats');

        // send status message
        sendStatus = function(s){
            socket.emit('status', s);
        }

        chat.find().limit(100).sort({_id:1}).toArray(function(err, res){
            if(err){
                throw err;
            }

            // emit messages
            socket.emit('output', res);
        });

        // Handle input events
        socket.on('input', function(data){
            let name = data.name;
            let message = data.message;

            if(name == '' || message == ''){
                sendStatus('Please enter a name and message');
            } else {
                chat.insert({name: name, message: message}, function(){
                    io.emit('output', [data]);

                    sendStatus({
                        message: 'Message sent',
                        clear: true
                    })

                    console.log(name + ":", message)
                });
            }
        });

        // Handle delete message
        socket.on('delete', function(data){
            chat.remove({_id: data._id}, function(){
                console.log('deleted message with id:', data._id);
                socket.emit('deleted');
            })
        })
    })
});