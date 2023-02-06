using System.Net.Http.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Pokemon.API.Controllers.v1;

namespace Pokemon.API.Test
{
    public class GetPokemonTest
    {
        private readonly WebApplicationFactory<PokemonController> _application;

        public GetPokemonTest()
        {
            _application = new WebApplicationFactory<PokemonController>();
        }


        [Theory]
        [InlineData("pikachu", HttpStatusCode.OK)]
        [InlineData("mr-mime", HttpStatusCode.OK)]
        [InlineData("marowak", HttpStatusCode.OK)]
        [InlineData("carlos", HttpStatusCode.NotFound)]
        [InlineData("pepe grillo", HttpStatusCode.NotFound)]
        public async Task CorrectStatusCodeBasedOnPokemonName(String name, HttpStatusCode expectedStatusCode)
        {
            var httpClient = _application.CreateClient();
            
            var response = await httpClient.GetAsync($"/api/v1/Pokemon/{name}");

            Assert.Equal(expectedStatusCode, response.StatusCode);
        }
    }
}