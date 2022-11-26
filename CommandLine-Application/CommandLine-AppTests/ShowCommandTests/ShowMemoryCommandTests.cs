using CommandLine_App.GlobalCommands.ShowCommandChildren;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLine_AppTests.ShowCommandTests
{
    [TestClass]
    public class ShowMemoryCommandTests
    {
        private ShowMemoryCommand _command;

        [TestInitialize]
        public void TestInitialize()
        {
            _command = new ShowMemoryCommand();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _command = null;
        }

        [TestMethod]
        public void Execute_AnyCountParamsExcept1and2_False()
        {
            string[] param1 = new string[0];
            string[] param2 = new string[3] { "some", "param", "421" };

            var actual1 = _command.Execute(param1);
            var actual2 = _command.Execute(param2);

            Assert.IsFalse(actual1);
            Assert.IsFalse(actual2);
        }

        [TestMethod]
        public void Execute_CorrectParam_True()
        {
            string[] param = new string[1] { "1000" };

            var actual = _command.Execute(param);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Execute_IncorrectParam_False()
        {
            string[] param = new string[1] { "thousand" };

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
        public void Execute_BothCorrectParams_True()
        {
            string[] param = new string[2] { "1000", "2000" };

            var actual = _command.Execute(param);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Execute_FirstCorrectSecondIncorrect_tParams_False()
        {
            string[] param = new string[2] { "500", "two thousands" };

            var actual = _command.Execute(param);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Execute_FirstIncorrectSecondCorrect_tParams_False()
        {
            string[] param = new string[2] { "hundred", "200" };

            var actual = _command.Execute(param);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Execute_BothIncorrectParams_False()
        {
            string[] param = new string[2] { "test", "param" };

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
