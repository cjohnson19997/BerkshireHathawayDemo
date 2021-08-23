using System;
using System.Collections.Generic;
using System.Text;

namespace BerkshireHathaway
{
    /// <summary>
    /// Model for storing the User to Reason information
    /// </summary>
    public class Reason
    {
        private string _ReasonText { get; set; }

        private User _User { get; set; }
        public Reason(string reasonText, string name)
        {
            _ReasonText = reasonText;
            _User = new User(name);

        }

        public string ReasonText
        {
            get
            {
                return _ReasonText;
            }
            set
            {
                _ReasonText = value;
            }
        }

        public User User
        {
            get
            {
                return _User;
            }
            set
            {
                _User = value;
            }
        }

    }
}
