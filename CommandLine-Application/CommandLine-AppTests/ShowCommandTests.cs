using CommandLine_App.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_AppTests
{
    [TestClass]
    public class ShowCommandTests
    {
        private ShowCommand _cmd;
        private List<string> _args;

        [TestInitialize]
        public void TestInitialize()
        {
            _args = new List<string>();
            _cmd = new ShowCommand();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            GC.Collect(0);
        }

        [TestMethod]
        public void Execute_EmptyParam_False()
        {
            bool expected = false;

            var actual = _cmd.Execute(_args);

            Assert.AreEqual(expected, actual);
        }
    }
}
