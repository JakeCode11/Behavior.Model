namespace MathCog2.UserModeling
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
