namespace TMS.EntitiesDTO
{
    public class TaskUserDTO
    {
        public string UserId { get; set; }
        public UserAppDTO User { get; set; }

        public int TaskId { get; set; }
        public TaskDTO Task { get; set; }
    }
}