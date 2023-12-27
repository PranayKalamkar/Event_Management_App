﻿using Event_Management_App.Extension;
using Event_Management_App.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Event_Management_App.Controllers.User
{
    public class UserController : Controller
    {

        public readonly IConfiguration _configuration;
        public string connectionString;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpModel sign)
        {
            using(MySqlConnection connection = new MySqlConnection(connectionString))
            {
                const string query = "Insert into SignUp(Username,Email,SignUpPassword,ConfirmSignUpPassword) values (@Username,@Email,@SignUpPassword,@ConfirmSignUpPassword);";
                using(MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Username", sign.Username);
                    command.Parameters.AddWithValue("@Email", sign.Email);
                    command.Parameters.AddWithValue("@SignUpPassword", sign.SignUpPassword);
                    command.Parameters.AddWithValue("@ConfirmSignUpPassword", sign.ConfirmSignUpPassword);
                    command.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (IsValidUser(username, password))
            {
                // Redirect to the dashboard or another secure page
                return RedirectToAction("Dashboard","Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username or password";
                return View();
            }
           
        }

        private bool IsValidUser(string username, string password)
        {
            using(MySqlConnection connection = new MySqlConnection(connectionString))
            {
                const string query = "select count(*) from SignUp where Username=@Username AND  ConfirmSignUpPassword=@ConfirmSignUpPassword";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@ConfirmSignUpPassword", password);

                    long count = (long)command.ExecuteScalar(); // Use long to accommodate larger values

                    return count > 0;

                    //object result = command.ExecuteScalar();

                    //if (result != null)
                    //{
                    //    int count = Convert.ToInt32(result);
                    //    return count > 0;
                    //}

                    //return false;
                }
            }
        }

    }
}
