using FOA_Server.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FOA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BI_ChartsController : ControllerBase
    {
        // GET: api/<BI_ChartsController>
        [HttpGet("Get_RemovedPost_vs_IHRAcategory")]
        public List<Object> Get_RemovedPost_vs_IHRAcategory()
        {
            try
            {
                return BI_chart.ReadPostStatusVsIHRAcaterory();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/<BI_ChartsController>
        [HttpGet("Get_Top5KeyWordsAndHashtages")]
        public List<string> Get_Top5KeyWordsAndHashtages()
        {
            try
            {
                return BI_chart.ReadTop5KeyWordsAndHashtages();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
