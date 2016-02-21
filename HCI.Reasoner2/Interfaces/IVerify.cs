using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserModeling
{
    /*
     * User Modeling Verification API
     * 
     */ 
    public interface IVerify
    {
        /// <summary>
        /// API for the verification
        /// </summary>
        /// <param name="source">User Input</param>
        /// <param name="target">Knowledge Base</param>
        /// <returns>Scaffolds Feedback</returns>
        object Verify(object source, object target);
    }
}
