using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities.IO;
using System.IO;
using Utilities.DataTypes.ExtensionMethods;
using Utilities.IO.ExtensionMethods;
using Utilities.Reflection.ExtensionMethods;
using Utilities.Math.ExtensionMethods;
using Utilities.Serialization;
using Utilities.FileFormats.Zip;
namespace WorkingTimeCaculation
{
    public class FileDbEngine<T> where T : class, new()
    {
        private T db = new T();
        private string fileDbPath;

        public FileDbEngine()
        {
            this.fileDbPath = DirectoryHelper.CombineWithCurrentExeDir("db.xml");
        }

        public FileDbEngine(string fileName, string extension)
        {
            this.fileDbPath = DirectoryHelper.CombineWithCurrentExeDir(fileName + extension);
        }

        public FileDbEngine(string fileDbPath)
        {
            this.fileDbPath = fileDbPath;
        }

        public void SetDB(T db)
        {
            this.db = db;
        }

        public T LoadFileDB()
        {

            FileInfo fileInfo = new FileInfo(this.fileDbPath);

            var bytes = fileInfo.ReadBinary();

            this.db = FormatterMg.XMLDerObjectFromBytes(typeof(T), bytes) as T;

            return db;
        }

        public void Save(bool IsBackup = true)
        {
            string x = FormatterMg.XMLSerObjectToString(this.db);

            FileInfo xf = new FileInfo(this.fileDbPath);
            xf.Save(x, Encoding.UTF8);

            if (IsBackup)
            {
                string basePath = Path.GetDirectoryName(this.fileDbPath);
                string backupPath = Path.Combine(basePath, "db_backup");

                string zipFileName = "db_" + DateTime.Now.ToString("yyyy_MMdd_HHmm") + "_.zip";
                string zipFileFullpath = Path.Combine(backupPath, zipFileName);

                if (!Directory.Exists(backupPath)) Directory.CreateDirectory(backupPath);

                ZipFile zip = new ZipFile(zipFileFullpath, true);
                zip.AddFile(this.fileDbPath);
                zip.Dispose();
            }
        }


    }
}
