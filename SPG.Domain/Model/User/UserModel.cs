using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
namespace SPG.Domain.Model 
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Login { get; set; }

        [Required]
        [MaxLength(100)] // Adjust max length as per your requirements
        private byte[] PasswordHash { get; set; } = [];

        [Required]
        [MaxLength(32)] // Adjust max length as per your requirements
        private byte[] Salt { get; set; } = [];

        public UserModel(string login, string password)
        {
            Login = login;
            SetPassword(password);
        }

        // Method to set the password securely
        public void SetPassword(string password)
        {
            // Generate a random salt
            Salt = GenerateSalt();

            // Hash the password with the salt
            PasswordHash = HashPasswordWithSalt(password, Salt);
        }

        // Method to verify if the entered password matches the stored hash
        public bool VerifyPassword(string password)
        {
            byte[] enteredPasswordHash = HashPasswordWithSalt(password, Salt);
            return StructuralComparisons.StructuralEqualityComparer.Equals(enteredPasswordHash, PasswordHash);
        }

        // Helper method to generate a random salt
        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[32];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        // Helper method to hash the password with the salt
        private static byte[] HashPasswordWithSalt(string password, byte[] salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] passwordWithSaltBytes = new byte[passwordBytes.Length + salt.Length];
            passwordBytes.CopyTo(passwordWithSaltBytes, 0);
            salt.CopyTo(passwordWithSaltBytes, passwordBytes.Length);

            return SHA256.HashData(passwordWithSaltBytes);
        }
    }
}
