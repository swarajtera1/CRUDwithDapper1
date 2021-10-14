 using CrudService.Services;
using Dapper;
using DomainData.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;

namespace CRUDwithDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUDController : Controller
    {
        private readonly IDapper _dapper;

        public CRUDController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create(Parameters data)
        {
            ResponseObject resObject;

            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("Id", data.Id, DbType.Int32);
                dbparams.Add("Name", data.Name, DbType.String);
                dbparams.Add("Age", data.Age, DbType.Int32);
                var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_Add_Article]"
                    , dbparams,
                    commandType: CommandType.StoredProcedure));

                resObject = new ResponseObject
                {
                    Message = "Create Successfully",
                    Status = "success",
                    Data = result,
                    HttpStatusCode = HttpStatusCode.OK
                };
                return Json(resObject);

            }
            catch (System.Exception e)
            {
                resObject = new ResponseObject
                {
                    Message = "Creation Failed",
                    Status = "error",
                    Data = null,
                    HttpStatusCode = HttpStatusCode.BadRequest
                };
                return Json(resObject);
            }


        }

        [HttpGet(nameof(GetById))]
        public async Task<Parameters> GetById(int Id)
        {

            var result = await Task.FromResult(_dapper.Get<Parameters>($"Select * from [Dummy] where Id = {Id}", null, commandType: CommandType.Text));
            return result;

        }

        [HttpGet(nameof(Get))]
        public async Task<List<Parameters>> Get()
        {
            var result = await Task.FromResult(_dapper.GetAll<Parameters>($"Select * from [Dummy]", null, commandType: CommandType.Text));
            return result;

        }

        [HttpDelete(nameof(Delete))]
        public async Task<IActionResult> Delete(int Id)
        {
            ResponseObject resObject;
            try
            {
                var result = await Task.FromResult(_dapper.Get<int>($"Delete [Dummy] Where Id = {Id}", null, commandType: CommandType.Text));
                resObject = new ResponseObject
                {
                    Message = "Delete Successfully",
                    Status = "success",
                    Data = result,
                    HttpStatusCode = HttpStatusCode.OK
                };
                return Json(resObject);

            }
            catch (System.Exception e)
            {
                resObject = new ResponseObject
                {
                    Message = "Delete Failed",
                    Status = "error",
                    Data = null,
                    HttpStatusCode = HttpStatusCode.BadRequest
                };
                return Json(resObject);
            }
        }

        [HttpPatch(nameof(Update))]
        public async Task<IActionResult> Update(Parameters data)
        {
            ResponseObject resObject;
            try
            {
                var dbPara = new DynamicParameters();
                dbPara.Add("Id", data.Id);
                dbPara.Add("Name", data.Name, DbType.String);
                dbPara.Add("Age", data.Age, DbType.String);

                var updateArticle = Task.FromResult(_dapper.Update<int>("[dbo].[SP_Update_Article]",
                                dbPara,
                                commandType: CommandType.StoredProcedure));
                //  return updateArticle;
                resObject = new ResponseObject
                {
                    Message = "Updated Successfully",
                    Status = "success",
                    Data = updateArticle.Result,
                    HttpStatusCode = HttpStatusCode.OK
                };
                return Json(resObject);


            }
            catch (System.Exception e)
            {
                resObject = new ResponseObject
                {
                    Message = "Update failed",
                    Status = "error",
                    Data = null,
                    HttpStatusCode = HttpStatusCode.BadRequest
                };
                return Json(resObject);
            }

        }
    }
}