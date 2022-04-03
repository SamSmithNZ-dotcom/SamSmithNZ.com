﻿using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Web.Models.MandMCounter;
using SamSmithNZ.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Controllers
{
    public class MandMCounterController : Controller
    {
        private readonly IMandMCounterServiceAPIClient _ServiceApiClient;
 
        public MandMCounterController(IMandMCounterServiceAPIClient ServiceApiClient)
        {
            _ServiceApiClient = ServiceApiClient;
        }

        public async Task<IActionResult> Index(string volumeUnit = null,  
            float? quantity = null, 
            string containerUnit = null,
            float? height = null, 
            float? width = null, 
            float? length = null,
            float? radius = null,
            float? mandMResult = null, 
            float? peanutMandMResult = null, 
            float? skittlesResult = null)
        {
            List<string> unitsForVolume = await _ServiceApiClient.GetUnitsForVolume();
            List<string> unitsForContainer = await _ServiceApiClient.GetUnitsForContainer();
            IndexViewModel model = new(unitsForVolume, unitsForContainer);
            if (volumeUnit != null)
            {
                model.VolumeUnit = volumeUnit;
            }
            if (quantity != null)
            {
                model.Quantity = (float)quantity;
            }
            if (containerUnit != null)
            {
                model.ContainerUnit = containerUnit;
            }
            if (height != null)
            {
                model.Height = (float)height;
            }
            if (width != null)
            {
                model.Width = (float)width;
            }
            if (length != null)
            {
                model.Length = (float)length;
            }
            if (radius != null)
            {
                model.Radius = (float)radius;
            }
            if (mandMResult != null)
            {
                model.MandMResult = (float)mandMResult;
            }
            if (mandMResult != null)
            {
                model.PeanutMandMResult = (float)peanutMandMResult;
            }
            if (mandMResult != null)
            {
                model.SkittlesResult = (float)skittlesResult;
            }
            model.ActiveTab = "0";
            if (radius != null)
            {
                model.ActiveTab = "2";
            }
            else if (length != null)
            {
                model.ActiveTab = "1";
            }

            return View(model);
        }

        public async Task<IActionResult> RunVolumeCalculation(string VolumeUnit, string txtQuantity)
        {
            float mandMResult = 0f;
            float peanutMandMResult = 0f;
            float skittlesResult = 0f;
            float quantity;
            if (float.TryParse(txtQuantity, out quantity) == true)
            {
                mandMResult = await _ServiceApiClient.GetMandMDataForUnit(VolumeUnit, quantity);
                peanutMandMResult = await _ServiceApiClient.GetPeanutMandMDataForUnit(VolumeUnit, quantity);
                skittlesResult = await _ServiceApiClient.GetSkittlesDataForUnit(VolumeUnit, quantity);
            }

            return RedirectToAction("Index", new
            {
                volumeUnit = VolumeUnit,
                quantity = txtQuantity,
                mandMResult = mandMResult.ToString("0.0"),
                peanutMandMResult = peanutMandMResult.ToString("0.0"),
                skittlesResult = skittlesResult.ToString("0.0")
            });
        }

        public async Task<IActionResult> RunRectangleCalculation(string ContainerUnit, string txtHeight, string txtWidth, string txtLength)
        {
            float mandMResult = 0f;
            float peanutMandMResult = 0f;
            float skittlesResult = 0f;
            float height;
            float width;
            float length;
            if (float.TryParse(txtHeight, out height) == true &&
                float.TryParse(txtWidth, out width) == true &&
                float.TryParse(txtLength, out length) == true)
            {
                mandMResult = await _ServiceApiClient.GetMandMDataForRectangle(ContainerUnit, height, width, length);
                peanutMandMResult = await _ServiceApiClient.GetPeanutMandMDataForRectangle(ContainerUnit, height, width, length);
                skittlesResult = await _ServiceApiClient.GetSkittlesDataForRectangle(ContainerUnit, height, width, length);
            }

            return RedirectToAction("Index", new
            {
                containerUnit = ContainerUnit,
                height = txtHeight,
                width = txtWidth,
                length = txtLength,
                mandMResult = mandMResult.ToString("0.0"),
                peanutMandMResult = peanutMandMResult.ToString("0.0"),
                skittlesResult = skittlesResult.ToString("0.0")
            });
        }

        public async Task<IActionResult> RunCylinderCalculation(string ContainerUnit, string txtHeight, string txtRadius)
        {
            float mandMResult = 0f;
            float peanutMandMResult = 0f;
            float skittlesResult = 0f;
            float height;
            float radius;
            if (float.TryParse(txtHeight, out height) == true &&
                float.TryParse(txtRadius, out radius) == true)
            {
                mandMResult = await _ServiceApiClient.GetMandMDataForCylinder(ContainerUnit, height, radius);
                peanutMandMResult = await _ServiceApiClient.GetPeanutMandMDataForCylinder(ContainerUnit, height, radius);
                skittlesResult = await _ServiceApiClient.GetSkittlesDataForCylinder(ContainerUnit, height, radius);
            }

            return RedirectToAction("Index", new
            {
                containerUnit = ContainerUnit,
                height = txtHeight,
                radius = txtRadius,
                mandMResult = mandMResult.ToString("0.0"),
                peanutMandMResult = peanutMandMResult.ToString("0.0"),
                skittlesResult = skittlesResult.ToString("0.0")
            });
        }

        public IActionResult About()
        {
            return RedirectToAction("About", "Home");
        }
    }
}