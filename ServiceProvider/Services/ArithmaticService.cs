﻿using Authenticator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;

namespace ServiceProvider.Services
{
    public static class ArithmaticService
    {


        public static int AddTwoNums(int num1, int num2)
        {

            int sum = num1 + num2;
            return sum;
        }

        public static int AddThreeNums(int num1, int num2, int num3)
        {

            int sum = num1 + num2 + num3;
            return sum;
        }

        public static int MulTwoNums(int num1, int num2)
        {

            int sum = num1 * num2;
            return sum;
        }

        public static int MulThreeNums(int num1, int num2, int num3)
        {

            int sum = num1 * num2 * num3;
            return sum;
        }
    }
}