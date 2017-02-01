using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogmvc.Models
{
    /* Aqui pondremos las clases que me hagan falta de ViewModels que son clases para trabajar con distintos modelos para pasar datos de controladores a vistas y viceversa
     * Este tipo de clases veo que en asp.net mvc se usan mucho.   
     */
    /*
    public class UserViewModels
    {
        public string siteurl { get; set; }
        public string home { get; set; }
        public string blogname { get; set; }
        public string blogdescription { get; set; }
        public string user_can_register { get; set; }
        public string post_per_page { get; set; }
        public string admin_email { get; set; }
       

    }
    */
    public class UserCanRegister
    {
        public UserCanRegister()
        {

        }
        public string option { get; set; }
        public int value { get; set; }
        //public IEnumerable<user_can_register> UserCanRegister { get; set; }
    }

}
