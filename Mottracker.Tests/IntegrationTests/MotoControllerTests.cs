using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Mottracker.Application.Dtos.Moto;
using Xunit;

namespace Mottracker.Tests.IntegrationTests;

public class MotoControllerTests : IClassFixture<WebApplicationFactory<Mottracker.Program>>
{
 private readonly WebApplicationFactory<Mottracker.Program> _factory;

 public MotoControllerTests(WebApplicationFactory<Mottracker.Program> factory)
 {
 Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
 _factory = factory;
 }

 [Fact]
 public async Task CreateMoto_ShouldReturnCreated()
 {
 var client = _factory.CreateClient();

 var request = new MotoRequestDto
 {
 IdMoto =0,
 PlacaMoto = "XYZ1234",
 ModeloMoto = "Honda Biz",
 AnoMoto =2023,
 IdentificadorMoto = "ID-123",
 QuilometragemMoto =100,
 EstadoMoto = Mottracker.Domain.Enums.Estados.NoPatioCorreto,
 CondicoesMoto = "Boa",
 MotoPatioOrigemId = null
 };

 var response = await client.PostAsJsonAsync("/api/v1/moto", request);
 Assert.Equal(HttpStatusCode.Created, response.StatusCode);

 var created = await response.Content.ReadFromJsonAsync<MotoResponseDto>();
 Assert.NotNull(created);
 Assert.Equal(request.PlacaMoto, created.PlacaMoto);
 Assert.True(created.IdMoto >0);
 }

 [Fact]
 public async Task DeleteMoto_ShouldRemoveAndReturnNotFoundOnGet()
 {
 var client = _factory.CreateClient();

 // create first
 var request = new MotoRequestDto
 {
 IdMoto =0,
 PlacaMoto = "DEL1234",
 ModeloMoto = "Honda CG",
 AnoMoto =2022,
 IdentificadorMoto = "ID-DEL",
 QuilometragemMoto =50,
 EstadoMoto = Mottracker.Domain.Enums.Estados.NoPatioCorreto,
 CondicoesMoto = "Ok"
 };

 var createResp = await client.PostAsJsonAsync("/api/v1/moto", request);
 Assert.Equal(HttpStatusCode.Created, createResp.StatusCode);
 var created = await createResp.Content.ReadFromJsonAsync<MotoResponseDto>();
 Assert.NotNull(created);

 var id = created.IdMoto;

 // delete
 var deleteResp = await client.DeleteAsync($"/api/v1/moto/{id}");
 Assert.True(deleteResp.StatusCode == HttpStatusCode.NoContent || deleteResp.StatusCode == HttpStatusCode.OK);

 // get should be404
 var getResp = await client.GetAsync($"/api/v1/moto/{id}");
 Assert.Equal(HttpStatusCode.NotFound, getResp.StatusCode);
 }
}
