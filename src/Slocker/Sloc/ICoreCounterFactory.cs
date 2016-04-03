using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Slocker
{
    /// <summary>
    /// Core counter functionality
    /// </summary>
    public interface ICoreCounterFactory
    {
        /// <summary>
        /// Returns codes without block comments
        /// </summary>
        /// <param name="input">non-sparated(i.e. not splitted by line delimitor('\n')) whole lines of codes in a file</param>
        /// <returns>Codes without block-comments</returns>
        string RemoveBlockComments(string input);
        
        /// <summary>
        /// Remove single line comment. If return value is null or empty, counter does not count it.
        /// </summary>
        /// <param name="input">single line of code</param>
        /// <returns>Comment removed line</returns>
        string RemoveSingleComment(string input);

        /// <summary>
        /// Remove other things
        /// </summary>
        /// <param name="input">single line of code</param>
        /// <returns>(Something) removed line</returns>
        string RemoveMiscThings(string input);
    }

    public class NullCoreCounterFactory : ICoreCounterFactory
    {
        public string RemoveBlockComments(string input)
        {
            return input;
        }

        public string RemoveLanguageSpecificElements(string input)
        {
            return input;
        }

        public string RemoveMiscThings(string input)
        {
            return input;
        }

        public string RemoveSingleComment(string input)
        {
            return input;
        }
    }


}
