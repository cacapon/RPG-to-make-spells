using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestMPSystem
{
    MP TestApp;

    [SetUp]
    public void Setup()
    {
        TestApp = new MP(100f);
    }

    [Test]
    public void TestChangeMPMaxCheck()
    {
        // MPは最大値より超えない

        TestApp.ChangeMP(-20);

        TestApp.ChangeMP(30);

        Assert.That(100f, Is.EqualTo(TestApp.CurrentMP));

        TestApp.ChangeMP(10);

        Assert.That(100f, Is.EqualTo(TestApp.CurrentMP));
    }

    [Test]
    public void TestChangeMPSpendCheck()
    {
        // MPは現在のMPより多く消費することが出来ない。

        TestApp.ChangeMP(-50);

        Assert.That(50f, Is.EqualTo(TestApp.CurrentMP));

        TestApp.ChangeMP(-60);

        Assert.That(50f, Is.EqualTo(TestApp.CurrentMP));


        //丁度の場合は消費できる
        TestApp.ChangeMP(-50);

        Assert.That(0f, Is.EqualTo(TestApp.CurrentMP));

    }


}
