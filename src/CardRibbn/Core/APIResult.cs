using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardRibbn.Core
{

    public interface IAPIResult
    {
        string ApiStatus { get; }
        string Message { get; }
        object Entity { get; }
    }
    public class APIResult : IAPIResult
    {
        private string _apiStatus;
        private string _message;
        private object _entity;
        public APIResult(string apiStatus, string message, object entity = null)
        {
            _apiStatus = apiStatus;
            _message = message;
            _entity = entity;
        }
        
        public string ApiStatus
        {
            get
            {
                return _apiStatus;
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
        }

        public object Entity
        {
            get
            {
                return _entity;
            }
        }
    }
}
