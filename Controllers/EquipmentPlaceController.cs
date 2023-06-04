using Microsoft.AspNetCore.Mvc;
using tk_web.Domain.ViewModels.Booking;
using tk_web.Service.Interfaces;
using tk_web.Service;
using tk_web.Service.Implementations;
using tk_web.Domain.Extension;
using tk_web.Domain.ViewModels.EquipmentPlace;

namespace tk_web.Controllers
{
    public class EquipmentPlaceController : Controller
    {
        private readonly IEquipmentPlaceService _equipmentPlaceService;       
        private readonly ReportService _report;


        public EquipmentPlaceController(IEquipmentPlaceService equipmentPlaceService, ReportService report)
        {
            _equipmentPlaceService = equipmentPlaceService;
            _report = report;
        }

        [HttpGet]
        public IActionResult ShowEquipmentPlaces()
        {
            var response = _equipmentPlaceService.GetEquipmentPlaces();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }

        public async Task<IActionResult> DeleteEquipmentPlace(int id)
        {
            var response = await _equipmentPlaceService.DeleteEquipmentPlace(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("ShowEquipmentPlaces");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
            {
                return PartialView("EquipmentPlaceForm");
            }

            var response = await _equipmentPlaceService.GetEquipmentPlace(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView("EquipmentPlaceForm", response.Data);
            }

            ModelState.AddModelError("", response.Description);
            return PartialView("EquipmentPlaceForm");
        }

        [HttpPost]
        public async Task<IActionResult> Save(EquipmentPlaceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = model.Id == 0 ? await _equipmentPlaceService.CreateEquipmentPlace(model) : await _equipmentPlaceService.EditEquipmentPlace(model);

                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return Json(new { description = response.Description });
                }
                return BadRequest(new { errorMessage = response.Description });
            }

            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().Join();
            return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage });
        }

        [HttpGet]
        public async void CreateReport()
        {
            _report.CreateExcel();
        }
    }
}
