﻿namespace CRA.ClientLibrary.AzureProvider
{
    using CRA.ClientLibrary.DataProvider;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    /// <summary>
    /// Definition for AzureProviderImpl
    /// </summary>
    public class AzureProviderImpl : IDataProvider
    {
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudTableClient _tableClient;
        private readonly string _storageConnectionString;

        public AzureProviderImpl()
        {
            _storageConnectionString = null;
#if !DOTNETCORE
            _storageConnectionString = ConfigurationManager.AppSettings.Get("AZURE_STORAGE_CONN_STRING");
#endif
            if (_storageConnectionString == null)
            {
                _storageConnectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONN_STRING");
            }
            if (_storageConnectionString == null)
            {
                throw new InvalidOperationException("Azure storage connection string not found. Use appSettings in your app.config to provide this using the key AZURE_STORAGE_CONN_STRING, or use the environment variable AZURE_STORAGE_CONN_STRING.");
            }

            _storageAccount = CloudStorageAccount.Parse(_storageConnectionString);
            _tableClient = _storageAccount.CreateCloudTableClient();
        }

        public AzureProviderImpl(string storageConnectionString)
        {
            _storageConnectionString = storageConnectionString;
            _storageAccount = CloudStorageAccount.Parse(_storageConnectionString);
            _tableClient = _storageAccount.CreateCloudTableClient();
        }

        public IVertexInfoProvider GetVertexInfoProvider()
            => new AzureVertexInfoProvider(CreateTableIfNotExists("cravertextable"));

        public IEndpointInfoProvider GetEndpointInfoProvider()
            => new AzureEndpointInfoProvider(CreateTableIfNotExists("craendpointtable"));

        public IVertexConnectionInfoProvider GetVertexConnectionInfoProvider()
            => new AzureVertexConnectionInfoProvider(CreateTableIfNotExists("craconnectiontable"));

        public IShardedVertexInfoProvider GetShardedVertexInfoProvider()
            => new AzureShardedVertexInfoProvider(CreateTableIfNotExists("crashardedvertextable"));

        private CloudTable CreateTableIfNotExists(string tableName)
        {
            CloudTable table = _tableClient.GetTableReference(tableName);
            try
            {
                table.CreateIfNotExistsAsync().Wait();
            }
            catch { }

            return table;
        }

        public IBlobStorageProvider GetBlobStorageProvider()
            => new AzureBlobProvider(_storageAccount.CreateCloudBlobClient(), "cra");

    }
}
