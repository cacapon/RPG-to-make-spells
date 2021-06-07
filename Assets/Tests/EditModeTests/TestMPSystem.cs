using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestMPSystem
{
    ManaPoint TestApp;

    [SetUp]
    public void Setup()
    {
        TestApp = new ManaPoint(100f);
    }

    [Test]
    public void TestChangeMPMaxCheck()
    {
        // MPÇÕç≈ëÂílÇÊÇËí¥Ç¶Ç»Ç¢

        TestApp.ChangeMP(-20);

        TestApp.ChangeMP(30);

        Assert.That(100f, Is.EqualTo(TestApp.CurrentMP));

        TestApp.ChangeMP(10);

        Assert.That(100f, Is.EqualTo(TestApp.CurrentMP));
    }

    [Test]
    public void TestChangeMPSpendCheck()
    {
        // MPÇÕåªç›ÇÃMPÇÊÇËëΩÇ≠è¡îÔÇ∑ÇÈÇ±Ç∆Ç™èoóàÇ»Ç¢ÅB


        TestApp.ChangeMP(-50);

        Assert.That(50f, Is.EqualTo(TestApp.CurrentMP));

        TestApp.ChangeMP(-60);

        Assert.That(50f, Is.EqualTo(TestApp.CurrentMP));
    }


}
