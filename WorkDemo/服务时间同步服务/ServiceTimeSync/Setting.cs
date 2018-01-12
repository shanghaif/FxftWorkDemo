using NRails.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTimeSync
{
    public class Setting : ParameterSetting
    {
        public string Name = "";
        public string ServiceAssembly;

        public string mongodb;
        public string database;
        public string mainTable;
        public string MySqlCon;

        public int Hours;
        public int Minutes;

    }
}
