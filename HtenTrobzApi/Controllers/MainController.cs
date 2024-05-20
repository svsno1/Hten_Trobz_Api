using HtenTrobzApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace HtenTrobzApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class MainController : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            if (ModelState.IsValid)
            {
                if (Common.ValidateUser(userName, password))
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Email, userName)
                    };

                    ClaimsIdentity identity = new ClaimsIdentity(claims, "Cookie");

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    DateTimeOffset expirationDate = DateTimeOffset.UtcNow.AddDays(10);

                    await HttpContext.SignInAsync(
                        scheme: "CookieAuthentication",
                        principal: principal,
                        properties: new AuthenticationProperties()
                        {
                            IsPersistent = true,
                            ExpiresUtc = expirationDate,
                        });

                    return Json(new LoginResponse { Code = 0, Expires = expirationDate });
                }
            }

            return Json(new LoginResponse { Code = 1 });
        }

        [HttpPost]
        public async Task<string> Alert(ApiInput input)
        {
            try
            {
                using (var context = new HtenContext())
                {
                    if (input.type.ToUpper() == "DELETE")
                    {
                        return HandleDeleteRequest(input, context);
                    }
                    else
                    {
                        return await HandleNonDeleteRequestAsync(input, context);
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string HandleDeleteRequest(ApiInput input, HtenContext context)
        {
            var entity = input.endpoint switch
            {
                "getItems" => context.MaterialLists.FirstOrDefault(o => o.ComName == input.code) as object,
                "getCustomers" => context.Customers.FirstOrDefault(o => o.Code == input.code),
                "getJobs" => context.Sites.FirstOrDefault(o => o.Code == input.code),
                "getVehicle" => context.Trucks.FirstOrDefault(o => o.Code == input.code),
                "getDrivers" => context.Drivers.FirstOrDefault(o => o.Code == input.code),
                "getSO" => context.Orders.FirstOrDefault(o => o.Code == input.code),
                _ => null
            };

            if (entity != null)
            {
                switch (input.endpoint)
                {
                    case "getSO":
                        var orderDetail = context.OrderDetails.FirstOrDefault(o => o.OrderCode == input.code);
                        if (orderDetail != null)
                        {
                            context.OrderDetails.Remove(orderDetail);
                        }
                        context.Orders.Remove((Order)entity);
                        break;
                    default:
                        context.Remove(entity);
                        break;
                }

                context.SaveChanges();
            }

            return "OK";
        }

        private async Task<string> HandleNonDeleteRequestAsync(ApiInput input, HtenContext context)
        {
            var client = new HttpClient();
            var user = HttpContext.User.GetUserId();
            string baseUrl = "https://phudoanh-staging.trobz.com/";
            object? requestBody = input.endpoint switch
            {
                "getItems" => new { domain = "[('default_code','=','" + input.code + "')]" },
                "getCustomers" => new { domain = "[('ref','=','" + input.code + "')]" },
                _ => null
            };

            if (requestBody == null) return "Invalid endpoint";

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Get, baseUrl + input.endpoint)
            {
                Content = content
            };
            request.Headers.Add("Authorization", "b972feef41f0f0d55acfebf0985ef1b89fb12c84");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var jsonResult = await response.Content.ReadAsStringAsync();

            return input.endpoint switch
            {
                "getItems" => ProcessItemResponse(jsonResult, input, context, user),
                "getCustomers" => ProcessCustomerResponse(jsonResult, input, context, user),
                _ => "Invalid endpoint"
            };
        }

        private string ProcessItemResponse(string jsonResult, ApiInput input, HtenContext context, string user)
        {
            var itemRes = JsonConvert.DeserializeObject<ApiReponse<ItemInput>>(jsonResult);
            if (itemRes?.data != null && itemRes.error == null)
            {
                foreach (var item in itemRes.data)
                {
                    if (input.type.ToUpper() == "CREATE")
                    {
                        var material = new MaterialList
                        {
                            ComName = item.ItemCode,
                            Description = item.ItemName,
                            Unit = item.Uom?.Name,
                            Density = item.Uom?.Ratio,
                        };
                        context.MaterialLists.Add(material);
                    }
                    else if (input.type.ToUpper() == "UPDATE")
                    {
                        var material = context.MaterialLists.FirstOrDefault(o => o.ComName == item.ItemCode);
                        if (material != null)
                        {
                            material.Description = item.ItemName;
                            material.Unit = item.Uom?.Name;
                            material.Density = item.Uom?.Ratio;
                        }
                    }
                }

                context.SaveChanges();
                return "OK";
            }

            return "Error processing items";
        }

        private string ProcessCustomerResponse(string jsonResult, ApiInput input, HtenContext context, string user)
        {
            var customerRes = JsonConvert.DeserializeObject<ApiReponse<CustomerInput>>(jsonResult);
            if (customerRes?.data != null && customerRes.error == null)
            {
                foreach (var item in customerRes.data)
                {
                    if (input.type.ToUpper() == "CREATE")
                    {
                        var customer = new Customer
                        {
                            Code = item.CusCode,
                            Name = item.CusName,
                            AddressLine1 = item.Address,
                            Telephone = item.Phone,
                            Note = item.Note,
                            CreateLog = DateTime.Now,
                            UserCreate = user,
                        };
                        context.Customers.Add(customer);
                    }
                    else if (input.type.ToUpper() == "UPDATE")
                    {
                        var customer = context.Customers.FirstOrDefault(o => o.Code == item.CusCode);
                        if (customer != null)
                        {
                            customer.Name = item.CusName;
                            customer.AddressLine1 = item.Address;
                            customer.Telephone = item.Phone;
                            customer.Note = item.Note;
                            customer.LastModifyLog = DateTime.Now;
                            customer.UserChange = user;
                        }
                    }
                }

                context.SaveChanges();
                return "OK";
            }

            return "Error processing customers";
        }
    }
}
