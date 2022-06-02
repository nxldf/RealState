using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Homes.HomePhotos.Dto
{
    public class CreateOrEditHomePhotoDto : NullableIdDto
    {
        public string ContainerName { get; set; }
        public string OrginalAddress { get; set; }
        public string ThumbnailAddress { get; set; }
        public long HomeId { get; set; }

    }
}
