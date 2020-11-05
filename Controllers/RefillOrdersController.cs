using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RefillApi.Models;
using RefillApi.Provider;
using RefillApi.Repository;

namespace RefillApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    
    public class RefillOrdersController : ControllerBase
    {
        //static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(RefillOrdersController));
        /*  private List<RefillOrder> refill= new List<RefillOrder>()
          {
              new RefillOrder{ Id =1, RefillDate = Convert.ToDateTime("2020-11-24 12:12:00 PM"),DrugQuantity = 10, RefillDelivered=true,Payment=true, SubscriptionId=1},
              new RefillOrder{ Id =2, RefillDate = Convert.ToDateTime("2020-11-24 12:12:00 PM"),DrugQuantity = 10, RefillDelivered=true,Payment=true, SubscriptionId=1},
              new RefillOrder{ Id =3, RefillDate = Convert.ToDateTime("2020-11-24 12:12:00 PM"),DrugQuantity = 10, RefillDelivered=false,Payment=false, SubscriptionId=1}
          };
   */

        //IRefillOrderRepository _refillOrderRepository;
        IRefillOrderProvider _refillOrderProvider;
        public RefillOrdersController(IRefillOrderProvider refillOrderProvider)
        {
          //  _refillOrderRepository = refillOrderRepository;
            _refillOrderProvider = refillOrderProvider;
        }


       [HttpGet("RefillStatus/{id}")]
        public IActionResult RefillStatus(int id)
        {
            return Ok(_refillOrderProvider.RefillStatus(id));
        }

        [HttpGet("RefillDues/{id}")]
        public IActionResult RefillDues(int id)
        {
            return Ok(_refillOrderProvider.RefillDues(id));
        }

        [HttpPost("AdhocRefill{PolicyId}/{MemberId}/{SubscriptionId}")]
        public IActionResult AdhocRefill([FromRoute] int PolicyId, int MemberId, int SubscriptionId)
        {
            // drugId and Location is taken from subscription service with the help of MemberId
            return Ok(_refillOrderProvider.AdhocRefill( PolicyId,  MemberId,  SubscriptionId));

        }
    }
}
