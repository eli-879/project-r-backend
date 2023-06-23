using MediatR;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Entities;

namespace ProjectR.Application.Users.Create
{
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(Guid.NewGuid(), request.username, request.email, request.password, DateTime.UtcNow);

            _userRepository.InsertUser(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);


        }
    }
}
