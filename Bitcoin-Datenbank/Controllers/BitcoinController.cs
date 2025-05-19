using AksicDavid_LB_M295.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AksicDavid_LB_M295.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/bitcoin")]
    public class BitcoinController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly CoinMarketCapService _coinService;

        public BitcoinController(ApplicationDbContext context, CoinMarketCapService coinService)
        {
            _context = context;
            _coinService = coinService;
        }

        //  CREATE - Neuen Bitcoin Holding hinzufügen
        [HttpPost("holdings")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddHolding([FromBody] BitcoinHolding holding)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Prüfen, ob eine Holding mit derselben ID existiert
            var existingHolding = await _context.BitcoinHoldings.FindAsync(holding.Id);
            if (existingHolding != null)
            {
                return Conflict(new { message = $"Eine Holding mit der ID {holding.Id} existiert bereits." });
            }

            _context.BitcoinHoldings.Add(holding);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHoldingById), new { id = holding.Id }, holding);
        }

        // READ - Alle Bitcoin Holdings abrufen
        [HttpGet("holdings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<BitcoinHolding>>> GetHoldings()
        {
            var holdings = await _context.BitcoinHoldings.ToListAsync();
            if (holdings == null || holdings.Count == 0)
            {
                return NotFound("Keine Holdings gefunden.");
            }
            return Ok(holdings);
        }

        // READ - Einzelnen Bitcoin Holding abrufen
        [HttpGet("holdings/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BitcoinHolding>> GetHoldingById(int id)
        {
            var holding = await _context.BitcoinHoldings.FindAsync(id);
            if (holding == null)
            {
                return NotFound($"Keine Holding mit der ID {id} gefunden.");
            }
            return Ok(holding);
        }

        // UPDATE - Existierenden Bitcoin Holding aktualisieren
        [HttpPut("holdings/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateHolding(int id, [FromBody] BitcoinHolding updatedHolding)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedHolding.Id)
            {
                return BadRequest("Die ID in der URL stimmt nicht mit der ID in der Anfrage überein.");
            }

            var existingHolding = await _context.BitcoinHoldings.FindAsync(id);
            if (existingHolding == null)
            {
                return NotFound($"Keine Holding mit der ID {id} gefunden.");
            }

            existingHolding.Amount = updatedHolding.Amount;
            existingHolding.PriceAtPurchase = updatedHolding.PriceAtPurchase;
            existingHolding.PurchaseDate = updatedHolding.PurchaseDate;

            await _context.SaveChangesAsync();
            return Ok(existingHolding);
        }

        // DELETE - Bitcoin Holding löschen
        [HttpDelete("holdings/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteHolding(int id)
        {
            var holding = await _context.BitcoinHoldings.FindAsync(id);
            if (holding == null)
            {
                return NotFound($"Keine Holding mit der ID {id} gefunden.");
            }

            _context.BitcoinHoldings.Remove(holding);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // READ - Bitcoin Preis abrufen
        [HttpGet("price")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBitcoinPrice()
        {
            try
            {
                double price = await _coinService.GetBitcoinPrice();
                return Ok(new { price });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Interner Fehler: {ex.Message}");
            }
        }
    }
}
