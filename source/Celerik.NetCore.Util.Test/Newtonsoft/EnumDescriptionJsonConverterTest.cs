﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Celerik.NetCore.Util.Test
{
    public enum Feeling
    {
        [System.ComponentModel.Description(":(")]
        Sad = 1,

        [System.ComponentModel.Description(":)")]
        Happy = 2
    }

    public class Employee
    {
        [JsonConverter(typeof(EnumDescriptionJsonConverter))]
        public Feeling? Feeling { get; set; }
    }

    [TestClass]
    public class EnumDescriptionJsonConverterTest : UtilBaseTest
    {
        [TestMethod]
        public void SerializeSettedValue()
        {
            var me = new Employee { Feeling = Feeling.Sad };
            var meSerialized = JsonConvert.SerializeObject(me);

            Assert.AreEqual(true, meSerialized.Contains(":(", StringComparison.InvariantCulture));
        }

        [TestMethod]
        public void SerializeUnsettedValue()
        {
            var me = new Employee();
            var meSerialized = JsonConvert.SerializeObject(me);

            Assert.AreEqual(true, meSerialized.Contains("null", StringComparison.InvariantCulture));
        }
    }
}
