using System;
using System.Security.Permissions;
using Microsoft.Win32;

namespace SimpleServicePermissionsChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(banner);

            // Create a RegistryKey, which will access the services folder in the registry
            RegistryKey rk = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services", false);

            // Retrieve all the subkeys for the specified key.
            String[] names = rk.GetSubKeyNames();

            Console.WriteLine("Wirtable Services:");
            Console.WriteLine("-----------------------");

            bool found = false;

            //Iterate through keys in services folder and chick if they are writable.
            foreach (String s in names)
            {
                if (checkIsWritable(s))
                {
                    Console.WriteLine($"{rk.Name}\\{s}");
                    found = true;
                }

            }

            if (!found)
            {
                Console.WriteLine("No writable services found");
            }

            Console.WriteLine();
            Console.WriteLine("Finished");
            Console.WriteLine("[ENTER] to quit....");
            Console.ReadLine();
        }

        static Boolean checkIsWritable(string s)
        {
            try
            {
                RegistryKey aKey = Registry.LocalMachine.OpenSubKey($"SYSTEM\\CurrentControlSet\\Services\\{s}", true);
                return true;
            }
            catch (System.Security.SecurityException)
            {
                return false;
            }
        }

        static string banner = "" +
            "+-------------------------------+\n" +
            "|        Simple  Service        |\n" +
            "|      Permissions Checker      |\n" +
            "|        By Ari Weinberg        |\n" +
            "|                               |\n" +
            "|      www.ariweinberg.xyz      |\n" +
            "+-------------------------------+\n";


    }
}
