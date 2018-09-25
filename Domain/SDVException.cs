using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class SDVException : Exception
    {
        public List<ErrorField> Errors { get; private set; }

        public SDVException()
        {
            Errors = new List<ErrorField>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in this.Errors)
            {
                sb.AppendLine(item.Message);
            }
            return sb.ToString();
        }

        public SDVException(List<ErrorField> errors)
        {
            if (errors == null)
            {
                Errors = new List<ErrorField>();
            }
            else
            {
                this.Errors = errors;
            }
        }

        public SDVException(string message) : base(message) { Errors = new List<ErrorField>(); }
        public SDVException(string message, Exception inner) : base(message, inner) { }
        protected SDVException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
