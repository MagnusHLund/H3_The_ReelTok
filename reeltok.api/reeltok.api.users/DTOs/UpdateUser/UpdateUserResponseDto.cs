using reeltok.api.users.Entities;

namespace reeltok.api.users.DTOs.UpdateUser
{
    public class UpdateUserResponseDto : BaseResponseDto
    {
        public UserEntity UserEntity { get; set; }

        public UpdateUserResponseDto(UserEntity userEntity, bool success = true) : base(success)
        {
            UserEntity = userEntity;
        }

        public UpdateUserResponseDto() { }
    }
}