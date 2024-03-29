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
using Pleskalizer.Api.DAL.Seeds;
using Microsoft.AspNetCore.Authorization;
using IO.Swagger.Models;

namespace IO.Swagger.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class L7ApiController : ControllerBase
    { 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="caseId"></param>
        /// <param name="captureId"></param>
        /// <param name="addressA"></param>
        /// <param name="addressB"></param>
        /// <param name="protocolL4"></param>
        /// <param name="portA"></param>
        /// <param name="portB"></param>
        /// <param name="sessionId"></param>
        /// <param name="aggregateFrom"></param>
        /// <param name="aggregateTo"></param>
        /// <response code="200">Success</response>
        /// <response code="0">  | HTTP status code | Description | | - -- -- -- -- -- -- -- - | - -- -- -- -- -- | | **401** | Unauthorized | | **404** | Not found | </response>
        [HttpGet]
        [Route("/api/v1/{caseId}/L7/{captureId}/{addressA}_{addressB}/{protocolL4}/{portA}_{portB}/{sessionId}/Aggregate")]
        
        [SwaggerOperation("LAggregate")]
        [SwaggerResponse(statusCode: 200, type: typeof(L7ConversationStatisticsDetailDTO), description: "Success")]
        [SwaggerResponse(statusCode: 0, type: typeof(ErrorDTO), description: "  | HTTP status code | Description | | - -- -- -- -- -- -- -- - | - -- -- -- -- -- | | **401** | Unauthorized | | **404** | Not found | ")]
        public virtual IActionResult LAggregate([FromRoute][Required]Guid? caseId, [FromRoute][Required]Guid? captureId, [FromRoute][Required]string addressA, [FromRoute][Required]string addressB, [FromRoute][Required]string protocolL4, [FromRoute][Required]int? portA, [FromRoute][Required]int? portB, [FromRoute][Required]Guid? sessionId, [FromQuery]DateTime? aggregateFrom, [FromQuery]DateTime? aggregateTo)
        { 
            var detailModelOriginal = L7Seeds.L7DetailListSeed.FirstOrDefault(x => x.CaptureId == captureId);

            //Deep copy
            var serializedDetailModel = Newtonsoft.Json.JsonConvert.SerializeObject(detailModelOriginal);
            var detailModel = Newtonsoft.Json.JsonConvert.DeserializeObject<L7ConversationStatisticsDetailDTO>(serializedDetailModel);

            var addressATimestamp = getTimestamp(aggregateFrom ?? DateTime.Now);
            var addressBTimestamp = getTimestamp(aggregateTo ?? DateTime.Now);

            detailModel!.AToBFlowStatisticsSnapshots = detailModel.AToBFlowStatisticsSnapshots
                .Where(x =>
                    x.FirstSeenTimestampTicks >= addressATimestamp &&
                    x.LastSeenTimestampTicks <= addressBTimestamp)
                .ToList();
            detailModel.BToAFlowStatisticsSnapshots = detailModel.BToAFlowStatisticsSnapshots
                .Where(x =>
                    x.FirstSeenTimestampTicks >= addressATimestamp &&
                    x.LastSeenTimestampTicks <= addressBTimestamp)
                .ToList();

            double getTimestamp(DateTime date)
            {
                var baseDate = new DateTime(1970, 01, 01);
                return date.Subtract(baseDate).TotalSeconds;
            }

            return StatusCode(200, detailModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Retrieve page of items. To retrieve the next page, pass the retrieved paging state from the previous API call to the next. NULL paging state denotes the last page.</remarks>
        /// <param name="caseId"></param>
        /// <param name="pagingState"></param>
        /// <param name="pageSize"></param>
        /// <response code="200">Success</response>
        /// <response code="0">  | HTTP status code | Description | | - -- -- -- -- -- -- -- - | - -- -- -- -- -- | | **401** | Unauthorized | | **404** | Not found | </response>
        [HttpGet]
        [Route("/api/v1/{caseId}/L7")]
        
        [SwaggerOperation("LGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(L7ConversationStatisticsListDTOPageQueryResultDTO), description: "Success")]
        [SwaggerResponse(statusCode: 0, type: typeof(ErrorDTO), description: "  | HTTP status code | Description | | - -- -- -- -- -- -- -- - | - -- -- -- -- -- | | **401** | Unauthorized | | **404** | Not found | ")]
        public virtual IActionResult LGet([FromRoute][Required]Guid? caseId, [FromQuery]byte[] pagingState, [FromQuery]int? pageSize)
        { 
            int pageState = ByteArrToInt(pagingState);
            int _pageSize = pageSize ?? 10;
            var messages = L7Seeds.L7ListSeed.Items.Skip(pageState * _pageSize).Take(_pageSize).ToList();
            var data = new L7ConversationStatisticsListDTOPageQueryResultDTO();
            data.Items = messages;
            return StatusCode(200, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="caseId"></param>
        /// <param name="captureId"></param>
        /// <param name="addressA"></param>
        /// <param name="addressB"></param>
        /// <param name="protocolL4"></param>
        /// <param name="portA"></param>
        /// <param name="portB"></param>
        /// <param name="sessionId"></param>
        /// <response code="200">Success</response>
        /// <response code="0">  | HTTP status code | Description | | - -- -- -- -- -- -- -- - | - -- -- -- -- -- | | **401** | Unauthorized | | **404** | Not found | </response>
        [HttpGet]
        [Route("/api/v1/{caseId}/L7/{captureId}/{addressA}_{addressB}/{protocolL4}/{portA}_{portB}/{sessionId}")]
        
        [SwaggerOperation("LGet_0")]
        [SwaggerResponse(statusCode: 200, type: typeof(L7ConversationStatisticsDetailDTO), description: "Success")]
        [SwaggerResponse(statusCode: 0, type: typeof(ErrorDTO), description: "  | HTTP status code | Description | | - -- -- -- -- -- -- -- - | - -- -- -- -- -- | | **401** | Unauthorized | | **404** | Not found | ")]
        public virtual IActionResult LGet_0([FromRoute][Required]Guid? caseId, [FromRoute][Required]Guid? captureId, [FromRoute][Required]string addressA, [FromRoute][Required]string addressB, [FromRoute][Required]string protocolL4, [FromRoute][Required]int? portA, [FromRoute][Required]int? portB, [FromRoute][Required]Guid? sessionId)
        {
            var detailModel = L7Seeds.L7DetailListSeed
                .FirstOrDefault(x => x.CaptureId == captureId);
            return StatusCode(200, detailModel);
        }
        private int ByteArrToInt(byte[] byteArr)
        {
            return (Int32)(BitConverter.ToInt16(byteArr, 0));
        }
    }
}
