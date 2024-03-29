﻿using BrowserStackExample;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Threading.Tasks;


namespace BrowserStackIntegration
{
    public class DayPartingScreen : GetScreenandPageConfigs
    {
        public DayPartingScreen(string profile, string device) : base(profile, device){}

        //ANDROID
        public async Task AndroidUserCanAccessTheDayPartingScreen()
        {
            for (int i = 0; ; i++)
            {
                if (i >= 15) Assert.Fail("Intro Banner is not being displayed.");
                    try
                    {
                        if (IsAndroidElementPresent("ad|-4|non-module|advertisementModule|0|manually placed in splash-screen|"))
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = $"Splashscreen is not being displayed. {ex}";
                        Debug.WriteLine(message);
                        Console.WriteLine(message);
                    }
                await Wait(1);
            }
        }
        public async Task AndroidDayPartingBannerIsGenerated()
        {
            await AndroidUserCanAccessTheDayPartingScreen();
            for (int i = 0; ; i++)
            {
                if (i >= 15) Assert.Fail("Day Parting Banner and/or 'Sponsored by' messages are not being displayed.");
                try
                {
                    var dayPartingBannerElement = androidDriver.FindElementByAccessibilityId("non-module|-4|ad|advertisementModule|0|manually placed in splash-screen|");
                    var sponsoredByElement = androidDriver.FindElementByAccessibilityId("non-module|-4|ad|advertisementModule|0|manually placed in splash-screen|");
                    break;
                }
                catch (Exception ex)
                {
                        string message = $"Day Parting Banner and/or 'Sponsored by' messages are not being displayed. {ex}";
                        Debug.WriteLine(message);
                        Console.WriteLine(message);
                }
                await Wait(1);
            }
        }
        public async Task AndroidDayPartingScreenAdIsPresent()
        {
            //await AndroidUserCanAccessTheDayPartingScreen();
            for (int i = 0; ; i++)
            {
                if (i >= 25) Assert.Fail("The Day Parting Screen Ad is not present.");
                try
                {
                    var dayPartingAdElement = androidDriver.FindElementByAccessibilityId("non-module|-4|ad|advertisementModule|0|manually placed in splash-screen|");
                    break;
                }
                catch (Exception ex)
                {
                    string message = $"The Day Parting Screen Ad is not present. {ex}";
                    Debug.WriteLine(message);
                    Console.WriteLine(message);
                }
                await Wait(1);
            }
        }



        //IOS
        public async Task IOSUserCanAccessTheDayPartingScreen()
        {
            for (int i = 0; ; i++)
            {
                await ApproveiOSAlerts();
                if (i >= 15) Assert.Fail("Intro Banner is not being displayed.");
                try
                {
                    var dayPartingScreenElement = iosDriver.FindElementByName("Good Morning Sponsored By non-module|-4|ad|advertisementModule|0|manually placed in splash-screen|");
                    break;
                }
                catch (Exception ex)
                {
                    string message = $"Splashscreen is not being displayed. {ex}";
                    Debug.WriteLine(message);
                    Console.WriteLine(message);
                }
                await Wait(1);
            }
        }
        public async Task IOSDayPartingBannerIsGenerated()
        {
            //await IOSUserCanAccessTheDayPartingScreen();
            for (int i = 0; ; i++)
            {
                await ApproveiOSAlerts();
                if (i >= 15) Assert.Fail("Day Parting Banner and/or 'Sponsored by' messages are not being displayed.");
                try
                {
                    var dayPartingBannerElement = iosDriver.FindElementByName("Good Morning Sponsored By non-module|-4|ad|advertisementModule|0|manually placed in splash-screen|");
                    var sponsoredByElement = iosDriver.FindElementByName("Sponsored By non-module|-4|ad|advertisementModule|0|manually placed in splash-screen|");
                    break;
                }
                catch (Exception ex)
                {
                    string message = $"Day Parting Banner and/or 'Sponsored by' messages are not being displayed. {ex}";
                    Debug.WriteLine(message);
                    Console.WriteLine(message);
                }
                await Wait(1);
            }
        }
    }
}
