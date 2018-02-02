using NRails.Configuration;

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
