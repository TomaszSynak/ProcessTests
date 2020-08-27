namespace ProcessTests.Api.ProcessTests.Infrastructure
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Components.Authenticate.Models;

    internal class HttpRequestFactory
    {
        private readonly JwtTokenDto _jwtTokenDto;

        public HttpRequestFactory(JwtTokenDto jwtTokenDto) => _jwtTokenDto = jwtTokenDto;

        public static HttpRequestMessage CreateAuthGetRequest(string url, JwtTokenDto tokenDto)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenDto.Token);

            return httpRequest;
        }

        public HttpRequestMessage CreateGetRequest(string url) => new HttpRequestMessage(HttpMethod.Get, url);

        public HttpRequestMessage CreateAuthGetRequest(string url) => CreateAuthGetRequest(url, _jwtTokenDto);

        public HttpRequestMessage CreatePostRequest(string url, HttpContent content = null)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);

            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpRequest.Headers.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));

            if (content != null)
            {
                httpRequest.Content = content;
            }

            return httpRequest;
        }

        public HttpRequestMessage CreateAuthPostRequest(string url, HttpContent content = null)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);

            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _jwtTokenDto.Token);

            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpRequest.Headers.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));

            if (content != null)
            {
                httpRequest.Content = content;
            }

            return httpRequest;
        }
    }
}
