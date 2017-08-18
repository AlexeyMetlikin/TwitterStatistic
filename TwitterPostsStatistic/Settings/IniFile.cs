using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TwitterPostsStatistic.Settings
{
    public class IniFile
    {
        private string _filePath;

        [DllImport("kernel32")]
        static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string section, string key, string _default, StringBuilder retVal, int size, string filePath);

        // получаем пусть до файла и его имя.
        public IniFile(string fileName)
        {
            _filePath = new FileInfo(fileName).FullName.ToString();
        }

        // читаем ini-файл и возвращаем значение указного ключа из заданной секции.
        public string ReadINI(string section, string key)
        {
            var retVal = new StringBuilder(255);
            GetPrivateProfileString(section.ToLower(), key.ToLower(), "", retVal, 255, _filePath);
            return retVal.ToString();
        }

        // проверяем, есть ли ключ в секции
        public bool KeyExists(string key, string section = null)
        {
            return ReadINI(section, key).Length > 0;
        }
    }
}
