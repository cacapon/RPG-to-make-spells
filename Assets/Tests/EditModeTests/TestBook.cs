using NUnit.Framework;

public class TestBook
{
    Book TestApp;

    [SetUp]
    public void Setup()
    {
        TestApp = new Book(maxPage:3, firstPage:0);
    }

    [Test]
    public void TestUseMagic()
    {
        //TODO
        //現時点でログ表示のみ
    }

    [Test]
    public void TestTurn()
    {
        //0以下にはならない。
        TestApp.Turn(false);
        Assert.That(0, Is.EqualTo(TestApp.NowPage));

        TestApp.Turn(true);
        Assert.That(1, Is.EqualTo(TestApp.NowPage));

        TestApp.Turn(true);
        Assert.That(2, Is.EqualTo(TestApp.NowPage));

        TestApp.Turn(true);
        Assert.That(3, Is.EqualTo(TestApp.NowPage));

        //最大値以上にはならない。
        TestApp.Turn(true);
        Assert.That(3, Is.EqualTo(TestApp.NowPage));

        //0以下にはならない。
        TestApp.Turn(false);
        Assert.That(2, Is.EqualTo(TestApp.NowPage));

    }

}
