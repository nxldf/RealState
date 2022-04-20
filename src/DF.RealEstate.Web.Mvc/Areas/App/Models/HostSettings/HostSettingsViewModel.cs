using System.Collections.Generic;
using Abp.Application.Services.Dto;
using DF.RealEstate.Configuration.Host.Dto;
using DF.RealEstate.Editions.Dto;

namespace DF.RealEstate.Web.Areas.App.Models.HostSettings
{
    public class HostSettingsViewModel
    {
        public HostSettingsEditDto Settings { get; set; }

        public List<SubscribableEditionComboboxItemDto> EditionItems { get; set; }

        public List<ComboboxItemDto> TimezoneItems { get; set; }

        public List<string> EnabledSocialLoginSettings { get; set; } = new List<string>();
    }
}