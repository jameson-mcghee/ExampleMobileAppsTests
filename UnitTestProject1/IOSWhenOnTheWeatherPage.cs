﻿using NUnit.Framework;
using BrowserStackIntegration;
using System.Threading.Tasks;
using System;
using System.Diagnostics;

namespace MobileAppTests
{
    [TestFixture("parallel", "iphone-7")]
    [TestFixture("parallel", "iphone-8")]
    [TestFixture("parallel", "iphone-8-plus")]
    [TestFixture("parallel", "iphone-se")]
    [TestFixture("parallel", "iphone-xs")]
    [TestFixture("parallel", "iphone-xsmax")]
    [TestFixture("parallel", "ipad-pro")] //Tablet
    [TestFixture("parallel", "ipad-5th")] //Tablet
    [Parallelizable(ParallelScope.Fixtures)]
    public class IOSWhenOnTheWeatherPage : WeatherPage
    {
        public IOSWhenOnTheWeatherPage(string profile, string device) : base(profile, device) { }

        //[Test]
        public async Task TheWeatherPageIsPresent()
        {
            await IOSHomePageIsPresent();

            for (int i = 0; ; i++)
            {
                await SwipeRightToLeftOnIOS();

                if (i >= 5) Assert.Fail("The Weather Page is not present.");
                try
                {
                    if (IsiOSElementPresent("page||weather-wrapper||||"))
                        break;
                }
                catch (Exception ex)
                {
                    string message = $"The Weather Page is not present. {ex}";
                    Debug.WriteLine(message);

                    Console.WriteLine(message);
                }
               await Wait(1);
            }

        }

        //[Test]
        public async Task TheWeatherPageBannerAdIsPresent()
        {
            await IOSWeatherPageIsPresent();
            for (int i = 0; ; i++)
            {
                if (i >= 10) Assert.Fail("The Weather Page banner ad is not present.");
                try
                {
                    if (IsiOSElementPresent("ad|-3|non-module|advertisementModule|0|manually placed in page-wrapper|"))
                        break;
                }
                catch (Exception ex)
                {
                    string message = $"The Weather Page banner ad is not present. {ex}";
                    Debug.WriteLine(message);

                    Console.WriteLine(message);
                }
               await Wait(1);
            }
        }

        //[Test]
        public async Task ConfirmAdModulesArePresentAndCount()
        {
            await IOSWeatherPageIsPresent();

            int adModuleCount = 0;

            for (int i = 0; ; i++)
            {
                if (i >= 90) Assert.Fail("Could not find the last element on the Weather Page prior to time out.");
                try
                {
                    await ScrollDownOnIOS();

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
                    string message = $"Could not find the last element on the Weather Page. {ex}";
                    Debug.WriteLine(message);

                    Console.WriteLine(message);
                }
               await Wait(1);
            }

            Console.Write("Number of ad modules on the Weather Page: " + adModuleCount);
        }

        //TODO: Weather Page: Compare the screenConfig modules from the IOSWeatherPageScreenConfigRequest() method to those on the Weather Page

    }
}
