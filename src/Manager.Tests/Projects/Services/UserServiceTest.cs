using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using FluentAssertions;
using Manager.Core.Exceptions;
using Manager.Domain.Entities;
using Manager.Infra.Interfaces;
using Manager.Services.DTO;
using Manager.Services.Interfaces;
using Manager.Services.Mappers;
using Manager.Services.Services;
using Manager.Tests.Fixtures;
using Moq;
using Xunit;

namespace Manager.Tests.Projects.Services
{
    public class UserServiceTest
    {
        //Subject Under Test
        private readonly IUserService _sut;
        
        //Mocks
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public UserServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _sut = new UserService(_userRepositoryMock.Object);
        }


        #region Create Calls
        
        [Fact(DisplayName = "Create Valid user")]
        [Trait("Category", "Services")]
        public async Task CreateAsync_WhenUserIsValid_Returns_UserDTO()
        {
            //Arrange
            var userDto = UserFixture.CreateValidUserDTO();

            var userCreated = UserMapper.MapToUser(userDto);

            _userRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(() => null);

            _userRepositoryMock.Setup(x => x.CreateAsync(It.IsAny <User> ()))
                .ReturnsAsync(() => userCreated);
            
            //Act
            var result = await _sut.CreateAsync(userDto);

            //Assert
            //Assert.Equal(result, userDTO);
            result.Should()
                .BeEquivalentTo(UserMapper.MapToUserDto(userCreated));
        }

        [Fact(DisplayName = "Create When User Exists")]
        [Trait("Category", "Services")]
        public void CreateAsync_WhenUserExits_Returns_EmptyOptional()
        {
            //Arrange
            var user = UserFixture.CreateValidUserDTO();
            var userExists = UserFixture.CreateValidUser();
            
            _userRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(() => userExists);

    
            //Act
            Func<Task<UserDTO>> act = async () =>
            {
                return await _sut.CreateAsync(user);
            };

            //Assert
            act.Should()
                .Throw<DomainException>()
                .WithMessage("User is already registered.");
        }
        
        [Fact(DisplayName = "Create When User is Invalid")]
        [Trait("Category", "Services")]
        public void CreateAsync_WhenUserIsInvalid_Returns_EmptyOptional()
        {
            // Arrange
            var userToCreate = UserFixture.CreateInvalidUserDTO();

            _userRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(() => null);

            // Act
            Func<Task<UserDTO>> act = async () =>
            {
                return await _sut.CreateAsync(userToCreate);
            };


            // Act
            act.Should().Throw<Exception>();
        }

        #endregion
        
        #region GetAll Calls
        
        [Fact(DisplayName = "Get All Users")]
        [Trait("Category", "Services")]
        public async Task GetAllAsync_WhenUsersExists_Returns_AListOfUserDTO()
        {
            // Arrange
            var usersFound = UserFixture.CreateListValidUser();

            _userRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(() => usersFound);

            // Act
            var result = await _sut.GetAllAsync();

            // Assert
            result.Should()
                .BeEquivalentTo(UserMapper.MapToUserDtos(usersFound));
        }
        
        [Fact(DisplayName = "Get All Users When None User Found")]
        [Trait("Category", "Services")]
        public void GetAllAsync_WhenNoneUserFound_Returns_EmptyList()
        {
            // Arrange
            _userRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(() => null);

            // Act
            Func<Task<List<UserDTO>>> act = async () =>
            {
                return await _sut.GetAllAsync();
            };

            // Assert
            act.Should().Throw<ArgumentNullException>();

        }
        
        #endregion
    }
}