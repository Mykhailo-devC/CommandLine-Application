using CommandLine_App.Abstraction;
using CommandLine_App.GlobalCommands.HelpCommandChildren;
using CommandLine_App.GlobalCommands.ShowCommandChildren;
using CommandLine_App.Pools;
using CommandLine_App.ProcessService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_AppTests.HelpCommandTests
{
    [TestClass]
    public class HelpCmdCommandTests
    {
        private HelpCmdCommand _command;

        [TestInitialize]
        public void TestInitialize()
        {
          
            var dict = new Dictionary<string, Dictionary<string, Command>>()
            {
                { "show", new Dictionary<string, Command>(){ { "all", new ShowAllCommand(new ProcessWrapper()) } } },
            };

            var mock = new Mock<IPool<Dictionary<string, Command>>>();
            mock.Setup(s => s.Pool).Returns(dict);
            _command = new HelpCmdCommand(mock.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _command = null;
        }

        [TestMethod]
        public void Execute_EmptyParam_False()
        {
            string[] param = new string[0];

            var actual = _command.Execute(param);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Execute_OneCorrectParam_True()
        {
            string[] param = new string[1] {"show"};

            var actual = _command.Execute(param);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Execute_OneInorrectParam_False()
        {
            string[] param = new string[1] { "k" };

            var actual = _command.Execute(param);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Execute_WhiteSpaceParam_False()
        {
            string[] param = new string[1] { " " };

            var actual = _command.Execute(param);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Execute_MoreThenOneParams_False()
        {
            string[] params1 = new string[2] { "show", " "};
            string[] params2 = new string[3] { "ki", "all" , "12" };

            var actual1 = _command.Execute(params1);
            var actual2 = _command.Execute(params2);

            Assert.IsFalse(actual1);
            Assert.IsFalse(actual2);
        }

    }
}
