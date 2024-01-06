using MVCCRUD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCRUD.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Student(ketsTable obj) //iss controller ko reciever banadiya(edit function ke liye),Jab mai edit karu toh data iss wale form mai pahuch jae jo ki add krta hai toh muhe alag se nhi bana padega koi aur add/submit function
        {
           /* if (obj!=null)
            {*/
                return View(obj); //agar kuch edit hua toh view abb us data ke sath student view mai chalejaega
            //}
            
               /* else
                {
                    return View(); //otherxise vhi purana vala show hoga kyuki change hi nhi hua kuch
                }*/
            
        }
        [HttpPost]
        public ActionResult AddStudent(ketsTable model)//recieve karenge data
        {
            //ketsTable obj= new ketsTable();
            //obj.Name = model.Name;  //yha recieved data dedenge
            //obj.Address = model.Address;
            //obj.Age = model.Age;
            //obj.Department = model.Department;
            //obj.Salary = model.Salary;
            //obj.ketsTable.Add(obj);


            // agar sara data valid hoga tabhi submit hoega


            using (KetanDBEntities dbObj = new KetanDBEntities())
            {
                dbObj.ketsTables.Add(model);
                
                if (model.ID == 0)
                {
                    dbObj.ketsTables.Add(model);
                    dbObj.SaveChanges();
                }
                else
                {
                    dbObj.Entry(model).State=EntityState.Modified;
                    dbObj.SaveChanges();
                }
            }
            

            ModelState.Clear();
            return View("Student");
        }

        public ActionResult StudentList()
        {
            using (KetanDBEntities dbObj = new KetanDBEntities())
            {
                var res = dbObj.ketsTables.ToList(); //loading the database
                return View(res);
            }
        }
        public ActionResult Delete(int id)
        {
            using (KetanDBEntities dbObj = new KetanDBEntities())
            {
                var res = dbObj.ketsTables.Where(x => x.ID == id).First();//jab ID match hojaegi humari parameter wali id se
                dbObj.ketsTables.Remove(res);
                dbObj.SaveChanges();
                var List = dbObj.ketsTables.ToList();//referesh hokr
                return View("StudentList",List);//new record dikhade
            }
                
        }
    }
}