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
    [Authorize(Roles = "Admin")]
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
        public IActionResult Error()
        {
            List<string> li = new List<string>() { "PostKabel", "PostDrone", "PostSpelcomputer", "PostHorloge", "PostFotocamera", "PostSchoen" };

            ViewBag.listofitems = li.ToList();
            return View();
        }

        public IActionResult Succes()
        {
            List<string> li = new List<string>() { "PostKabel", "PostDrone", "PostSpelcomputer", "PostHorloge", "PostFotocamera", "PostSchoen" };

            ViewBag.listofitems = li.ToList();


            return View();
        }


        [HttpPost("UploadFiles")]
        public async Task<IActionResult> PostKabel(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            string error = "";

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
                        try
                        {
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
                        }
                        catch (FormatException ex)
                        {
                            System.Console.WriteLine(ex.Message);
                            System.Console.WriteLine("CATCHED ERRRRRRROR-------------------------------------");
                            error = "ERROR";
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            System.Console.WriteLine(e.Message);
                            System.Console.WriteLine("ERRRRRRRRRRRRRRRRORRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");
                            error = "ERROR";

                        }

                        _context.SaveChanges();
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
            }

            if (error == "ERROR")
            {

                return RedirectToAction("Error", "Parser");


            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return RedirectToAction("Succes");

        }



        [HttpPost("")]
        public Task<IActionResult> PostSwitch(List<IFormFile> files, string typeOfUploading)
        {

            // switch (typeOfUploading)
            // {

            //     case "PostDrone":
            //         await PostDrone(files);
            //         break;

            //     case "PostFotocamera":
            //         await PostFotocamera(files);
            //         break;

            //     case "PostHorloge":
            //         await PostHorloge(files);
            //         break;

            //     case "PostKabel":
            //         await PostKabel(files);
            //         break;


            //     case "PostSchoen":
            //         await PostSchoen(files);
            //         break;

            //     case "PostSpelcomputer":
            //         await PostSpelcomputer(files);
            //         break;


            // }


            //return Ok(new { count = files.Count });
            if (typeOfUploading == "PostKabel")
            {
                return PostKabel(files);
            }


            if (typeOfUploading == "PostDrone")
            {
                return PostDrone(files);
            }

            if (typeOfUploading == "PostFotocamera")
            {
                return PostFotocamera(files);
            }
            if (typeOfUploading == "PostHorloge")
            {
                return PostHorloge(files);
            }
            if (typeOfUploading == "PostSchoen")
            {
                return PostSchoen(files);
            }
            if (typeOfUploading == "PostSpelcomputer")
            {
                return PostSpelcomputer(files);
            }



            return ViewBag();
        }



        [HttpPost("PostDrone")]
        public async Task<IActionResult> PostDrone(List<IFormFile> files)
        {
            string error = "";


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
                        try
                        {


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
                        }
                        catch (FormatException ex)
                        {
                            System.Console.WriteLine(ex.Message);
                            System.Console.WriteLine("CATCHED ERRRRRRROR-------------------------------------");
                            error = "ERROR";
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            System.Console.WriteLine(e.Message);
                            System.Console.WriteLine("ERRRRRRRRRRRRRRRRORRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");
                            error = "ERROR";

                        }

                        _context.SaveChanges();
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
            }

            if (error == "ERROR")
            {
                return RedirectToAction("Error");
            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return RedirectToAction("Succes");

        }



        [HttpPost("PostSpelcomputer")]
        public async Task<IActionResult> PostSpelcomputer(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            string error = "";

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
                        try
                        {


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
                        }

                        catch (FormatException ex)
                        {
                            System.Console.WriteLine(ex.Message);
                            System.Console.WriteLine("CATCHED ERRRRRRROR-------------------------------------");
                            error = "ERROR";
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            System.Console.WriteLine(e.Message);
                            System.Console.WriteLine("ERRRRRRRRRRRRRRRRORRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");
                            error = "ERROR";

                        }


                        _context.SaveChanges();
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
            }

            if (error == "ERROR")
            {
                return RedirectToAction("Error");
            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return RedirectToAction("Succes");
        }


        [HttpPost("PostHorloge")]
        public async Task<IActionResult> PostHorloge(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            string error = "";

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
                        try
                        {


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
                        }

                        catch (FormatException ex)
                        {
                            System.Console.WriteLine(ex.Message);
                            System.Console.WriteLine("CATCHED ERRRRRRROR-------------------------------------");
                            error = "ERROR";
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            System.Console.WriteLine(e.Message);
                            System.Console.WriteLine("ERRRRRRRRRRRRRRRRORRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");
                            error = "ERROR";

                        }

                        _context.SaveChanges();
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
            }

            if (error == "ERROR")
            {
                return RedirectToAction("Error");
            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return RedirectToAction("Succes");
        }


        [HttpPost("PostFotocamera")]
        public async Task<IActionResult> PostFotocamera(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            string error = "";

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
                        try
                        {


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
                        }

                        catch (FormatException ex)
                        {
                            System.Console.WriteLine(ex.Message);
                            System.Console.WriteLine("CATCHED ERRRRRRROR-------------------------------------");
                            error = "ERROR";
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            System.Console.WriteLine(e.Message);
                            System.Console.WriteLine("ERRRRRRRRRRRRRRRRORRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");
                            error = "ERROR";

                        }

                        _context.SaveChanges();
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
            }

            if (error == "ERROR")
            {
                return RedirectToAction("Error");
            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return RedirectToAction("Succes");
        }






        [HttpPost("PostSchoen")]
        public async Task<IActionResult> PostSchoen(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            string error = "";

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
                        try
                        {


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
                        }

                        catch (FormatException ex)
                        {
                            System.Console.WriteLine(ex.Message);
                            System.Console.WriteLine("CATCHED ERRRRRRROR-------------------------------------");
                            error = "ERROR";
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            System.Console.WriteLine(e.Message);
                            System.Console.WriteLine("ERRRRRRRRRRRRRRRRORRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");
                            error = "ERROR";

                        }

                        _context.SaveChanges();
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
            }

            if (error == "ERROR")
            {
                return RedirectToAction("Error");
            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return RedirectToAction("Succes");
        }


    }
}
