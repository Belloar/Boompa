namespace Boompa.DTO
{
    public class ContestRecordDTO
    {
        public record ReturnRecord(string userName, string speedAccuracyRatio, DateOnly date, int expEarned,int numberOfRounds);
        public record CreateRecord( string CategoryName, DateOnly Date,DateTime LastModifiedOn, int ExpEarned,int NumberOfRounds, string? SpeedAccuracyRatio);
        public record UpdateRecord( string CategoryName, DateTime LastModifiedOn, int ExpEarned, int NumberOfRounds, string? SpeedAccuracyRatio);
    }
}
