const mongo = require('mongodb').MongoClient;
const io = require('socket.io').listen(4000).sockets;

const url = 'mongodb://localhost/chat';
const dbname = 'chat';

//Connect to DB
mongo.connect(url, function(err, db){
    if(err){
        throw err;
    }

    console.log('MongoDB Connected...')
});