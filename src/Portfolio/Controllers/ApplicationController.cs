﻿using System;
using System.Web.Mvc;
using Portfolio.Lib;

namespace Portfolio.Controllers
{
    public abstract class ApplicationController : Controller
    {
        public const string DEFAULT_FORM_ERROR_MESSAGE = "There is something wrong with the form. Please correct the errors and try again.";
        
        private IHttpSessionAdapter sessionAdapter;

        public virtual IHttpSessionAdapter SessionAdapter
        {
            get { return sessionAdapter ?? (sessionAdapter = HttpSessionAdapter.Deserialize(Session)); }
            set { sessionAdapter = value; }
        }

        protected virtual void CheckModelState(Func<ActionResult> onInvalidModelState = null)
        {
            // By default, if the model state is not valid, we are going to display the form again.
            // If you don't like this default functionality, I suggest you pass in an appropriate action.
            if (onInvalidModelState == null)
                onInvalidModelState = () => View(ViewData.Model);

            if (!ViewData.ModelState.IsValid)
                onInvalidModelState.Invoke().ExecuteResult(ControllerContext);
        }

        protected override void OnException(ExceptionContext exceptionContext)
        {
            base.OnException(exceptionContext);
        }
    }
}
