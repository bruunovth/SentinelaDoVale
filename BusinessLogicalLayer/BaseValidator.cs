using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    public class BaseValidator <T>
    {
        protected List<ErrorField> errors = new List<ErrorField>();

        protected void AddError(ErrorField error)
        {
            errors.Add(error);
        }

        protected void AddError(string field, string message)
        {
            ErrorField errorField = new ErrorField();
            errorField.Field = field;
            errorField.Message = message;
            errors.Add(errorField);
        }

        protected virtual void Validate(T item)
        {
            CheckErrors();
        }

        protected void CheckErrors()
        {
            if (errors.Count != 0)
            {
                throw new SDVException(errors);
            }
        }
    }
}
