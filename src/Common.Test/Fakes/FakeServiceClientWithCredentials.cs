﻿//
// Copyright (c) Microsoft.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Rest;

namespace Microsoft.Azure.Common.Test.Fakes
{
    public class FakeServiceClientWithCredentials : ServiceClient<FakeServiceClientWithCredentials>
    {
        private Uri _baseUri;
        private SubscriptionCloudCredentials _credentials;

        /// <summary>
        /// Initializes a new instance of the FakeServiceClientWithCredentials class.
        /// </summary>
        private FakeServiceClientWithCredentials()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the FakeServiceClientWithCredentials class.
        /// </summary>
        public FakeServiceClientWithCredentials(SubscriptionCloudCredentials credentials, Uri baseUri)
            : base()
        {
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }
            if (baseUri == null)
            {
                throw new ArgumentNullException("baseUri");
            }
            this._credentials = credentials;
            this._baseUri = baseUri;

            this.Credentials.InitializeServiceClient(this);
        }

        /// <summary>
        /// Initializes a new instance of the FakeServiceClientWithCredentials class.
        /// </summary>
        public FakeServiceClientWithCredentials(SubscriptionCloudCredentials credentials, Uri baseUri, HttpClientHandler rootHandler, params DelegatingHandler[] handlers)
            : base(rootHandler, handlers)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }
            if (baseUri == null)
            {
                throw new ArgumentNullException("baseUri");
            }
            this._credentials = credentials;
            this._baseUri = baseUri;

            this.Credentials.InitializeServiceClient(this);
        }

         /// <summary>
        /// Initializes a new instance of the FakeServiceClientWithCredentials class.
        /// </summary>
        public FakeServiceClientWithCredentials(SubscriptionCloudCredentials credentials, Uri baseUri, params DelegatingHandler[] handlers)
            : base(handlers)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }
            if (baseUri == null)
            {
                throw new ArgumentNullException("baseUri");
            }
            this._credentials = credentials;
            this._baseUri = baseUri;

            this.Credentials.InitializeServiceClient(this);
        }

        /// <summary>
        /// Initializes a new instance of the FakeServiceClientWithCredentials class.
        /// </summary>
        public FakeServiceClientWithCredentials(SubscriptionCloudCredentials credentials, params DelegatingHandler[] handlers)
            : base(handlers)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }
            this._credentials = credentials;
            this._baseUri = new Uri("https://TBD");

            this.Credentials.InitializeServiceClient(this);
        }

                /// <summary>
        /// Initializes a new instance of the FakeServiceClientWithCredentials class.
        /// </summary>
        public FakeServiceClientWithCredentials(SubscriptionCloudCredentials credentials, HttpClientHandler rootHandler, params DelegatingHandler[] handlers)
            : base(rootHandler, handlers)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }
            this._credentials = credentials;
            this._baseUri = new Uri("https://TBD");

            this.Credentials.InitializeServiceClient(this);
        }

        protected override void InitializeHttpClient(HttpClientHandler httpMessageHandler, params DelegatingHandler[] handlers)
        {
            base.InitializeHttpClient(httpMessageHandler, handlers);
            HttpClient.Timeout = TimeSpan.FromSeconds(300);
        }

        public Uri BaseUri
        {
            get { return _baseUri; }
        }

        public SubscriptionCloudCredentials Credentials
        {
            get { return _credentials; }
        }

        
        public async Task<HttpResponseMessage> DoStuff()
        {
            // Construct URL
            string url = "http://www.microsoft.com";
            
            // Create HTTP transport objects
            HttpRequestMessage httpRequest = null;
            
            httpRequest = new HttpRequestMessage();
            httpRequest.Method = HttpMethod.Get;
            httpRequest.RequestUri = new Uri(url);

            await this.Credentials.ProcessHttpRequestAsync(httpRequest, new CancellationToken());
                
            // Set Headers
            httpRequest.Headers.Add("x-ms-version", "2013-11-01");
                
            // Set Credentials
            var cancellationToken = new CancellationToken();
            cancellationToken.ThrowIfCancellationRequested();
            return await HttpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
        }
    }
}
