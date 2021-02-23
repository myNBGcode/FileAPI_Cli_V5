using System;
using System.Collections.Generic;
using System.Text;

namespace FileapiCli.Core
{
    public interface IUserInfo
    {
        public string UserName { get; set; }
        public string Registry { get; set; }
        public string SubjectUser { get; set; }
        public string SubjectRegistry { get; set; }
    }
    public class UserInfo : IUserInfo
    {
        public string UserName { get; set; }

        public string Registry { get; set; }

        public string SubjectUser { get; set; }


        public string SubjectRegistry { get; set; }
    }
   
}
