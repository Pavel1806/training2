﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionIoc
{
    [ImportConstructor]
    class CustomerBLL
    {

        public CustomerBLL(ICustomerDAL dal, Logger logger) 
        { 
            
        }
    }
}
