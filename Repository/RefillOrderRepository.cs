﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RefillApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RefillApi.Repository
{
    public class RefillOrderRepository : IRefillOrderRepository
    {

        private List<RefillOrder> refill = new List<RefillOrder>()
        {
            new RefillOrder{ Id =1, RefillDate = Convert.ToDateTime("2020-11-24 12:12:00 PM"),DrugQuantity = 10, RefillDelivered=true,Payment=true, SubscriptionId=1},
            new RefillOrder{ Id =2, RefillDate = Convert.ToDateTime("2020-11-24 12:12:00 PM"),DrugQuantity = 10, RefillDelivered=true,Payment=true, SubscriptionId=1},
            new RefillOrder{ Id =3, RefillDate = Convert.ToDateTime("2020-11-24 12:12:00 PM"),DrugQuantity = 10, RefillDelivered=false,Payment=false, SubscriptionId=1}
        };

        // public RefillOrder AdhocRefill( int PolicyId, int MemberId, int SubscriptionId)
        public RefillOrder RefillStatus(int SubscriptionId)
        {

            var result = refill.Last(x => (x.SubscriptionId == SubscriptionId) && (x.RefillDelivered) && (x.Payment));
            return result;

           // throw new NotImplementedException();
        }

        public int RefillDues(int id)
        {
            var result = refill.Count(x => (x.SubscriptionId == id) && (!x.RefillDelivered) && (!x.Payment));
            return (result);
            // throw new NotImplementedException();
        }

       // public RefillOrder RefillStatus(int SubscriptionId)
         public RefillOrder AdhocRefill(int PolicyId, int MemberId, int SubscriptionId)
        {
            // drugId and Location is taken from subscription service with the help of MemberId

            int DrugId = 1;
            string Location = "Delhi";
            RefillOrder result = new RefillOrder();

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject("hello"), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync("https://localhost:44393/api/DrugsApi/getDispatchableDrugStock/" + DrugId + "/" + Location, content).Result)
                {

                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }

                    var data = response.Content.ReadAsStringAsync().Result;

                    var check = JsonConvert.DeserializeObject<bool>(data);

                    if (check)
                    {
                        result.Id = 9;
                        result.RefillDate = DateTime.Now;
                        result.DrugQuantity = 10;
                        result.RefillDelivered = false;
                        result.Payment = false;
                        result.SubscriptionId = SubscriptionId;
                    }

                    return result;
                }
            }
        }

     
    }
}
