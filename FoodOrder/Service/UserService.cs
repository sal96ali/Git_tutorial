using Firebase.Database;
using FoodOrder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;

namespace FoodOrder.Service
{
    public class UserService
    {
        FirebaseClient client;
        public UserService()
        {
            client = new FirebaseClient("https://food-a3285.firebaseio.com/");
        }

        public async Task<bool> IsUserExists(string uname)
        {
            var user = (await client.Child("Users").OnceAsync<User>())
                        .Where(u => u.Object.Username == uname).FirstOrDefault();
            return (user != null);
        }

        public async Task<bool> RegisterUser(string uname,string passwd)
        {
            if (await IsUserExists(uname) == false)
            {
                await client.Child("Users").PostAsync(new User() { Username = uname, Password = passwd });
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> LoginUser(string uname, string passwd)
        {
            var user = (await client.Child("Users").OnceAsync<User>()).Where(u => u.Object.Username == uname).Where(u => u.Object.Password == passwd).
                FirstOrDefault();
            return (user != null);
        }
    }
}
