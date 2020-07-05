using MyBlog.Business.Concrete;
using MyBlog.EntityFramework.ComplexTypes;
using MyBlog.EntityFramework.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Business.Abstract
{
    public interface ILoginService
    {
        BusinessLayerResult<User> LoginControl(string Username, string Password);
        List<UsersRoleses> GetRoles(User user);
        string Encrypt(string text);
        string Decrypt(string cipher);
        User GetUser(string username, string password);
        User Get(int id);
        List<User> GetUsers();
        BusinessLayerResult<User> CheckUserNameAndEmail(string username, string email);
        int AddUser(User user);
        void UpdateUser(User user);
        BusinessLayerResult<User> RegisterControl(User user);
        BusinessLayerResult<User> ActivateUser(Guid id);
        void DeleteUser(User user);
      
    }
}
