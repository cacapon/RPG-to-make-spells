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
        //�����_�Ń��O�\���̂�
    }

    [Test]
    public void TestTurn()
    {
        //0�ȉ��ɂ͂Ȃ�Ȃ��B
        TestApp.Turn(false);
        Assert.That(0, Is.EqualTo(TestApp.NowPage));

        TestApp.Turn(true);
        Assert.That(1, Is.EqualTo(TestApp.NowPage));

        TestApp.Turn(true);
        Assert.That(2, Is.EqualTo(TestApp.NowPage));

        TestApp.Turn(true);
        Assert.That(3, Is.EqualTo(TestApp.NowPage));

        //�ő�l�ȏ�ɂ͂Ȃ�Ȃ��B
        TestApp.Turn(true);
        Assert.That(3, Is.EqualTo(TestApp.NowPage));

        //0�ȉ��ɂ͂Ȃ�Ȃ��B
        TestApp.Turn(false);
        Assert.That(2, Is.EqualTo(TestApp.NowPage));

    }

}
