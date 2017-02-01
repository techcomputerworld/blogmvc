using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace blogmvc.Models
{
    public class Post
    {
        //por defecto pondremos el campo de PostgreSQL y asi no hay que hacer nada si el usuario usa PostgreSQL


        //en el constructor definire el tipo de campo segun en la DB que conectemos
        public Post()
        {
            /*veremos en que DB estamos conectados y dependiendo de eso crearemos el campo
             * LongText = 4 GB MySQL
             * Text campo ilimitado en PostgreSQL
             * nvarchar en SQL Server             
             */
            //saber que DB es la que estamos manejando
            CheckDB();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("post_id")]
        public Int64 PostId { get; set; }
        [Column("post_date")]
        public DateTime Post_date { get; set; }
        //Column System.ComponentModel.DataAnnotations.Schema.ColumnAttribute ponerlo en Typename = "text"
       
        // Esta columna sera definida en el metodo CheckDB
        [Column("post_content")]
        public string Post_Content { get; set; }

        [Column("post_title")]
        public string Post_title { get; set; }
        [Column("post_modified")]
        public DateTime PostModified { get; set; }
        [Column("post_status")]
        public string PostStatus { get; set; }
        [Column("post_name")]
        public string PostName { get; set; }
        [Column("post_type")]
        public string PostType { get; set; }
        [Column("post_Parent")]
        public Int64 PostParent { get; set; }

        //claves foraneas 
        //campo que vendra de la tabla aspnetusers es la relacion con la tabla aspnetusers que tengo que crearla
        //[Column("email_author")]       
        //[ForeignKey("email_author")]
        public string ApplicationUserID { get; set; }
        [ForeignKey("ApplicationUserID")]
        public ApplicationUser ApplicationUser { get; set; }
        //propiedad de navegacion que hace de 1 a muchos Post de 1 y Comment de muchos
        public ICollection<Comment> Comment { get; set; }

        #region methods
        // este metodo es practicamente igual en Comment tengo que ver la forma de usar la OOP para cambiarlo 
        protected void CheckDB()
        {
            //aqui voy a preguntar por la DB y voy a cambiar segun me haga falta
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            string StringDB = applicationDbContext.DatabaseCheck;
            if (StringDB == "SQLServer")
            {
                ColumnAttribute colum = new ColumnAttribute(Post_Content);
                colum.TypeName = "nvarchar";
            }
            if (StringDB == "PostgreSQL")
            {
                ColumnAttribute colum = new ColumnAttribute(Post_Content);
                colum.TypeName = "text";
            }
            if (StringDB == "MySQL")
            {
                ColumnAttribute colum = new ColumnAttribute(Post_Content);
                colum.TypeName = "LongText";
            }


        }
        
        #endregion
    }
}
