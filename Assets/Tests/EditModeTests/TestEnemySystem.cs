using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

public class TestEnemySystem
{
    Enemy TestApp;

    [SetUp]
    public void Setup()
    {
        int MaxHP = 100;
        int AtkPoint = 10;
        float AtkInterbal = 1.0f;
        TestApp = new Enemy(MaxHP,AtkPoint,AtkInterbal);
    }

    [Test]
    public void TestNextAttackIntervalCounter()
    {
        
        TestApp.AttackIntervalCounter(0.5f);
        Assert.That(0.5f, Is.EqualTo(TestApp.AttackInterval));

        TestApp.AttackIntervalCounter(0.5f);
        Assert.That(0.0f, Is.EqualTo(TestApp.AttackInterval));

        // この関数自体は、数字を減らすだけなので-以下になるのは想定内の挙動
        TestApp.AttackIntervalCounter(0.5f);
        Assert.That(-0.5f, Is.EqualTo(TestApp.AttackInterval));
    }

    [Test]
    public void TestAttack()
    {
        // 小数点は第2位まで許容
        var comparer = new FloatEqualityComparer(0.01f);

        // AttackIntervalが0以下でない場合は攻撃しないので数値は0
        Assert.That(0, Is.EqualTo(TestApp.Attack()));

        TestApp.AttackIntervalCounter(0.3f);
        Assert.That(0, Is.EqualTo(TestApp.Attack()));
        Assert.That(0.7f, Is.EqualTo(TestApp.AttackInterval).Using(comparer));

        TestApp.AttackIntervalCounter(0.3f);
        Assert.That(0, Is.EqualTo(TestApp.Attack()));
        Assert.That(0.4f, Is.EqualTo(TestApp.AttackInterval).Using(comparer));

        TestApp.AttackIntervalCounter(0.3f);
        Assert.That(0, Is.EqualTo(TestApp.Attack()));
        Assert.That(0.1f, Is.EqualTo(TestApp.AttackInterval).Using(comparer));

        // 0以下になった場合に、攻撃をする。その際時間はリセット
        TestApp.AttackIntervalCounter(0.3f);
        Assert.That(10, Is.EqualTo(TestApp.Attack()));
        Assert.That(1.0f, Is.EqualTo(TestApp.AttackInterval).Using(comparer));

        // 時間ぴったりの場合も攻撃する
        TestApp.AttackIntervalCounter(1.0f);
        Assert.That(10, Is.EqualTo(TestApp.Attack()));
        Assert.That(1.0f, Is.EqualTo(TestApp.AttackInterval).Using(comparer));
    }

    [Test]
    public void TestDamage()
    {
        TestApp.Damage(40);
        Assert.That(60, Is.EqualTo(TestApp.CurrentHP));

        TestApp.Damage(40);
        Assert.That(20, Is.EqualTo(TestApp.CurrentHP));

        // この関数自体は、数字を減らすだけなので-以下になるのは想定内の挙動
        TestApp.Damage(40);
        Assert.That(-20, Is.EqualTo(TestApp.CurrentHP));
    }

    [Test]
    public void TestIsDead()
    {
        Assert.That(false, Is.EqualTo(TestApp.IsDead()));

        TestApp.Damage(50);
        Assert.That(false, Is.EqualTo(TestApp.IsDead()));

        //HP0で死亡判定がtrueになる。
        TestApp.Damage(50);
        Assert.That(true, Is.EqualTo(TestApp.IsDead()));

        //それ以下も同様。
        TestApp.Damage(1);
        Assert.That(true, Is.EqualTo(TestApp.IsDead()));

    }
}
