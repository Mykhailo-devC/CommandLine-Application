using CommandLine_App.Abstraction;
using CommandLine_App.Commands;
using CommandLine_App.GlobalCommands.HelpCommandChildren;
using CommandLine_App.Pools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_AppTests.HelpCommandTests
{
    [TestClass]
    public class HelpGlobalCommandTests
    {
        private HelpGlobalCommand _command;

        [TestInitialize]
        public void TestInitialize()
        {
            var mock = new Mock<IPool<Dictionary<string, Command>>>();
            mock.Setup(s => s.Pool).Returns(new Dictionary<string, Dictionary<string, Command>>());

            _command = new HelpGlobalCommand(mock.Object);
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
            string[] param = new string[1] {" "};

            var actual = _command.Execute(param);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Execute_AnyParams_False()
        {
            string[] params1 = new string[1] { "show" };
            string[] params2 = new string[2] { "ki", "all" };

            var actual1 = _command.Execute(params1);
            var actual2 = _command.Execute(params2);

            Assert.IsFalse(actual1);
            Assert.IsFalse(actual2);
        }

    }
}
