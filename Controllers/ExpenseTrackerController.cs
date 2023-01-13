using Expense_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Expense_Tracker.Models.Services;

namespace Expense_Tracker.Controllers
{
    public class ExpenseTrackerController : Controller
    {
        private AllRepository<categories> AllRepository1;
        private AllRepository<expenses> AllRepository2;
        private AllRepository<TotalLimit> AllRepository3;
        // GET: ExpenseTracker

        public ExpenseTrackerController()
        {
            AllRepository1 = new AllRepository<categories>();
            AllRepository2 = new AllRepository<expenses>();
            AllRepository3 = new AllRepository<TotalLimit>();
        }
        //All Operation On categories Table
        //Get the list of categories 
        public ActionResult catList()
        {
            ViewBag.total_exp = AllRepository1.GetAll().Sum(m => m.cexpenselimit);

            return View(from cat in AllRepository1.GetAll() select cat);
        }

        //Add the categories 
        [HttpGet]
        public ActionResult CreateCat()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCat(categories categories)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AllRepository1.Insert(categories);
                    AllRepository1.SaveAll();
                    TempData["Message"] = "Categories Added..";
                    return RedirectToAction("catList");

                }
            }
            catch (Exception ex) { return View(ex); }
            return View();
        }

        //Edit the categories

        [HttpGet]

        public ActionResult EditCat(long? id)
        {
            if(id == null)
            {
                return RedirectToAction("CreateCat");
            }else
            {
                categories cat = AllRepository1.GetByID(id);
                return View(cat);
            }

        }
        [HttpPost]
        public ActionResult EditCat(long id, categories cat)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AllRepository1.Update(cat);
                    AllRepository1.SaveAll();
                    TempData["Message"] = "Updated..";
                    return RedirectToAction("catList");
                }
            }
            catch
            {
                return View();
            }
            return View();

        }

        //Delete the categories

        public ActionResult DeleteCat(long? id)
        {if(id == null)
            {
                return RedirectToAction("catList");
            }else
            {
                AllRepository1.Delete(id);
                AllRepository1.SaveAll();
                return RedirectToAction("catList");

            }
        }

        //Expense Operations

        //Adding values into the expense list 

        [HttpGet]
        public ActionResult CreateExpenses()
        {
            ViewBag.cid = new SelectList(AllRepository1.GetAll(), "cid", "cname");
            return View();
        }
        [HttpPost]
        public ActionResult CreateExpenses(expenses expenses)
        {
            ViewBag.cid = new SelectList(AllRepository1.GetAll(), "cid", "cname");
            try
            {
                if (ModelState.IsValid)
                {
                    expenses.date = DateTime.Now;
                    AllRepository2.Insert(expenses);
                    AllRepository2.SaveAll();
                    TempData["Message"] = "Expenses Added..";
                    return RedirectToAction("ExpensesList");
                }
            }
            catch { return View(); }
            return View();

        }

        //list of Expenses

        public ActionResult ExpensesList()
        {
            ViewBag.remainingamount = AllRepository1.GetAll().Sum(c => c.cexpenselimit) - AllRepository2.GetAll().Sum(e => e.amount);
            return View(from ex in AllRepository2.GetAll() select ex);
        }

        //Edit expense list 
        [HttpGet]
        public ActionResult EditExpenses(long? id)
        {
            if (id == null)
            {
                return RedirectToAction("CreateExpenses");
            }else
            {

                ViewBag.cid = new SelectList(AllRepository1.GetAll(), "cid", "cname");
                expenses eList = AllRepository2.GetByID(id);
                return View(eList);
            }
        }
        [HttpPost]

        public ActionResult EditExpenses(long? id, expenses expenses)
        {
            ViewBag.cid = new SelectList(AllRepository1.GetAll(), "cid", "cname");
            try
            {
                if (ModelState.IsValid)
                {
                    expenses.date = DateTime.Now;
                    AllRepository2.Update(expenses);
                    AllRepository2.SaveAll();
                    TempData["Message"] = "Updated..";
                    return RedirectToAction("ExpensesList");

                }

            }
            catch
            {
                return View();
            }
            return View();
        }

        // Delete the data from the expenses
        public ActionResult DeleteExpenses(long? id)
        {
            if(id == null)
            {
                return RedirectToAction("ExpensesList");
            }
            else
            { 
                AllRepository2.Delete(id);
                AllRepository2.SaveAll();
                TempData["Message"] = "Deleted..";
                return RedirectToAction("ExpensesList");
            }
        }


        public ActionResult Test()
        {
            return View();
        }


    //dashboard
        public ActionResult FilterData()
        {
            var te = AllRepository3.GetAll().Sum(t => t.totalExpenselimit);
            var totalc = AllRepository1.GetAll().Sum(c => c.cexpenselimit); ;
            if(totalc > te)
            {
                ViewBag.msg = "Update Your Expense Limit ";
            }
            ViewBag.TotalExpense = AllRepository3.GetAll().Sum(t => t.totalExpenselimit);
            ViewBag.total = AllRepository1.GetAll().Sum(c => c.cexpenselimit);
            ViewBag.remainingamount = AllRepository3.GetAll().Sum(t => t.totalExpenselimit) - AllRepository1.GetAll().Sum(c => c.cexpenselimit);



            /*            ViewBag.remainingamount = AllRepository1.GetAll().Sum(c => c.cexpenselimit) - AllRepository2.GetAll().Sum(e => e.amount);*/




            var list = (from c in AllRepository1.GetAll()
                        join e in AllRepository2.GetAll() on c.cid equals e.cid into table1
                        from e in table1.ToList()
                        select new ViewModel
                        {
                            cat = c,
                            exp = e
                        });
            /*            var list2 = from e in AllRepository2.GetAll()
                                    join c in AllRepository1.GetAll() on e.cid equals c.cid into table2
                                    from c in table2.ToList()
                                    select new ViewModel
                                    {
                                        cat = c,
                                        exp = e

                                    };
            */
            /*var list2 = from e in AllRepository2.GetAll()
                        join c in AllRepository1.GetAll() on e.cid equals c.cid into table2
                        from c in table2.ToList()
                        select new ViewModel
                        {
                            cat = c,
                            exp = e
                        };*/
            /*            list = list.Union(list2);*/
            return View(list);
            
            
        }
        //for filtering the data when user click of specific category
        public ActionResult FilterData2(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("FilterData");
            }
            else
            {
                var tcel = AllRepository1.GetByID(id).cexpenselimit;
                var tea = AllRepository2.GetAll().Where(e => e.cid == id).Sum(e => e.amount);

                if (tea > tcel)
                {
                    ViewBag.msg = "Update your Expenses limit";
                }

                ViewBag.amount = AllRepository1.GetByID(id).cexpenselimit;
                ViewBag.remaining2 = AllRepository1.GetByID(id).cexpenselimit - AllRepository2.GetAll().Where(e => e.cid == id).Sum(e => e.amount);
                var list2 = from e in AllRepository2.GetAll()
                            join c in AllRepository1.GetAll() on e.cid equals c.cid into table1
                            from c in table1.Where(a => a.cid == id).ToList()

                            select new ViewModel
                            {
                                cat = c,
                                exp = e
                            };

                return View(list2);
            }


        }
                
        
        /// <summary>
        /// for add a total expense limit 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateTotalLimit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTotalLimit(TotalLimit totalExpenses)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AllRepository3.Insert(totalExpenses);
                    AllRepository3.SaveAll();
                    TempData["Message"] = "Updated..Total limit....";
                    return RedirectToAction("FilterData");
                }
            }
            catch
            {
                return View();
            }

            return View();
        }
    }
}