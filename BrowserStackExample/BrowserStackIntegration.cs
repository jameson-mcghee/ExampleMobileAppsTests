﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using BrowserStack;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;

namespace BrowserStackMobileAppTests
{
    public class BrowserStackIntegration
    {
        protected IOSDriver<IOSElement> iosDriver;
        protected AndroidDriver<AndroidElement> androidDriver;
        protected string profile;
        protected string device;
        private Local browserStackLocal;

        public BrowserStackIntegration(string profile, string device)
        {
            this.profile = profile;
            this.device = device;
        }

        [SetUp]
        public void Init()
        {
            var androidCapabilities = GetAppCapabilities("androidCapabilities", "androidEnvironments");
            if (androidCapabilities != null)
                androidDriver = new AndroidDriver<AndroidElement>(new Uri("http://" + ConfigurationManager.AppSettings.Get("server") + "/wd/hub/"), androidCapabilities);

            var iosCapabilities = GetAppCapabilities("iosCapabilities", "iosEnvironments");
            if (iosCapabilities != null)
                iosDriver = new IOSDriver<IOSElement>(new Uri("http://" + ConfigurationManager.AppSettings.Get("server") + "/wd/hub/"), iosCapabilities);

        }

        public DesiredCapabilities GetAppCapabilities(string capabilitiesSectionName, string environmentsSectionName)
        {
            NameValueCollection capabilities = ConfigurationManager.GetSection($"{capabilitiesSectionName}/{profile}") as NameValueCollection;
            NameValueCollection environments = ConfigurationManager.GetSection($"{environmentsSectionName}/{device}") as NameValueCollection;

            if (environments == null) return null;

            DesiredCapabilities capability = new DesiredCapabilities();

            Array.ForEach(capabilities.AllKeys, key => capability.SetCapability(key, capabilities[key]));
            Array.ForEach(environments.AllKeys, key => capability.SetCapability(key, environments[key]));

            var userName = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME") ??
                           ConfigurationManager.AppSettings.Get("user");
            var accessKey = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY") ??
                            ConfigurationManager.AppSettings.Get("key");

            capability.SetCapability("browserstack.user", userName);
            capability.SetCapability("browserstack.key", accessKey);

            var appId = Environment.GetEnvironmentVariable("BROWSERSTACK_APP_ID");
            if (appId != null)
                capability.SetCapability("app", appId);

            if (capability.GetCapability("browserstack.local") != null &&
                capability.GetCapability("browserstack.local").ToString() == "true")
            {
                browserStackLocal = new Local();
                var bsLocalArgs = new List<KeyValuePair<string, string>>
                    {new KeyValuePair<string, string>("key", accessKey)};
                browserStackLocal.start(bsLocalArgs);
            }

            return capability;
        }

        [TearDown]
        public void CleanUp()
        {
            if (androidDriver != null)
                androidDriver.Quit();
            if (iosDriver != null)
                iosDriver.Quit();
            if (browserStackLocal != null)
            {
                browserStackLocal.stop();
            }
        }
    }
}
