using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Parstech.Shop.Shared.Protos.SectionAdmin;
using Parstech.Shop.Web.Services.GrpcClients;
using Shop.Application.DTOs.Section;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parstech.Shop.Tests
{
    [TestFixture]
    public class SectionAdminGrpcClientTests
    {
        private Mock<SectionAdminService.SectionAdminServiceClient> _mockClient;
        private Mock<IConfiguration> _mockConfiguration;
        private SectionAdminGrpcClient _sectionAdminGrpcClient;

        [SetUp]
        public void Setup()
        {
            _mockClient = new Mock<SectionAdminService.SectionAdminServiceClient>();
            _mockConfiguration = new Mock<IConfiguration>();

            // Configure the mock configuration
            _mockConfiguration.Setup(x => x["GrpcServer:Url"]).Returns("https://localhost:5001");

            // Create the client with the mocks
            _sectionAdminGrpcClient = new SectionAdminGrpcClient(_mockConfiguration.Object);
        }

        [Test]
        public async Task GetSectionsAndDetailsAsync_ReturnsExpectedSections()
        {
            // Arrange
            var expectedResponse = new SectionAndDetailsListResponse();
            var sectionAndDetailsDto = new Shared.Protos.SectionAdmin.SectionAndDetailsDto
            {
                Id = 1,
                Title = "Test Section",
                Description = "Test Description",
                IsActive = true,
                Sort = 1,
                StoreId = 1
            };
            expectedResponse.Sections.Add(sectionAndDetailsDto);

            _mockClient.Setup(x => x.GetSectionsAndDetailsAsync(
                It.IsAny<SectionRequest>(), 
                null, 
                null, 
                default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _sectionAdminGrpcClient.GetSectionsAndDetailsAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual("Test Section", result[0].Title);
            Assert.AreEqual("Test Description", result[0].Description);
            Assert.AreEqual(true, result[0].IsActive);
        }

        [Test]
        public async Task CreateSectionAsync_ReturnsSuccessResponse()
        {
            // Arrange
            var sectionDto = new SectionDto
            {
                Title = "New Section",
                Description = "New Description",
                IsActive = true,
                Sort = 1,
                StoreId = 1
            };

            var expectedResponse = new ResponseDto
            {
                IsSuccessed = true,
                Message = "آیتم جدید با موفقیت افزوده شد"
            };

            _mockClient.Setup(x => x.CreateSectionAsync(
                It.IsAny<Shared.Protos.SectionAdmin.SectionDto>(),
                null,
                null,
                default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _sectionAdminGrpcClient.CreateSectionAsync(sectionDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessed);
            Assert.AreEqual("آیتم جدید با موفقیت افزوده شد", result.Message);
        }
    }
} 