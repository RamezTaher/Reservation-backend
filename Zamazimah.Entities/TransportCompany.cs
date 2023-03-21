﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamazimah.Entities
{
    public class TransportCompany
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }    
        public string? NameEn { get; set; }    
        public string? State { get; set; }    
    }
}
