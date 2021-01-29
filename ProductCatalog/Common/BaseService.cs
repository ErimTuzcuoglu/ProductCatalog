using System.Collections.Generic;
using System.Linq;

namespace ProductCatalog.Common
{
    public class BaseService
    {
        protected readonly ICollection<string> _errors = new List<string>();

        protected bool IsOperationValid()
        {
            return !_errors.Any();
        }

        protected void AddError(string error)
        {
            _errors.Add(error);
        }

        protected void ClearErrors()
        {
            _errors.Clear();
        }
    }
}