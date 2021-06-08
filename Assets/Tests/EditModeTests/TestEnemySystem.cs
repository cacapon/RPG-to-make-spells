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

        // ���̊֐����̂́A���������炷�����Ȃ̂�-�ȉ��ɂȂ�̂͑z����̋���
        TestApp.AttackIntervalCounter(0.5f);
        Assert.That(-0.5f, Is.EqualTo(TestApp.AttackInterval));
    }

    [Test]
    public void TestAttack()
    {
        // �����_�͑�2�ʂ܂ŋ��e
        var comparer = new FloatEqualityComparer(0.01f);

        // AttackInterval��0�ȉ��łȂ��ꍇ�͍U�����Ȃ��̂Ő��l��0
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

        // 0�ȉ��ɂȂ����ꍇ�ɁA�U��������B���̍ێ��Ԃ̓��Z�b�g
        TestApp.AttackIntervalCounter(0.3f);
        Assert.That(10, Is.EqualTo(TestApp.Attack()));
        Assert.That(1.0f, Is.EqualTo(TestApp.AttackInterval).Using(comparer));

        // ���Ԃ҂�����̏ꍇ���U������
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

        // ���̊֐����̂́A���������炷�����Ȃ̂�-�ȉ��ɂȂ�̂͑z����̋���
        TestApp.Damage(40);
        Assert.That(-20, Is.EqualTo(TestApp.CurrentHP));
    }

    [Test]
    public void TestIsDead()
    {
        Assert.That(false, Is.EqualTo(TestApp.IsDead()));

        TestApp.Damage(50);
        Assert.That(false, Is.EqualTo(TestApp.IsDead()));

        //HP0�Ŏ��S���肪true�ɂȂ�B
        TestApp.Damage(50);
        Assert.That(true, Is.EqualTo(TestApp.IsDead()));

        //����ȉ������l�B
        TestApp.Damage(1);
        Assert.That(true, Is.EqualTo(TestApp.IsDead()));

    }
}
