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

    #region MUSCLE_GROUP
    public const string CREATE_MUSCLE_GROUP = "CreateMuscleGroup";
    public const string GET_MUSCLE_GROUP_BY_ID = "GetMuscleGroupById";
    public const string GET_MUSCLE_GROUP_COUNT = "GetMuscleGroupCount";
    public const string GET_MUSCLE_GROUPS_PAGINATED = "GetMuscleGroupsPaginated";
    public const string DELETE_MUSCLE_GROUP_BY_ID = "DeleteMuscleGroupById";
    #endregion

    #region MOVEMENT
    public const string CREATE_MOVEMENT = "CreateMovement";
    public const string GET_MOVEMENT_BY_ID = "GetMovementById";
    public const string GET_MOVEMENT_COUNT = "GetMovementCount";
    public const string GET_MOVEMENTS_PAGINATED = "GetMovementsPaginated";
    public const string DELETE_MOVEMENT_BY_ID = "DeleteMovementById";
    #endregion

    #region WORKOUT
    public const string CREATE_WORKOUT = "CreateWorkout";
    public const string GET_WORKOUT_BY_ID = "GetWorkoutById";
    public const string GET_WORKOUT_COUNT = "GetWorkoutCount";
    public const string GET_WORKOUTS_PAGINATED = "GetWorkoutsPaginated";
    public const string DELETE_WORKOUT_BY_ID = "DeleteWorkoutById";
    #endregion
}
