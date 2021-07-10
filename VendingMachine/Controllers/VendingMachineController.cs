using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachineApi.Classes;
using VendingMachineApi.Models;
using VendingMachineApi.Services;

namespace VendingMachineApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendingMachineController : ControllerBase
    {

        #region Constants

        private const string GET_ERROR_MESSAGE = "ERROR: VendingMachineController.Get(): Error occured in Get controller for VendingMachine";

        #endregion
        #region Instance Variables

        private readonly VendingMachineService _service;
        private VendingMachine _vendingMachine;
        #endregion
        #region Public Methods

        public VendingMachineController(VendingMachineService service)
        {
            _service = service;
            _vendingMachine = TempMemory.VendingMachine;
        }

        [HttpGet]
        public ActionResult<string> Get([FromQuery] VendingFilter filter)
        {
            ActionResult retval = BadRequest(filter);

            try
            {
                if (!ModelState.IsValid)
                {
                    retval = BadRequest(filter);
                }
                else
                {
                    retval = new OkObjectResult(_vendingMachine.Drinks);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(GET_ERROR_MESSAGE + ex);
            }

            return retval;
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] Order order)
        {
            ActionResult retval = BadRequest(order);

            try
            {
                if (!ModelState.IsValid)
                {
                    retval = BadRequest(order);
                }
                else
                {
                    retval = new OkObjectResult(_service.ProcessOrder(ref _vendingMachine,ref  order));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(GET_ERROR_MESSAGE + ex);
            }

            return retval;
        }

        #endregion
    }

}
