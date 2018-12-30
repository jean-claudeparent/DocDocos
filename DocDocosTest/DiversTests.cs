using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DocDocos;



namespace MokaDocosTest
{
    [TestClass]
    public  class DiversTests
    {
        [TestMethod]
        public void NomFichierTest()
        {
            DocDocosDA monMoka =
                new DocDocosDA();
            Assert.AreEqual("c__app_test1.html",
                monMoka.NormaliserNomFichier(
                    "c:\\app/test(ceciestenleve)"));
            Assert.AreEqual("c__app_test2",
               monMoka.NormaliserNomFichier("c:\\app/test(ceciestenleve)",""));
        }

    }

}

