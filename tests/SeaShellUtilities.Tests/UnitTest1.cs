using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaShellUtilities;
using System;
using System.Linq;

namespace SeaShellUtilities.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestArrayRotate()
    {
        var original = new int[] { 1, 2, 3, 4, 5 };
        var rotated = new int[] { 3, 4, 5, 1, 2 };

        CollectionAssert.AreEqual(rotated, original.Rotate(2, "left"));
        CollectionAssert.AreEqual(rotated, original.Rotate(3, "right"));
    }

    [TestMethod]
    public void TestArrayRotateInPlace()
    {
        var original = new int[] { 1, 2, 3, 4, 5 };
        var rotated = new int[] { 3, 4, 5, 1, 2 };
        var final = new int[] { 2, 3, 4, 5, 1 };

        original.RotateInPlace(2, "left");
        CollectionAssert.AreEqual(original, rotated);
        original.RotateInPlace(5, "right");
        CollectionAssert.AreEqual(original, rotated);
        original.RotateInPlace(4, "left");
        CollectionAssert.AreEqual(original, final);
    }

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