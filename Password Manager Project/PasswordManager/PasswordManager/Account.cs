/*
 * Program:         PasswordManager
 * File:            Account.cs
 * Date:            June 5th, 2022
 * Author:          Saeed Alsabawi, Ivan Kepseu
 * Description:     Getters and Setters for the Account class.
 */

using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    [DataContract]
    class Account
    {
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string LoginUrl { get; set; }
        [DataMember]
        public string AccountNum { get; set; }
        [DataMember]
        public Password Password { get; set; }


    } // end class Item
}