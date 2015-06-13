using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.Security;
using eLibrary.DAL;
using eLibrary.Model;

namespace eLibrary.Models
{
    public class DynamicAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        //UnitOfWork work = new UnitOfWork();

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            Membership.GetUser(true);
            UnitOfWork work = new UnitOfWork();
            Roles = null;
            string UserName = "";

            // int UserNameInt;

            //  List<Person> thePerson;
            //  Person theRealPerson;

            string theControllerName;
            string theActionName;
            dynamic controllerName;
            dynamic actionName;
            try
            {
                //  Roles.GetAllRoles().ToList()
                controllerName = httpContext.Request.RequestContext.RouteData.Values["controller"];
                UserName = httpContext.User.Identity.Name;

                // System.Web.HttpContext.Current.User.Identity.
                string[] rolesArray;
                RolePrincipal r = (RolePrincipal)System.Web.HttpContext.Current.User;
                rolesArray = r.GetRoles();
                string theRoles = rolesArray[0];


                theControllerName = Convert.ToString(controllerName);
                actionName = httpContext.Request.RequestContext.RouteData.Values["action"];
                theActionName = Convert.ToString(actionName);

                // Get this string (roles) from a database or somewhere dynamic using the controllerName and actionName
                //Roles = "Role1,Role2,Role3"; // i.e.  GetRolesFromDatabase(controllerName, actionName);

                List<MyRole> theRole = work.MyRoleRepository.Get(a => a.RoleName.Equals(theRoles)).ToList();


                switch (theControllerName)
                {
                    case "Chapter":
                        string[] activities20 = theRole[0].Upload.Split('-');
                        List<string> activityList20 = new List<string>();
                        foreach (var activity in activities20)
                        {
                            if (activity.Equals("Delete"))
                            {
                                activityList20.Add("DeleteChapter");
                            }
                            if (activity.Equals("List"))
                            {
                                activityList20.Add("Index");
                            }
                            else
                            {
                                activityList20.Add(activity);
                            }

                        }

                        // activityList.Add(activities);
                        foreach (var activity in activityList20)
                        {
                            if (activity.Equals(actionName))
                            {
                                Roles = theRole[0].RoleName;
                                return base.AuthorizeCore(httpContext);
                            }
                        }

                        break;

                    case "Exam":

                        string[] activities0 = theRole[0].Exam.Split('-');
                        List<string> activityList0 = new List<string>();
                        foreach (var activity in activities0)
                        {
                            if (activity.Equals("List"))
                            {
                                activityList0.Add("Index");
                            }
                            else
                            {
                                activityList0.Add(activity);
                            }

                        }

                        // activityList.Add(activities);
                        foreach (var activity in activityList0)
                        {
                            if (activity == "Index")
                            {
                                if (actionName == "LoadExamCodes")
                                {
                                    Roles = theRole[0].RoleName;
                                    return base.AuthorizeCore(httpContext);
                                }
                                if (actionName == "Index")
                                {
                                    Roles = theRole[0].RoleName;
                                    return base.AuthorizeCore(httpContext);
                                }
                            }
                            else
                            {
                                if (activity.Equals(actionName))
                                {
                                    Roles = theRole[0].RoleName;
                                    return base.AuthorizeCore(httpContext);
                                }
                            }
                        }

                        break;


                    case "Book":

                        string[] activities1 = theRole[0].Book.Split('-');
                        List<string> activityList1 = new List<string>();
                        foreach (var activity in activities1)
                        {
                            if (activity.Equals("List"))
                            {
                                activityList1.Add("Index");
                            }
                            //if (activity.Equals("List"))
                            //{
                            //    activityList1.Add("Index");
                            //}
                            else
                            {
                                activityList1.Add(activity);
                            }

                        }
                        //  roles
                        // activityList.Add(activities);
                        foreach (var activity in activityList1)
                        {
                            if (activity == "Create")
                            {
                                if (actionName == "Create2")
                                {
                                    Roles = theRole[0].RoleName;
                                    return base.AuthorizeCore(httpContext);
                                }

                            }
                            else
                            {
                                if (activity.Equals(actionName))
                                {
                                    Roles = theRole[0].RoleName;
                                    return base.AuthorizeCore(httpContext);
                                }
                            }
                        }

                        break;


                    case "BorrowedItem":

                        string[] activities2 = theRole[0].BorrowedItem.Split('-');
                        List<string> activityList2 = new List<string>();
                        foreach (var activity in activities2)
                        {
                            if (activity.Equals("List"))
                            {
                                activityList2.Add("Index");
                            }
                            else
                            {
                                activityList2.Add(activity);
                            }

                        }

                        // activityList.Add(activities);
                        foreach (var activity in activityList2)
                        {
                            if (activity.Equals(actionName))
                            {
                                Roles = theRole[0].RoleName;
                                return base.AuthorizeCore(httpContext);
                            }
                        }

                        break;


                    case "UploadAdditionalChapterMaterial":

                        string[] activities3 = theRole[0].Upload.Split('-');
                        List<string> activityList3 = new List<string>();
                        foreach (var activity in activities3)
                        {
                            if (activity.Equals("List"))
                            {
                                activityList3.Add("Index");
                            }
                            else
                            {
                                activityList3.Add(activity);
                            }

                        }

                        // activityList.Add(activities);
                        foreach (var activity in activityList3)
                        {
                            if (activity.Equals(actionName))
                            {
                                Roles = theRole[0].RoleName;
                                return base.AuthorizeCore(httpContext);
                            }
                        }

                        break;


                    case "Shelf":

                        string[] activities9 = theRole[0].Shelf.Split('-');
                        List<string> activityList9 = new List<string>();
                        foreach (var activity in activities9)
                        {
                            if (activity.Equals("List"))
                            {
                                activityList9.Add("Index");
                            }
                            else
                            {
                                activityList9.Add(activity);
                            }

                        }

                        // activityList.Add(activities);
                        foreach (var activity in activityList9)
                        {
                            if (activity.Equals(actionName))
                            {
                                Roles = theRole[0].RoleName;
                                return base.AuthorizeCore(httpContext);
                            }
                        }

                        break;


                    case "UploadLessonNote":

                        string[] activities4 = theRole[0].Upload.Split('-');
                        List<string> activityList4 = new List<string>();
                        foreach (var activity in activities4)
                        {
                            if (activity.Equals("List"))
                            {
                                activityList4.Add("Index");
                            }
                            else
                            {
                                activityList4.Add(activity);
                            }

                        }

                        // activityList.Add(activities);
                        foreach (var activity in activityList4)
                        {
                            if (activity.Equals(actionName))
                            {
                                Roles = theRole[0].RoleName;
                                return base.AuthorizeCore(httpContext);
                            }
                        }

                        break;

                    case "UploadTextBook":

                        string[] activities5 = theRole[0].Upload.Split('-');
                        List<string> activityList5 = new List<string>();
                        foreach (var activity in activities5)
                        {
                            if (activity.Equals("List"))
                            {
                                activityList5.Add("Index");
                            }
                            else
                            {
                                activityList5.Add(activity);
                            }

                        }

                        // activityList.Add(activities);
                        foreach (var activity in activityList5)
                        {
                            if (activity.Equals(actionName))
                            {
                                Roles = theRole[0].RoleName;
                                return base.AuthorizeCore(httpContext);
                            }
                        }

                        break;


                    case "Course":

                        string[] activities99 = theRole[0].Course.Split('-');
                        List<string> activityList99 = new List<string>();
                        foreach (var activity in activities99)
                        {
                            if (activity.Equals("List"))
                            {
                                activityList99.Add("Index");
                            }
                            else
                            {
                                activityList99.Add(activity);
                            }

                        }

                        // activityList.Add(activities);
                        foreach (var activity in activityList99)
                        {
                            if (activity == "Create")
                            {
                                if (actionName == "Create2")
                                {
                                    Roles = theRole[0].RoleName;
                                    return base.AuthorizeCore(httpContext);
                                }

                            }
                            else
                            {
                                if (activity.Equals(actionName))
                                {
                                    Roles = theRole[0].RoleName;
                                    return base.AuthorizeCore(httpContext);
                                }
                            }
                        }

                        break;

                    //case "StudentFees":

                    //    string[] activities5 = theRole[0].StudentFees.Split('-');
                    //    List<string> activityList5 = new List<string>();
                    //    foreach (var activity in activities5)
                    //    {
                    //        if (activity.Equals("List"))
                    //        {
                    //            activityList5.Add("Index");
                    //        }
                    //        else
                    //        {
                    //            activityList5.Add(activity);
                    //        }

                    //    }

                    //    // activityList.Add(activities);
                    //    foreach (var activity in activityList5)
                    //    {
                    //        if (activity.Equals(actionName))
                    //        {
                    //            Roles = theRole[0].RoleName;
                    //            return base.AuthorizeCore(httpContext);
                    //        }
                    //    }

                    //    break;




                    //case "Subject":

                    //    string[] activities6 = theRole[0].Subject.Split('-');
                    //    List<string> activityList6 = new List<string>();
                    //    foreach (var activity in activities6)
                    //    {
                    //        if (activity.Equals("List"))
                    //        {
                    //            activityList6.Add("Index");
                    //        }
                    //        else
                    //        {
                    //            activityList6.Add(activity);
                    //        }

                    //    }

                    //    // activityList.Add(activities);
                    //    foreach (var activity in activityList6)
                    //    {
                    //        if (activity.Equals(actionName))
                    //        {
                    //            Roles = theRole[0].RoleName;
                    //            return base.AuthorizeCore(httpContext);
                    //        }
                    //    }

                    //    break;



                    //case "SubjectRegistration":

                    //    string[] activities7 = theRole[0].ClassSubject.Split('-');
                    //    List<string> activityList7 = new List<string>();
                    //    foreach (var activity in activities7)
                    //    {
                    //        if (activity.Equals("List"))
                    //        {
                    //            activityList7.Add("Index");
                    //        }
                    //        else
                    //        {
                    //            activityList7.Add(activity);
                    //        }

                    //    }

                    //    // activityList.Add(activities);
                    //    foreach (var activity in activityList7)
                    //    {
                    //        if (activity.Equals(actionName))
                    //        {
                    //            Roles = theRole[0].RoleName;
                    //            return base.AuthorizeCore(httpContext);
                    //        }
                    //    }

                    //    break;
                }

                //work.MyRoleRepository.Get()


                Roles = "SuperAdmin"; // i.e.  GetRolesFromDatabase(controllerName, actionName);

                return base.AuthorizeCore(httpContext);
            }
            catch (Exception e)
            {

                Roles = "SuperAdmin"; // i.e.  GetRolesFromDatabase(controllerName, actionName);

                return base.AuthorizeCore(httpContext);
            }
        }
    }
}