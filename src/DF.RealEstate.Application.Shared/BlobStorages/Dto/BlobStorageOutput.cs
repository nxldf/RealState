using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.BlobStorages.Dto
{
    public class BlobStorageOutput
    {
        public bool Succeed { get; set; }
        public string Url { get; set; }
        public string Url_2 { get; set; }
        public string Message { get; set; }
        public string BaseAddress { get; set; }
    }
}
