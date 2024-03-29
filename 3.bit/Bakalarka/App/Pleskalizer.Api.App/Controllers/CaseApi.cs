/*
 * REST API Decoder
 *
 * REST API Versions
 *
 * OpenAPI spec version: 1.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Pleskalizer.Api.DAL.Seeds;
using Microsoft.AspNetCore.Authorization;
using IO.Swagger.Models;
using MPACKAGE.LibDomain.Configurations;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;

namespace IO.Swagger.Controllers
{


    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class CaseApiController : ControllerBase
    {

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Retrieve page of items. To retrieve the next page, pass the retrieved paging state from the previous API call to the next. NULL paging state denotes the last page.</remarks>
        /// <param name="pagingState"></param>
        /// <param name="pageSize"></param>
        /// <response code="200">Success</response>
        /// <response code="0">  | HTTP status code | Description | | - -- -- -- -- -- -- -- - | - -- -- -- -- -- | | **401** | Unauthorized | </response>
        [HttpGet]
        [Produces("application/json")]
        [Route("/api/v1/Case")]
        [SwaggerOperation("CaseGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(CaseListDTOPageQueryResultDTO), description: "Success")]
        [SwaggerResponse(statusCode: 0, type: typeof(ErrorDTO), description: "  | HTTP status code | Description | | - -- -- -- -- -- -- -- - | - -- -- -- -- -- | | **401** | Unauthorized | ")]
        public virtual IActionResult CaseGet([FromQuery]byte[] pagingState, [FromQuery]int? pageSize)
        { 
            return StatusCode(200, CaseSeeds.CaseList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="caseId"></param>
        /// <response code="200">Success</response>
        /// <response code="0">  | HTTP status code | Description | | - -- -- -- -- -- -- -- - | - -- -- -- -- -- | | **401** | Unauthorized | | **404** | Not found | </response>
        [HttpGet]
        [Route("/api/v1/Case/{caseId}")]
        
        [SwaggerOperation("CaseGet_0")]
        [SwaggerResponse(statusCode: 200, type: typeof(CaseDetailDTO), description: "Success")]
        [SwaggerResponse(statusCode: 0, type: typeof(ErrorDTO), description: "  | HTTP status code | Description | | - -- -- -- -- -- -- -- - | - -- -- -- -- -- | | **401** | Unauthorized | | **404** | Not found | ")]
        public virtual IActionResult CaseGet_0([FromRoute][Required]Guid? caseId)
        {
            var caseDetail = CaseSeeds.CaseList.Items.Find(x => x.Id == caseId);
            return StatusCode(200, caseDetail);
        }
    }
}
