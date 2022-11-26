using CommandLine_App.Abstraction;
using CommandLine_App.GlobalCommands.HelpCommandChildren;
using CommandLine_App.GlobalCommands.ShowCommandChildren;
using CommandLine_App.HelperService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_AppTests.HelpCommandTests
{
    [TestClass]
    public class HelpParamCommandTests
    {
        private HelpParamCommand _command;

        [TestInitialize]
        public void TestInitialize()
        {
            var dict = new Dictionary<string, Dictionary<string, Command>>()
            {
                { "show", new Dictionary<string, Command>(){ { "memory", new ShowMemoryCommand() } } },
            };

            var helperMock = new Mock<IHelper>();
            var poolMock = new Mock<IPool<Dictionary<string, Command>>>();
            poolMock.Setup(s => s.Pool).Returns(dict);

            _command = new HelpParamCommand(poolMock.Object, helperMock.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _command = null;
        }

        [TestMethod]
        public void Execute_AnyCountParamsExcept2_False()
        {
            string[] param1 = new string[0];
            string[] param2 = new string[1] { " " };
            string[] param3 = new string[3] { "show", "al", "2" };

            var actual1 = _command.Execute(param1);
            var actual2 = _command.Execute(param2);
            var actual3 = _command.Execute(param3);

            Assert.IsFalse(actual1);
            Assert.IsFalse(actual2);
            Assert.IsFalse(actual3);
        }

        [TestMethod]
        public void Execute_BothCorrectParams_True()
        {
            string[] param = new string[2] { "show", "memory" };

            var actual = _command.Execute(param);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Execute_FirstCorrectSecondIncorrect_tParams_False()
        {
            string[] param = new string[2] { "show", "" };

            var actual = _command.Execute(param);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Execute_FirstIncorrectSecondCorrect_tParams_False()
        {
            string[] param = new string[2] { "sh", "memory" };

            var actual = _command.Execute(param);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Execute_BothIncorrectParams_False()
        {
            string[] param = new string[2] { "sh", "mem" };

            var actual = _command.Execute(param);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Execute_BothWhiteSpaceParams_False()
        {
            string[] param = new string[2] { " ", " " };

            var actual = _command.Execute(param);

            Assert.IsFalse(actual);
        }

    }
}
