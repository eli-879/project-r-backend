using MediatR;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Exceptions;

namespace ProjectR.Application.Users.Delete
{
    internal sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUserRepository repository, IUnitOfWork unitOfWork)
        {
            _userRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.UserId);

            if (user is null)
            {
                throw new UserNotFoundException(request.UserId);
            }

            _userRepository.DeleteUser(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
