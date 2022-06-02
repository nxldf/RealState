using Abp.Authorization;
using DF.RealEstate.Authorization;
using DF.RealEstate.BlobStorages.Dto;
using LazZiya.ImageResize;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.RealEstate.BlobStorages
{
    public class BlobStorageAppService : RealEstateAppServiceBase
    {

        private bool ThumbnailCallback()
        {
            return false;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration)]
        public async Task<BlobStorageOutput> UploadCommonImages(IFormFile file, int MaxWith = 1200)
        {
            Guid g = Guid.NewGuid();
            Guid t = Guid.NewGuid();
            var extension = Path.GetExtension(file.FileName);
            var fileName = g.ToString() + extension;
            var thumbnailName = t.ToString() + extension;

            using (var stream = file.OpenReadStream())
            {
                using (Image img = Image.FromStream(stream))
                {
                    using (Image resizedImg = img.Width < MaxWith ? img : img.ScaleByWidth(MaxWith))
                    {
                        using (var ms = new MemoryStream())
                        {
                            resizedImg.Save(ms, ImageFormat.Jpeg);
                            await UploadAsync(fileName, "images", ms.ToArray(), "image/jpeg");
                        }
                    }
                }
            }
            using (var stream = file.OpenReadStream())
            {
                using (Image img = Image.FromStream(stream))
                {
                    Image.GetThumbnailImageAbort myCallback =new Image.GetThumbnailImageAbort(ThumbnailCallback);
                    using (Image myThumbnail = img.Width < 40 ? img : img.GetThumbnailImage(40, 40, myCallback, IntPtr.Zero))
                    {
                        using (var ms = new MemoryStream())
                        {
                            myThumbnail.Save(ms, ImageFormat.Jpeg);
                            await UploadAsync(thumbnailName, "thumbnailImages", ms.ToArray(), "image/jpeg");
                        }
                    }
                }
            }
            return new BlobStorageOutput() { Succeed = true, Url = fileName,Url_2 = thumbnailName, BaseAddress = AppConsts.AzureImagePath };
        }

        [AbpAuthorize(AppPermissions.Pages_Administration)]
        public void DeleteFile(string path)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AppConsts.AzureBlobConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();

            // Make sure container is there
            var blobContainer = blobClient.GetContainerReference("images");

            var blob = blobContainer.GetBlockBlobReference(path);
            blob.DeleteIfExistsAsync();
        }

        private async Task UploadAsync(string fileName, string Container, byte[] byteArray, string ContentType)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AppConsts.AzureBlobConnectionString);
                var blobClient = storageAccount.CreateCloudBlobClient();

                // Make sure container is there
                var blobContainer = blobClient.GetContainerReference(Container);
                await blobContainer.CreateIfNotExistsAsync();

                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(fileName);
                blockBlob.Properties.ContentType = ContentType;
                await blockBlob.UploadFromByteArrayAsync(byteArray, 0, byteArray.Length);
            }
            catch (Exception ex)
            {

            }
        }

        /*        private async Task UploadAsync(string fileName, string Container, byte[] byteArray, string ContentType)
        {
            try
            {
                Uri storageAccount =new Uri(AppConsts.AzureImagePath);
                //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AppConsts.AzureBlobConnectionString);
                //var blobClient = storageAccount.CreateCloudBlobClient();

                var storage = new StorageCredentials("gifthopeblob", "1TRcGEBiylP/3cM/4fIkRBm874Jrfnn/nLMPQ2NlzrP5QL55DNh2ClVGtjhdwBfYwUDdtiulV1+KTPWMTsOVbw==");

                CloudBlobContainer blobContainer = new CloudBlobContainer(storageAccount, storage);
                
                // Make sure container is there
                //var blobContainer = blobClient.GetContainerReference(Container);
                await blobContainer.CreateIfNotExistsAsync();

                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(fileName);
                blockBlob.Properties.ContentType = ContentType;
                await blockBlob.UploadFromByteArrayAsync(byteArray, 0, byteArray.Length);
            }
            catch (Exception ex)
            {

            }
        }
         */
    }
}
