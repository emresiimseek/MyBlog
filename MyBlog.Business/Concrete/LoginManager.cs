using FrameworkCore.Aspect.PostSharp.LogAspect;
using FrameworkCore.CrossCuttingConcerns.Logging.Log4Net.Logger;
using FrameworkCore.Helpers;
using MyBlog.Business.Abstract;
using MyBlog.DataAccsess.Abstract;
using MyBlog.EntityFramework.ComplexTypes;
using MyBlog.EntityFramework.Concrete;
using MyBlog.EntityFramework.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Business.Concrete
{
    public class LoginManager : ILoginService
    {
        BusinessLayerResult<User> businessLayerResult = new BusinessLayerResult<User>();
        private IUserDal _userDal;
        public IUsersRoleDal _usersRoleDal { get; set; }
        static string key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob009X";
        public LoginManager(IUserDal userDal, IUsersRoleDal usersRoleDal)
        {
            _userDal = userDal;
            _usersRoleDal = usersRoleDal;
        }
        public string Decrypt(string cipher)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }

        public string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        [LogAspect(typeof(DatabaseLogger))]
        public BusinessLayerResult<User> LoginControl(string username, string password)
        {
            password = Encrypt(password);
            User user = _userDal.Get(x => x.Username == username && x.Password == password);
            if (user != null)
            {
                businessLayerResult.Result = user;
                if (_userDal.Get(x => x.IsActive == false && x.Id == user.Id) != null)
                {
                    businessLayerResult.AddError(MessagesCodes.UserIsNotActive, "Kullanıcı aktif değil lütfen e-postanızı kontrol ediniz.");
                    return businessLayerResult;
                }

            }
            else
            {
                businessLayerResult.AddError(MessagesCodes.UsernameandPasswordNotMatch, "Kullanıcı Adı ve Parola Uyuşmamaktadır!");
                return businessLayerResult;
            }

            return businessLayerResult;
        }

        public List<UsersRoleses> GetRoles(User user)
        {
            List<UsersRoleses> usersRoles = _userDal.GetUsersRoleses(user);

            return usersRoles;

        }

        public User GetUser(string username, string password)
        {
            password = Encrypt(password);
            return _userDal.Get(u => u.Username == username && u.Password == password);
        }

        //[SecuredOperation(Roles ="Admin")]
        public List<User> GetUsers()
        {
            return _userDal.GetAll();
        }

        public User Get(int id)
        {

            return _userDal.Get(u => u.Id == id);
        }

        public int AddUser(User user)
        {
            user.Password = Encrypt(user.Password);
            return _userDal.Add(user);
        }

        public void UpdateUser(User user)
        {

             user.Password= Encrypt(user.Password);
            _userDal.Update(user);
        }

        public BusinessLayerResult<User> CheckUserNameAndEmail(string username, string email)
        {
            User user = _userDal.Get(u => u.Username == username || u.Email == email);
            if (user != null)
            {

                if (user.Username == username)
                {
                    businessLayerResult.AddError(MessagesCodes.UserNameIsAlreadyExist, "Bu kullanıcı adı kullanılmaktadır!");
                }
                if (user.Email == email)
                {
                    businessLayerResult.AddError(MessagesCodes.EmailAlreadyExist, "Bu email kullanılmaktadır!");
                }
            }

            return businessLayerResult;
        }

        public BusinessLayerResult<User> RegisterControl(User user)
        {

            businessLayerResult.Result = user;
            businessLayerResult = CheckUserNameAndEmail(user.Username, user.Email);
            if (businessLayerResult.Error.Count > 0)
            {
                return businessLayerResult;
            }
            Guid guid = Guid.NewGuid();
            user.ActivateGuid = guid;
            user.IsActive = false;
            int res = AddUser(businessLayerResult.Result);
            if (res > 1)
            {
                businessLayerResult.AddError(MessagesCodes.ErrorRegister, "Kayıt işlemi sırasında beklenmedik bir hata oluştu!");
            }
            else
            {
                string siteUri = ConfigHelper.GetConfigPar<string>("SiteRootUri");
                string activateUri = String.Format("{0}/Login/UserActivate/{1}", siteUri, user.ActivateGuid);
//String.Format("Merhaba,</br>Aramıza hoşgeldin aşağıdaki linke tıklayarak hesabını aktif edebilirsin.</br><a href='{0}' target='_blank'>tıklayınız.</a>", activateUri);

                MailHelper.SendMail(String.Format("Merhaba,</br>Aramıza hoşgeldin aşağıdaki linke tıklayarak hesabını aktif edebilirsin.</br><a href='{0}' target='_blank'>tıklayınız.</a>", activateUri), user.Email, "Blog Sitesi Hesap Aktifleştirme.");
            }
            return businessLayerResult;
        }

        public BusinessLayerResult<User> ActivateUser(Guid id)
        {
            BusinessLayerResult<User> businessLayerResult = new BusinessLayerResult<User>();

            businessLayerResult.Result = _userDal.Get(u => u.ActivateGuid == id);

            if (businessLayerResult.Result != null)
            {
                if (businessLayerResult.Result.IsActive)
                {
                    businessLayerResult.AddError(MessagesCodes.UserAlreadyActive, "Kullanıcı zaten aktif edilmiştir.");
                    return businessLayerResult;
                }
                businessLayerResult.Result.IsActive = true;
                _userDal.Update(businessLayerResult.Result);
            }
            else
            {
                businessLayerResult.AddError(MessagesCodes.ActivateIdDoesNotExist, "Aktifleştirilecek kullanıcı bulunamadı.");
            }
            return businessLayerResult;
        }

        public void DeleteUser(User user)
        {

            _userDal.Delete(user);
        }

        
    }
}
