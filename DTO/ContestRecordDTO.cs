namespace Boompa.DTO
{
    public class ContestRecordDTO
    {
        public record ReturnRecordDTO(string speedAccuracyRatio, DateOnly date, int expEarned,int numberOfRounds);
        public record CreateRecordDTO(Guid challengerId, Guid categoryId, string speedAccuracyRatio, DateOnly date, int expEarned,int numberOfRounds);
    }
}
