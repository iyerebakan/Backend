using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EF.Settings
{
    public class DatabaseSettings
    {
        public DatabaseType DatabaseType { get; set; }
        public string ConnectionString { get; set; }
    }
}
