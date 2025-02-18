using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.Models;
using WebApiProject.Repositories;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyOfficeController : ControllerBase
    {
        private readonly MyOfficeRepository _repository;

        public MyOfficeController(MyOfficeRepository repository)
        {
            _repository = repository;
        }

    
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MyOfficeModel model)
        {
            await _repository.InsertAsync(model);
            return Ok(new { message = " 新增成功！" });
        }

   
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _repository.GetAllAsync();
            return Ok(data);
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MyOfficeModel model)
        {
            model.ID = id;  
            await _repository.UpdateAsync(model);
            return Ok(new { message = " 更新成功！" });
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repository.DeleteAsync(id);
            return Ok(new { message = " 刪除成功！" });
        }
    }
}
