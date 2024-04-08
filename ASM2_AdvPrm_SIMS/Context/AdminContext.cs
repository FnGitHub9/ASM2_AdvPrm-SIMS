using ASM2_AdvPrm_SIMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ASM2_AdvPrm_SIMS.Context
{
    public class AdminContext
    {
        private int nextAdminId = 1;
        public List<Admin> Admins { get; set; }
        private readonly string filePath;

        public AdminContext(string filePath)
        {
            this.filePath = filePath;
            Admins = ReadDataFromCsvAndUpdateId(filePath);
        }

        public void AddAdmin(Admin admin)
        {
            // Assuming Username is unique, check for existing username before adding
            if (!Admins.Any(a => a.Username == admin.Username))
            {
                admin.Id = nextAdminId++;
                Admins.Add(admin);
                WriteDataToCsv(filePath);
            }
            else
            {
                Console.WriteLine($"Admin with username {admin.Username} already exists.");
            }
        }

        public void UpdateAdmin(int adminID, Admin updateAdmin)
        {
            Admin existingAdmin = Admins.FirstOrDefault(a => a.Id == adminID);

            if (existingAdmin != null)
            {
                // Update Username only if provided, otherwise keep existing username
                existingAdmin.Username = updateAdmin.Username ?? existingAdmin.Username;
                existingAdmin.FirstName = updateAdmin.FirstName;
                existingAdmin.LastName = updateAdmin.LastName;

                WriteDataToCsv(filePath);
            }
        }

        private List<Admin> ReadDataFromCsvAndUpdateId(string filePath)
        {
            List<Admin> admins = new List<Admin>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // Skip the header line
                    reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] parts = line.Split(',');

                        if (parts.Length == 4) // Assuming there are 4 fields: Id, Username, FirstName, LastName
                        {
                            int Id = int.Parse(parts[0]);
                            string Username = parts[1];
                            string FirstName = parts[2];
                            string LastName = parts[3];

                            admins.Add(new Admin
                            {
                                Id = Id,
                                Username = Username,
                                FirstName = FirstName,
                                LastName = LastName
                            });

                            // Update nextAdminId if necessary
                            if (Id >= nextAdminId)
                            {
                                nextAdminId = Id + 1;
                            }
                        }
                    }
                }
                Console.WriteLine("Data read from CSV file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading from CSV file: {ex.Message}");
            }

            return admins;
        }

        public void DeleteAdmin(int adminID)
        {
            Admin adminToRemove = Admins.FirstOrDefault(a => a.Id == adminID);
            if (adminToRemove != null)
            {
                Admins.Remove(adminToRemove);
                WriteDataToCsv(filePath);
            }
            else
            {
                Console.WriteLine($"Admin with ID {adminID} not found.");
            }
        }

        private void WriteDataToCsv(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write header line
                    writer.WriteLine("Id,Username,FirstName,LastName");

                    // Write data for each admin
                    foreach (Admin admin in Admins)
                    {
                        writer.WriteLine($"{admin.Id},{admin.Username},{admin.FirstName},{admin.LastName}");
                    }
                }
                Console.WriteLine("Data written to CSV file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to CSV file: {ex.Message}");
            }
        }
    }
}