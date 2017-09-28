using System.Collections.Generic;

namespace Chess.Domain
{
    public class MovementResult
    {
        public bool WasSuccessful { get; set; }
        public string ReasonForFailure { get; set; }
        public List<Flag> Flags { get; set; }

        public MovementResult()
        {
            Flags = new List<Flag>();
        }
    }
}
