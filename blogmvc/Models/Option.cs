using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace blogmvc.Models
{
    public class Option
    {
        
        [Key]
        [Column("option_id")]
        public Int32 OptionID { get; set; }
        /*
        [Column("option_name")]
        public string OptionName { get; set; }
        [Column("option_value")]
        public string OptionValue { get; set; }
        [Column("autoload")]
        public string Autoload { get; set; }
        */
        public string siteurl { get; set; }
        public string home { get; set; }
        public string blogname { get; set; }
        public string blogdescription { get; set; }
        public string user_can_register { get; set; }
        public string post_per_page { get; set; }
        public string admin_email { get; set; }
        //FK de la tabla Users 
        public string ApplicationUserID { get; set; }
        [ForeignKey("ApplicationUserID")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
