﻿using NUnit.Framework;
using BrowserStackIntegration;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace MobileAppTests
{
    //[TestFixture("parallel", "iphone-8")]
    //[TestFixture("parallel", "iphone-8-plus")]
    //[TestFixture("parallel", "iphone-se")]
    [TestFixture("parallel", "iphone-xs")]
    //[TestFixture("parallel", "ipad-pro")]
    //[TestFixture("parallel", "ipad-5th")]
    [Parallelizable(ParallelScope.Fixtures)]
    public class WhenOnTheIOSWatchPage : WatchPage
    {
        public WhenOnTheIOSWatchPage(string profile, string device) : base(profile, device) { }

        [Test]
        public async Task TheWatchPageIsPresent()
        {
            await IOSWeatherPageIsPresent();

            SwipeRightToLeftOnIOS();
            if (IsiOSElementPresent("page||watch-page-wrapper||||"))
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail("The Watch Page is not present.");
            }
        }

        [Test]
        public async Task TheWatchPageBannerAdIsPresent()
        {
            await IOSWatchPageIsPresent();
            for (int i = 0; ; i++)
            {
                if (i >= 10) Assert.Fail("The Watch Page banner ad is not present.");
                try
                {
                    if (IsiOSElementPresent("ad|-3|non-module|advertisementModule|0|manually placed in page-wrapper|"))
                        break;
                }
                catch (Exception ex)
                {
                    string message = $"The Watch Page banner ad is not present. {ex}";
                    Debug.WriteLine(message);
                    //Debug.ReadLine();
                    Console.WriteLine(message);
                }
                Wait(1);
            }
        }

        [Test]
        public async Task ConfirmAdModulesArePresentAndCount()
        {
            await IOSWatchPageIsPresent();

            int adModuleCount = 0;

            for (int i = 0; ; i++)
            {
                if (i >= 120) Assert.Fail("Could not find the last element on the Watch Page prior to time out.");
                try
                {
                    ScrollDownOnIOS();

                    if (IsiOSElementPresent("module|advertisement"))
                    {
                        adModuleCount++;
                    }
                    if (IsiOSElementPresent("module|last"))
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    string message = $"Could not find the last element on the Watch Page. {ex}";
                    Debug.WriteLine(message);
                    //Debug.ReadLine();
                    Console.WriteLine(message);
                }
                Wait(1);
            }

            Console.Write("Number of ad modules on the Watch Page: " + adModuleCount);
        }
    }
}
