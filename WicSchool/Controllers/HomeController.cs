using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WicSchool.Models;
using static WicSchool.Data.BusinessLogic.EmployeeProcessor;
using static WicSchool.Data.BusinessLogic.AccountantProcessor;
using static WicSchool.Data.BusinessLogic.TechnicianProcessor;


namespace WicSchool.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewEmployees(string search = null)
        {

            ViewBag.Search = search;

            var data = new List<WicSchool.Data.Models.Employee>();
            if (!string.IsNullOrEmpty(search))
            {
                data = SearchEmployee(search);
            } else
            {
                data = LoadEmployees();
            }
                
            List<Employee> employees = new List<Employee>();

            foreach (var row in data)
            {
                employees.Add(new Employee
                {
                    Id = row.Id,
                    EmployeeId = row.EmployeeId,
                    EmployeeName = row.EmployeeName,
                    EmployeeType = row.EmployeeType,
                    RatePerHour = row.RatePerHour

                });
            }

            return View(employees);
        }

        [HttpGet]
        [Route("Home/Details/{id:int}")]
        public ActionResult Details(int? id)
        {
            ViewBag.Message = "Your details page.";
            if (id == null)
            {
                return HttpNotFound();
            }
            var model = GetEmployee((int)id).First();

            Employee employee = new Employee
            {
                EmployeeId = model.EmployeeId,
                EmployeeName = model.EmployeeName.Trim(),
                EmployeeType = model.EmployeeType.Trim(),
                RatePerHour = model.RatePerHour
            };

            if (model.EmployeeType.Contains("accountant"))
            {
                var modelAcount = GetAccountant((int)id).First();
                employee.Accountant = new Accountant
                {
                    CPANumber = modelAcount.CPANumber,
                };
            }
            else if (model.EmployeeType.Contains("technician"))
            {
                var modelTech = GetTechnician((int)id).First();
                employee.Technician = new Technician
                {
                    ACSNumber = modelTech.ACSNumber,
                    Expire = modelTech.Expire,
                };
            }

            return View(employee);
        }

        [HttpGet]
        [Route("Home/Edit/{id:int}")]
        public ActionResult Edit(int? id)
        {
            ViewBag.Message = "Your edit page.";

            if (id == null)
            {
                return HttpNotFound();
            }

            var model = GetEmployee((int)id).First();

            Employee employee = new Employee
            {
                EmployeeId = model.EmployeeId,
                EmployeeName = model.EmployeeName.Trim(),
                EmployeeType = model.EmployeeType.Trim(),
                RatePerHour = model.RatePerHour
            };

            employee.Technician = new Technician
            {
                Expire = DateTime.Now
            };

            if (model.EmployeeType.Contains("accountant"))
            {
                var modelAcount = GetAccountant((int)id).First();
                employee.Accountant = new Accountant
                {
                    CPANumber = modelAcount.CPANumber.Trim(),
                };
            }
            else if (model.EmployeeType.Contains("technician"))
            {
                var modelTech = GetTechnician((int)id).First();
                employee.Technician = new Technician
                {
                    ACSNumber = modelTech.ACSNumber.Trim(),
                    Expire = modelTech.Expire,
                };
            }

            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Home/Edit/{id:int}")]
        public ActionResult Edit(int? id, Employee model)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                var modelOld = GetEmployee((int)id).First();
                int recordEdited = EditEmployee(
                    model.EmployeeId,
                    model.EmployeeName,
                    model.EmployeeType,
                    model.RatePerHour,
                    (int)id
                    );

                if (model.EmployeeType.Contains("accountant"))
                {
                    if (modelOld.EmployeeType.Trim().Equals(model.EmployeeType.Trim()))
                    {
                        System.Diagnostics.Debug.WriteLine(model.Accountant.CPANumber);
                        EditAccountant((int)id, model.Accountant.CPANumber);
                    } 
                    else
                    {
                        DeleteTechnician((int)id);
                        CreateAccountant(
                           (int)id,
                           model.Accountant.CPANumber
                       );
                    }
                }
                else if (model.EmployeeType.Contains("technician"))
                {
                    if (modelOld.EmployeeType.Trim().Equals(model.EmployeeType.Trim()))
                    {
                        EditTechnician((int)id, model.Technician.ACSNumber, model.Technician.Expire);
                    }
                    else
                    {
                        DeleteAccountant((int)id);
                        CreateTechnician(
                            (int)id,
                            model.Technician.ACSNumber,
                            model.Technician.Expire
                        );
                    }    
                }

                return RedirectToAction("ViewEmployees");
            }

            return View();
        }

        [HttpGet]
        [Route("Home/Delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            ViewBag.Message = "Your delete page.";
            if (id == null)
            {
                return HttpNotFound();
            }
            var model = GetEmployee(id).First();

            DeleteEmployee(id);
            try
            {
                if (model.EmployeeType.Contains("accountant"))
                {
                    DeleteAccountant(id);
                }
                else if (model.EmployeeType.Contains("technician"))
                {
                    DeleteTechnician(id);
                }
            } catch (Exception e)
            {

            }
            return RedirectToAction("ViewEmployees");
        }

        public ActionResult SignUp()
        {
            ViewBag.Message = "Your signup page.";

            Employee employee = new Employee();
            employee.Technician = new Technician
            {
                Expire = DateTime.Now
            };

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(Employee model)
        {
            if (ModelState.IsValid)
            {
                int id = CreateEmployee(
                    model.EmployeeId,
                    model.EmployeeName,
                    model.EmployeeType,
                    model.RatePerHour
                    );
                
                if(model.EmployeeType.Contains("accountant"))
                {
                    CreateAccountant(
                        id,
                        model.Accountant.CPANumber
                    );
                } 
                else if (model.EmployeeType.Contains("technician"))
                {
                    CreateTechnician(
                        id,
                        model.Technician.ACSNumber,
                        model.Technician.Expire
                    );
                }

                return RedirectToAction("ViewEmployees");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Calculate()
        {
            ViewBag.TaxValue = String.Format("{0:C}", 0);
            ViewBag.Value = String.Format("{0:C}", 0);
            return View();
        }

        [HttpPost]
        public ActionResult Calculate(int employeeId, double hours)
        {
            try
            {
                var model = GetEmployeeByEmployeeID(employeeId).First();
                Employee employee = new Employee
                {
                    EmployeeId = model.EmployeeId,
                    EmployeeName = model.EmployeeName,
                    EmployeeType = model.EmployeeType,
                    RatePerHour = model.RatePerHour
                };

                double value = employee.CalculateWage(hours);
                double taxValue = employee.CalculateTax(hours);

                ViewBag.Message = "";
                ViewBag.EmployeeId = employeeId;
                ViewBag.Hours = hours;
                ViewBag.TaxValue = String.Format("{0:C}", taxValue);
                ViewBag.Value = String.Format("{0:C}", value);
                return View();
            } 
            catch (Exception e)
            {
                ViewBag.Message = "User not found";
                ViewBag.TaxValue = String.Format("{0:C}", 0);
                ViewBag.Value = String.Format("{0:C}", 0);
                return View();
            }
        }
    }
}