﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class TestUtilityTest : BaseTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NullFunction()
        {
            TestUtility.AssertThrows<ArgumentException>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void ExceptionNotTriggered()
        {
            TestUtility.AssertThrows<ArgumentException>(() => { 
            });
        }
    }
}