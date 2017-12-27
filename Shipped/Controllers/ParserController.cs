using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using login2.Data;
using login2.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using login2.Models.AccountViewModels;
using login2.Services;

namespace login2.Controllers
{
    //[Authorize(Roles="Admin")]
    public class ParserController : Controller
    {

        private readonly ApplicationDbContext _context;
        public ParserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            List<string> li = new List<string>() { "PostKabel", "PostDrone", "PostSpelcomputer", "PostHorloge", "PostFotocamera", "PostSchoen" };

            ViewBag.listofitems = li.ToList();



            return View();
        }
        public IActionResult Kabel()
        {

            return View();
        }

        public IActionResult Drone()
        {

            return View();
        }

        public IActionResult Spelcomputer()
        {

            return View();
        }

        public IActionResult Horloge()
        {

            return View();
        }

        public IActionResult Fotocamera()
        {

            return View();
        }

        public IActionResult Schoen()
        {

            return View();
        }







        [HttpPost("UploadFiles")]
        public async Task<IActionResult> PostKabel(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    if (formFile.FileName.EndsWith(".csv"))
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                        var sr = new StreamReader(formFile.OpenReadStream());
                        while (!sr.EndOfStream)
                        {
                            var line = sr.ReadLine();
                            var data = line.Split(new[] { ',' });
                            var kabel = new Kabel()
                            {

                                Type = data[0],
                                Naam = data[1],
                                Prijs = /* Veranderen naar Float?*/int.Parse(data[2]),
                                Merk = data[3],
                                Lengte = int.Parse(data[4]),
                                Aantal = int.Parse(data[5]),
                                Afbeelding = data[6],
                                Aantal_gekocht = int.Parse(data[7]),
                                CategorieId = 1
                            };
                            _context.Kabels.Add(kabel);
                        }
                        _context.SaveChanges();
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return Ok(new { count = files.Count, size, filePath });
        }


        [HttpPost("")]
        public async Task<IActionResult> PostSwitch(List<IFormFile> files, string typeOfUploading)
        {

            switch (typeOfUploading)
            {

                case "PostDrone":
                    await PostDrone(files);
                    break;
                
                case "PostFotocamera":
                    await PostFotocamera(files);
                    break;

                case "PostHorloge":
                    await PostHorloge(files);
                    break;

                case "PostKabel":
                    await PostKabel(files);
                    break;
                
                case "PostSchoen":
                    await PostSchoen(files);
                    break;

                case "SpelComputer":
                    await PostSpelcomputer(files);
                    break;
                
                
            }


            // if (typeOfUploading == "PostDrone")
            // {
            //     await PostDrone(files);

            // }

            // else if (typeOfUploading == "PostSpelcomputer")
            // {
            //     await PostSpelcomputer(files);
            // }

            // else if (typeOfUploading == "PostHorloge")
            // {
            //     await PostHorloge(files);
            // }

            // else if (typeOfUploading == "PostSchoen"){
            //     await PostSchoen(files);
            // }





            return Ok(new { count = files.Count });
            
        }



        [HttpPost("PostDrone")]
        public async Task<IActionResult> PostDrone(List<IFormFile> files)
        {


            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    if (formFile.FileName.EndsWith(".csv"))
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                        var sr = new StreamReader(formFile.OpenReadStream());
                        while (!sr.EndOfStream)
                        {
                            var line = sr.ReadLine();
                            var data = line.Split(new[] { ',' });
                            var drone = new Drone()
                            {
                                Type = data[0],
                                Naam = data[1],
                                Prijs = /* Veranderen naar Float?*/int.Parse(data[2]),
                                Merk = data[3],
                                Kleur = data[4],
                                Aantal = int.Parse(data[5]),
                                Afbeelding = data[6],
                                Aantal_gekocht = int.Parse(data[7]),
                                CategorieId = 1
                            };

                            _context.Drones.Add(drone);
                        }

                        _context.SaveChanges();
                    }
                }
            }


            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return Ok(new { count = files.Count });
        }



        [HttpPost("PostSpelcomputer")]
        public async Task<IActionResult> PostSpelcomputer(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    if (formFile.FileName.EndsWith(".csv"))
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                        var sr = new StreamReader(formFile.OpenReadStream());
                        while (!sr.EndOfStream)
                        {
                            var line = sr.ReadLine();
                            var data = line.Split(new[] { ',' });
                            var spelcomputer = new Spelcomputer()
                            {
                                Type = data[0],
                                Naam = data[1],
                                Prijs = /* Veranderen naar Float?*/int.Parse(data[2]),
                                Merk = data[3],
                                Geheugen = int.Parse(data[4]),
                                Aantal = int.Parse(data[5]),
                                Afbeelding = data[6],
                                Aantal_gekocht = int.Parse(data[7]),
                                CategorieId = 1
                            };

                            _context.Spelcomputers.Add(spelcomputer);
                        }

                        _context.SaveChanges();
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return Ok(new { count = files.Count, size, filePath });
        }


        [HttpPost("PostHorloge")]
        public async Task<IActionResult> PostHorloge(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    if (formFile.FileName.EndsWith(".csv"))
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                        var sr = new StreamReader(formFile.OpenReadStream());
                        while (!sr.EndOfStream)
                        {
                            var line = sr.ReadLine();
                            var data = line.Split(new[] { ',' });
                            var horloge = new Horloge()
                            {

                                Type = data[0],
                                Naam = data[1],
                                Prijs = /* Veranderen naar Float?*/int.Parse(data[2]),
                                Merk = data[3],
                                Kleur = data[4],
                                Aantal = int.Parse(data[5]),
                                Afbeelding = data[6],
                                Aantal_gekocht = int.Parse(data[7]),
                                CategorieId = 1
                            };

                            _context.Horloges.Add(horloge);
                        }

                        _context.SaveChanges();
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return Ok(new { count = files.Count, size, filePath });
        }


        [HttpPost("PostFotocamera")]
        public async Task<IActionResult> PostFotocamera(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    if (formFile.FileName.EndsWith(".csv"))
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                        var sr = new StreamReader(formFile.OpenReadStream());
                        while (!sr.EndOfStream)
                        {
                            var line = sr.ReadLine();
                            var data = line.Split(new[] { ',' });
                            var fotocamera = new Fotocamera()
                            {
                                Type = data[0],
                                Naam = data[1],
                                Prijs = /* Veranderen naar Float?*/int.Parse(data[2]),
                                Merk = data[3],
                                Megapixels = data[4],
                                Aantal = int.Parse(data[5]),
                                Afbeelding = data[6],
                                Aantal_gekocht = int.Parse(data[7]),
                                CategorieId = 1
                            };

                            _context.Fotocameras.Add(fotocamera);
                        }

                        _context.SaveChanges();
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return Ok(new { count = files.Count, size, filePath });
        }






        [HttpPost("PostSchoen")]
        public async Task<IActionResult> PostSchoen(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    if (formFile.FileName.EndsWith(".csv"))
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                        var sr = new StreamReader(formFile.OpenReadStream());
                        while (!sr.EndOfStream)
                        {
                            var line = sr.ReadLine();
                            var data = line.Split(new[] { ',' });
                            var schoen = new Schoen()
                            {
                                Type = data[0],
                                Naam = data[1],
                                Prijs = /* Veranderen naar Float?*/int.Parse(data[2]),
                                Merk = data[3],
                                Kleur = data[4],
                                Aantal = int.Parse(data[5]),
                                Afbeelding = data[6],
                                Aantal_gekocht = int.Parse(data[7]),
                                Maat = int.Parse(data[8]),
                                CategorieId = 1
                            };

                            _context.Schoenen.Add(schoen);
                        }

                        _context.SaveChanges();
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return Ok(new { count = files.Count, size, filePath });
        }







    }
}
