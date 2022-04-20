using System.Collections.Generic;
using Abp.Localization;
using DF.RealEstate.Install.Dto;

namespace DF.RealEstate.Web.Models.Install
{
    public class InstallViewModel
    {
        public List<ApplicationLanguage> Languages { get; set; }

        public AppSettingsJsonDto AppSettingsJson { get; set; }
    }
}
