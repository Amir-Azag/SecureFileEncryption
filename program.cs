using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SecureFileEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Secure File Encryption Tool ===\n");

            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Encrypt a file");
            Console.WriteLine("2. Decrypt a file\n");

            Console.Write("Option: ");
            string option = Console.ReadLine();

            if (option == "1")
            {
                EncryptFile();
            }
            else if (option == "2")
            {
                DecryptFile();
            }
            else
            {
                Console.WriteLine("Invalid option selected.");
            }
        }

        static void EncryptFile()
        {
            Console.Write("\nEnter the full path of the file to encrypt: ");
            string inputFile = Console.ReadLine();

            Console.Write("Enter a password: ");
            string password = ReadPassword();

            Console.Write("Enter the output file path: ");
            string outputFile = Console.ReadLine();

            try
            {
                FileEncrypt(inputFile, outputFile, password);
                Console.WriteLine("\nFile encrypted successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }

        static void DecryptFile()
        {
            Console.Write("\nEnter the full path of the file to decrypt: ");
            string inputFile = Console.ReadLine();

            Console.Write("Enter the password: ");
            string password = ReadPassword();

            Console.Write("Enter the output file path: ");
            string outputFile = Console.ReadLine();

            try
            {
                FileDecrypt(inputFile, outputFile, password);
                Console.WriteLine("\nFile decrypted successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }

        static void FileEncrypt(string inputFile, string outputFile, string password)
        {
            byte[] salt = GenerateRandomSalt();

            using FileStream fsCrypt = new FileStream(outputFile, FileMode.Create);
            fsCrypt.Write(salt, 0, salt.Length);

            using RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;

            var key = new Rfc2898DeriveBytes(password, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.CFB;

            using CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write);
            using FileStream fsIn = new FileStream(inputFile, FileMode.Open);

            byte[] buffer = new byte[1048576]; // 1MB buffer
            int read;
            while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
            {
                cs.Write(buffer, 0, read);
            }
        }

        static void FileDecrypt(string inputFile, string outputFile, string password)
        {
            byte[] salt = new byte[32];

            using FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);
            fsCrypt.Read(salt, 0, salt.Length);

            using RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;

            var key = new Rfc2898DeriveBytes(password, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.CFB;

            using CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read);
            using FileStream fsOut = new FileStream(outputFile, FileMode.Create);

            byte[] buffer = new byte[1048576]; // 1MB buffer
            int read;
            while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
            {
                fsOut.Write(buffer, 0, read);
            }
        }

        static byte[] GenerateRandomSalt()
        {
            byte[] salt = new byte[32];
            using RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(salt);
            return salt;
        }

        static string ReadPassword()
        {
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo info;
            do
            {
                info = Console.ReadKey(true);
                if (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Backspace)
                {
                    password.Append(info.KeyChar);
                    Console.Write("*");
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password.Length--;
                        Console.Write("\b \b");
                    }
                }
            } while (info.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password.ToString();
        }
    }
}
