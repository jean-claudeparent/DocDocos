using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocDocosCLI;
using System;

namespace DocDocosCLITest
{
    [TestClass]
    public class CLItest
    {
        [TestMethod]
        public void ArgumentTest()
        {
            DocDocosCLI.Program.UnitTest = true;

            //Argument voide
            try
            {
                DocDocosCLI.Program.Main(new string[0]);
                Assert.Fail(
                    "Une exception aurait dû se produire");
            } catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains(
                    "Usage"),
                    "Erreur :" + ex.ToString ());
                Assert.IsTrue(ex.Message.Contains(
                    "DocFocos -f ficgierdoc.xml -r repertoiresortie [-g gabarit.html]"),
                    "Erreur :" + ex.ToString());
            }


        }
    }
}
