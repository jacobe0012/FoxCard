using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFramework
{
    public interface IUID
    {
        long Id { get; }
    }

    public static class IChildIdExtensions
    {
        public static int AsInt(this IUID self)
        {
            return (int)self.Id;
        }

        public static bool IsValid(this IUID self)
        {
            return self.Id != 0;
        }
    }
}
