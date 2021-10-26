﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
    }
}