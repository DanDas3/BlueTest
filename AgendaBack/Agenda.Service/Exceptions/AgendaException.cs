﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Service.Exceptions
{
    public class AgendaException : Exception
    {
        public AgendaException(string message) : base (message)
        {
            
        }
    }
}
