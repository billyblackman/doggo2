﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Doggo.Models;
using Doggo.Models.ViewModels;
using Doggo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Doggo.Controllers
{
    public class DogsController : Controller
    {

        private readonly OwnerRepository _ownerRepo;
        private readonly DogRepository _dogRepo;

        public DogsController(IConfiguration config)
        {
            _ownerRepo = new OwnerRepository(config);
            _dogRepo = new DogRepository(config);
        }

        // GET: DogController1
        public ActionResult Index()
        {
            int ownerId = GetCurrentUserId();

            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(ownerId);

            return View(dogs);
        }

        // GET: DogController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DogController1/Create
        public ActionResult Create()
        {
            List<Owner> owners = _ownerRepo.GetAllOwners();

            DogFormViewModel vm = new DogFormViewModel()
            {
                Dog = new Dog(),
                Owners = owners
            };

            return View(vm);
        }

        // POST: DogController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dog dog)
        {
            try
            {
                _dogRepo.AddDog(dog);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        // GET: DogController1/Edit/5
        public ActionResult Edit(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);

            return View(dog);
        }

        // POST: DogController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Dog dog)
        {
            try
            {
                _dogRepo.UpdateDog(dog);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(dog);
            }
        }

        // GET: DogController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DogController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
