using Application.Contracts;
using Domain.Entities;
using Moq;

namespace Application.unitTests.Mocks;

public static class UserMockRepository
{
    public static Mock<IUserRepository> GetUserRepository()
    {

        var users = new List<UserEntity>
        {
            new UserEntity
            { 
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Bio = "Software Developer",
                Email = "johndoe@example.com",
                Token = "abc123xyz",
                IsVerified = true,
                UserName = "johndoe123",
                Password = "P@ssw0rd",
                ProfilePicture = "profile1.jpg"
            },
            new UserEntity
            {
                Id = 2,
                FirstName = "Alice",
                LastName = "Smith",
                Bio = "Graphic Designer",
                Email = "alice.smith@example.com",
                Token = "def456uvw",
                IsVerified = true,
                UserName = "alice_designs",
                Password = "SecurePwd123",
                ProfilePicture = "profile2.jpg"
            },
            new UserEntity
            {
                Id = 3,
                FirstName = "Bob",
                LastName = "Johnson",
                Bio = null, // Bio is optional (null)
                Email = "bob.johnson@example.com",
                Token = null, // Token is optional (null)
                IsVerified = false,
                UserName = "bob123",
                Password = "MySecretPwd",
                ProfilePicture = null // ProfilePicture is optional (null)
            }
        };

        var mockUserRepository = new Mock<IUserRepository>();

        mockUserRepository.Setup(u => u.GetByIdAsync(It.IsAny<int>()))
            .Returns((int userId) =>
            {
                // Simulate fetching the user by ID
                var user = users.FirstOrDefault(u => u.Id == userId);
                return Task.FromResult(user)!;
            });
        
        mockUserRepository.Setup(u => u.Exists(It.IsAny<int>()))
            .ReturnsAsync((int userId) =>
            {
                return users.Any(user => user.Id == userId);
            });

        return mockUserRepository;
    }
}