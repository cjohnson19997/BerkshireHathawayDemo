using System;
using System.Collections.Generic;
using System.Text;

namespace BerkshireHathaway
{
    /// <summary>
    /// Model for storing Username
    /// </summary>
    public class User
    {
        private string _Name { get; set; }
        public User(string name)
        {
            _Name = name;

        } 

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

    }
}
