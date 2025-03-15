using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Parstech.Shop.Shared.Protos.ProductDetailAdmin;
using Parstech.Shop.Web.Services.GrpcClients;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.Response;
using Xunit;

namespace Shop.Web.Tests.Services.GrpcClients
{
    public class ProductDetailAdminGrpcClientTests
    {
        private readonly Mock<ProductDetailAdminService.ProductDetailAdminServiceClient> _mockClient;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly ProductDetailAdminGrpcClient _sut;

        public ProductDetailAdminGrpcClientTests()
        {
            _mockClient = new Mock<ProductDetailAdminService.ProductDetailAdminServiceClient>();
            _mockConfiguration = new Mock<IConfiguration>();
            
            // Setup configuration to return a valid URL
            _mockConfiguration
                .Setup(x => x[It.Is<string>(s => s == "GrpcSettings:ApiUrl")])
                .Returns("https://localhost:5001");
                
            // Using reflection to set the private field
            _sut = new ProductDetailAdminGrpcClient(_mockConfiguration.Object);
            var fieldInfo = typeof(ProductDetailAdminGrpcClient).GetField("_client", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            fieldInfo.SetValue(_sut, _mockClient.Object);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var productId = 1;
            var expectedProduct = new Parstech.Shop.Shared.Protos.ProductDetailAdmin.ProductDto
            {
                Id = productId,
                Title = "Test Product",
                Description = "Test Description",
                IsActive = true
            };
            
            _mockClient
                .Setup(x => x.GetProductByIdAsync(
                    It.Is<ProductRequest>(r => r.ProductId == productId),
                    null, null, default))
                .ReturnsAsync(expectedProduct);

            // Act
            var result = await _sut.GetProductByIdAsync(productId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(productId);
            result.Title.Should().Be("Test Product");
            result.Description.Should().Be("Test Description");
            result.IsActive.Should().BeTrue();
        }

        [Fact]
        public async Task CreateProductAsync_ShouldReturnSuccessResponse_WhenProductIsValid()
        {
            // Arrange
            var product = new ProductDto
            {
                Title = "New Product",
                Description = "New Description",
                IsActive = true
            };
            
            var expectedResponse = new Parstech.Shop.Shared.Protos.ProductDetailAdmin.ResponseDto
            {
                IsSuccessed = true,
                Message = "Product created successfully",
                Object = "1" // Product ID
            };
            
            _mockClient
                .Setup(x => x.CreateProductAsync(
                    It.IsAny<Parstech.Shop.Shared.Protos.ProductDetailAdmin.ProductDto>(),
                    null, null, default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _sut.CreateProductAsync(product);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessed.Should().BeTrue();
            result.Message.Should().Be("Product created successfully");
            result.Object.Should().Be("1");
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldReturnSuccessResponse_WhenProductIsValid()
        {
            // Arrange
            var product = new ProductDto
            {
                Id = 1,
                Title = "Updated Product",
                Description = "Updated Description",
                IsActive = true
            };
            
            var expectedResponse = new Parstech.Shop.Shared.Protos.ProductDetailAdmin.ResponseDto
            {
                IsSuccessed = true,
                Message = "Product updated successfully"
            };
            
            _mockClient
                .Setup(x => x.UpdateProductAsync(
                    It.IsAny<Parstech.Shop.Shared.Protos.ProductDetailAdmin.ProductDto>(),
                    null, null, default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _sut.UpdateProductAsync(product);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessed.Should().BeTrue();
            result.Message.Should().Be("Product updated successfully");
        }

        [Fact]
        public async Task GetCategoriesAsync_ShouldReturnListOfCategories()
        {
            // Arrange
            var expectedResponse = new CategoryListResponse();
            expectedResponse.Categories.Add(new Parstech.Shop.Shared.Protos.ProductDetailAdmin.CategoryDto
            {
                Id = 1,
                Title = "Category 1"
            });
            expectedResponse.Categories.Add(new Parstech.Shop.Shared.Protos.ProductDetailAdmin.CategoryDto
            {
                Id = 2,
                Title = "Category 2"
            });
            
            _mockClient
                .Setup(x => x.GetCategoriesAsync(
                    It.IsAny<EmptyRequest>(),
                    null, null, default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _sut.GetCategoriesAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result[0].GroupId.Should().Be(1);
            result[0].GroupTitle.Should().Be("Category 1");
            result[1].GroupId.Should().Be(2);
            result[1].GroupTitle.Should().Be("Category 2");
        }

        [Fact]
        public async Task GetProductPropertiesAsync_ShouldReturnListOfProperties_WhenProductExists()
        {
            // Arrange
            var productId = 1;
            var expectedResponse = new ProductPropertyListResponse();
            expectedResponse.Properties.Add(new Parstech.Shop.Shared.Protos.ProductDetailAdmin.ProductPropertyDto
            {
                Id = 1,
                ProductId = productId,
                PropertyId = 1,
                PropertyName = "Color",
                Value = "Red"
            });
            expectedResponse.Properties.Add(new Parstech.Shop.Shared.Protos.ProductDetailAdmin.ProductPropertyDto
            {
                Id = 2,
                ProductId = productId,
                PropertyId = 2,
                PropertyName = "Size",
                Value = "Large"
            });
            
            _mockClient
                .Setup(x => x.GetProductPropertiesAsync(
                    It.Is<ProductRequest>(r => r.ProductId == productId),
                    null, null, default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _sut.GetProductPropertiesAsync(productId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result[0].Id.Should().Be(1);
            result[0].ProductId.Should().Be(productId);
            result[0].PropertyName.Should().Be("Color");
            result[0].Value.Should().Be("Red");
            result[1].Id.Should().Be(2);
            result[1].ProductId.Should().Be(productId);
            result[1].PropertyName.Should().Be("Size");
            result[1].Value.Should().Be("Large");
        }
    }
} 