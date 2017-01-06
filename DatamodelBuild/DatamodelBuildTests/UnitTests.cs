using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileSystemLibrary;
using System.Collections.Generic;

namespace DatamodelBuildTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestFileSystemHelper()
        {
            FileSystemHelper fsh = new FileSystemHelper();

            List<String> fns = fsh.getFileNames("c:\\tmp\\", "Sheet.", "xlsx");
            Assert.IsTrue(fns.Contains("c:\\tmp\\Sheet.Alle importer.xlsx"));
        }
    }
}
