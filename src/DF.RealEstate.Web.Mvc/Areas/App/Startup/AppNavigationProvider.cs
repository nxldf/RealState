﻿using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using DF.RealEstate.Authorization;

namespace DF.RealEstate.Web.Areas.App.Startup
{
    public class AppNavigationProvider : NavigationProvider
    {
        public const string MenuName = "App";

        public override void SetNavigation(INavigationProviderContext context)
        {
            var menu = context.Manager.Menus[MenuName] = new MenuDefinition(MenuName, new FixedLocalizableString("Main Menu"));

            var address = new MenuItemDefinition(                            
                        AppPageNames.Common.Administration,
                        L("Locations"),
                        url: "App/Addresses",
                        icon: "flaticon-pin",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Addresses)                     
                  );

            var amenities = new MenuItemDefinition(
                    AppPageNames.Common.Administration,
                    L("Amenities"),
                    url: "App/Amenities",
                    icon: "flaticon2-indent-dots",
                    permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Amenities)

                    );
            var homes = new MenuItemDefinition(
                        AppPageNames.Common.Administration,
                        L("Homes"),
                        url: "App/Homes",
                        icon: "flaticon-home-1",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Homes)
                    
                 );
            var advertisment = new MenuItemDefinition(
                        AppPageNames.Common.Administration,
                        L("Advertisements"),
                        url: "App/Advertisements",
                        icon: "flaticon2-speaker",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Homes)

                  );

            menu
                .AddItem(new MenuItemDefinition(
                        AppPageNames.Host.Dashboard,
                        L("Dashboard"),
                        url: "App/HostDashboard",
                        icon: "flaticon-line-graph",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Host_Dashboard)
                    )
                ).AddItem(new MenuItemDefinition(
                    AppPageNames.Host.Tenants,
                    L("Tenants"),
                    url: "App/Tenants",
                    icon: "flaticon-list-3",
                    permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Tenants)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Host.Editions,
                        L("Editions"),
                        url: "App/Editions",
                        icon: "flaticon-app",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Editions)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Tenant.Dashboard,
                        L("Dashboard"),
                        url: "App/TenantDashboard",
                        icon: "flaticon-line-graph",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Tenant_Dashboard)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.Administration,
                        L("Administration"),
                        icon: "flaticon-interface-8"
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.OrganizationUnits,
                            L("OrganizationUnits"),
                            url: "App/OrganizationUnits",
                            icon: "flaticon-map",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_OrganizationUnits)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.Roles,
                            L("Roles"),
                            url: "App/Roles",
                            icon: "flaticon-suitcase",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Roles)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.Users,
                            L("Users"),
                            url: "App/Users",
                            icon: "flaticon-users",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Users)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.Languages,
                            L("Languages"),
                            url: "App/Languages",
                            icon: "flaticon-tabs",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Languages)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.AuditLogs,
                            L("AuditLogs"),
                            url: "App/AuditLogs",
                            icon: "flaticon-folder-1",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_AuditLogs)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Host.Maintenance,
                            L("Maintenance"),
                            url: "App/Maintenance",
                            icon: "flaticon-lock",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Host_Maintenance)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Tenant.SubscriptionManagement,
                            L("Subscription"),
                            url: "App/SubscriptionManagement",
                            icon: "flaticon-refresh",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.UiCustomization,
                            L("VisualSettings"),
                            url: "App/UiCustomization",
                            icon: "flaticon-medical",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_UiCustomization)
                        )
                    .AddItem(new MenuItemDefinition(
                            AppPageNames.Host.Settings,
                            L("Settings"),
                            url: "App/HostSettings",
                            icon: "flaticon-settings",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Host_Settings)
                        )
                    )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Tenant.Settings,
                            L("Settings"),
                            url: "App/Settings",
                            icon: "flaticon-settings",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Tenant_Settings)
                        )
                    )
                )
                .AddItem(homes)
                .AddItem(address)
                .AddItem(amenities)                
                .AddItem(advertisment);



        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, RealEstateConsts.LocalizationSourceName);
        }
    }
}
