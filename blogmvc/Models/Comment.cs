using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace blogmvc.Models
{
    public class Comment
    {
        public Comment()
        {
            //aqui pienso hacer la discriminacion si usamos SQL Server, MySQL o PostgreSQL o SQLite que sirve para apps pequeñas 
            CheckDB();
        }
        [Key]
        [Column("comment_id")]
        public Int64 CommentID { get; set; }
        
        [Column("comment_author_email")]
        public string CommentAuthorEmail { get; set; }
        [Column("comment_date")]
        public DateTime CommentDate { get; set; }
        [Column("comment_date_gmt")]
        public DateTime CommentDateGmt { get; set; }
        [Column("comment_author_ip")]
        public string CommentAuthorIP { get; set; }
        //campo longtext en MySQL 
        [Column("comment_content")]
        public string CommentContent { get; set; }
        [Column("comment_aproved")]
        public string CommentAproved { get; set; }

        /* 
         * Yo creo que asi deberia salir la clave externa con el nombre que yo necesito que es comment_post_id clave id de post 
         *
         *   
        */
        //claves foraneas
        public string ApplicationUserID { get; set; }
        [ForeignKey("ApplicationUserID")]
        public ApplicationUser ApplicationUser { get; set; }

        [Column("comment_post_id")]
        public Int64 CommentPostID { get; set; }
        [ForeignKey("CommentPostID")]
            
        public virtual Post Post { get; set; }
        //clave externa de la tabla Users a la tabla Comment
        //public virtual IdentityUser IdentityUser { get; set; }
        //campo CommentAuthor sera el id del usuario en cuestion asociado a un mail
        //[Column("comment_author")]
        //public int CommentAuthor { get; set; }

        #region methods
        // este metodo es practicamente igual en Post tengo que ver la forma de usar la OOP para cambiarlo 
        protected void CheckDB()
        {
            //aqui voy a preguntar por la DB y voy a cambiar segun me haga falta
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            string StringDB = applicationDbContext.DatabaseCheck;
            if (StringDB == "SQLServer")
            {
                ColumnAttribute colum = new ColumnAttribute(CommentContent);
                colum.TypeName = "nvarchar";
            }
            if (StringDB == "PostgreSQL")
            {
                ColumnAttribute colum = new ColumnAttribute(CommentContent);
                colum.TypeName = "text";
            }
            if (StringDB == "MySQL")
            {
                ColumnAttribute colum = new ColumnAttribute(CommentContent);
                colum.TypeName = "LongText";
            }


        }


    }
    #endregion

}

