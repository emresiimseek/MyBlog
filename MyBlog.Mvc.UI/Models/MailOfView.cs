using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlog.Mvc.UI.Models
{
    public class MailOfView
    {
        public string Name { get; set; }
        [Required, EmailAddress, DisplayName("Gönderici E-posta Adresi")]
        public string toMail { get; set; }
        [Required, MinLength(20), DisplayName("E-Posta içeriği")]
        public string body { get; set; }
        [Required, DisplayName("Başlık")]
        public string subject { get; set; }
    }
}