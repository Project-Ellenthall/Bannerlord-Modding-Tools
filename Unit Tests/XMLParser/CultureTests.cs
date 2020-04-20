using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.XMLParser
{
    [TestClass]
    public class CultureTests
    {
        [TestMethod]
        public void TestLoad()
        {
            string path = @"C:\Program Files (x86)\Steamtwo\steamapps\common\Mount & Blade II Bannerlord\Modules\SandBoxCore\ModuleData";
            string name = "spcultures.xml";
            TaleWorldsXMLParser.Culture.CultureParser parser = new TaleWorldsXMLParser.Culture.CultureParser(path, name);
            Assert.AreEqual(16, parser.GetAll().Count);
        }
        [TestMethod]
        public void TestBackup()
        {
            string path = @"C:\Program Files (x86)\Steamtwo\steamapps\common\Mount & Blade II Bannerlord\Modules\SandBoxCore\ModuleData";
            string name = "spcultures.xml";
            TaleWorldsXMLParser.Culture.CultureParser parser = new TaleWorldsXMLParser.Culture.CultureParser(path, name);
            parser.Save();
            Assert.IsTrue(System.IO.File.Exists(parser.BackupPath));
        }
        [TestMethod]
        public void TestRestoreBackup()
        {
            string path = @"C:\Program Files (x86)\Steamtwo\steamapps\common\Mount & Blade II Bannerlord\Modules\SandBoxCore\ModuleData";
            string name = "spcultures.xml";
            TaleWorldsXMLParser.Culture.CultureParser parser = new TaleWorldsXMLParser.Culture.CultureParser(path, name);
            parser.RestoreBackup();
        }
    }
}
