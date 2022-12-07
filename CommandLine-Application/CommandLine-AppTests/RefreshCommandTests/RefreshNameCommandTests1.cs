using CommandLine_App.GlobalCommands.RefreshCommandChildren;
using CommandLine_App.GlobalCommands.ShowCommandChildren;
using CommandLine_App.ProcessService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_AppTests.RefreshCommandTests
{
    [TestClass]
    public class RefreshNameCommandTests
    {
        private RefreshName _command;

        [TestInitialize]
        public void TestInitialize()
        { 

            _command = new RefreshName();
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
        public void Execute_WhiteSpaceParam_True()
        {
            string[] param = new string[1] { "" };

            var actual = _command.Execute(param);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Execute_CorrectParam_True()
        {
            string[] param = new string[1] { "firefox" };

            var actual = _command.Execute(param);

            Assert.IsTrue(actual);
        }

    }
}
