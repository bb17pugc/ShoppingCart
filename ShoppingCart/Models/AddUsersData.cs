using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class AddUsersData
    {
        List<string> Users = new List<string>();

        public void AddUser(string User)
        {
            Users.Add(User);
        }
        public void RemoveUser(string User)
        {
            Users.Remove(User);
        }
        public List<string> GetUsersList()
        {
            return Users;
        }
        public void ClearList()
        {
            Users.Clear();
        }
    }
}