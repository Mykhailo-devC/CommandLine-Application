using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandLine_App.InputValidatorService;
using System;
using System.Collections.Generic;
using System.Text;
using CommandLine_App.Utilities.Implementations;
using System.Linq;
using CommandLine_App.Pools;

namespace CommandLine_App.InputValidatorService.Tests
{
    [TestClass()]
    public class InputParserTests
    {
        private InputParser _inputParser;
        [TestInitialize]
        public void TestInitialize()
        {
            _inputParser = new InputParser();
        }

        [TestMethod()]
        public void TryParseUserInputTest_NoInput_False()
        {
            var userInput = new List<string>();
            var expectResult = new InputParseResult();
            var expectValue = false;

            var actualValue = _inputParser.TryParseUserInput(userInput, out InputParseResult actualResult);

            Assert.AreEqual(expectValue, actualValue);
            Assert.AreEqual(expectResult, actualResult);
        }

        [TestMethod()]
        public void TryParseUserInputTest_WhiteSpaceInput_False()
        {
            var userInput = "     ".Split(' ').ToList();
            var expectResult = new InputParseResult();
            var expectValue = false;

            var actualValue = _inputParser.TryParseUserInput(userInput, out InputParseResult actualResult);

            Assert.AreEqual(expectValue, actualValue);
            Assert.AreEqual(expectResult, actualResult);
        }

        [TestMethod()]
        public void TryParseUserInputTest_Show_False()
        {
            var userInput = "show".Split(' ').ToList();

            var expectResult = new InputParseResult();
            expectResult.command = CommandType.Show;

            var expectValue = false;

            var actualValue = _inputParser.TryParseUserInput(userInput, out InputParseResult actualResult);

            Assert.AreEqual(expectValue, actualValue);
            Assert.AreEqual(expectResult, actualResult);
        }

        [TestMethod()]
        public void TryParseUserInputTest_Kill_All_True()
        {
            var userInput = "kill all".Split(' ').ToList();

            var expectResult = new InputParseResult();
            expectResult.command = CommandType.Kill;
            expectResult.parameter = ParameterType.All;
            expectResult.arguments = new string[0];

            var expectValue = true;

            var actualValue = _inputParser.TryParseUserInput(userInput, out InputParseResult actualResult);

            Assert.AreEqual(expectValue, actualValue);
            Assert.AreEqual(expectResult, actualResult);
        }

        [TestMethod()]
        public void TryParseUserInputTest_Start_Name_True()
        {
            var userInput = "start name firefox".Split(' ').ToList();

            var expectResult = new InputParseResult();
            expectResult.command = CommandType.Start;
            expectResult.parameter = ParameterType.Name;
            expectResult.arguments = new string[1] {"firefox"};

            var expectValue = true;

            var actualValue = _inputParser.TryParseUserInput(userInput, out InputParseResult actualResult);

            Assert.AreEqual(expectValue, actualValue);
            Assert.AreEqual(expectResult, actualResult);
        }

        [TestMethod()]
        public void TryParseUserInputTest_Refresh_Memory_1_True()
        {
            var userInput = "refresh memory 1000".Split(' ').ToList();

            var expectResult = new InputParseResult();
            expectResult.command = CommandType.Refresh;
            expectResult.parameter = ParameterType.Memory;
            expectResult.arguments = new string[1] { "1000" };

            var expectValue = true;

            var actualValue = _inputParser.TryParseUserInput(userInput, out InputParseResult actualResult);

            Assert.AreEqual(expectValue, actualValue);
            Assert.AreEqual(expectResult, actualResult);
        }

        [TestMethod()]
        public void TryParseUserInputTest_Refresh_Memory_2_True()
        {
            var userInput = "show memory 1000 3000".Split(' ').ToList();

            var expectResult = new InputParseResult();
            expectResult.command = CommandType.Show;
            expectResult.parameter = ParameterType.Memory;
            expectResult.arguments = new string[2] { "1000", "3000" };

            var expectValue = true;

            var actualValue = _inputParser.TryParseUserInput(userInput, out InputParseResult actualResult);

            Assert.AreEqual(expectValue, actualValue);
            Assert.AreEqual(expectResult, actualResult);
        }

        [TestMethod()]
        public void TryParseUserInputTest_Kill_Pid_True()
        {
            var userInput = "kill pid 4".Split(' ').ToList();

            var expectResult = new InputParseResult();
            expectResult.command = CommandType.Kill;
            expectResult.parameter = ParameterType.Pid;
            expectResult.arguments = new string[1] { "4" };

            var expectValue = true;

            var actualValue = _inputParser.TryParseUserInput(userInput, out InputParseResult actualResult);

            Assert.AreEqual(expectValue, actualValue);
            Assert.AreEqual(expectResult, actualResult);
        }

        [TestMethod()]
        public void TryParseUserInputTest_Kill_Pid_NoParam_False()
        {
            var userInput = "kill pid".Split(' ').ToList();

            var expectResult = new InputParseResult();
            expectResult.command = CommandType.Kill;
            expectResult.parameter = ParameterType.Pid;
            expectResult.arguments = new string[0];

            var expectValue = false;

            var actualValue = _inputParser.TryParseUserInput(userInput, out InputParseResult actualResult);

            Assert.AreEqual(expectValue, actualValue);
            Assert.AreEqual(expectResult, actualResult);
        }

        [TestMethod()]
        public void TryParseUserInputTest_Kill_Pid_WhiteSpaceParam_False()
        {
            var userInput = "kill pid ".Split(' ').ToList();

            var expectResult = new InputParseResult();
            expectResult.command = CommandType.Kill;
            expectResult.parameter = ParameterType.Pid;
            expectResult.arguments = new string[1] {""};

            var expectValue = false;

            var actualValue = _inputParser.TryParseUserInput(userInput, out InputParseResult actualResult);

            Assert.AreEqual(expectValue, actualValue);
            Assert.AreEqual(expectResult, actualResult);
        }

        [TestMethod()]
        public void TryParseUserInputTest_Kill_Pi_False()
        {
            var userInput = "kill pi 4".Split(' ').ToList();

            var expectResult = new InputParseResult();
            expectResult.command = CommandType.Kill;

            var expectValue = false;

            var actualValue = _inputParser.TryParseUserInput(userInput, out InputParseResult actualResult);

            Assert.AreEqual(expectValue, actualValue);
            Assert.AreEqual(expectResult, actualResult);
        }

        [TestMethod()]
        public void TryParseUserInputTest_Sho_all_False()
        {
            var userInput = "sho all".Split(' ').ToList();

            var expectResult = new InputParseResult();

            var expectValue = false;

            var actualValue = _inputParser.TryParseUserInput(userInput, out InputParseResult actualResult);

            Assert.AreEqual(expectValue, actualValue);
            Assert.AreEqual(expectResult, actualResult);
        }

        [TestMethod()]
        public void TryParseUserInputTest_Help_True()
        {
            var userInput = "help".Split(' ').ToList();

            var expectResult = new InputParseResult();
            expectResult.IsHelp = true;

            var expectValue = true;

            var actualValue = _inputParser.TryParseUserInput(userInput, out InputParseResult actualResult);

            Assert.AreEqual(expectValue, actualValue);
            Assert.AreEqual(expectResult, actualResult);
        }

        [TestMethod]
        public void TryParseUserInputTest_Help_Sho_False()
        {
            var userInput = "help sho".Split(' ').ToList();

            var expectResult = new InputParseResult();
            expectResult.IsHelp = true;

            var expectValue = false;

            var actualValue = _inputParser.TryParseUserInput(userInput, out InputParseResult actualResult);

            Assert.AreEqual(expectValue, actualValue);
            Assert.AreEqual(expectResult, actualResult);
        }

        [TestMethod]
        public void TryParseUserInputTest_Help_Show_True()
        {
            var userInput = "help show".Split(' ').ToList();

            var expectResult = new InputParseResult();
            expectResult.command = CommandType.Show;
            expectResult.IsHelp = true;

            var expectValue = true;

            var actualValue = _inputParser.TryParseUserInput(userInput, out InputParseResult actualResult);

            Assert.AreEqual(expectValue, actualValue);
            Assert.AreEqual(expectResult, actualResult);
        }

        [TestMethod]
        public void TryParseUserInputTest_Help_Show_Al_False()
        {
            var userInput = "help show al".Split(' ').ToList();

            var expectResult = new InputParseResult();
            expectResult.command = CommandType.Show;
            expectResult.IsHelp = true;

            var expectValue = false;

            var actualValue = _inputParser.TryParseUserInput(userInput, out InputParseResult actualResult);

            Assert.AreEqual(expectValue, actualValue);
            Assert.AreEqual(expectResult, actualResult);
        }

        [TestMethod]
        public void TryParseUserInputTest_Help_Show_All_True()
        {
            var userInput = "help show all".Split(' ').ToList();

            var expectResult = new InputParseResult();
            expectResult.command = CommandType.Show;
            expectResult.parameter = ParameterType.All;
            expectResult.IsHelp = true;

            var expectValue = true;

            var actualValue = _inputParser.TryParseUserInput(userInput, out InputParseResult actualResult);

            Assert.AreEqual(expectValue, actualValue);
            Assert.AreEqual(expectResult, actualResult);
        }
    }
}