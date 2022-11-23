using CommandLine_App.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CommandLine_AppTests
{
    [TestClass]
    public class HelpCommandTests
    {
        private HelpCommand _cmd;
        private List<string> _args;

        [TestInitialize]
        public void TestInitialize()
        {
            _args = new List<string>();
            _cmd = new HelpCommand();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            GC.Collect(0);
        }

        [TestMethod]
        public void Execute_EmptyParam_True()
        {
            bool expected = true;

            var actual = _cmd.Execute(_args);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Execute_WrongCommand_False()
        {
            bool expected = false;
            _args.Add("sho");

            var actual = _cmd.Execute(_args);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Execute_CorrectCommand_True()
        {
            bool expected = true;
            _args.Add("kill");

            var actual = _cmd.Execute(_args);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Execute_CorrectCommandWrongParam_False()
        {
            bool expected = false;
            _args.Add("start");
            _args.Add("a");

            var actual = _cmd.Execute(_args);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Execute_CorrectCommandCorrectParam_True()
        {
            bool expected = true;
            _args.Add("refresh");
            _args.Add("-n");

            var actual = _cmd.Execute(_args);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Execute_WrongCommandWrongParam_Fasle()
        {
            bool expected = false;
            _args.Add("refre");
            _args.Add("a");

            var actual = _cmd.Execute(_args);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Execute_MoreThen2Params_Fasle()
        {
            bool expected = false;
            _args.Add("show");
            _args.Add("pid");
            _args.Add("123");

            var actual = _cmd.Execute(_args);

            Assert.AreEqual(expected, actual);
        }

    }
}
