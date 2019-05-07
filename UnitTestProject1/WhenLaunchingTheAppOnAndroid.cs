﻿using NUnit.Framework;
using System.Threading.Tasks;
using BrowserStackIntegration;
using System.Linq;
using System.Diagnostics;
using System;
using System.Threading;

namespace MobileAppTests
{
    //[TestFixture("parallel", "pixel")]
    //[TestFixture("parallel", "pixel-2")]
    //[TestFixture("parallel", "pixel-3")]
    //[TestFixture("parallel", "galaxy-s7")]
    //[TestFixture("parallel", "galaxy-s8")]
    [TestFixture("parallel", "galaxy-s9")]
    //[TestFixture("parallel", "galaxy-note8")]
    //[TestFixture("parallel", "galaxy-note9")]
    //[TestFixture("parallel", "galaxy-note4")]
    ////[TestFixture("parallel", "galaxy-s6")] //App or one of the otherApps cannot be run on version 5.0.
    ////[TestFixture("parallel", "nexus-9")] //Tablet
    ////[TestFixture("parallel", "galaxy-tabs4")] //Tablet
    [Parallelizable(ParallelScope.Fixtures)]
    public class WhenLaunchingTheAppOnAndroid : DayPartingScreen
    {
        public WhenLaunchingTheAppOnAndroid(string profile, string device) : base(profile, device) { }

        //[Test]
        public async Task TheUserCanAccessTheDayPartingScreen()
        {
            await AndroidUserCanAccessTheDayPartingScreen();
            Assert.IsTrue(androidDriver.FindElementByAccessibilityId
                ("non-module|-4|ad|advertisementModule|0|manually placed in splash-screen|").Displayed);

        }

        //[Test]
        public async Task TheDayPartingBannerIsGenerated()
        {
            await AndroidDayPartingBannerIsGenerated();

            string sponsoredByElement = androidDriver.FindElementByAccessibilityId("non-module|-4|ad|advertisementModule|0|manually placed in splash-screen|").Text;
            string dayPartingBannerElement = androidDriver.FindElementByAccessibilityId("non-module|-4|ad|advertisementModule|0|manually placed in splash-screen|").Text;

            Assert.IsTrue(sponsoredByElement.Any());
            Assert.IsTrue(dayPartingBannerElement.Any());
            Assert.IsTrue(dayPartingBannerElement.Contains("Good "), dayPartingBannerElement + "Day Parting Banner does not contain 'Good ***'.");
            Assert.IsTrue(sponsoredByElement.Contains("Sponsored By"), sponsoredByElement + "'Sponsored by' message does not contain 'Sponsored By'.");
        }

        [Test]
        public async Task TheDayPartingScreenAdIsPresent()
        {
            for (int i = 0; ; i++)
            {
                if (i >= 40) Assert.Fail("The Day Parting Screen Ad is not present.");
                try
                {
                    if (IsAndroidElementPresent("non-module|-4|ad|advertisementModule|0|manually placed in splash-screen|"))
                        break;
                }
                catch (Exception ex)
                {
                    string message = $"The Day Parting Screen Ad is not present. {ex}";
                    Debug.WriteLine(message);
                    //Debug.ReadLine();
                    Console.WriteLine(message);
                }
                Thread.Sleep(TimeSpan.FromMilliseconds(250));
            }
        }

        //[Test]
        public async Task TheGoogleAnalyticsCallsArePresent()
        {
            await TheDayPartingScreenAdIsPresent();
            await GetNetworkLogs();

        }
    }
}
