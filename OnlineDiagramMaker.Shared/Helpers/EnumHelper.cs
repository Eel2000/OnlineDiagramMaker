using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineDiagramMaker.Shared.Helpers
{
    public static class EnumHelper
    {
        public static IEnumerable<T> GetEnumItems<T>()
            =>Enum.GetValues(typeof(T)).Cast<T>().ToList();

        public static T GetValueOf<T>(string t)
#pragma warning disable CS8603 // Possible null reference return.
            => Enum.GetValues(typeof(T)).Cast<T>().FirstOrDefault(x => x!.ToString() == t);
#pragma warning restore CS8603 // Possible null reference return.
    }
}
