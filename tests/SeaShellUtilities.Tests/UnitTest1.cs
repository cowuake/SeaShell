using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaShellUtilities;

namespace SeaShellUtilities.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestIsAllXxxer()
    {
        Assert.IsTrue("HELLO!!!".IsAllUpper());
        Assert.IsFalse("HellO".IsAllUpper());
        Assert.IsFalse("".IsAllUpper());
        Assert.IsFalse("   ".IsAllUpper());
        Assert.IsTrue("hello!!!".IsAllLower());
        Assert.IsTrue("helo!@*&%!".IsAllLower());
        Assert.IsFalse("heLlo!@*&%!".IsAllLower());
        Assert.IsFalse("".IsAllLower());
        Assert.IsFalse("   ".IsAllLower());
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