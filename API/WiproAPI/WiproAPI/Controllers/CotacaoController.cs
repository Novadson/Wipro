using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WiproAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CotacaoController : ControllerBase
    {

        [HttpGet]
        public string[] Get()
        {
         return new string [] {"NVADSON","DASDSADSA","DASDSADSA" };
        }
    }
}
