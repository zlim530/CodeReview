using FluentData;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopshelfDemo
{
    public static class FluentDataDB
    {
        public static IDbContext Context()
        {
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
            var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection connection = factory.CreateConnection();

            var connectStr = ConfigurationManager.ConnectionStrings["ReturnMailInfo"].ConnectionString;
            return new DbContext().ConnectionString(connectStr, new SqlAzureProvider());
        }
    }
}
