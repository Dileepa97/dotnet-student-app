using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Data.SqlClient;

namespace student_mgt_app.Data
{
    public class MigrationRunner
    {
        private readonly string connectionString;
        private readonly string migrationFolderPath;

        public MigrationRunner(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("StudentAppDbConnectionString"); 
            this.migrationFolderPath = configuration["MigrationSettings:MigrationScriptsRoot"];
        }

        public void RunMigrations()
        {
            if (Directory.Exists(migrationFolderPath))
            {
                foreach (string scriptFile in Directory.GetFiles(migrationFolderPath, "*.sql"))
                {
                    string script = File.ReadAllText(scriptFile);
                    ExecuteScript(script);
                }
            }
            else
            {
                Console.WriteLine("Migration folder not found.");
            }
        }

        private void ExecuteScript(string script)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(script, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
