﻿using FOA_Server.Models.DAL;

namespace FOA_Server.Models
{
    public class Language
    {
        public string Lang { get; set; }
        public int LangID { get; set; }
        public Language() { }
        public Language(string lang, int langID)
        {
            Lang = lang;
            LangID = langID;
        }

     


    }
}
