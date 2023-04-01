﻿//using AutoMapper;
//using FTS.Model.Account;
//using FTS.Model.Entities;
//using Email;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.DotNet.Scaffolding.Shared.Messaging;

//namespace FTS_Web.Controllers
//{
    




//    public class UserLoginControlller : Controller
//    {
//        private readonly IMapper _mapper;
//        private readonly UserManager<User> _userManager;
//        private readonly SignInManager<User> _signInManager;
//        private readonly IEmailSender _emailSender;

//        public UserLoginControlller(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender)
//        {
//            _mapper = mapper;
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _emailSender = emailSender;
//        }

//        [HttpGet]
//        public IActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> EmployeeRegister(EmployeeMasterModel employeeMasterModel)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(employeeMasterModel);
//            }

//            var user = _mapper.Map<User>(employeeMasterModel);

//            var result = await _userManager.CreateAsync(user, employeeMasterModel.Password);
//            if (!result.Succeeded)
//            {
//                foreach (var error in result.Errors)
//                {
//                    ModelState.TryAddModelError(error.Code, error.Description);
//                }

//                return View(employeeMasterModel);
//            }

//            await _userManager.AddToRoleAsync(user, "Visitor");

//            return RedirectToAction(nameof(HomeController.Index), "Home");
//        }

//        [HttpGet]
//        public IActionResult Login(string returnUrl = null)
//        {
//            ViewData["ReturnUrl"] = returnUrl;
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Login(userloginModel userModel, string returnUrl = null)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(userModel);
//            }

//            Random rand = new Random();
//            int random = rand.Next(100000,999999);

//            var result = await _signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, userModel.RememberMe, false);
//            if (result.Succeeded)
//            {
//                return RedirectToLocal(returnUrl);
//            }
//            else
//            {
//                ModelState.AddModelError("", "Invalid UserName or Password");
//                return View();
//            }
//        }

       

//        [HttpGet]
//        public IActionResult ForgotPassword()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> ForgotPassword(FTS.Model.Entities.ForgotPasswordModel forgotPasswordModel)
//        {
//            if (!ModelState.IsValid)
//                return View(forgotPasswordModel);

//            var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
//            if (user == null)
//                return RedirectToAction(nameof(ForgotPasswordConfirmation));

//            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
//            var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);

//            var message = new Message(new string[] { user.Email }, "Reset password token", callback, null);
//            await _emailSender.SendEmailAsync(message);

//            return RedirectToAction(nameof(ForgotPasswordConfirmation));
//        }

//        public IActionResult ForgotPasswordConfirmation()
//        {
//            return View();
//        }

//        [HttpGet]
//        public IActionResult ResetPassword(string token, string email)
//        {
//            var model = new ResetPasswordModel { Token = token, Email = email };
//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
//        {
//            if (!ModelState.IsValid)
//                return View(resetPasswordModel);

//            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
//            if (user == null)
//                RedirectToAction(nameof(ResetPasswordConfirmation));

//            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
//            if (!resetPassResult.Succeeded)
//            {
//                foreach (var error in resetPassResult.Errors)
//                {
//                    ModelState.TryAddModelError(error.Code, error.Description);
//                }

//                return View();
//            }

//            return RedirectToAction(nameof(ResetPasswordConfirmation));
//        }

//        [HttpGet]
//        public IActionResult ResetPasswordConfirmation()
//        {
//            return View();
//        }

//        private IActionResult RedirectToLocal(string returnUrl)
//        {
//            if (Url.IsLocalUrl(returnUrl))
//                return Redirect(returnUrl);
//            else
//                return RedirectToAction(nameof(HomeController.Index), "Home");
//        }
//    }
//    }
