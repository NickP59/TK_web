using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System.Composition;
using tk_web.DAL.Interfaces;
using tk_web.Domain.Extension;
using tk_web.Domain.Models;
using tk_web.Domain.ViewModels.Booking;
using tk_web.Models;
using tk_web.Service;
using tk_web.Service.Implementations;
using tk_web.Service.Interfaces;


namespace tk_web.Controllers
{

    public class BookingController : Controller
    {

        private readonly IBookingService _bookingService;
        private readonly IEquipmentService _equipmentService;
        private readonly IEventService _eventService;
        private readonly IParticipantService _participantService;
        private readonly ReportService _report;


        public BookingController(IBookingService bookingService, IEquipmentService equipmentService, IEventService eventService, IParticipantService participantService, ReportService report)
        {
            _bookingService = bookingService;
            _eventService = eventService;
            _equipmentService = equipmentService;
            _participantService = participantService;
            _report = report;
        }

        [HttpGet]
        public IActionResult ShowBookings()
        {
            var response =  _bookingService.GetBookings();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }

        public async Task<IActionResult> DeleteBooking(int id)
        {
            var response = await _bookingService.DeleteBooking(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("ShowBookings");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
            {
                return PartialView("BookingForm");
            }

            var response = await _bookingService.GetBooking(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView("BookingForm", response.Data);
            }

            ModelState.AddModelError("", response.Description);
            return PartialView("BookingForm");
        }

        [HttpPost]
        public async Task<IActionResult> Save(BookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = model.Id == 0 ? await _bookingService.CreateBooking(model) : await _bookingService.EditBooking(model);

                //if (model.Id == 0)
                //{
                //    var response = await _bookingService.CreateBooking(model);
                //}
                //else
                //{
                //    var response = await _bookingService.EditBooking(model);
                //}
                //return RedirectToAction("ShowBookings");

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

        //[HttpGet]
        //public async Task<ActionResult> GetBooking(int id, bool isJson)
        //{
        //    var response = await _bookingService.GetBooking(id);
        //    if (isJson)
        //    {
        //        return Json(response.Data);
        //    }

        //    return PartialView("BookingForm", response.Data);
        //}

        //[HttpGet]
        //public async Task<ActionResult> GetParticipant(int id, bool isJson)
        //{
        //    var response = await _participantService.GetParticipant(id);
        //    if (isJson)
        //    {
        //        return Json(response.Data);
        //    }

        //    return PartialView("BookingForm", response.Data);
        //}

        [HttpPost]
        public async Task<IActionResult> GetParticipant(string term, int page = 1, int pageSize = 5)
        {
            var response = await _participantService.GetParticipant(term);
            return Json(response.Data);
        }

        //[HttpPost]
        //public JsonResult GetParticipantNames()
        //{
        //    var types = _participantService.GetParticipantsNames();
        //    return Json(types.Data);
        //}

        //[HttpGet]
        //public async Task<ActionResult> GetEquipment(int id, bool isJson)
        //{
        //    var response = await _equipmentService.GetEquipment(id);
        //    if (isJson)
        //    {
        //        return Json(response.Data);
        //    }

        //    return PartialView("BookingForm", response.Data);
        //}

        [HttpPost]
        public async Task<IActionResult> GetEquipment(string term, int page = 1, int pageSize = 5)
        {
            var response = await _equipmentService.GetEquipment(term);
            return Json(response.Data);
        }

        //[HttpGet]
        //public async Task<ActionResult> GetEvent(int id, bool isJson)
        //{
        //    var response = await _eventService.GetEvent(id);
        //    if (isJson)
        //    {
        //        return Json(response.Data);
        //    }

        //    return PartialView("BookingForm", response.Data);
        //}

        [HttpPost]
        public async Task<IActionResult> GetEvent(string term, int page = 1, int pageSize = 5)
        {
            var response = await _eventService.GetEvent(term);
            return Json(response.Data);
        }

        [HttpGet]
        public async void CreateReport()
        {
             _report.CreateExcel();
        }
    }
}
