﻿

using Bogus;
using System.Net.Http.Json;
using TheWayToGerman.Api.DTO;
using TheWayToGerman.Api.DTO.Admin;
using TheWayToGerman.Api.ResponseObject.Admin;
using TheWayToGerman.Core.Cqrs.Queries;

namespace IntegrationTest.Endpoints;

public class AdminTest
{
    readonly Faker Faker = new Faker();
    readonly HttpClient client;

    public AdminTest()
    {
        client = WebApplicationBuilder.ApiClient();
    }

    [Fact]
    public async Task UpdateAdmin_CorrectInformation_ShouldReturnOK()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        UpdateAdminDTO UpdateAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        await client.PostAsJsonAsync("api/v1/Admin", createAdminDTO);
        await client.Authenticate(createAdminDTO.Username, createAdminDTO.Password);
        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
    }




    [Fact]
    public async Task UpdateAdmin_DuplicateUserName_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        CreateAdminDTO createAdminDTO2 = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        UpdateAdminDTO UpdateAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(8),
            Username = createAdminDTO.Username,
        };
        await client.PostAsJsonAsync("api/v1/Admin", createAdminDTO);
        await client.PostAsJsonAsync("api/v1/Admin", createAdminDTO2);
        await client.Authenticate(createAdminDTO2.Username, createAdminDTO2.Password);

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAdmin_DuplicateEmail_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        CreateAdminDTO createAdminDTO2 = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        UpdateAdminDTO UpdateAdminDTO = new()
        {
            Email = createAdminDTO.Email,
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        await client.PostAsJsonAsync("api/v1/Admin", createAdminDTO);
        await client.PostAsJsonAsync("api/v1/Admin", createAdminDTO2);
        await client.Authenticate(createAdminDTO2.Username, createAdminDTO2.Password);

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAdmin_PasswordLessThan8Char_ShouldReturnHttpBadRequest()
    {

        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        UpdateAdminDTO UpdateAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),
        };
        await client.PostAsJsonAsync("api/v1/Admin", createAdminDTO);
        await client.Authenticate(createAdminDTO.Username, createAdminDTO.Password);

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAdmin_UsernameIsNull_ShouldReturnHttpBadRequest()
    {

        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        UpdateAdminDTO UpdateAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(7),
            Username = null,
        };
        await client.PostAsJsonAsync("api/v1/Admin", createAdminDTO);
        await client.Authenticate(createAdminDTO.Username, createAdminDTO.Password);

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAdmin_EmailIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        UpdateAdminDTO UpdateAdminDTO = new()
        {
            Email = null,
            Name = Faker.Person.FirstName,
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),
        };
        await client.PostAsJsonAsync("api/v1/Admin", createAdminDTO);
        await client.Authenticate(createAdminDTO.Username, createAdminDTO.Password);

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAdmin_NameIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        UpdateAdminDTO UpdateAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = null,
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),
        };
        await client.PostAsJsonAsync("api/v1/Admin", createAdminDTO);
        await client.Authenticate(createAdminDTO.Username, createAdminDTO.Password);

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAdmin_NoDataChanges_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        UpdateAdminDTO UpdateAdminDTO = new()
        {
            Email = createAdminDTO.Email,
            Name = createAdminDTO.Name,
            Password = createAdminDTO.Password,
            Username = createAdminDTO.Username,
        };
        await client.PostAsJsonAsync("api/v1/Admin", createAdminDTO);
        await client.Authenticate(createAdminDTO.Username, createAdminDTO.Password);

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(UpdateAdminDTO), HttpMethod.Put);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task GetAdmins_NonExistingName_ShouldReturnHttpOkWithEmptyArray()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };
        GetAdminsDTO GetAdminDTO = new()
        {
            Name = "SomeRandomName",
        };
        await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(createAdminDTO), HttpMethod.Post);
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(GetAdminDTO), HttpMethod.Get);
        //execute
        //validate
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        var userlist = await result.Content.ReadFromJsonAsync<IEnumerable<GetAdminsQueryResponse>>();
        Assert.NotNull(userlist);
        Assert.Empty(userlist);
    }
    [Fact]
    public async Task GetAdmins_ExistingName_ShouldReturnHttpOkWithArrayOfMatchedUser()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = "SomeSpecificName",
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        GetAdminsDTO GetAdminDTO = new()
        {
            Name = "SomeSpecificName",
        };
        await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(createAdminDTO), HttpMethod.Post);
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(GetAdminDTO), HttpMethod.Get);
        //execute
        //validate
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        var userlist = await result.Content.ReadFromJsonAsync<IEnumerable<GetAdminsQueryResponse>>();
        Assert.NotNull(userlist);
        Assert.Contains(userlist, x => x.Email == createAdminDTO.Email);
        Assert.Contains(userlist, x => x.Name == createAdminDTO.Name);
        Assert.Contains(userlist, x => x.Username == createAdminDTO.Username);
    }
    [Fact]
    public async Task DeleteAdmin_ExistingAdmin_ShouldReturnHttpNoContent()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = "SomeSpecificName",
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),
        };
        var createAdminResult=await client.SendAsync<CreateAdminResponse>("api/v1/Admin", Helper.CreateJsonContent(createAdminDTO), HttpMethod.Post); //create an admin    
        DeleteAdminDTO DeleteAdminDTO = new()
        {
            Id = createAdminResult.Id
        };

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(DeleteAdminDTO), HttpMethod.Delete); //get its information

        //validate
        Assert.Equal(System.Net.HttpStatusCode.NoContent, result.StatusCode);

    }
    [Fact]
    public async Task DeleteAdmin_NotExistingAdmin_ShouldReturnHttpNotFound()
    {
        //prepare
        await client.Authenticate();
        DeleteAdminDTO DeleteAdminDTO = new()
        {
            Id = Guid.NewGuid(),
        };

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(DeleteAdminDTO), HttpMethod.Delete); //get its information

        //validate
        Assert.Equal(System.Net.HttpStatusCode.NotFound, result.StatusCode);

    }
    [Fact]
    public async Task AddAdmin_UniqueValues_ShouldReturnHttpOKAndValidGuid()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.PostAsJsonAsync("api/v1/Admin", createAdminDTO);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
    }


    [Fact]
    public async Task AddAdmin_DuplicateUserName_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        CreateAdminDTO createAdminDTO2 = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = createAdminDTO.Username,

        };

        //execute
        await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(createAdminDTO), HttpMethod.Post);

        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(createAdminDTO2), HttpMethod.Post);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddAdmin_DuplicateEmail_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };
        CreateAdminDTO createAdminDTO2 = new()
        {
            Email = createAdminDTO.Email,
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(8),
            Username = Faker.Internet.UserName(),

        };

        //execute
        await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(createAdminDTO), HttpMethod.Post);

        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(createAdminDTO2), HttpMethod.Post);
        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddAdmin_PasswordLessThan8Char_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(createAdminDTO), HttpMethod.Post);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddAdmin_UsernameIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = null,

        };

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(createAdminDTO), HttpMethod.Post);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddAdmin_EmailIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = null,
            Name = Faker.Name.FullName(),
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(createAdminDTO), HttpMethod.Post);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
    [Fact]
    public async Task AddAdmin_NameIsNull_ShouldReturnHttpBadRequest()
    {
        //prepare
        await client.Authenticate();
        CreateAdminDTO createAdminDTO = new()
        {
            Email = Faker.Internet.Email(),
            Name = null,
            Password = Faker.Internet.Password(7),
            Username = Faker.Internet.UserName(),

        };

        //execute
        var result = await client.SendAsync("api/v1/Admin", Helper.CreateJsonContent(createAdminDTO), HttpMethod.Post);

        //validate
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }
}
