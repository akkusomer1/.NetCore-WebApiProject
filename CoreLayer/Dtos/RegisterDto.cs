﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos
{
    //Kayıt olma
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
