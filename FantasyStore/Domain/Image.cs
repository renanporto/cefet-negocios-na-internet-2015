﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStore.Domain
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }

        public Product Product { get; set; }
    }
}
