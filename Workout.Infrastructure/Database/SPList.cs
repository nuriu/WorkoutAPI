namespace Workout.Infrastructure.Database;

public static class SPList
{
    #region USER
    public const string CREATE_USER = "CreateUser";
    public const string IS_USER_EXISTS = "IsUserExists";
    public const string DELETE_USER_BY_ID = "DeleteUserById";
    public const string GET_USER_COUNT = "GetUserCount";
    public const string GET_USER_BY_ID = "GetUserById";
    public const string GET_USERS_PAGINATED = "GetUsersPaginated";
    #endregion

    #region DIFFICULTY_LEVEL
    public const string CREATE_DIFFICULTY_LEVEL = "CreateDifficultyLevel";
    public const string GET_DIFFICULTY_LEVEL_BY_ID = "GetDifficultyLevelById";
    public const string GET_DIFFICULTY_LEVEL_COUNT = "GetDifficultyLevelCount";
    public const string GET_DIFFICULTY_LEVELS_PAGINATED = "GetDifficultyLevelsPaginated";
    public const string DELETE_DIFFICULTY_LEVEL_BY_ID = "DeleteDifficultyLevelById";
    #endregion
}
