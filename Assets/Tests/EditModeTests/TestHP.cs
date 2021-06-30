using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class TestHP
{
    HP TestApp;

    [SetUp]
    public void Setup()
    {
        PlayerData TestPlayerData = AssetDatabase.LoadAssetAtPath<PlayerData>("Assets/Tests/EditModeTests/TestHPData.asset");
        TestApp = new HP(TestPlayerData);
    }

    [Test]
    public void TestChangeHP()
    {
        //FutureHP�̂݉e����^����
        TestApp.ChangeHP(-20);

        Assert.That(80f, Is.EqualTo(TestApp.P.FutureHP));
        Assert.That(100f, Is.EqualTo(TestApp.P.CurrentHP));

        TestApp.ChangeHP(10);

        Assert.That(90f, Is.EqualTo(TestApp.P.FutureHP));
        Assert.That(100f, Is.EqualTo(TestApp.P.CurrentHP));
    }

    [Test]
    public void TestPersistentHP()
    {
        TestApp.PersistentHP(-20);

        // FHP:100 CHP:80 -> 100
        // FutureHP���ς���Ă��Ȃ��ꍇ�A�ω����Ȃ��B
        Assert.That(100f, Is.EqualTo(TestApp.P.CurrentHP));

        TestApp.ChangeHP(-20);
        TestApp.PersistentHP(15);

        // FHP:80 CHP:85
        Assert.That(85f, Is.EqualTo(TestApp.P.CurrentHP));

        // FHP:80 CHP:70 -> 80
        TestApp.PersistentHP(15);
        Assert.That(80f, Is.EqualTo(TestApp.P.CurrentHP)); //FutureHP�ȉ��ɂ͂Ȃ�Ȃ�

        // FHP:90 CHP:85
        TestApp.ChangeHP(10);
        TestApp.PersistentHP(5);
        Assert.That(85f, Is.EqualTo(TestApp.P.CurrentHP));

        // FHP:90 CHP:95 -> 90
        TestApp.PersistentHP(10);
        Assert.That(90f, Is.EqualTo(TestApp.P.CurrentHP)); //�񕜎���FutureHP�ȏ�ɂȂ�Ȃ�

        // FHP:100 CHP:110 -> 100
        TestApp.ChangeHP(10);
        TestApp.PersistentHP(15);
        Assert.That(100f, Is.EqualTo(TestApp.P.CurrentHP)); //�ő�l�𒴂��Ȃ�
    }

    [Test]
    public void TestHPWhenWinning()
    {
        TestApp.ChangeHP(-50);
        TestApp.PersistentHP(25);

        TestApp.HPWhenWinning();

        Assert.That(75f, Is.EqualTo(TestApp.P.CurrentHP));
        Assert.That(75f, Is.EqualTo(TestApp.P.FutureHP));

        TestApp.ChangeHP(15);
        TestApp.HPWhenWinning();

        Assert.That(90f, Is.EqualTo(TestApp.P.CurrentHP));
        Assert.That(90f, Is.EqualTo(TestApp.P.FutureHP));
    }
}
