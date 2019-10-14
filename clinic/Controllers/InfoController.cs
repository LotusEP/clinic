using clinic.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace clinic.Controllers
{
    public class InfoController : Controller
    {
        // GET: Info
        public ActionResult Index()
        {
            return View();
        }

        //2nd version of email ---------------------------------------------------------------------------------------------------------------------------
        [HttpGet]
        //call the sign up form with nothing in the field
        public ActionResult Signup()
        {
            //temp clinic information that will be switch for a database
            List<clinicModel> listofClinic = new List<clinicModel>();
            listofClinic.Add(new clinicModel
            {
                name = "Muslim American Social Services (MASS)",
                T = "skyty7564@yahoo.com",
                PhoneNumber = 9044198006,
                Address = "2251 St. John Bluff Rd S. Jacksonville, FL 32246",
                Age = 18,
                Housing = "N/A",
                Income = 2082,
                Insurance = false,
                ID =1
            }); 
            listofClinic.Add(new clinicModel
            {
                name = "Mission House Jax",
                T = "N01390588@unf.edu",
                PhoneNumber = 9042416767,
                Address = "800 Shetter Ave Jacksonville Beach, FL, 32250",
                Housing = "N/A",
                Income = 2023,
                Insurance = false,
                ID = 2
            }); 


            ViewBag.Collection = listofClinic;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //call after the form is submitted
        public ActionResult Signup(Patient patient, string [] clinicT)
        {
            
            //string result = collection["clinicT"];
            string message = "Patient Email: " + patient.Email + "\n" + "Patient Phone Number: " + patient.CellPhone + "\n" + "Patient Address: " + patient.Street
           + "\n" + patient.City + "\n" + patient.State + "\n" + patient.Zip;



            try
            {
                if (ModelState.IsValid)
                {
                    var mess = new MailMessage();
                    var senderEmail = new MailAddress("[email of sender", "[name of the sender]");
                    var receiverEmail = new MailAddress(patient.Email, "Receiver");
                    var password = "[password of the sender";
                    var sub = "New Patient";
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        //look up the host and port number of the host
                        Host = "smtp-mail.outlook.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };

                    mess.From = senderEmail;
                    mess.To.Add (receiverEmail);
                    for (int i = 0; i < clinicT.Length; i++)
                    {
                        if (clinicT != null)
                        {
                            //add adiitonal email recevier, use To if you want the recevier to see who have also recevier the email, otherwise use Bcc to hider other recevier.
                            mess.Bcc.Add(clinicT[i]);
                            
                        }
                     
                    }
                    //add the subject and message to the email
                    mess.Subject = sub;
                    mess.Body = message;

                    using (smtp)
                    {
                        smtp.Send(mess);
                    }
         

                    //using (var mess = new MailMessage(senderEmail, receiverEmail)
                    //{
                    //    Subject = sub,
                    //    Body = body
                    //})        
                    //{
                    //    smtp.Send(mess);
                    //}


                    //redirect to the httpget to display the form with no data
                    return RedirectToAction("Signup");
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();

        }


        //ignore every thing below this line
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public ActionResult test()
        {
            List<clinicModel> listofClinic = new List<clinicModel>();
            listofClinic.Add(new clinicModel{name = "MASS", T = "Yes"});
            listofClinic.Add(new clinicModel { name = "Jac", T = "Yes" });


            ViewBag.Collection = listofClinic;
            return View();
        }

        // 1st version of email-----------------------------------------------------------------------------------------------------------
        public ViewResult PatientSignUp()
        {
            List<clinicModel> listofClinic = new List<clinicModel>();
            listofClinic.Add(new clinicModel { name = "MASS", T = "skyty7564@yahoo.com" });
            listofClinic.Add(new clinicModel { name = "Jac", T = "N01390588@unf.edu" });

            ViewBag.Collection = listofClinic;
            return View();
        }

        [HttpPost]
        public ActionResult PatientSignUp(string Fname, string Lname, string Email, string phone, string street, string state, string zip, string city)
        {
            //message for the body
            string message = "Patient Email: " + Email + "\n" + "Patient Phone Number: " + phone + "\n" + "Patient Address: " + street + "\n" + city + "\n" + state + "\n" + zip;

            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("[email of the sender]", "[name of the sender]");
                    var receiverEmail = new MailAddress(Email, "Receiver");
                    var password = "[password of the sender]";
                    var sub = "New Patient";
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        //look up the host and port number of the email service
                        Host = "smtp-mail.outlook.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }
    }
}