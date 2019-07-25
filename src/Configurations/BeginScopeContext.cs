namespace CrawlerWave.LogTestHelper.Configurations
{
    /// <summary>
    /// The custom Logger scope abstraction
    /// </summary>
    public class BeginScopeContext
    {
        /// <summary>
        /// the scope object
        /// </summary>
        public object Scope { get; set; }

        /// <summary>
        /// the logger 
        /// </summary>
        public string LoggerName { get; set; }
    }
}