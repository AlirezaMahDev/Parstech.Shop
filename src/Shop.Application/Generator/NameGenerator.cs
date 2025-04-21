using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Application.Generator
{
    public static class NameGenerator
    {
        public static string GenerateUniqCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
