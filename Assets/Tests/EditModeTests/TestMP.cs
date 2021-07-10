using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class TestMP
{
    MP TestApp;

    [SetUp]
    public void Setup()
    {
        PlayerData TestPlayerData = AssetDatabase.LoadAssetAtPath<PlayerData>("Assets/Tests/EditModeTests/TestMPData.asset");
        TestApp = new MP(TestPlayerData);
    }

    [Test]
    public void TestChangeMPMaxCheck()
    {
        TestApp.ChangeMP(-20);

        TestApp.ChangeMP(30);

        Assert.That(100f, Is.EqualTo(TestApp.P.CurrentMP));

        TestApp.ChangeMP(10);

        Assert.That(100f, Is.EqualTo(TestApp.P.CurrentMP));
    }

    [Test]
    public void TestChangeMPSpendCheck()
    {
        TestApp.ChangeMP(-50);

        Assert.That(50f, Is.EqualTo(TestApp.P.CurrentMP));

        TestApp.ChangeMP(-60);

        Assert.That(50f, Is.EqualTo(TestApp.P.CurrentMP));

        TestApp.ChangeMP(-50);

        Assert.That(0f, Is.EqualTo(TestApp.P.CurrentMP));

    }
}
