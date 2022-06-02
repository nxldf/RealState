using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Homes.HomePhotos.Dto
{
    public class HomePhotoDto :NullableIdDto
    {
        public string ContainerName { get; set; }
        public string _OrginalAddress { get; set; }
        public string _ThumbnailAddress { get; set; }
        public long HomeId { get; set; }

        public string OrginalAddress
        {
            get { return string.IsNullOrEmpty(_OrginalAddress) ? "" : AppConsts.AzureImagePath + _OrginalAddress; }
            set { _OrginalAddress = value; }
        }

        public string ThumbnailAddress
        {
            get { return string.IsNullOrEmpty(_ThumbnailAddress) ? "" : AppConsts.AzureImagePath + _ThumbnailAddress; }
            set { _ThumbnailAddress = value; }
        }
    }
}
