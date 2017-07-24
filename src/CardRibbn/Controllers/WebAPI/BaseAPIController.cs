using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CardRibbn.Models.AccountViewModels;
using CardRibbn.Core;
using CardRibbn.Models;
using Microsoft.AspNetCore.Identity;
using CardRibbn.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CardRibbn.Controllers.WebAPI
{
    public abstract class BaseAPIController : Controller
    {
        private readonly UserManager<CardCraftUser> _userManager;
        private readonly SignInManager<CardCraftUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;

        public BaseAPIController(
           UserManager<CardCraftUser> userManager,
           SignInManager<CardCraftUser> signInManager,
           IEmailSender emailSender,
           ISmsSender smsSender,
           ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<BaseAPIController>();
        }

        public ActionContext ActionContext
        {
            get
            {
                return new ActionContext()
                {
                    HttpContext = HttpContext
                };
            }
        }

        public UserManager<CardCraftUser> UserManager
        {
            get
            {
                return _userManager;
            }
        }

        public SignInManager<CardCraftUser> SignManager
        {
            get
            {
                return _signInManager;
            }
        }

        public IEmailSender EmailSender
        {

            get
            {
                return _emailSender;
            }
        }

        public ISmsSender SmsSender
        {
            get
            {
                return _smsSender;
            }
        }

        public ILogger Logger
        {
            get
            {
                return _logger;
            }
        }
        public OkObjectResult SuccessfulAPIResult(string apiStatus, string message, object dataObject = null)
        {
            IAPIResult result = new APIResult(apiStatus, message, dataObject);
            return Ok(result);
        }

        public BadRequestObjectResult ErrorAPIResult(string apiStatus, string message, object dataObject = null)
        {
            IAPIResult result = new APIResult(apiStatus, message, dataObject);
            return BadRequest(result);
        }

        public BadRequestObjectResult BusinessLogicErrorResult(string apiStatus = Constants.Status.INVALID_MODELSTATE, string message = "Bad modelstate")
        {
            IAPIResult result = new ModelErrorResult(ActionContext, apiStatus, message);
            return BadRequest(result);
        }

        public BadRequestObjectResult IdentityResultLogicError(IdentityResult identityResult, string apiStatus = null, string apiMessage = null)
        {
            IAPIResult result = new IdentityErrorResult(identityResult, apiStatus, apiMessage);
            return BadRequest(result);
        }

        public static string APILog(string endPoint, string statusCode, string apiStatus, string apiMessage, object data = null)
        {
            if (data == null)
            {
                return string.Format("/{0} - Status Code: {1} - API Status: {2} - Message: {3}", endPoint, statusCode, apiStatus, apiMessage);
            }
            else
            {
                string dataJsonString = string.Empty;

                try
                {
                    dataJsonString = JsonConvert.SerializeObject(data, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        Formatting = Formatting.None,
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
                }
                catch { }

                return string.Format("/{0} - Status Code: {1} - API Status: {2} - Message: {3} - Data: {4}", endPoint, statusCode, apiStatus, apiMessage, dataJsonString);
            }
        }
    }
}