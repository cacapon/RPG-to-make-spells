using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestTimer
{
    Timer TestApp;

    [SetUp]
    public void Setup()
    {
        TestApp = new Timer();
    }

    [Test]
    public void TestStart()
    {
        TestApp.Start();
        Assert.That(true, Is.EqualTo(TestApp.IsTimerRun));
    }
    [Test]
    public void TestStop()
    {
        TestApp.Start();
        Assert.That(true, Is.EqualTo(TestApp.IsTimerRun));
        TestApp.Stop();
        Assert.That(false, Is.EqualTo(TestApp.IsTimerRun));
    }
    [Test]
    public void TestCountup()
    {
        //�X�C�b�`�������Ă��Ȃ��ƃJ�E���g�A�b�v����Ȃ��B
        TestApp.Countup(10f);
        Assert.That(0f, Is.EqualTo(TestApp.Timercount));

        TestApp.Start();
        TestApp.Countup(10f);
        Assert.That(10f, Is.EqualTo(TestApp.Timercount));

        //�X�C�b�`OFF�ɂ��Ă��J�E���g�A�b�v�͎c�����܂܁B
        TestApp.Stop();
        TestApp.Countup(10f);
        Assert.That(10f, Is.EqualTo(TestApp.Timercount));


    }

    [Test]
    public void TestReset()
    {
        TestApp.Start();
        TestApp.Countup(10f);
        Assert.That(10f, Is.EqualTo(TestApp.Timercount));

        TestApp.Reset();
        Assert.That(0f, Is.EqualTo(TestApp.Timercount));

    }
}
