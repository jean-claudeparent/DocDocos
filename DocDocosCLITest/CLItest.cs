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

            //Argument vide
            try
            {
                DocDocosCLI.Program.Main(new string[0]);
                Assert.Fail(
                    "Une exception aurait dû se produire");
            } catch (Exception ex)
            {
                Assert.IsTrue(Program.MessageUT.Contains(
                    "Usage"),
                    "Erreur :" + Program.MessageUT);
                Assert.IsTrue(Program.MessageUT.Contains(
                    "DocFocos -f ficgierdoc.xml -r repertoiresortie [-g gabarit.html]"),
                    "Erreur :" + Program.MessageUT);
                Assert.AreEqual(99, Program.CodeRetourUT); 
            }

            //Argument^pas de -f
            try
            {
                string[] args = new string[4];
                args[0] = "-g";
                args[1] = "gabarit";
                args[2] = "-r";
                args[3] = "répertoire";
                
                DocDocosCLI.Program.Main(args);
                Assert.Fail(
                    "Une exception aurait dû se produire");
            }
            catch 
            {
                Assert.IsTrue(Program.MessageUT.Contains(
                    "Le fichier xml n'a pas été spécifié"),
                    "Erreur cas pas de -f :" + Program.MessageUT);
                Assert.AreEqual(99,
                    Program.CodeRetourUT);                     
            }



        }
    }
}
