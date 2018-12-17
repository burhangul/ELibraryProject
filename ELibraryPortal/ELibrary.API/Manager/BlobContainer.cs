﻿using AutoMapper.Configuration;
using ELibrary.API.Configuration;
using ELibrary.API.Models.Abstract;
using ELibrary.Entities.Concrete;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELibrary.API.Manager
{
    public class BlobContainer<T> : IBlobContainer<T> where T : AppFile
    {
        public async Task<CloudBlobContainer> CreateFolderAsync(T tmodel)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.Instance.GetConnectionString("StorageConnection"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference($"fileuploads");
            await container.CreateIfNotExistsAsync();

            return container;
        }

        public async Task<CloudBlockBlob> UploadFileAsync(CloudBlobContainer container, T tmodel)
        {
            CloudBlockBlob blob = container.GetBlockBlobReference($"files{tmodel.ModuleType}/{tmodel.ModuleId}/{tmodel.UniqueName.ToLower()}");

            using (var fileStream = System.IO.File.OpenRead(tmodel.FilePath))
            {
                await blob.UploadFromStreamAsync(fileStream);

                return blob;
            }
        }
        public string SignUrl(string blobName, string blobPath, DateTime? startTime, DateTime? expiryTime)
        {
            if (!startTime.HasValue || !expiryTime.HasValue)
                return "Start time or expiry time is missing";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.Instance.GetConnectionString("StorageConnection"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(blobPath);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName.ToLower());

            SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
            sasConstraints.SharedAccessStartTime = startTime.Value.AddMinutes(-5);
            sasConstraints.SharedAccessExpiryTime = expiryTime.Value;
            sasConstraints.Permissions = SharedAccessBlobPermissions.Read;

            //Generate shared access URL 
            string sasContainerToken = blockBlob.GetSharedAccessSignature(sasConstraints);

            return blockBlob.Uri + sasContainerToken;
        }
    }
}