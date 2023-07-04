using Microsoft.EntityFrameworkCore;

namespace JWTToken.Models
{
    public class UserConstrants
    {



        public static List<UserModel> Users = new()
        {
            new UserModel()
            {
                Username = "admin", Password = "admin" , Role="Admin"


            }
        };




        //public async Task<UserLogin> GetUserCredential(UserLogin userLogin)
        //{
        //    // var context = await _context.userModels.SingleOrDefaultAsync(u => u.Username == username );
        //    var context = await _context.userModels.SingleOrDefaultAsync(x => x.Username.ToLower() ==
        //                        userLogin.Username.ToLower() && x.Password == userLogin.Password);
        //    if (context == null)
        //    {
        //        return null;
        //    }
        //    return null;
        //}

    }
}




