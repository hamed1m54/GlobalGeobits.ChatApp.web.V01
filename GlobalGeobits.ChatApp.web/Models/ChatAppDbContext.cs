using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GlobalGeobits.ChatApp.web.Models
{
    public class ChatAppDbContext : DbContext
    {

        //map the users model class to SQL Database table
        public DbSet<Users> Users { get; set; }

        //map the users model class to SQL Database table
        public DbSet<Messages> Messages { get; set; }
    }
}