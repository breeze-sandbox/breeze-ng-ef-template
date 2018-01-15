var fs = require('fs');
var config = require('../local.conf').config;
var execFile = require('child_process').execFile;

var exe = fs.realpathSync(config.metadataGen);
var dll = fs.realpathSync(config.metadataDll);

var child = execFile(exe,
  ['-i', '../server/Template.Data/bin/debug/Template.Data.dll', 
  '-o', './src/app/model/entities/Template.metadata.json'], (error, stdout, stderr) => {
    if (error) {
        console.error('stderr', stderr);
        throw error;
    }
    console.log('stdout', stdout);
});
