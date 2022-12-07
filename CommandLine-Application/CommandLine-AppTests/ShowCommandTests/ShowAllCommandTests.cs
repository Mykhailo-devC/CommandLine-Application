using CommandLine_App.Abstraction;
using CommandLine_App.GlobalCommands.HelpCommandChildren;
using CommandLine_App.GlobalCommands.ShowCommandChildren;
using CommandLine_App.ProcessService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_AppTests.ShowCommandTests
{
    [TestClass]
    public class ShowAllCommandTests
    {
        private ShowAll _command;

        [TestInitialize]
        public void TestInitialize()
        {
            _command = new ShowAll();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _command = null;
        }

        [TestMethod]
        public void Execute_EmptyParam_True()
        {
            string[] param = new string[0];

            var actual = _command.Execute(param);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Execute_WhiteSpaceParam_False()
        {
            string[] param = new string[1] { " " };

            var actual = _command.Execute(param);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Execute_AnyParams_False()
        {
            string[] params1 = new string[1] { "" };
            string[] params2 = new string[2] { "21321", "all" };

            var actual1 = _command.Execute(params1);
            var actual2 = _command.Execute(params2);

            Assert.IsFalse(actual1);
            Assert.IsFalse(actual2);
        }

    }
}
