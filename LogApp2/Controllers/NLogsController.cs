using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogApp2.Models;
using Microsoft.Data.SqlClient;
using System.Web.Http.Description;

namespace LogApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NLogsController : ControllerBase
    {
        private readonly Context _context;

        public NLogsController(Context context)
        {
            _context = context;
        }

        // GET: api/NLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogApp2.Models.NLogs>>> GetNLog()
        {
            return await _context.NLogs.ToListAsync();
        }

        // GET: api/NLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogApp2.Models.NLogs>> GetNLog(int id)
        {
            var nLog = await _context.NLogs.FindAsync(id);

            if (nLog == null)
            {
                return NotFound();
            }

            return nLog;
        }

        // PUT: api/NLogs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNLog(int id, LogApp2.Models.NLogs nLog)
        {
            if (id != nLog.Id)
            {
                return BadRequest();
            }

            _context.Entry(nLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NLogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/NLogs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<LogApp2.Models.NLogs>> PostNLog(LogApp2.Models.NLogs nLog)
        {
            _context.NLogs.Add(nLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNLog", new { id = nLog.Id }, nLog);
        }

        // DELETE: api/NLogs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LogApp2.Models.NLogs>> DeleteNLog(int id)
        {
            var nLog = await _context.NLogs.FindAsync(id);
            if (nLog == null)
            {
                return NotFound();
            }

            _context.NLogs.Remove(nLog);
            await _context.SaveChangesAsync();

            return nLog;
        }

        private bool NLogExists(int id)
        {
            return _context.NLogs.Any(e => e.Id == id);
        }

        [HttpGet("{id}/{yroven}/{text}/{start}/{end}")]
        public async Task<ActionResult<IEnumerable<NLogs>>> Search(int id, string yroven, string text, string start, string end)
        {
            IQueryable<NLogs> query = _context.NLogs;
            if (query == null)
            {
                return NotFound();
            }
            if (id != 0)
                query = query.Where(e => e.Id==id);
            if (!string.IsNullOrEmpty(yroven) && yroven != "null")
            {
                query = query.Where(e => e.Level.Contains(yroven));
            }
            if (!string.IsNullOrEmpty(text) && text != "null")
            {
                query = query.Where(e => e.Logger == text || e.Message == text);
            }
            if (start != null && start != "null")
            {
                query = query.Where(e => e.Logged >= Convert.ToDateTime(start));
            }
            if (end != null && end != "null")
            {
                query = query.Where(e => e.Logged <= Convert.ToDateTime(end).AddDays(1));
            }

            return await query.ToListAsync();
        }


    }
}
