using BusinessLogicLayer.Services.QouteServiceContainer;
using DataAccessLayer.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace projectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QouteController : Controller
    {
        private readonly IQouteService _qouteService;
        public QouteController(IQouteService qouteService)
        {
            _qouteService = qouteService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="qouteDTO"></param>
        /// <returns></returns>
        [HttpPost("CreateQoute")]
        [Authorize]
        public async Task<IActionResult> Create(QouteDTO qouteDTO)
        {
            var output = await _qouteService.CreateQoute(qouteDTO);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            else
            {
                return Ok(output);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Qoute"></param>
        /// <returns></returns>
        [HttpPut("UpdateQoute")]
        [Authorize]
        public async Task<IActionResult> Update(QouteDTO qoute)
        {
            var output = await _qouteService.UpdateQoute(qoute);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            else
            {
                return Ok(output);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sermonSeriesId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteQoute")]
        [Authorize]
        public async Task<IActionResult> Delete(int qouteId)
        {
            var output = await _qouteService.DeleteQoute(qouteId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            else
            {
                return Ok(output);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllQoutes")]
        public async Task<IActionResult> GetAllQoute(bool isFiltered)
        {
            var output = await _qouteService.GetAllQoutes(isFiltered);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            else
            {
                return Ok(output.Result);
            }

        }
        
        [HttpGet("GetAllQoutesForAdmin")]
        public async Task<IActionResult> GetAllQoutesForAdmin( )
        {
            var output = await _qouteService.GetAllQoutesForAdmin( );
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            else
            {
                return Ok(output.Result);
            }

        }

        [HttpGet("GetQoute")]
        public async Task<IActionResult> GetQoute(int qouteId)
        {
            var output = await _qouteService.GetQoute(qouteId);
            if (output.IsErrorOccured)
            {
                return BadRequest();
            }
            else
            {
                return Ok(output.Result);
            }

        }
    }
}
