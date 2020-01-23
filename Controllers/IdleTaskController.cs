using System;
using LongRunningRest.Models;
using Microsoft.AspNetCore.Mvc;

namespace LongRunningRest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdleTaskController : Controller
    {
        private readonly IDoSomething _doSomething;

        public IdleTaskController(IDoSomething doSomething)
        {
            _doSomething = doSomething;
        }

        [Route("run")]
        public IActionResult RunTask()
        {
            var taskId = _doSomething.Idle();

            return Accepted($"jobs/{taskId}");
        }

        [HttpGet]
        [Route("jobs/{jobId}")]
        public string CheckIdleTaskJob(Guid jobId)
        {
            var status =_doSomething.CheckStatus(jobId);

            return status;
        }

        [HttpPost]
        [Route("notify")]
        public IActionResult WebHook([FromBody] WebHookInput input)
        {
            if (input.action == "NotifyWhenDone")
            {
                _doSomething.Notify(input.url);
            }

            return Ok();
        }
    }
}