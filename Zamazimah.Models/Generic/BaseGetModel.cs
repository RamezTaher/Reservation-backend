﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamazimah.Models.Generic
{
	public class BaseGetModel
	{
		public int Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
	}
}
