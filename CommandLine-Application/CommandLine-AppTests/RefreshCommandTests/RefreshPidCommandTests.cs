using CommandLine_App.GlobalCommands.RefreshCommandChildren;
using CommandLine_App.GlobalCommands.ShowCommandChildren;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_AppTests.RefreshCommandTests
{
    [TestClass]
    public class RefreshPidCommandTests
    {
        private RefreshPidCommand _command;

        [TestInitialize]
        public void TestInitialize()
        {
            _command = new RefreshPidCommand();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _command = null;
        }

        [TestMethod]
        public void Execute_AnyCountParamsExcept1_False()
        {
            string[] param1 = new string[0];
            string[] param2 = new string[2] { "some", "param" };

            var actual1 = _command.Execute(param1);
            var actual2 = _command.Execute(param2);

            Assert.IsFalse(actual1);
            Assert.IsFalse(actual2);
        }

        [TestMethod]
        public void Execute_WhiteSpaceParam_False()
        {
            string[] param = new string[1] { " " };

            var actual = _command.Execute(param);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Execute_CorrectParam_True()
        {
            string[] param = new string[1] { "4" };

            var actual = _command.Execute(param);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Execute_IncorrectParam_False()
        {
            string[] param = new string[1] { "five" };

            var actual = _command.Execute(param);

            Assert.IsFalse(actual);
        }

    }
}
