using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalApp.Models;

namespace HospitalApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DalController : ControllerBase
    {
        private readonly HospitalContext _context;

        public DalController(HospitalContext context)
        {
            _context = context;
        }

        // GET: api/Dal
        [HttpGet]
        public ActionResult<IEnumerable<Dal>> GetDallar()
        {
            return _context.Dallar.ToList();
        }

        // GET: api/Dal/5
        [HttpGet("{id}")]
        public ActionResult<Dal> GetDal(int id)
        {
            var dal = _context.Dallar.Find(id);

            if (dal == null)
            {
                return NotFound();
            }

            return dal;
        }

        // POST: api/Dal
        [HttpPost]
        public ActionResult<Dal> PostDal(Dal dal)
        {
            _context.Dallar.Add(dal);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetDal), new { id = dal.DalId }, dal);
        }

        // PUT: api/Dal/5
        [HttpPut("{id}")]
        public IActionResult PutDal(int id, Dal dal)
        {
            if (id != dal.DalId)
            {
                return BadRequest();
            }

            _context.Entry(dal).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Dal/5
        [HttpDelete("{id}")]
        public ActionResult<Dal> DeleteDal(int id)
        {
            var dal = _context.Dallar.Find(id);

            if (dal == null)
            {
                return NotFound();
            }

            _context.Dallar.Remove(dal);
            _context.SaveChanges();

            return dal;
        }
    }
}
