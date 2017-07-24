using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardRibbn.Core
{
    public class ModelErrorResult:IAPIResult
    {
        private string _apiStatus;
        private string _message;
        private object _entity;
        private ActionContext actionContext;

        public ModelErrorResult(ActionContext context, string apiStatus = Constants.Status.INVALID_MODELSTATE, string message = "Bad model state")
        {
            this.actionContext = context;

            if(actionContext.ModelState != null && actionContext.ModelState.IsValid == false)
            {

                List<InvalidModelProperty> invalidModelStates = new List<InvalidModelProperty>();

                var modelState = actionContext.ModelState;
                foreach (var model in modelState)
                {
                    int separatorIndex = model.Key.IndexOf(".") + 1; //breaks up the 'model.ProviderKey' to just 'ProviderKey'
                    string modelKey = model.Key;
                    InvalidModelProperty invalidItem = new InvalidModelProperty();
                    if (model.Value.Errors.Count > 0)
                    {
                        invalidItem.ErrorItem = modelKey.Remove(0, separatorIndex);
                        invalidItem.Reason = model.Value.Errors[0].ErrorMessage;
                        invalidModelStates.Add(invalidItem);
                    }
                }

                if(invalidModelStates.Count > 0)
                {
                    message = string.Empty;

                    foreach(var error in invalidModelStates)
                    {
                        message = message + error.Reason + "\n";    
                    }
                }

                _apiStatus = apiStatus;
                _message = message;
                _entity = invalidModelStates;

            }
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
    }
}
