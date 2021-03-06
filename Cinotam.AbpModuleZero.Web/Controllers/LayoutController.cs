﻿using Abp.Application.Navigation;
using Abp.Configuration.Startup;
using Abp.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.Threading;
using Cinotam.AbpModuleZero.Web.Models.Layout;
using Cinotam.ModuleZero.AppModule.Languages;
using Cinotam.ModuleZero.AppModule.Sessions;
using System.Web.Mvc;

namespace Cinotam.AbpModuleZero.Web.Controllers
{
    public class LayoutController : AbpModuleZeroControllerBase
    {
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly ISessionAppService _sessionAppService;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly ILanguageManager _languageManager;
        public LayoutController(
            IUserNavigationManager userNavigationManager,
            ILocalizationManager localizationManager,
            ISessionAppService sessionAppService,
            IMultiTenancyConfig multiTenancyConfig,
            ILanguageManager languageManager,
            ILanguageAppService languageAppService)
        {
            _userNavigationManager = userNavigationManager;
            _sessionAppService = sessionAppService;
            _multiTenancyConfig = multiTenancyConfig;
            _languageManager = languageManager;
        }

        [ChildActionOnly]
        public PartialViewResult TopMenu(string activeMenu = "")
        {
            var model = new TopMenuViewModel
            {
                MainMenu = AsyncHelper.RunSync(() => _userNavigationManager.GetMenuAsync("MainMenu", AbpSession.ToUserIdentifier())),
                ActiveMenuItemName = activeMenu
            };

            return PartialView("_TopMenu", model);
        }

        [ChildActionOnly]
        //Edited just for the sample
        public PartialViewResult LanguageSelection(string partialView = "")
        {
            var model = new LanguageSelectionViewModel
            {
                CurrentLanguage = _languageManager.CurrentLanguage,
                Languages = _languageManager.GetLanguages()
            };
            if (partialView.IsNullOrEmpty())
            {

                return PartialView("_LanguageSelection", model);
            }

            return PartialView(partialView, model);

        }


        [ChildActionOnly]
        public PartialViewResult UserMenuOrLoginLink()
        {
            UserMenuOrLoginLinkViewModel model;

            if (AbpSession.UserId.HasValue)
            {
                model = new UserMenuOrLoginLinkViewModel
                {
                    LoginInformations = AsyncHelper.RunSync(() => _sessionAppService.GetCurrentLoginInformations()),
                    IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
                };
            }
            else
            {
                model = new UserMenuOrLoginLinkViewModel
                {
                    IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled
                };
            }

            return PartialView("_UserMenuOrLoginLink", model);
        }

    }
}