using Event_Management_App.DataManager.DAL;
using Event_Management_App.DataManager.IDAL;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Event_Management_App.Extension
{
    public static class DataMaanger
    {
        public static IServiceCollection AddAppSetting(this IServiceCollection services)
        {
            services.AddScoped<IDBManager>(AddDBManager);
            //services.AddScoped<IEmployeeBAL, EmployeeBAL>;
            //services.AddScoped<IEventBAL, EventBAL>();

            return services;
        }
        internal static IDBManager AddDBManager(IServiceProvider serviceProvider)
        {
            IConfiguration Configuration = serviceProvider.GetRequiredService<IConfiguration>();

            string dbconstr = Configuration.GetConnectionString("DefaultConnection");
            return GetDBManager(dbconstr);


        }
        public static IDBManager GetDBManager(string connectionString)
        {
            DbConnection dbconn = new MySqlConnection(connectionString);
            return new DBManager(dbconn);
        }
    }
}
