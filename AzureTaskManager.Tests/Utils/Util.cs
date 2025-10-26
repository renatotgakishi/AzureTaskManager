using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using Moq;
using System;
using System.Security.Claims;
using System.Collections.Specialized;
using System.Linq;

namespace AzureTaskManager.Tests.Utils
{
    // --- MOCKS CONCRETOS ---

    public class MockHttpCookies : HttpCookies
    {
        private readonly List<IHttpCookie> _addedCookies = new List<IHttpCookie>();
        public override IHttpCookie CreateNew() => Mock.Of<IHttpCookie>();
        public override void Append(IHttpCookie cookie) => _addedCookies.Add(cookie);

        public override void Append(string name, string value)
        {
            throw new NotImplementedException();
        }
    }

    // 1. Mock para a Resposta HTTP
    public class MockHttpResponseData : HttpResponseData
    {
        private HttpHeadersCollection _headers = new HttpHeadersCollection();

        public override HttpStatusCode StatusCode { get; set; }
        public override Stream Body { get; set; } = new MemoryStream();

        public override HttpHeadersCollection Headers
        {
            get => _headers;
            set => _headers = value;
        }

        public override HttpCookies Cookies { get; } = new MockHttpCookies();

        public MockHttpResponseData(FunctionContext context) : base(context)
        {
            // Opcional: Se sua versão do SDK possui o WriteStringAsync como abstrato, você pode 
            // descomentar e testar a assinatura base com CancellationToken, que é mais comum.
            // Se isso FALHAR novamente, mantenha-o COMENTADO, pois ele é um método de EXTENSÃO.
            //
            /*
            // public override Task WriteStringAsync(string value, CancellationToken cancellationToken)
            // {
            //     var bytes = Encoding.UTF8.GetBytes(value);
            //     Body.Write(bytes, 0, bytes.Length);
            //     Body.Position = 0;
            //     return Task.CompletedTask;
            // }
            */
        }
    }

    // 2. Mock para a Requisição HTTP
    public class MockHttpRequestData : HttpRequestData
    {
        private readonly Stream _body;
        private readonly FunctionContext _context;

        public override Stream Body => _body;
        public override HttpHeadersCollection Headers { get; } = new HttpHeadersCollection();
        public override IReadOnlyCollection<IHttpCookie> Cookies { get; } = Array.Empty<IHttpCookie>();

        public override string Method { get; } = "POST";
        public override Uri Url { get; } = new Uri("http://localhost/api/tasks");
        public override NameValueCollection Query { get; } = new NameValueCollection();
        public override IReadOnlyCollection<ClaimsIdentity> Identities { get; } = Array.Empty<ClaimsIdentity>();

        // Retorna nosso MockHttpResponseData na criação de uma resposta
        public override HttpResponseData CreateResponse() => new MockHttpResponseData(_context);

        public MockHttpRequestData(FunctionContext context, object bodyObject) : base(context)
        {
            _context = context;
            if (bodyObject != null)
            {
                var json = JsonSerializer.Serialize(bodyObject);
                _body = new MemoryStream(Encoding.UTF8.GetBytes(json));
            }
            else
            {
                _body = new MemoryStream();
            }
        }
    }

    // 3. Classe de Fábrica (Utility Class)
    public static class HttpRequestDataFactory
    {
        public static FunctionContext CreateMockFunctionContext()
        {
            return new Mock<FunctionContext>().Object;
        }

        public static HttpRequestData Create(FunctionContext context, object bodyObject)
        {
            return new MockHttpRequestData(context, bodyObject);
        }
    }
}