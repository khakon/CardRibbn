using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardRibbn.Core
{
    public class IdentityErrorResult : IAPIResult
    {
        private string _apiStatus;
        private string _message;
        private object _entity;

        public IdentityErrorResult(IdentityResult result, string apiStatus = null, string apiMessage = null)
        {

            GetErrors(result);

            if (!string.IsNullOrEmpty(apiStatus))
                _apiStatus = apiStatus;
            else
                _apiStatus = Constants.Status.IDENTITY_RESULT_ISSUE;

            if (!string.IsNullOrEmpty(apiMessage))
                _message = apiMessage;
        }

        public string ApiStatus
        {
            get
            {
                return _apiStatus;
            }
        }

        public object Entity
        {
            get
            {
                return _entity;
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
        }

        private void GetErrors(IdentityResult result)
        {
            string message = string.Empty;

            foreach (var error in result.Errors)
            {
                message = message + error.Description + "\n";
            }

            _message = message;
        }
    }
}
