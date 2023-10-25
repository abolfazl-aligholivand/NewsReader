
namespace NewsReader.Helpers
{
    public class SecurityUtility
    {
        private readonly IConfiguration _config;

        public SecurityUtility(IConfiguration config)
        {
            this._config = config;
        }

        public bool PasswordVerification(string entryPassword, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(entryPassword));
                for (int i = 0; i < computedHash.Length; i++)
                    if (computedHash[i] != passwordHash[i])
                        return false;

                return true;
            }
        }

        //public string GenerateJwtToken(User user)
        //{
        //    byte[] secret = Encoding.ASCII.GetBytes(_config.GetSection("Jwt:AuthenticationJWTSecret").Value);

        //    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        //    SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, user.UserTitle),
        //            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //            new Claim(ClaimTypes.Role, user.Role())
        //        }),
        //        Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config.GetSection("Jwt:TokenTimeOut").Value)),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    SecurityToken token = handler.CreateToken(descriptor);
        //    return handler.WriteToken(token);
        //}

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }
    }
}