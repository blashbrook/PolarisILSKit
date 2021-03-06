﻿using Clc.Polaris.Api.Helpers;
using Clc.Polaris.Api.Models;
using System;
using System.Net.Http;
using System.Xml.Linq;


namespace Clc.Polaris.Api
{
	public partial class PapiClient
	{
        /// <summary>
        /// Uses patron supplied credentials to suspend a hold request.
        /// </summary>
        /// <param name="barcode">The patron's barcode.</param>
        /// <param name="requestId">The ID of the hold request.</param>
        /// <param name="activationDate">The date the hold request will become active.</param>
        /// <param name="password">The patron's password.</param>
        /// <param name="userId">The ID of the user processing the request..</param>
        /// <returns>An error code and message from the API, if any.</returns>
        /// <seealso cref="HoldRequestCancelResult"/>
        public PapiResponse<HoldRequestActivationResult> HoldRequestSuspend(string barcode, string password, int requestId, DateTime activationDate, int userId = 1)
        {
            var xml = HoldRequestHelper.BuildActivationXml(userId, activationDate);
            var url = $"/PAPIService/REST/public/v1/1033/100/1/patron/{barcode}/holdrequests/{requestId}/inactive";
            return Execute<HoldRequestActivationResult>(HttpMethod.Put, url, pin: password, body: xml);
        }

        /// <summary>
        /// Uses staff credentials to suspend a hold request for a user.
        /// </summary>
        /// <param name="barcode">The patron's barcode.</param>
        /// <param name="requestId">The ID of the hold request.</param>
        /// <param name="activationDate">The date the hold request will become active.</param>
		/// <param name="userId">The ID of the user processing the request..</param>
        /// <returns>An error code and message from the API, if any.</returns>
        /// <seealso cref="HoldRequestCancelResult"/>
        public PapiResponse<HoldRequestActivationResult> HoldRequestSuspendOverride(string barcode, int requestId, DateTime activationDate, int userId = 1)
        {
            var xml = HoldRequestHelper.BuildActivationXml(userId, activationDate);
            var url = $"/PAPIService/REST/public/v1/1033/100/1/patron/{barcode}/holdrequests/{requestId}/inactive";
            return OverrideExecute<HoldRequestActivationResult>(HttpMethod.Put, url, body: xml);
        }
    }
}