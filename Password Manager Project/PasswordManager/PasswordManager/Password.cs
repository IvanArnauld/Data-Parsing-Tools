/*
 * Program:         PasswordManager
 * File:            Password.cs
 * Date:            June 5th, 2022
 * Author:          Saeed Alsabawi, Ivan Kepseu
 * Description:     Getters and Setters for the Password class.
 */
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    [DataContract]
    class Password
    {
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public int StrengthNum { get; set; }
        [DataMember]
        public string StrengthText { get; set; }
        [DataMember]
        public string LastReset { get; set; }
    }
}
