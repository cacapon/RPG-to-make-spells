using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestTouchEvent
{
    Touch TestApp;

    [SetUp]
    public void Setup()
    {
        TestApp = new Touch(flickCriteriaDuration :  0.3f, 
                            flickCriteriaDistance : 20.0f);
    }


    //Tapèåè
    //  1.distanceX < FlickCriteriaDistance
    //  2.distanceY < FlickCriteriaDistance
    //  3.duration  < flickCriteriaDuration

    [TestCase( 0f,  0f, 0.1f,  true)]   //1:ok 2:ok 3:ok
    [TestCase(25f,  0f, 0.1f, false)]   //1:ng 2:ok 3:ok
    [TestCase( 0f, 25f, 0.1f, false)]   //1:ok 2:ng 3:ok
    [TestCase( 0f,  0f, 0.5f, false)]   //1:ok 2:ok 3:ng
    [Test]
    public void TestIsTap(float x_end, float y_end, float duration, bool condition)
    {
        TestApp.position.x.begin = 0f;
        TestApp.position.y.begin = 0f;
        TestApp.position.x.end = x_end;
        TestApp.position.y.end = y_end;

        TestApp.durationTimer.Start();
        TestApp.durationTimer.Countup(duration);
        TestApp.durationTimer.Stop();

        Assert.That(condition, Is.EqualTo(TestApp.IsTap()));

        TestApp.durationTimer.Reset();
    }

    //Flickèåè
    //  1.distanceY   >  distanceX
    //  2.end - bigin >= flickCriteriaDistance: 20.0f
    //  3.duration    <  flickCriteriaDuration:  0.3f

    [TestCase(25f,  0f, 0.1f,  true)]   //1:ok 2:ok 3:ok
    [TestCase(25f, 30f, 0.1f, false)]   //1:ng 2:ok 3:ok
    [TestCase(15f,  0f, 0.1f, false)]   //1:ok 2:ng 3:ok
    [TestCase(25f,  0f, 0.4f, false)]   //1:ok 2:ok 3:ng
    [Test]
    public void TestIsFlick(float x_end, float y_end, float duration, bool condition)
    {
        TestApp.position.x.begin = 0f;
        TestApp.position.y.begin = 0f;
        TestApp.position.x.end   = x_end;
        TestApp.position.y.end   = y_end;

        TestApp.durationTimer.Start();
        TestApp.durationTimer.Countup(duration);
        TestApp.durationTimer.Stop();


        Assert.That(condition, Is.EqualTo(TestApp.IsFlick()));

        TestApp.durationTimer.Reset();
    }
}
