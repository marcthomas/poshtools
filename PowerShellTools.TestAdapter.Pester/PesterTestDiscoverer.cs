﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Language;
using System.Xml;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

namespace PowerShellTools.TestAdapter.Pester
{
  
    [DefaultExecutorUri(PesterTestExecutor.ExecutorUriString)]
    [FileExtension(".ps1")]
    public class PesterTestDiscoverer : ITestDiscoverer
    {
        public void DiscoverTests(IEnumerable<string> sources, IDiscoveryContext discoveryContext,
            IMessageLogger logger, ITestCaseDiscoverySink discoverySink)
        {
            GetTests(sources, discoverySink);
        }

        public static List<TestCase> GetTests(IEnumerable<string> sources, ITestCaseDiscoverySink discoverySink)
        {
            List<TestCase> tests = new List<TestCase>();
            foreach (string source in sources)
            {
                Token[] tokens;
                ParseError[] errors;
                var ast = Parser.ParseFile(source, out tokens, out errors);

                var testAsts = ast.FindAll(m => (m is CommandAst) && (m as CommandAst).GetCommandName() == "Describe", true);

                foreach (CommandAst contextAst in testAsts)
                {
                    var nameAst = (StringConstantExpressionAst)contextAst.CommandElements[1];
                    var contextName = nameAst.Value;

                    var testcase = new TestCase(contextName, PesterTestExecutor.ExecutorUri, source)
                    {
                        CodeFilePath = source,
                    };

                    testcase.LineNumber = contextAst.Extent.StartLineNumber;


                    if (discoverySink != null)
                    {
                        discoverySink.SendTestCase(testcase);
                    }

                    tests.Add(testcase);
                }
            }
            return tests;
        }
    }
}