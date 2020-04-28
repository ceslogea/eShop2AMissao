using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Common.Mediator.Result
{
    public class MediatorResult
    {
        public static MediatorResult Ok = new MediatorResult();

        public bool HasValidation => _validations.Count > 0;
        private List<string> _validations = new List<string>();
        public IList<string> Validations => _validations;
        public MediatorResult() { }

        public void AddValidation(string validation)
            => _validations.Add(validation);
    }

    public class MediatorResult<TResponse> : MediatorResult
    {
        public TResponse Data { get; private set; }
        public MediatorResult() { }

        public MediatorResult(TResponse data)
        {
            Data = data;
        }
    }
}
