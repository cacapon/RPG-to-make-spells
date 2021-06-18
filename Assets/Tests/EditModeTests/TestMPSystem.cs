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
        // MP�͍ő�l��蒴���Ȃ�

        TestApp.ChangeMP(-20);

        TestApp.ChangeMP(30);

        Assert.That(100f, Is.EqualTo(TestApp.CurrentMP));

        TestApp.ChangeMP(10);

        Assert.That(100f, Is.EqualTo(TestApp.CurrentMP));
    }

    [Test]
    public void TestChangeMPSpendCheck()
    {
        // MP�͌��݂�MP��葽������邱�Ƃ��o���Ȃ��B

        TestApp.ChangeMP(-50);

        Assert.That(50f, Is.EqualTo(TestApp.CurrentMP));

        TestApp.ChangeMP(-60);

        Assert.That(50f, Is.EqualTo(TestApp.CurrentMP));


        //���x�̏ꍇ�͏���ł���
        TestApp.ChangeMP(-50);

        Assert.That(0f, Is.EqualTo(TestApp.CurrentMP));

    }


}
