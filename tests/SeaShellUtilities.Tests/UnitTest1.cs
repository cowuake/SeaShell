using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaShellUtilities;

namespace SeaShellUtilities.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestReverseString()
    {
        Assert.AreEqual("hello", Utilities.ReverseString("olleh"));
    }

    [TestMethod]
    public void TestReverseWordsInString()
    {
        Assert.AreEqual("hello world!", Utilities.ReverseWordsInString("olleh !dlrow"));
        //Assert.AreEqual("hello world!", Utilities.ReverseWordsInString("hello world!"));
        //System.Console.WriteLine(Utilities.ReverseWordsInString("!dlrow olleh"));
    }
}