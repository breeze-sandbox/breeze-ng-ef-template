var tsGen = require('./entity-generator/tsgen-core');

tsGen.generate({
    inputFileName: './src/app/model/entities/Template.metadata.json',
    outputFolder: './src/app/model/entities/template',
    camelCase: true,
    kebabCaseFileNames: true,
    baseClassName: 'BaseEntity',
    codePrefix: 'Template'
});
