using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestHPSystem
{
    HitPoint TestApp;

    [SetUp]
    public void Setup()
    {
        TestApp = new HitPoint(100f);
    }

    [Test]
    public void TestChangeHP()
    {
        //FutureHPのみ影響を与える
        TestApp.ChangeHP(-20);

        Assert.That(80f,  Is.EqualTo(TestApp.FutureHP));
        Assert.That(100f, Is.EqualTo(TestApp.CurrentHP));

        TestApp.ChangeHP(10);

        Assert.That(90f, Is.EqualTo(TestApp.FutureHP));
        Assert.That(100f, Is.EqualTo(TestApp.CurrentHP));
    }

    [Test]
    public void TestPersistentHP()
    {
        TestApp.PersistentHP(-20);

        // FHP:100 CHP:80 -> 100
        // FutureHPが変わっていない場合、変化しない。
        Assert.That(100f, Is.EqualTo(TestApp.CurrentHP));

        TestApp.ChangeHP(-20);
        TestApp.PersistentHP(15);

        // FHP:80 CHP:85
        Assert.That(85f, Is.EqualTo(TestApp.CurrentHP));

        // FHP:80 CHP:70 -> 80
        TestApp.PersistentHP(15);
        Assert.That(80f, Is.EqualTo(TestApp.CurrentHP)); //FutureHP以下にはならない

        // FHP:90 CHP:85
        TestApp.ChangeHP(10);
        TestApp.PersistentHP(5);
        Assert.That(85f, Is.EqualTo(TestApp.CurrentHP));

        // FHP:90 CHP:95 -> 90
        TestApp.PersistentHP(10);
        Assert.That(90f, Is.EqualTo(TestApp.CurrentHP)); //回復時はFutureHP以上にならない

        // FHP:100 CHP:110 -> 100
        TestApp.ChangeHP(10);
        TestApp.PersistentHP(15);
        Assert.That(100f, Is.EqualTo(TestApp.CurrentHP)); //最大値を超えない
    }

    [Test]
    public void TestHPWhenWinning()
    {
        TestApp.ChangeHP(-50);
        TestApp.PersistentHP(25);

        TestApp.HPWhenWinning();

        Assert.That(75f, Is.EqualTo(TestApp.CurrentHP));
        Assert.That(75f, Is.EqualTo(TestApp.FutureHP));

        TestApp.ChangeHP(15);
        TestApp.HPWhenWinning();

        Assert.That(90f, Is.EqualTo(TestApp.CurrentHP));
        Assert.That(90f, Is.EqualTo(TestApp.FutureHP));
    }
}
