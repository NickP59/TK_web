using Microsoft.AspNetCore.Mvc;
using tk_web.Service.Interfaces;
using tk_web.Service;
using tk_web.Service.Implementations;
using tk_web.Domain.ViewModels.Booking;
using tk_web.Domain.ViewModels.Equipment;
using tk_web.Domain.Extension;

namespace tk_web.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly IEquipmentService _equipmentService;
        private readonly IEquipmentPlaceService _equipmentPlaceService;
        private readonly IEquipmentTypeService _equipmentTypeService;
        private readonly ReportService _report;

        public EquipmentController(IEquipmentService equipmentService, IEquipmentPlaceService equipmentPlaceService, IEquipmentTypeService equipmentTypeService, ReportService report)
        {
            _equipmentService = equipmentService;
            _equipmentPlaceService = equipmentPlaceService;
            _equipmentTypeService = equipmentTypeService;
            _report = report;
        }

        [HttpGet]
        public IActionResult ShowEquipments()
        {
            var response = _equipmentService.GetEquipments();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }

        public async Task<IActionResult> DeleteEquipment(int id)
        {
            var response = await _equipmentService.DeleteEquipment(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("ShowEquipments");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
            {
                return PartialView("EquipmentForm");
            }

            var response = await _equipmentService.GetEquipment(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView("EquipmentForm", response.Data);
            }

            ModelState.AddModelError("", response.Description);
            return PartialView("EquipmentForm");
        }

        [HttpPost]
        public async Task<IActionResult> Save(EquipmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = model.Id == 0 ? await _equipmentService.CreateEquipment(model) : await _equipmentService.EditEquipment(model);

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
        public async Task<ActionResult> GetEquipmentType(int id, bool isJson)
        {
            var response = await _equipmentTypeService.GetEquipmentType(id);
            if (isJson)
            {
                return Json(response.Data);
            }

            return PartialView("EquipmentForm", response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> GetEquipmentType(string term, int page = 1, int pageSize = 5)
        {
            var response = await _equipmentTypeService.GetEquipmentType(term);
            return Json(response.Data);
        }

        [HttpGet]
        public async Task<ActionResult> GetEquipmentPlace(int id, bool isJson)
        {
            var response = await _equipmentTypeService.GetEquipmentType(id);
            if (isJson)
            {
                return Json(response.Data);
            }

            return PartialView("EquipmentForm", response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> GetEquipmentPlace(string term, int page = 1, int pageSize = 5)
        {
            var response = await _equipmentTypeService.GetEquipmentType(term);
            return Json(response.Data);
        }
    }
}
