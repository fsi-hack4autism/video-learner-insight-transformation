const config = {
    endpoint: "https://usecase2cosmosdbacct.documents.azure.com:443/",
    key: "",
    databaseId: "usecase2cosmoscontainer",
    containerId: "container1",
    partitionKey: { kind: "Hash", paths: ["/category"] }
};

module.exports = config;