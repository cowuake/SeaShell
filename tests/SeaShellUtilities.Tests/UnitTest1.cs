using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaShellUtilities;

namespace SeaShellUtilities.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestIsAllXxxerCase()
    {
        Assert.IsTrue("HELLO!!!".IsAllUpperCase());
        Assert.IsFalse("HellO".IsAllUpperCase());
        Assert.IsTrue("hello!!!".IsAllLowerCase());
        Assert.IsTrue("helo!@*&%!".IsAllLowerCase());
        Assert.IsFalse("heLlo!@*&%!".IsAllLowerCase());
    }

    [TestMethod]
    public void TestReverseString()
    {
        Assert.AreEqual("hello", Utilities.ReverseString("olleh"));
    }

    [TestMethod]
    public void TestReverseWordsInString()
    {
        Assert.AreEqual("hello world!", Utilities.ReverseWordsInString("olleh !dlrow"));
    }
}