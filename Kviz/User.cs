using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kviz
{
    public class User
    {
        private string name;
        private string password;
        private bool admin;

        public string Name 
        {
            get { return name; }
            set { name = value; }
        }
        public string Password 
        { 
            get { return password; } 
            set { password = value; }
        }
        public bool Admin
        {
            get { return admin; }
            set { admin = value; }
        }

        public User()
        {
            Name = "Nobody";
            Password = "";
            Admin = false;
        }
        public User(string name, string password) 
        { 
            Name = name;
            Password = password;
            Admin = false;
        }
        public User(string name, string password, bool admin)
        {
            Name = name;
            Password = password;
            Admin = admin;
        }
        public override string ToString()
        {
            return $"Name: {Name}, Password: {Password}, Admin: {Admin}";
        }
    }
}
