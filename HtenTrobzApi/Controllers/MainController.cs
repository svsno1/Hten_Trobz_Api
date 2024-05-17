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
                var context = new HtenContext();

                if (input.type.ToUpper() == "DELETE")
                {
                    switch (input.endpoint)
                    {
                        case "getItems":
                            var material = context.MaterialLists.FirstOrDefault(o => o.ComName == input.code);
                            if (material != null)
                            {
                                context.MaterialLists.Remove(material);
                                context.SaveChanges();
                            }
                            break;
                        case "getCustomers":
                            var customer = context.Customers.FirstOrDefault(o => o.Code == input.code);
                            if (customer != null)
                            {
                                context.Customers.Remove(customer);
                                context.SaveChanges();
                            }
                            break;
                        case "getJobs":
                            var site = context.Sites.FirstOrDefault(o => o.Code == input.code);
                            if (site != null)
                            {
                                context.Sites.Remove(site);
                                context.SaveChanges();
                            }
                            break;
                        case "getVehicle":
                            var truck = context.Trucks.FirstOrDefault(o => o.Code == input.code);
                            if (truck != null)
                            {
                                context.Trucks.Remove(truck);
                                context.SaveChanges();
                            }
                            break;
                        case "getDrivers":
                            var driver = context.Drivers.FirstOrDefault(o => o.Code == input.code);
                            if (driver != null)
                            {
                                context.Drivers.Remove(driver);
                                context.SaveChanges();
                            }
                            break;
                        case "getSO":
                            var orderDetail = context.OrderDetails.FirstOrDefault(o => o.OrderCode == input.code);
                            if (orderDetail != null)
                            {
                                context.OrderDetails.Remove(orderDetail);
                            }

                            var order = context.Orders.FirstOrDefault(o => o.Code == input.code);
                            if (order != null)
                            {
                                context.Orders.Remove(order);
                            }

                            context.SaveChanges();
                            break;
                    }
                }
                else
                {
                    HttpClient client = new HttpClient();
                    var user = HttpContext.User.GetUserId();
                    string baseUrl = "https://phudoanh-staging.trobz.com/";
                    object requestBody; string json; StringContent content; HttpRequestMessage request; HttpResponseMessage response; string jsonResult;

                    switch (input.endpoint)
                    {
                        case "getItems":
                            requestBody = new
                            {
                                domain = "[('default_code','=','" + input.code + "')]"
                            };
                            json = JsonConvert.SerializeObject(requestBody);
                            content = new StringContent(json, Encoding.UTF8, "application/json");
                            request = new HttpRequestMessage(HttpMethod.Get, baseUrl + "getItems");
                            request.Headers.Add("Authorization", "b972feef41f0f0d55acfebf0985ef1b89fb12c84");
                            request.Content = content;
                            response = await client.SendAsync(request);
                            response.EnsureSuccessStatusCode();
                            jsonResult = await response.Content.ReadAsStringAsync();

                            var itemRes = JsonConvert.DeserializeObject<ApiReponse<ItemInput>>(jsonResult);
                            if (itemRes?.data != null && itemRes.error == null)
                            {
                                foreach (var item in itemRes.data)
                                {
                                    if (input.type.ToUpper() == "CREATE")
                                    {
                                        MaterialList material = new MaterialList()
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
                            break;
                        case "getCustomers":
                            requestBody = new
                            {
                                domain = "[('ref','=','" + input.code + "')]"
                            };
                            json = JsonConvert.SerializeObject(requestBody);
                            content = new StringContent(json, Encoding.UTF8, "application/json");
                            request = new HttpRequestMessage(HttpMethod.Get, baseUrl + "getCustomers");
                            request.Headers.Add("Authorization", "b972feef41f0f0d55acfebf0985ef1b89fb12c84");
                            request.Content = content;
                            response = await client.SendAsync(request);
                            response.EnsureSuccessStatusCode();
                            jsonResult = await response.Content.ReadAsStringAsync();

                            var customerRes = JsonConvert.DeserializeObject<ApiReponse<CustomerInput>>(jsonResult);
                            if (customerRes?.data != null && customerRes.error == null)
                            {
                                foreach (var item in customerRes.data)
                                {
                                    if (input.type.ToUpper() == "CREATE")
                                    {
                                        Customer customer = new Customer()
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
                            break;
                    }
                }

                return "Invalid";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
