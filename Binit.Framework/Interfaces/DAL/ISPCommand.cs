using System.Collections.Generic;

namespace Binit.Framework.Interfaces.DAL
{
    /// <summary>
    /// Interface used to managa a SP with name and params
    /// </summary>
    public interface ISPCommand
    {
        public string Name { get; }
        public Dictionary<string, object> Params { get; set; }
    }
}
