﻿using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Threading;

namespace BrowserStackMobileAppTests
{
    [TestFixture("parallel", "iphone-7")]
    [TestFixture("parallel", "iphone-7-plus")]
    [TestFixture("parallel", "iphone-8")]
    [TestFixture("parallel", "iphone-8-plus")]
    [TestFixture("parallel", "iphone-se")]
    [TestFixture("parallel", "iphone-xs")]
    [TestFixture("parallel", "ipad-pro")]
    [TestFixture("parallel", "ipad-5th")]
    [Parallelizable(ParallelScope.Fixtures)]
    public class WhenLaunchingTheAppOnApple : BrowserStackIntegration
    {
        public WhenLaunchingTheAppOnApple(string profile, string device) : base(profile, device) { }

        [Test]
        public void TheUserCanAccessTheHomePage()
        {
            var viewElements = androidDriver.FindElements(By.ClassName("XCUIElementTypeApplication"));
            Thread.Sleep(TimeSpan.FromSeconds(3));
            Assert.IsTrue(viewElements.Any());
        }

    }
}